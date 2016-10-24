using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace safewalk
{
	public interface SafewalkClient
	{
		SafewalkRespose authenticate(String accessToken, String username, String password) ;

        SafewalkRespose authenticate(String accessToken, String username, String password, String transactionId) ;
    
	}
}
