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


        static private ServerConnectivityHelper serverConnectivityHelper ;    
        static void Main(string[] args)
        {
        	testSafewalkClient(INTERNAL_USERNAME, INTERNAL_PASSWORD);
            testSafewalkClient_CREATE_USER(USERNAME, PASSWORD, FIRSTNAME, LASTNAME, MOBILEPHONE, EMAIL, PARENT);

        }
        
        private static void testSafewalkClient(String username, String password) {
        	serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            AuthenticationResponse response1 = client.authenticate(AUTHENTICATION_API_ACCESS_TOKEN, username, password);
            Console.WriteLine("AUTHENTICATION RESPONSE : " + response1);
        }

        private static void testSafewalkClient_CREATE_USER(String username, String password, string firstname, string lastname, string phone, string email, string parent)
        {
            serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            CreateUserResponse response1 = client.createUser(ADMIN_API_ACCESS_TOKEN, username, password, firstname, lastname, phone, email, parent);
            Console.WriteLine("AUTHENTICATION RESPONSE : " + response1);
        }

    }
    
}
