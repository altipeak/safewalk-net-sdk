﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using safewalk.helper;
using System.IO;

namespace safewalk
{
    public class SafewalkAuthClient : ISafewalkAuthClient
    {
		#region "consts"
		/* Authentication response */
		private readonly String JSON_AUTH_REPLY_MESSAGE_FIELD = "reply-message";
		private readonly String JSON_AUTH_REPLY_CODE_FIELD = "reply-code";
		private readonly String JSON_AUTH_CODE_FIELD = "code";
		private readonly String JSON_AUTH_TRANSACTION_FIELD = "transaction-id";
		private readonly String JSON_AUTH_USERNAME_ID_FIELD = "username";
		private readonly String JSON_AUTH_DETAIL_FIELD = "detail";

		private readonly String JSON_AUTH_CHALLENGE = "challenge";
		private readonly String JSON_AUTH_PURPOSE = "purpose";

		private readonly String JSON_AUTH_CODE = "code";

		#endregion

		#region "vars"
		private IServerConnectivityHelper ServerConnetivityHelper;
		private String AccessToken;
		#endregion

		#region "constr"
		public SafewalkAuthClient(IServerConnectivityHelper serverConnetivityHelper, String accessToken)
		{
			this.ServerConnetivityHelper = serverConnetivityHelper;
			this.AccessToken = accessToken;
		}
		#endregion

		#region "publics"
		public AuthenticationResponse Authenticate(String username, String password)
		{
			return this.Authenticate(username, password, null);
		}

		public AuthenticationResponse Authenticate(String username, String password, String transactionId)
		{
			var parameters = new Dictionary<String, String>() {
				{ "username", username },
				{ "password", password },
				{ "transaction_id", transactionId }
			};


			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + AccessToken }
			};

			Response response = ServerConnetivityHelper.post("/api/v1/auth/authenticate/?format=json", parameters, headers);

