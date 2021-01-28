using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class SessionKeyResponse : BaseResponse
    { 
        #region "vars"
        private readonly String challenge;
        private readonly String purpose;
		#endregion

		#region "constr"

		public SessionKeyResponse(int httpCode
									, String challenge
									, String purpose) : base(httpCode)
		{
			this.challenge = challenge;
			this.purpose = purpose;
		}

		public SessionKeyResponse(int httpCode
			, Dictionary<String, List<String>> errors) : base(httpCode, errors)
		{
			this.challenge = null;
			this.purpose = null;
		}
        #endregion

        #region "publics"
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.httpCode.ToString()).Append(SEPARATOR);

            if (this.challenge != null)
                sb.Append(this.challenge.ToString()).Append(SEPARATOR);
            else
                sb.Append(SEPARATOR);

            if (this.purpose != null)
                sb.Append(this.purpose.ToString()).Append(SEPARATOR);
            else
                sb.Append(SEPARATOR);

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
        
        public String GetChallenge()
        {
            return this.challenge;
        }
        #endregion
    }
}
