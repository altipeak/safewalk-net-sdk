using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class DeleteTokenAssociation : BaseResponse
    {
        #region "vars"
        private String Code;
        #endregion

        #region "constr"
        public DeleteTokenAssociation(int httpCode
                                    , String code) : base(httpCode)
        {
            this.Code = code;
        }

        public DeleteTokenAssociation(int httpCode
            , Dictionary<String, List<String>> errors) : base(httpCode, errors)
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
