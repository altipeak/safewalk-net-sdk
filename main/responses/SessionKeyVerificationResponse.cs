using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class SessionKeyVerificationResponse : BaseResponse
    {
        #region "vars"
        private readonly String code;
		#endregion

		#region "constr"

		public SessionKeyVerificationResponse(int httpCode
									, String code) : base(httpCode)
		{
			this.code = code; 
		}

		public SessionKeyVerificationResponse(int httpCode
			, Dictionary<String, List<String>> errors) : base(httpCode, errors)
		{
			this.code = null; 
		}

        #endregion

        #region "publics"
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.httpCode.ToString()).Append(SEPARATOR);

            if (this.code != null)
                sb.Append(this.code.ToString()).Append(SEPARATOR);
            
            foreach (KeyValuePair<String, List<String>> error in this.errors)
            {
                sb.Append(error.Key).Append(" [");
                foreach (String e in error.Value)
                {
                    sb.Append(e).Append(", ");
                }
                sb.Append("]").Append(SEPARATOR);
            }

            return sb.ToString();
        }
        #endregion
    }
}
