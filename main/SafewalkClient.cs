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
    
	}
}
