using System;
using System.Configuration;

using safewalk;
using safewalk.helper;

namespace test
{
    class Program
    {
        private static readonly String HOST = ConfigurationSettings.AppSettings["HOST"];
        private static readonly long   PORT = long.Parse(ConfigurationSettings.AppSettings["PORT"]);
        private static readonly String AUTHENTICATION_API_ACCESS_TOKEN = ConfigurationSettings.AppSettings["AUTHENTICATION_API_ACCESS_TOKEN"];
        private static readonly String ADMIN_API_ACCESS_TOKEN = ConfigurationSettings.AppSettings["ADMIN_API_ACCESS_TOKEN"];
        private static readonly String INTERNAL_USERNAME = ConfigurationSettings.AppSettings["INTERNAL_USERNAME"];
        private static readonly String INTERNAL_PASSWORD = ConfigurationSettings.AppSettings["INTERNAL_PASSWORD"];
        private static readonly String LDAP_USERNAME = ConfigurationSettings.AppSettings["LDAP_USERNAME"];
        private static readonly String LDAP_PASSWORD = ConfigurationSettings.AppSettings["LDAP_PASSWORD"];
    
        static private ServerConnectivityHelper serverConnectivityHelper ;    
        static void Main(string[] args)
        {
        	testSafewalkClient(INTERNAL_USERNAME, INTERNAL_PASSWORD);
 
        }
        
        private static void testSafewalkClient(String username, String password) {
        	serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
            SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
            SafewalkRespose response1 = client.authenticate(AUTHENTICATION_API_ACCESS_TOKEN, username, password);
            Console.WriteLine("AUTHENTICATION RESPONSE : " + response1);
        }
        
    }
    
}
