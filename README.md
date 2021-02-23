# .NET SDK module for Safewalk integration

* [Authentication API](#authentication-api)

## Authentication API

### Usage
```csharp
String host = "https://192.168.1.160";
long  port = 8443;
private static string AUTHENTICATION_API_ACCESS_TOKEN = "c4608fc697e844829bb5a27cce13737250161bd0";
private static string INTERNAL_USERNAME = "internal"; 
private static string  STATIC_PASSWORD_USERNAME = "internal";
private static string  FAST_AUTH_USERNAME = "fastauth";
private static bool BYPASS_SSL_CHECK = false;
/*push signature */
private static readonly string HASH = "AAAB9621D3AECD703833646742818CA64739FAEDDC82C726B8C756E89DB6BBBB";
private static readonly string DATA = "All the data here will be signed. This request was generated from Safewalk API.";
private static readonly string TITLE = "Sign Transaction";
private static readonly string BODY = "Push signature triggered from safewalk API";
        
serverConnectivityHelper = new ServerConnectivityHelper(HOST, PORT, BYPASS_SSL_CHECK);
SafewalkAuthClient client = new SafewalkAuthClient(serverConnectivityHelper, AUTHENTICATION_API_ACCESS_TOKEN);

/* Standard Authentication */
AuthenticationResponse response1 = client.Authenticate(INTERNAL_USERNAME, STATIC_PASSWORD_USERNAME);

/* External Authentication */
AuthenticationResponse response2 = client.AuthenticateExternal(INTERNAL_USERNAME);

/* Session Key Challenge and check status */
SessionKeyResponse responseA = client.CreateSessionKeyChallenge();
SessionKeyVerificationResponse responseB = client.VerifySessionKeyStatus(responseA.GetChallenge());

/* Push signature */
SignatureResponse response3 = client.SendPushSignature(FAST_AUTH_USERNAME, STATIC_PASSWORD_USERNAME, HASH, DATA, TITLE, BODY);
            
```
* host : The server host.
* port : The server port.
* AUTHENTICATION_API_ACCESS_TOKEN : The access token of the system user created to access the authentication-api.
* INTERNAL_USERNAME: An LDAP or internal user
* STATIC_PASSWORD_USERNAME : An LDAP or internal user with no licenses asigned and password authentication allowed. 
* FAST_AUTH_USER : The user registered in safewalk with a Fast:Auth:Sign license.
* BYPASS_SSL_CHECK : To allow untrusted certificates.
* HASH: The data hash.
* DATA: The data to sign. Data or body are required
* TITLE: The title displayed in the mobile device.
* BODY: The body of the push. Data or body are required
* 
### Authentication Response Examples (AuthenticationResponse class)

The response below show the result of providing valid credentials
```
200 | ACCESS_ALLOWED | admin
```

The response below show the result when the access token is not valid (to fix it, check for the access token in the superadmin console)
```
401 | Invalid token
```

The response below show the result when no access token is provided (to fix it, check for the access token in the superadmin console)
```
401 | Invalid bearer header. No credentials provided.
```

The response below show the result when the credentials (username / password) are not valid
```
401 | ACCESS_DENIED | Invalid credentials, please make sure you entered your username and up to date password/otp correctly
```

The response below show the result when the user is locked
```
401 | ACCESS_DENIED | The user is locked, please contact your system administrator
```

The response below show the result when the user is required to enter an OTP
```
401 | ACCESS_CHALLENGE | admin | Please enter your OTP code
```

### For Testing
1) EXECUTE TEST.EXE
(optional) edit test.exe.config to change the parameters passed to the test app