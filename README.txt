USAGE

serverConnectivityHelper = new ServerConnectivityHelperImpl(HOST, PORT);
SafewalkClient client = new SafewalkClientImpl(serverConnectivityHelper);
AuthenticationResponse response1 = client.authenticate(AUTHENTICATION_API_ACCESS_TOKEN, username, password);

FOR TESTING.

1) EXECUTE TEST.EXE

opcional) edit test.exe.config to change the parameters passed to the test app

 <add key ="HOST" value ="xxxxxxxxxxxxxxxx"/>
 <add key ="PORT" value ="yyyy"/>
 <add key ="AUTHENTICATION_API_ACCESS_TOKEN" value ="access"/>
 <add key ="ADMIN_API_ACCESS_TOKEN" value ="token"/>
 <add key ="INTERNAL_USERNAME" value ="usuario"/>
 <add key ="LDAP_USERNAME" value ="ldap"/>