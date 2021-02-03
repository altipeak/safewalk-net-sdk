using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using safewalk;
using safewalk.helper;

namespace test
{
    class Program
    {
        #region "consts"
        private static readonly System.Collections.Specialized.NameValueCollection settings = ConfigurationManager.AppSettings;                        

        private static readonly String HOST = settings["HOST"];
        private static readonly long   PORT = long.Parse(settings["PORT"]);
        private static readonly bool  SET_BYPASS_SSL_CERTIFICATE = bool.Parse(settings["setBypassSSLCertificate"]);
        
        private static readonly String AUTHENTICATION_API_ACCESS_TOKEN = settings["AUTHENTICATION_API_ACCESS_TOKEN"];
        private static readonly String ADMIN_API_ACCESS_TOKEN = settings["ADMIN_API_ACCESS_TOKEN"];
        private static readonly String INTERNAL_USERNAME = settings["INTERNAL_USERNAME"];
        private static readonly String INTERNAL_PASSWORD = settings["INTERNAL_PASSWORD"];

        private static readonly String LDAP_USERNAME = settings["LDAP_USERNAME"];
        private static readonly String LDAP_PASSWORD = settings["LDAP_PASSWORD"];

        /*create user*/
        private static readonly string USERNAME = settings["USER_username"];
        private static readonly string PASSWORD = settings["USER_password"];
        private static readonly string FIRSTNAME = settings["USER_firstName"];
        private static readonly string LASTNAME = settings["USER_lastName"];
        private static readonly string MOBILEPHONE = settings["USER_mobilePhone"];
        private static readonly string EMAIL = settings["USER_email"];
        private static readonly string PARENT = settings["USER_parent"];

        /*update user*/
        private static readonly string MOBILEPHONE_NEW = settings["USER_mobilePhone_new"];
        private static readonly string EMAIL_NEW = settings["USER_email_new"];

        /*set static pass*/
        private static readonly string PASSWORD_NEW = settings["USER_password_new"];

        /*push signature */
        private static readonly string HASH = settings["hash"];
        private static readonly string DATA = settings["data"];
        private static readonly string TITLE = settings["title"];
        private static readonly string BODY = settings["body"];

        #endregion

        static private IServerConnectivityHelper serverConnectivityHelper ;
        static private ISafewalkAuthClient client;
        static void Main(string[] args)
        {
            Console.WriteLine("BEGIN TEST");
            serverConnectivityHelper = new ServerConnectivityHelper(HOST, PORT, SET_BYPASS_SSL_CERTIFICATE);
            client = new SafewalkAuthClient(serverConnectivityHelper, AUTHENTICATION_API_ACCESS_TOKEN);

            TestUserCredentialsAuthenticationMethod();
            TestQRAuthenticationMethod();
            TestPushAuthenticationMethod();
            TestPushSignatureAuthenticationMethod();
            TestSecondStepAuthenticationMethod();

            Console.Read();
        }

        #region "tests"
        /// <summary>
        /// On this example a user without licenses is recommended to test one step / static password authentication.
        /// </summary>
         private static void TestUserCredentialsAuthenticationMethod() {
             AuthenticationResponse response1 = client.Authenticate("userName", "12345");
             Console.WriteLine("USER CREDENTIALS AUTHENTICATION RESPONSE : " + response1 + " METHOD " + response1.GetAttributes()["auth-method"]);
        }

        /// <summary>
        /// On this example first a sessionKey string is generated and then it's status is verified. When the sessionKey is generated, it can be copied and used with a third party QR code generator like https://es.qr-code-generator.com/ to be scanned and signed.
        /// </summary>
        /// <param name="username"></param>
        private static void TestQRAuthenticationMethod()
        {
            serverConnectivityHelper = new ServerConnectivityHelper(HOST, PORT, SET_BYPASS_SSL_CERTIFICATE);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper, AUTHENTICATION_API_ACCESS_TOKEN);

            // Here the sessionKey string is created. After it is printed in the console, it can be copied and pasted to https://es.qr-code-generator.com/, then a QR code will be generated to be signed with Fast Auth App. 
            SessionKeyResponse response1 = client.CreateSessionKeyChallenge();
            Console.WriteLine("GET SESSION KEY RESPONSE : " + response1);

            // After the QR is signed, the status will be ACCESS_ALLOWED. While the QR is not signed status will be ACCESS_PENDING. 
            SessionKeyVerificationResponse response2 = client.VerifySessionKeyStatus(response1.GetChallenge());
            Console.WriteLine("VERIFY SESSION KEY RESPONSE : " + response2);
        }

        /// <summary>
        /// On this example the same authenticate API as in UserCredentialsAuthenticationMethod is called, but with a user with a Fast:Auth license registered.
        /// </summary>
           private static void TestPushAuthenticationMethod()
        {
            AuthenticationResponse response1 = client.Authenticate("mobileUserName", "abcde");
            Console.WriteLine("PUSH AUTHENTICATION RESPONSE : " + response1);
        }


        private static void TestPushSignatureAuthenticationMethod()
        {
            SignatureResponse response1 = client.SendPushSignature("mobileUserName", "abcde", "A160E4F805C51261541F0AD6BC618AE10BEB3A30786A099CE67DBEFD4F7F929F", "All the data here will be signed. This request was generated from Safewalk API.", "Sign Transaction", "Push signature triggered from safewalk API");
            Console.WriteLine("PUSH SIGNATURE RESPONSE OPTION 1: " + response1);
            // On this example body parameter is empty 
            SignatureResponse response2 = client.SendPushSignature("mobileUserName", "abcde", "25A0DCC3DD1D78EF2D2FC5E6F606A0DB0ECD8B427A0417D8C94CC51139CF4FC8", "This call includes the data", "Sign Document", null);
            Console.WriteLine("PUSH SIGNATURE RESPONSE OPTION 2: " + response2);
            // On this example data and title parameters are empty 
            SignatureResponse response3 = client.SendPushSignature("mobileUserName", "abcde", "25A0DCC3DD1D78EF2D2FC5E6F606A0DB0ECD8B427A0417D8C94CC51139CF4FC8", null, null, "This call includes the body");
            Console.WriteLine("PUSH SIGNATURE RESPONSE OPTION 3: " + response3);
        }
        /// <summary>
        /// On this example, Safewalk is called as a 2nd step authentication, as identity was first validated with an external system.
        /// </summary>
        /// <param name="username"></param>
        private static void TestSecondStepAuthenticationMethod()
        {
            AuthenticationResponse response1 = client.SecondStepAuthentication("userName");
            Console.WriteLine("2ND STEP AUTHENTICATION RESPONSE : " + response1);
        }
        #endregion
    }

}
