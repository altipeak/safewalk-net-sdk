using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Json;
using safewalk.helper;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace safewalk
{
	public class SafewalkClientImpl : SafewalkClient
	{
		/* Authentication response */
		private readonly  String JSON_AUTH_REPLY_MESSAGE_FIELD = "reply-message";
		private readonly  String JSON_AUTH_CODE_FIELD = "code";
		private readonly  String JSON_AUTH_TRANSACTION_FIELD = "transaction-id";
		private readonly  String JSON_AUTH_USERNAME_ID_FIELD = "username";
		private readonly  String JSON_AUTH_DETAIL_FIELD = "detail";
		private readonly String JSON_AUTH_REPLY_CODE = "reply-code";

		private  ServerConnectivityHelper serverConnetivityHelper;

		#region "constr"
		public SafewalkClientImpl(ServerConnectivityHelper serverConnetivityHelper)
		{
			this.serverConnetivityHelper = serverConnetivityHelper;
		}
		#endregion
		
		#region "publics"
		public AuthenticationResponse authenticate(String accessToken, String username, String password)
		{
			return this.authenticate(accessToken, username, password, null);
		}
         
		public AuthenticationResponse authenticate(String accessToken, String username, String password, String transactionId)
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
            
			if (response.getResponseCode() == 200 || response.getResponseCode() == 401) {
				// convert string to stream
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);
            
				return new AuthenticationResponse(
					response.getResponseCode()
                , this.getAuthenticationCode(jsonResponse, JSON_AUTH_CODE_FIELD)
                , this.getString(jsonResponse, JSON_AUTH_TRANSACTION_FIELD)
                , this.getString(jsonResponse, JSON_AUTH_USERNAME_ID_FIELD)
                , this.getString(jsonResponse, JSON_AUTH_REPLY_MESSAGE_FIELD)
                , this.getString(jsonResponse, JSON_AUTH_DETAIL_FIELD)
                , this.getReplyCode(jsonResponse, JSON_AUTH_REPLY_CODE)
				);
			} else {
				return new AuthenticationResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
            
		}
		#endregion
		
		#region "privates"
		private String getString(IDictionary<string, JsonValue> json, String key)
		{
			if (json.ContainsKey(key))
			if (json[key] != null)
				return json[key].ToString();

			return "";
             
		}

		private Boolean getBoolean(IDictionary<string, JsonValue> json, String key)
		{
			if (json.ContainsKey(key))
			if (json[key] != null)
				return bool.Parse(json[key].ToString());

			return false;
            
		}

		private AuthenticationCode? getAuthenticationCode(IDictionary<string, JsonValue> json, String key)
		{
			if (json.ContainsKey(key) && json[key] != null) {
				return (AuthenticationCode)Enum.Parse(typeof(AuthenticationCode), (string)json[key]);
			}
			return null;
		}
		
		private ReplyCode? getReplyCode(IDictionary<string, JsonValue> json, String key)
		{
			if (json.ContainsKey(key) && json[key] != null) {
				return (ReplyCode)Enum.Parse(typeof(ReplyCode), (string)json[key]);
			}
			return null;
		}

		private Dictionary<String, List<String>> getErrors(String errors)
		{
			var result = new Dictionary<String, List<String>>();
			try {
                
				// convert string to stream
				byte[] byteArray = Encoding.UTF8.GetBytes(errors);
				//byte[] byteArray = Encoding.ASCII.GetBytes(contents);
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);
            
				List<string > keys = jsonResponse.Keys.ToList();
				int cant = 0;
				while (cant < keys.Count) {
					var key = (String)keys[cant];
					List<String> values = new List<string>();
					var data = jsonResponse[key];
					foreach (var d in data) {
						values.Add((String)d.Value);
					}
					result.Add(key, values);
					cant += 1;
				}
				return result;
			} catch (ArgumentException e) {
				return new Dictionary<String, List<String>>();
			} catch (FormatException e) {
				return new Dictionary<String, List<String>>();
			}
		}
		#endregion
	}
    
}