			if (response.getResponseCode() == 200 || response.getResponseCode() == 401)
			{
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
			}
			else
			{
				return new AuthenticationResponse(response.getResponseCode(), getErrors(response.getContent()));
			}

		}

		public ExternalAuthenticationResponse AuthenticateExternal(String username)
        {
			return this.ExternalAuthenticate(username, null);
        }
		public ExternalAuthenticationResponse ExternalAuthenticate(String username, String transactionId)
		{
			var parameters = new Dictionary<String, String>() {
				{ "username", username }, 
				{ "transaction_id", transactionId }
			};


			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + AccessToken }
			};

			Response response = ServerConnetivityHelper.post("/api/v1/auth/pswdcheckedauth/?format=json", parameters, headers);

			if (response.getResponseCode() == 200 || response.getResponseCode() == 401)
			{
				// convert string to stream
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);

				return new ExternalAuthenticationResponse(
					response.getResponseCode()
				, this.getAuthenticationCode(jsonResponse, JSON_AUTH_CODE_FIELD)
				, this.getString(jsonResponse, JSON_AUTH_TRANSACTION_FIELD)
				, this.getString(jsonResponse, JSON_AUTH_USERNAME_ID_FIELD)
				, this.getString(jsonResponse, JSON_AUTH_REPLY_MESSAGE_FIELD)
				, this.getString(jsonResponse, JSON_AUTH_DETAIL_FIELD)
				, this.getReplyCode(jsonResponse, JSON_AUTH_REPLY_CODE_FIELD)
				);
			}
			else
			{
				return new ExternalAuthenticationResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
		}


		public SessionKeyResponse CreateSessionKeyChallenge() 
		{
			return this.CreateSessionKeyChallenge(null);
		}
		public SessionKeyResponse CreateSessionKeyChallenge(String transactionId)
        {
			var parameters = new Dictionary<String, String>() {
				{ "transaction_id", transactionId },
			};


			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + AccessToken }
			};

			Response response = ServerConnetivityHelper.post("/api/v1/auth/session_key/?format=json", parameters, headers);

			if (response.getResponseCode() == 200 )
			{
				// convert string to stream
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);

				return new SessionKeyResponse(
					response.getResponseCode()
				, this.getStringWithoutQuotes(jsonResponse, JSON_AUTH_CHALLENGE)
				, this.getString(jsonResponse, JSON_AUTH_PURPOSE) 
				);
			}
			else
			{
				return new SessionKeyResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
		}

		public SessionKeyVerificationResponse VerifySessionKeyStatus(String sessionKey)
        {
			return this.VerifySessionKeyStatus(sessionKey, null);
		}
		public SessionKeyVerificationResponse VerifySessionKeyStatus(
			 String sessionKey
			, String transactionId)
		{
			var parameters = new Dictionary<String, String>() {
				{ "transaction_id", transactionId },
			};


			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + AccessToken }
			};

			var link = "/api/v1/auth/session_key/" + sessionKey + "/?format=json";
			Response response = ServerConnetivityHelper.get(link, parameters, headers);

			if (response.getResponseCode() == 200)
			{
				// convert string to stream
				byte[] byteArray = Encoding.UTF8.GetBytes(response.getContent());
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);

				return new SessionKeyVerificationResponse(
					response.getResponseCode()
				, this.getString(jsonResponse, JSON_AUTH_CODE) 
				);
			}
			else
			{
				return new SessionKeyVerificationResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
		}

		public SignatureResponse SendPushSignature(
			 String username
			, String password
			, String _hash
			, String _data
			, String title
			, String body)
		{
			var parameters = new Dictionary<String, String>() {
				{ "username", username },
				{ "password", password },
				{ "hash", _hash },
				{ "data", _data },
				{ "title", title },
				{ "body", body }
			};

			var headers = new Dictionary<String, String>() {
				{ "Authorization", "Bearer " + AccessToken }
			};

			Response response = ServerConnetivityHelper.post("/api/v1/auth/push_signature/", parameters, headers);
			if (response.getResponseCode() == 200)
			{
				return new SignatureResponse(response.getResponseCode());
			}
			else if (response.getResponseCode() == 400)
			{
				return new SignatureResponse(response.getResponseCode(), getErrors(response.getContent()));
			}
			else
			{
				return new SignatureResponse(response.getResponseCode(), getErrors(response.getContent()));
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

		private String getStringWithoutQuotes(IDictionary<string, JsonValue> json, String key)
		{
			if (json.ContainsKey(key))
				if (json[key] != null)
					return json[key].ToString().Replace("\"", "");

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
			if (json.ContainsKey(key) && json[key] != null)
			{
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
			if (json.ContainsKey(key) && json[key] != null)
			{
				return (ReplyCode)Enum.Parse(typeof(ReplyCode), (string)json[key]);
			}
			return null;
		}

		private Dictionary<String, List<String>> getErrors(String errors)
		{
			var result = new Dictionary<String, List<String>>();
			try
			{

				// convert string to stream
				byte[] byteArray = Encoding.UTF8.GetBytes(errors);
				//byte[] byteArray = Encoding.ASCII.GetBytes(contents);
				var stream = new MemoryStream(byteArray);

				var jsonResponse = (JsonObject)JsonValue.Load(stream);

				List<string> keys = jsonResponse.Keys.ToList();
				int cant = 0;
				while (cant < keys.Count)
				{
					var key = (String)keys[cant];
					List<String> values = new List<string>();
					var data = jsonResponse[key];
					foreach (var d in data)
					{
						values.Add((String)d.Value);
					}
					result.Add(key, values);
					cant += 1;
				}
				return result;
			}
			catch (ArgumentException e)
			{
				return new Dictionary<String, List<String>>();
			}
			catch (FormatException e)
			{
				return new Dictionary<String, List<String>>();
			}
		}


		#endregion
	}
}
