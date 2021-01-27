using System;
 

namespace safewalk
{
	/// <summary>
	/// Safewalk integration Client
	/// </summary>
	public interface ISafewalkAuthClient
    {
		/// <summary>
		/// Authenticates the user with the given credentials.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order
		/// If the given username has the format of "username@domain"; the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		AuthenticationResponse Authenticate(String username, String password);

		/// <summary>
		/// Authenticates the user with the given credentials.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order
		/// If the given username has the format of "username@domain"; the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="transactionId"></param>
		/// <returns></returns>
		AuthenticationResponse Authenticate(String username, String password, String transactionId);

		/// <summary>
		/// Standard authentication for external users.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order
		/// If the given username has the format of "username@domain" the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		ExternalAuthenticationResponse ExternalAuthenticate( String username);

		/// <summary>
		/// Standard authentication for external users.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order
		/// If the given username has the format of "username@domain" the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="username"></param> 
		/// <param name="transactionId"></param>
		/// <returns></returns>
		ExternalAuthenticationResponse ExternalAuthenticate(String username, String transactionId);

		/* QR Authentication */
		/* step 1 */
		/// <summary>
		/// Get's the sessionKey string to sign
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order.
		/// If the given username has the format of "username@domain" the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <returns></returns>
		SessionKeyResponse CreateSessionKeyChallenge();
		/// <summary>
		/// Get's the sessionKey string to sign
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order.
		/// If the given username has the format of "username@domain" the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="transactionId"></param>
		/// <returns></returns>
		SessionKeyResponse CreateSessionKeyChallenge( String transactionId);

		/* step 2 */
		/// <summary>
		/// Sends a Push signature to the mobile device.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order.
		/// If the given username has the format of "username@domain" the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="sessionKey"></param>
		/// <returns></returns>
		SessionKeyVerificationResponse VerifySessionKeyStatus(String sessionKey);
		/// <summary>
		/// Sends a Push signature to the mobile device.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order.
		/// If the given username has the format of "username@domain" the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="sessionKey"></param>
		/// <param name="transactionId"></param>
		/// <returns></returns>
		SessionKeyVerificationResponse VerifySessionKeyStatus(String sessionKey, String transactionId);

		/* signature API */
		/// <summary>
		/// Standard authentication for external users.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order.
		/// If the given username has the format of "username@domain" the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="_hash"></param>
		/// <param name="_data"></param>
		/// <param name="title"></param>
		/// <param name="body"></param>
		/// <returns></returns>
		SignatureResponse SendPushSignature(String username, String password, String _hash, String _data, String title, String body);

	}
}

 