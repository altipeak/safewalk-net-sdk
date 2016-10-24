using System;
using System.Text;

namespace safewalk
{
	public class SafewalkRespose
	{
		#region "vars"
		private readonly int httpCode;
		private readonly String httpContent;
		private const String SEPARATOR = " | ";
        #endregion

		#region "constr"
		
		public SafewalkRespose(int httpCode
                            , String httpContent)
		{
			this.httpCode = httpCode;
			this.httpContent = httpContent;
		}
    
		#endregion

		#region "Publics"
		public override  String ToString()
		{
			var sb = new StringBuilder();
			sb.Append(this.httpCode.ToString()).Append(SEPARATOR);
			sb.Append(this.httpContent);
			return sb.ToString();
		}

		public int getHttpCode()
		{
			return httpCode;
		}

		public String getHttpContent()
		{
			return httpContent;
		}
		
		#endregion
	}
}