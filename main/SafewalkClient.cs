using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
	public interface SafewalkClient
	{
		AuthenticationResponse authenticate(String accessToken, String username, String password) ;
    
		AuthenticationResponse authenticate(String accessToken, String username, String password, String transactionId) ;

		CreateUserResponse createUser(String accessToken, String username, String password, String firstName, String lastName, String mobilePhone, String email, String parent) ;

	}
}
