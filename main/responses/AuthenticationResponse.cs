using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
	public enum AuthenticationCode 
	{
		ACCESS_ALLOWED,
		ACCESS_CHALLENGE,
		ACCESS_DENIED
	}

	public enum ReplyCode
	{
		///<sumary>
		/// The user is locked. 
		///</sumary>
		USR_LOCKED,
		///<sumary>
		/// An internal error occured. 
		///</sumary>
		INTERNAL_SYSTEM_ERROR,
		///<sumary>
		/// Invalid credentials. 
		///</sumary>
		INVALID_CREDENTIALS,
		///<sumary>
		/// OTP required. This is the case where the user has a token with password required enabled (2 Step authentication).
		///</sumary>
		OTP_REQUIRED,
		///<sumary>
		/// The device (token) is out of sync
		///</sumary>
		DEVICE_OUT_OF_SYNC
	}
	
	public class AuthenticationResponse : BaseResponse
	{
		#region "vars"
		private readonly AuthenticationCode? code;
		private readonly String transactionId;
		private readonly String username;
		private readonly String replyMessage;
		private readonly String detail;
		private readonly ReplyCode? replyCode;
    
		
		#endregion

		#region "constr"
		
		public AuthenticationResponse(int httpCode
                                    , AuthenticationCode? code
                                    , String transactionId
                                    , String username
                                    , String replyMessage
                                    , String detail
                                    , ReplyCode? replyCode) : base(httpCode)
		{
			this.code = code;
			this.transactionId = transactionId;
			this.username = username;
			this.replyMessage = replyMessage; 
			this.detail = detail;
			this.replyCode = replyCode;
		}
    
		public AuthenticationResponse(int httpCode
			, Dictionary<String, List<String>> errors) : base(httpCode, errors)
		{
			this.code = null;
			this.transactionId = null;
			this.username = null;
			this.replyMessage = null; 
			this.detail = null;
			this.replyCode = null; 
		}
		#endregion

		#region "Publics"
		public override  String ToString()
		{
			var sb = new StringBuilder();
			sb.Append(this.httpCode.ToString()).Append(SEPARATOR);
			if (this.code != null)
				sb.Append(this.code).Append(SEPARATOR);
			if (this.transactionId != null)
				sb.Append(this.transactionId).Append(SEPARATOR);
			if (this.username != null)
				sb.Append(this.username).Append(SEPARATOR);
			if (this.replyMessage != null)
				sb.Append(this.replyMessage).Append(SEPARATOR);
			if (this.detail != null)
				sb.Append(this.detail).Append(SEPARATOR);
			if (this.replyCode != null)
				sb.Append(this.replyCode).Append(SEPARATOR);

			foreach (KeyValuePair<String, List<String>> error in this.errors) {
				sb.Append(error.Key).Append(" [");
				foreach (String e in error.Value) {
					sb.Append(e).Append(", ");
				}
				sb.Append("]").Append(SEPARATOR);
			}
        
			return sb.ToString();
		}

		public AuthenticationCode? getCode()
		{
			return code;
		}

		public String getTransactionId()
		{
			return transactionId;
		}

		public String getUsername()
		{
			return username;
		}

		public String getReplyMessage()
		{
			return replyMessage;
		}

		public int getHttpCode()
		{
			return httpCode;
		}

		public String getDetail()
		{
			return detail;
		}
		
		public ReplyCode? getReplyCode()
		{
			return replyCode;
		}
		#endregion
	}
}