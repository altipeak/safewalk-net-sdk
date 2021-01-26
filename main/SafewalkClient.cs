using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
	public interface SafewalkClient
	{
		AuthenticationResponse Authenticate(String accessToken, String username, String password) ;
    
		AuthenticationResponse Authenticate(String accessToken, String username, String password, String transactionId) ;

		CreateUserResponse CreateUser(String accessToken, String username, String password, String firstName, String lastName, String mobilePhone, String email, String parent) ;

		UpdateUserResponse UpdateUser(String accessToken, String username, String mobilePhone, String email);

		GetUserResponse GetUser(String accessToken, String username);

		DeleteUserResponse DeleteUser(String accessToken, String username);

		SetStaticPasswordResponse SetStaticPassword(String accessToken, String username, String password);

		AssociateTokenResponse AssociateToken(String accessToken, String username, DeviceType deviceType);

		AssociateTokenResponse AssociateToken(String accessToken, String username, DeviceType deviceType, Boolean sendRegistrationCode, Boolean sendDownloadLinks);

		GetTokenAssociationsResponse GetTokenAssociations(String accessToken, String username);

		DeleteTokenAssociation DeleteTokenAssociation(String accessToken, String username, DeviceType deviceType, String serialNumber);

		CreateRegistrationCode CreateRegistrationCode(String accessToken, String username);

	}
}
