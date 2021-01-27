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
        private static System.Collections.Specialized.NameValueCollection settings = ConfigurationManager.AppSettings;                        

        private static readonly String HOST = settings["HOST"];
        private static readonly long   PORT = long.Parse(settings["PORT"]);
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


        static private ServerConnectivityHelper serverConnectivityHelper ;    
        static void Main(string[] args)
        {
            /*auth client */
            testSafewalkClientStandardAuthentication(INTERNAL_USERNAME, INTERNAL_PASSWORD);
            testSafewalkClientExternalAuthentication(INTERNAL_USERNAME);
            testSafewalkClientGenerateChallenge(INTERNAL_USERNAME);
            testSafewalkClientsignature(INTERNAL_USERNAME, PASSWORD, HASH, DATA, TITLE, BODY);
            
        }
        
        private static void testSafewalkClientStandardAuthentication(String username, String password) {

            Console.WriteLine("Standard Authentication PROCESS : start");

            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper);
            AuthenticationResponse response1 = client.Authenticate(AUTHENTICATION_API_ACCESS_TOKEN, username, password);
            Console.WriteLine("Standard Authentication RESPONSE : " + response1);
            Console.WriteLine("Standard Authentication PROCESS : end");
        }

        private static void testSafewalkClientExternalAuthentication(String username)
        {

            Console.WriteLine("External Authentication PROCESS : start");

            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper);
            AuthenticationResponse response1 = client.ExternalAuthenticate(AUTHENTICATION_API_ACCESS_TOKEN, username);
            Console.WriteLine("External Authentication RESPONSE : " + response1);
            Console.WriteLine("External Authentication PROCESS : end");
        }

        private static void testSafewalkClientGenerateChallenge(String username)
        {

            Console.WriteLine("Generate Challenge - 1) Session Key PROCESS : start");

            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper);
            SessionKeyResponse response1 = client.CreateSessionKeyChallenge(AUTHENTICATION_API_ACCESS_TOKEN, username);
            Console.WriteLine("Generate Challenge - Session Key RESPONSE : " + response1);

            Console.WriteLine("\nGenerate Challenge - 2) Verify Session Key: start");
            SessionKeyVerificationResponse response2 = client.VerifySessionKeyStatus(AUTHENTICATION_API_ACCESS_TOKEN, username, response1.GetChallenge());
            Console.WriteLine("Generate Challenge - Session Key RESPONSE : " + response2);
            Console.WriteLine("Generate Challenge PROCESS : end");
        }

        private static void testSafewalkClientsignature(String username, String password, String _hash, String _data, String title, String body)
        {

            Console.WriteLine("Push signature PROCESS : start");

            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper);
            SignatureResponse response1 = client.SendPushSignature(AUTHENTICATION_API_ACCESS_TOKEN, username, password, _hash, _data, title, body);
            Console.WriteLine("Push signature RESPONSE : " + response1);
            Console.WriteLine("Push signature PROCESS : end");
        }
       
    }
    
}
