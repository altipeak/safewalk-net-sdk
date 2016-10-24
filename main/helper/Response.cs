using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace safewalk.helper
{
	public class Response
	{
		private readonly String content;
		private readonly int responseCode;

		public Response(String content, int responseCode)
		{
			this.content = content;
			this.responseCode = responseCode;
		}

		public String getContent()
		{
			return content;
		}

		public int getResponseCode()
		{
			return responseCode;
		}

	}
}
