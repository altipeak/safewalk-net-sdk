﻿using System;
using System.Collections.Generic;

namespace safewalk
{
    public class ExternalAuthenticationResponse : AuthenticationResponse
    {
		#region "constr"

		public ExternalAuthenticationResponse(int httpCode
									, Dictionary<String, String> attributes
									, AuthenticationCode? code
									, String transactionId
									, String username
									, String replyMessage
									, String detail
									, ReplyCode? replyCode) 
			: base(httpCode
				  , attributes
				  , code
				  , transactionId
				  , username
				  , replyMessage
				  , detail
				  , replyCode)
		{
		}

		public ExternalAuthenticationResponse(int httpCode
			, Dictionary<String, List<String>> errors) 
			: base(httpCode
				  , errors)
		{
		}
	}

    #endregion
}
