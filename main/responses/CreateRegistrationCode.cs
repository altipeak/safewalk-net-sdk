using System;
using System.Collections.Generic;
using System.Text;

namespace safewalk
{
    public class CreateRegistrationCode : BaseResponse
    {
        #region "vars"
        private String Code;
        #endregion

        #region "constr"
        public CreateRegistrationCode(int httpCode
                                    , Dictionary<String, String> attributes) : base(httpCode, attributes)
        {
            this.Code = null;
        }
        public CreateRegistrationCode(int httpCode
                                    , Dictionary<String, String> attributes
                                    , String code) : base(httpCode, attributes)
        {
            this.Code = code;
        }

        public CreateRegistrationCode(int httpCode
            , Dictionary<String, List<String>> errors
            , String code) : base(httpCode, errors)
        {

        }
        #endregion

        #region "publics"
        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.httpCode.ToString()).Append(SEPARATOR);
            sb.Append(this.Code.ToString()).Append(SEPARATOR);

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
