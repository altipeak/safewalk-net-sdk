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
        #region "consts"
        /* Authentication response */
        private readonly  String JSON_AUTH_REPLY_MESSAGE_FIELD = "reply-message";
		private readonly  String JSON_AUTH_REPLY_CODE_FIELD = "reply-code";
		private readonly  String JSON_AUTH_CODE_FIELD = "code";
		private readonly  String JSON_AUTH_TRANSACTION_FIELD = "transaction-id";
		private readonly  String JSON_AUTH_USERNAME_ID_FIELD = "username";
		private readonly  String JSON_AUTH_DETAIL_FIELD = "detail";
		/* Create user response */
		private readonly  String JSON_CREATE_USER_USERNAME_FIELD = "username";
		private readonly  String JSON_CREATE_USER_FIRST_NAME_FIELD = "first_name";
		private readonly  String JSON_CREATE_USER_LAST_NAME_FIELD = "last_name";
		private readonly  String JSON_CREATE_USER_DN_FIELD = "dn";
		private readonly  String JSON_CREATE_USER_DB_MOBILE_PHONE_FIELD = "db_mobile_phone";
		private readonly  String JSON_CREATE_USER_DB_EMAIL_FIELD = "db_email";
		private readonly  String JSON_CREATE_USER_LDAP_MOBILE_PHONE_FIELD = "ldap_mobile_phone";
		private readonly  String JSON_CREATE_USER_LDAP_EMAIL_FIELD = "ldap_email";
		private readonly  String JSON_CREATE_USER_STORAGE_FIELD = "user_storage";
		/* Get user response */
		private readonly  String JSON_GET_USER_USERNAME_FIELD = "username";
		private readonly  String JSON_GET_USER_FIRST_NAME_FIELD = "first_name";
		private readonly  String JSON_GET_USER_LAST_NAME_FIELD = "last_name";
		private readonly  String JSON_GET_USER_DN_FIELD = "dn";
		private readonly  String JSON_GET_USER_DB_MOBILE_PHONE_FIELD = "db_mobile_phone";
		private readonly  String JSON_GET_USER_DB_EMAIL_FIELD = "db_email";
		private readonly  String JSON_GET_USER_LDAP_MOBILE_PHONE_FIELD = "ldap_mobile_phone";
		private readonly  String JSON_GET_USER_LDAP_EMAIL_FIELD = "ldap_email";
		private readonly  String JSON_GET_USER_STORAGE_FIELD = "user_storage";
		private readonly  String JSON_GET_USER_IS_LOCKED_FIELD = "is_locked";
		/* Associate token response */
		private readonly  String JSON_ASSOCIATE_TOKEN_FAIL_TO_SEND_REG_CODE_FIELD = "fail_to_send_registration_code";
		private readonly  String JSON_ASSOCIATE_TOKEN_FAIL_TO_SEND_DOWNLOAD_LINKS_FIELD = "fail_to_send_download_links";
		/* Associations response */
		private readonly  String JSON_GET_TOKEN_ASSOCIATIONS_DEVICE_TYPE_FIELD = "type";
		private readonly  String JSON_GET_TOKEN_ASSOCIATIONS_SERIAL_NUMBER_FIELD = "serial_number";
		private readonly  String JSON_GET_TOKEN_ASSOCIATIONS_CONFIRMED_FIELD = "confirmed";
		private readonly  String JSON_GET_TOKEN_ASSOCIATIONS_PASSWORD_REQUIRED_FIELD = "password_required";
		/* Delete associations response */
		private readonly  String JSON_DELETE_TOKEN_ASSOCIATION_CODE_FIELD = "code";
		/* Create registration code response */
		private readonly  String JSON_CREATE_REGISTRATION_CODE_CODE_FIELD = "code";
        #endregion

        #region "vars"
        private ServerConnectivityHelper serverConnetivityHelper;
        #endregion

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
                , this.getReplyCode(jsonResponse, JSON_AUTH_REPLY_CODE_FIELD)
				);
			} else {
				return new AuthenticationResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
            
		}

		public CreateUserResponse createUser(string accessToken, string username, string password, string firstName, string lastName, string mobilePhone, string email, string parent)
		{
			var parameters = new Dictionary<String, String>() {
				{ "username", username },
				{ "password", password }, 
				{"first_name", firstName },
				{"last_name", lastName },
				{"mobile_phone", mobilePhone },
				{"email", email },
				{"parent", parent },
			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};

			Response response = serverConnetivityHelper.post("/api/v1/admin/user/?format=json", parameters, headers);

			if (response.getResponseCode() == 201)
            {
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);

				return new CreateUserResponse(
					response.getResponseCode()
					, this.getString(jsonResponse, JSON_CREATE_USER_USERNAME_FIELD)
					, this.getString(jsonResponse, JSON_CREATE_USER_FIRST_NAME_FIELD)
					, this.getString(jsonResponse, JSON_CREATE_USER_LAST_NAME_FIELD)
					, this.getString(jsonResponse, JSON_CREATE_USER_DN_FIELD)
					, this.getString(jsonResponse, JSON_CREATE_USER_DB_MOBILE_PHONE_FIELD)
					, this.getString(jsonResponse, JSON_CREATE_USER_DB_EMAIL_FIELD)
					, this.getString(jsonResponse, JSON_CREATE_USER_LDAP_MOBILE_PHONE_FIELD)
					, this.getString(jsonResponse, JSON_CREATE_USER_LDAP_EMAIL_FIELD)
					, (this.getString(jsonResponse, JSON_CREATE_USER_STORAGE_FIELD) != null ? this.getUserStorage(jsonResponse, JSON_CREATE_USER_STORAGE_FIELD) : null)
					);
			}
			else
            {
				return new CreateUserResponse(response.getResponseCode(), getErrors(response.getContent()));
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

		private UserStorage? getUserStorage(IDictionary<string, JsonValue> json, String key)
		{
			if (json.ContainsKey(key) && json[key] != null)
			{
				return (UserStorage)Enum.Parse(typeof(UserStorage), (string)json[key]);
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
