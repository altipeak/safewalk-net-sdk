using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using safewalk;
using safewalk.helper;


namespace dllUsageExample
{
    /// <summary>
    /// USAGE:
    /// include safewalk-sdk-net.dll into dependencies
    /// </summary>
    class Program
    {
        #region "consts"
        private static System.Collections.Specialized.NameValueCollection settings = ConfigurationManager.AppSettings;

        private static readonly String HOST = settings["HOST"];
        private static readonly long PORT = long.Parse(settings["PORT"]);
        private static readonly bool SET_BYPASS_SSL_CERTIFICATE = bool.Parse(settings["setBypassSSLCertificate"]);

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

        static private IServerConnectivityHelper serverConnectivityHelper;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*auth client */
            TestSafewalkClientStandardAuthentication(INTERNAL_USERNAME, INTERNAL_PASSWORD);
            TestSafewalkClientExternalAuthentication(INTERNAL_USERNAME);
            TestSafewalkClientGenerateChallenge(INTERNAL_USERNAME);
            TestSafewalkClientsignature(INTERNAL_USERNAME, INTERNAL_PASSWORD, HASH, DATA, TITLE, BODY);

            Console.Read();
        }

        #region "tests"
        private static void TestSafewalkClientStandardAuthentication(String username, String password)
        {

            Console.WriteLine("Standard Authentication PROCESS : start");

            serverConnectivityHelper = new ServerConnectivityHelper(HOST, PORT, SET_BYPASS_SSL_CERTIFICATE);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper, AUTHENTICATION_API_ACCESS_TOKEN);
            AuthenticationResponse response1 = client.Authenticate(username, password);
            Console.WriteLine("Standard Authentication RESPONSE : " + response1);
            Console.WriteLine("Standard Authentication PROCESS : end");
        }

        private static void TestSafewalkClientExternalAuthentication(String username)
        {

            Console.WriteLine("External Authentication PROCESS : start");

            serverConnectivityHelper = new ServerConnectivityHelper(HOST, PORT, SET_BYPASS_SSL_CERTIFICATE);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper, AUTHENTICATION_API_ACCESS_TOKEN);
            AuthenticationResponse response1 = client.AuthenticateExternal(username);
            Console.WriteLine("External Authentication RESPONSE : " + response1);
            Console.WriteLine("External Authentication PROCESS : end");
        }

        private static void TestSafewalkClientGenerateChallenge(String username)
        {

            Console.WriteLine("Generate Challenge - 1) Session Key PROCESS : start");

            serverConnectivityHelper = new ServerConnectivityHelper(HOST, PORT, SET_BYPASS_SSL_CERTIFICATE);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper, AUTHENTICATION_API_ACCESS_TOKEN);
            SessionKeyResponse response1 = client.CreateSessionKeyChallenge();
            Console.WriteLine("Generate Challenge - Session Key RESPONSE : " + response1);

            Console.WriteLine("\nGenerate Challenge - 2) Verify Session Key: start");
            SessionKeyVerificationResponse response2 = client.VerifySessionKeyStatus(response1.GetChallenge());
            Console.WriteLine("Generate Challenge - Session Key RESPONSE : " + response2);
            Console.WriteLine("Generate Challenge PROCESS : end");
        }

        private static void TestSafewalkClientsignature(String username, String password, String _hash, String _data, String title, String body)
        {

            Console.WriteLine("Push signature PROCESS : start");

            serverConnectivityHelper = new ServerConnectivityHelper(HOST, PORT, SET_BYPASS_SSL_CERTIFICATE);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper, AUTHENTICATION_API_ACCESS_TOKEN);
            SignatureResponse response1 = client.SendPushSignature(username, password, _hash, _data, title, body);
            Console.WriteLine("Push signature RESPONSE : " + response1);
            Console.WriteLine("Push signature PROCESS : end");
        }

        #endregion
    }
}
