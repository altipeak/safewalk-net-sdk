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
        

        static private ServerConnectivityHelper serverConnectivityHelper ;    
        static void Main(string[] args)
        {
        	testSafewalkClient(INTERNAL_USERNAME, INTERNAL_PASSWORD);
            testSafewalkClient_USER_CREATE(USERNAME, PASSWORD, FIRSTNAME, LASTNAME, MOBILEPHONE, EMAIL, PARENT);
            testSafewalkClient_USER_UPDATE(USERNAME, MOBILEPHONE_NEW, EMAIL_NEW);
            testSafewalkClient_USER_GET(USERNAME);
            testSafewalkClient_USER_DELETE(USERNAME);
            testSafewalkClient_SET_STATIC_PASSWORD(USERNAME, PASSWORD_NEW);
        }
        
        private static void testSafewalkClient(String username, String password) {

            Console.WriteLine("AUTHENTICATION PROCESS : start" );

            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            AuthenticationResponse response1 = client.Authenticate(AUTHENTICATION_API_ACCESS_TOKEN, username, password);
            Console.WriteLine("AUTHENTICATION RESPONSE : " + response1);
            Console.WriteLine("AUTHENTICATION PROCESS : end");
        }

        private static void testSafewalkClient_USER_CREATE(String username, String password, string firstname, string lastname, string phone, string email, string parent)
        {
            Console.WriteLine("USER_CREATE PROCESS : start"); 
            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            CreateUserResponse response1 = client.CreateUser(ADMIN_API_ACCESS_TOKEN, username, password, firstname, lastname, phone, email, parent);
            Console.WriteLine("USER_CREATE RESPONSE : " + response1);
            Console.WriteLine("USER_CREATE PROCESS : end");
        }

        private static void testSafewalkClient_USER_UPDATE(String username, string phone, string email)
        {
            Console.WriteLine("USER_UPDATE PROCESS : start");
            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            UpdateUserResponse response1 = client.UpdateUser(ADMIN_API_ACCESS_TOKEN, username, phone, email);
            Console.WriteLine("USER_UPDATE RESPONSE : " + response1);
            Console.WriteLine("USER_UPDATE PROCESS : end");
        }

        private static void testSafewalkClient_USER_GET(String username)
        {
            Console.WriteLine("USER_GET PROCESS : start");
            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            GetUserResponse response1 = client.GetUser(ADMIN_API_ACCESS_TOKEN, username);
            Console.WriteLine("USER_GET RESPONSE : " + response1);
            Console.WriteLine("USER_GET PROCESS : end");
        }

        private static void testSafewalkClient_USER_DELETE(String username)
        {
            Console.WriteLine("USER_DELETE PROCESS : start");
            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            DeleteUserResponse response1 = client.DeleteUser(ADMIN_API_ACCESS_TOKEN, username);
            Console.WriteLine("USER_DELETE RESPONSE : " + response1);
            Console.WriteLine("USER_DELETE PROCESS : end");
        }

        private static void testSafewalkClient_SET_STATIC_PASSWORD(String username, String password)
        {
            Console.WriteLine("SET_STATIC_PASSWORD PROCESS : start");
            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            SetStaticPasswordResponse response1 = client.SetStaticPassword(ADMIN_API_ACCESS_TOKEN, username, password);
            Console.WriteLine("SET_STATIC_PASSWORD RESPONSE : " + response1);
            Console.WriteLine("SET_STATIC_PASSWORD PROCESS : end");
        }
    }
    
}
