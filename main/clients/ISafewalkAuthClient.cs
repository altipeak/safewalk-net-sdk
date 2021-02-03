using System;
using System.Json;

namespace safewalk
{
	/// <summary>
	/// Safewalk integration Client
	/// 
	/// </summary>
	public interface ISafewalkAuthClient
    {
		/// <summary>
		/// Authenticates the user with the given credentials.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// 
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order
		/// If the given username has the format of "username@domain"; the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password">- Static password / OTP / Backup code</param>
		/// <returns><see cref="AuthenticationResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		AuthenticationResponse Authenticate(String username, String password);

		/// <summary>
		/// Authenticates the user with the given credentials.
		/// 
		/// The username will determine if the user is an internal user or an LDAP user.
		/// 
		/// If the given username has the format of "username" the user will be created as an internal user(In the database). If it doesn't exist, it will look in the LDAP following the priority order
		/// If the given username has the format of "username@domain"; the user will be created in the LDAP with the given domain.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password">Static password / OTP / Backup code</param>
		/// <param name="transactionId">It can be used to link the authentication transaction with a previous authentication transaction.</param>
		/// <returns><see cref="AuthenticationResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		AuthenticationResponse Authenticate(String username, String password, String transactionId);

		/// <summary>
		/// Generates a sessionKey challenge in the server. The key is commonly presented as a QR or a deeplink button to be read by the signer (Safewalk Fast Auth app)
		/// </summary>
		/// <returns><see cref="SessionKeyResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		SessionKeyResponse CreateSessionKeyChallenge();

		/// <summary>
		///  Generates a sessionKey challenge in the server. The key is commonly presented as a QR or a deeplink button to be read by the signer (Safewalk Fast Auth app)
		/// </summary>
		/// <param name="transactionId">It can be used to link the authentication transaction with a previous authentication transaction.</param>
		/// <returns><see cref="SessionKeyResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		SessionKeyResponse CreateSessionKeyChallenge( String transactionId);

		/// <summary>
		/// Retrieves the status of the provided sessionKey.
		/// </summary>
		/// <param name="sessionKey">The challenge obtained with <see cref="CreateSessionKeyChallenge">CreateSessionKeyChallenge()</see></param>
		/// <returns><see cref="SessionKeyVerificationResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		SessionKeyVerificationResponse VerifySessionKeyStatus(String sessionKey);

		/// <summary>
		/// Retrieves the status of the provided sessionKey.
		/// </summary>
		/// <param name="sessionKey">The challenge obtained with <see cref="CreateSessionKeyChallenge">CreateSessionKeyChallenge()</see></param>
		/// <param name="transactionId">It can be used to link the authentication transaction with a previous authentication transaction.</param>
		/// <returns><see cref="SessionKeyVerificationResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		SessionKeyVerificationResponse VerifySessionKeyStatus(String sessionKey, String transactionId);

		/// <summary>
		/// Generates a signature challenge to be signed by the Safewalk Fast Auth and sends a Push/Notification with the signature details to be signed/declined by the user.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="_hash">Sha-256 of the data to be signed</param>
		/// <param name="_data">The data to sign. Data or body are required.</param>
		/// <param name="title">The title displayed in the mobile device. Optional.</param>
		/// <param name="body">The body of the message. Data or body are required.</param>
		/// <returns><see cref="SignatureResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		SignatureResponse SendPushSignature(String username, String password, String _hash, String _data, String title, String body);


		/// <summary>
		/// Notifies Safewalk that the user/password was already validated by an external system. Safewalk will proceed according to the authentication policies defined for the specified user.
		/// </summary>
		/// <param name="username"></param>
		/// <returns><see cref="SecondStepAuthenticationResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		SecondStepAuthenticationResponse SecondStepAuthentication(String username);

		/// <summary>
		/// Notifies Safewalk that the user/password was already validated by an external system. Safewalk will proceed according to the authentication policies defined for the specified user.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="transactionId">It can be used to link the authentication transaction with a previous authentication transaction.</param>
		/// <returns><see cref="SecondStepAuthenticationResponse"/></returns>
		/// <exception cref="ConnectivityException" />
		SecondStepAuthenticationResponse SecondStepAuthentication(String username, String transactionId);

	}
}

 