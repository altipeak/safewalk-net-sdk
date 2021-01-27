using System;
 

namespace safewalk
{
	public interface ISafewalkAuthClient
    {
		AuthenticationResponse Authenticate(String accessToken, String username, String password);

		AuthenticationResponse Authenticate(String accessToken, String username, String password, String transactionId);

		ExternalAuthenticationResponse ExternalAuthenticate(String accessToken, String username);
		ExternalAuthenticationResponse ExternalAuthenticate(String accessToken, String username,  String excludeHybrid, String transactionId);

		/* QR Authentication */
		/* step 1 */
		SessionKeyResponse CreateSessionKeyChallenge(String accessToken, String username);
		SessionKeyResponse CreateSessionKeyChallenge(String accessToken, String username, String excludeHybrid, String transactionId);

		/* step 2 */
		SessionKeyVerificationResponse VerifySessionKeyStatus(String accessToken, String username, String sessionKey);
		SessionKeyVerificationResponse VerifySessionKeyStatus(String accessToken, String username, String sessionKey, String excludeHybrid, String transactionId);

		/* signature API */
		SignatureResponse SendPushSignature(String accessToken, String username, String password, String _hash, String _data, String title, String body);

	}
}

 