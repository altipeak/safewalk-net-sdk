using System;
using System.Collections.Generic;
using System.Text;
using safewalk.helper;

namespace safewalk
{
	public class SafewalkClientImpl : SafewalkClient
    {
		private  ServerConnectivityHelper serverConnetivityHelper;

		#region "constr"
		public SafewalkClientImpl(ServerConnectivityHelper serverConnetivityHelper)
		{
			this.serverConnetivityHelper = serverConnetivityHelper;
		}
		#endregion
		
		#region "publics"
		public SafewalkRespose authenticate(String accessToken, String username, String password)
		{
			return this.authenticate(accessToken, username, password, null);
		}
         
		public SafewalkRespose authenticate(String accessToken, String username, String password, String transactionId)
		{
			var parameters = new Dictionary<String, String>() { 
				{ "username", username },
				{ "password", password },
				{ "transaction_id", transactionId }            
			};
            

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};
               
			Response response = serverConnetivityHelper.post("/api/v1/auth/authenticate/?format=json", parameters, headers);

            byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
            return new SafewalkRespose(response.getResponseCode(), Encoding.ASCII.GetString(byteArray));
        }
		#endregion
		
	}
    
}
