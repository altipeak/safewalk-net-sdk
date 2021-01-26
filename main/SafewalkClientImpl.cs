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
		public AuthenticationResponse Authenticate(String accessToken, String username, String password)
		{
			return this.Authenticate(accessToken, username, password, null);
		}
         
		public AuthenticationResponse Authenticate(String accessToken, String username, String password, String transactionId)
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

		public CreateUserResponse CreateUser(string accessToken, string username, string password, string firstName, string lastName, string mobilePhone, string email, string parent)
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

		public UpdateUserResponse UpdateUser(String accessToken, String username, String mobilePhone, String email)
		{
			var parameters = new Dictionary<String, String>() {
				{"mobile_phone", mobilePhone },
				{"email", email },

			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};

			Response response = serverConnetivityHelper.put(String.Format("/api/v1/admin/user/%s/?format=json", username), parameters, headers);

			if (response.getResponseCode() == 200)
			{
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				return new UpdateUserResponse(response.getResponseCode());
			}
			else
            {
				return new UpdateUserResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
		}

		public GetUserResponse GetUser(String accessToken, String username)
        {
			var parameters = new Dictionary<String, String>() {
				{"format", "json" },
			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};

			Response response = serverConnetivityHelper.get(String.Format( "/api/v1/admin/user/%s/?format=json", username), parameters, headers);

			if (response.getResponseCode() == 200)
			{
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);

				return new GetUserResponse(
					response.getResponseCode()
					, this.getString(jsonResponse, JSON_GET_USER_USERNAME_FIELD)
					, this.getString(jsonResponse, JSON_GET_USER_FIRST_NAME_FIELD)
					, this.getString(jsonResponse, JSON_GET_USER_LAST_NAME_FIELD)
					, this.getString(jsonResponse, JSON_GET_USER_DN_FIELD)
					, this.getString(jsonResponse, JSON_GET_USER_DB_MOBILE_PHONE_FIELD)
					, this.getString(jsonResponse, JSON_GET_USER_DB_EMAIL_FIELD)
					, this.getString(jsonResponse, JSON_GET_USER_LDAP_MOBILE_PHONE_FIELD)
					, this.getString(jsonResponse, JSON_GET_USER_LDAP_EMAIL_FIELD)
					, (this.getString(jsonResponse, JSON_GET_USER_STORAGE_FIELD) != null ? this.getUserStorage(jsonResponse, JSON_GET_USER_STORAGE_FIELD) : null)
					, this.getBoolean(jsonResponse, JSON_GET_USER_IS_LOCKED_FIELD)
					);
			}
			else
			{
				return new GetUserResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
		}
		
		public DeleteUserResponse DeleteUser(String accessToken, String username)
        {
			var parameters = new Dictionary<String, String>() {				 
			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};

			Response response = serverConnetivityHelper.delete(String.Format("/api/v1/admin/user/%s/", username), parameters, headers);
			if (response.getResponseCode() == 204)
			{
				return new DeleteUserResponse(response.getResponseCode());
			}
			else
			{
				return new DeleteUserResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
		}

		public SetStaticPasswordResponse SetStaticPassword(String accessToken, String username, String password)
        {
			var parameters = new Dictionary<String, String>() {
				{ "username", username },
				{ "password", password },
			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};

			Response response = serverConnetivityHelper.post("/api/v1/admin/staticpassword/set/", parameters, headers);
			if (response.getResponseCode() == 200)
			{
				return new SetStaticPasswordResponse(response.getResponseCode());
			}
			else
			{
				return new SetStaticPasswordResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
		}

		public AssociateTokenResponse AssociateToken(String accessToken, String username, DeviceType deviceType)
        {
			return AssociateToken(accessToken, username, deviceType, null, null);
		}

		public AssociateTokenResponse AssociateToken(String accessToken, String username, DeviceType deviceType, Boolean? sendRegistrationCode, Boolean? sendDownloadLinks)
		{
			var parameters = new Dictionary<String, String>() {
				{"username", username },
				{"device_type", deviceType.getCode() },
			};

			if (sendDownloadLinks != null)
			{
				parameters.Add("send_download_links", sendDownloadLinks.ToString());
			}
			if (sendRegistrationCode != null)
			{
				parameters.Add("send_registration_code", sendRegistrationCode.ToString());
			}

			var headers = new Dictionary<String, String>() {
					{ "Authorization", "Bearer " + accessToken }
				};
			Response response = serverConnetivityHelper.post(String.Format("/api/v1/admin/user/%s/devices/?format=json", username), parameters, headers);

			if (response.getResponseCode() == 200)
			{
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);

				return new AssociateTokenResponse(response.getResponseCode()
											, this.getBoolean(jsonResponse, JSON_ASSOCIATE_TOKEN_FAIL_TO_SEND_REG_CODE_FIELD)
											, this.getBoolean(jsonResponse, JSON_ASSOCIATE_TOKEN_FAIL_TO_SEND_DOWNLOAD_LINKS_FIELD));
			}
			else
			{
				return new AssociateTokenResponse(response.getResponseCode(), getErrors(response.getContent()));
			}

		}

		public GetTokenAssociationsResponse GetTokenAssociations(String accessToken, String username)
		{
			var parameters = new Dictionary<String, String>() {
				{"format", "json" },
			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};

			Response response = serverConnetivityHelper.get(String.Format("/api/v1/admin/user/%s/devices/", username), parameters, headers);

			if (response.getResponseCode() == 200)
			{
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonArray = (JsonArray)JsonValue.Load(stream);

				List<TokenAssociation> associations = new List<TokenAssociation>();

				for (int i = 0; jsonArray.Count>0; i++ )
                {
					var json = (JsonObject)jsonArray[i];
					associations.Add(new TokenAssociation(
						  this.getString(json, JSON_GET_TOKEN_ASSOCIATIONS_DEVICE_TYPE_FIELD)
						, this.getString(json, JSON_GET_TOKEN_ASSOCIATIONS_SERIAL_NUMBER_FIELD)
						, this.getBoolean(json, JSON_GET_TOKEN_ASSOCIATIONS_CONFIRMED_FIELD)
						, this.getBoolean(json, JSON_GET_TOKEN_ASSOCIATIONS_PASSWORD_REQUIRED_FIELD)));
				}
				return new GetTokenAssociationsResponse(response.getResponseCode(), associations);
			}
			else
			{
				return new GetTokenAssociationsResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
		}

		public DeleteTokenAssociation DeleteTokenAssociation(String accessToken, String username, DeviceType deviceType, String serialNumber)
        {
			var parameters = new Dictionary<String, String>() {
				{"format", "json" },
			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};

			Response response = serverConnetivityHelper.delete(String.Format("/api/v1/admin/user/%s/devices/%s/%s/", username, deviceType.getCode(), serialNumber), parameters, headers);
			if (response.getResponseCode() == 200 || response.getResponseCode() == 400)
			{
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);

				return new DeleteTokenAssociation(response.getResponseCode(), this.getString(jsonResponse, JSON_DELETE_TOKEN_ASSOCIATION_CODE_FIELD));
			}
			else
			{
				return new DeleteTokenAssociation(response.getResponseCode(), getErrors(response.getContent()));
			}
		}

		public CreateRegistrationCode CreateRegistrationCode(String accessToken, String username)
        {
			var parameters = new Dictionary<String, String>() {
			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + accessToken }
			};

			Response response = serverConnetivityHelper.post(String.Format("/api/v1/admin/user/%s/registrationtoken/?format=json", username), parameters, headers);
			if (response.getResponseCode() == 200)
			{
				return new CreateRegistrationCode(response.getResponseCode());
			}
			else if (response.getResponseCode() == 400)
			{
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);
				return new CreateRegistrationCode(response.getResponseCode(), getErrors(response.getContent()), this.getString(jsonResponse, JSON_CREATE_REGISTRATION_CODE_CODE_FIELD));
			}
			else
			{
				return new CreateRegistrationCode(response.getResponseCode(), getErrors(response.getContent()), null);
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
