using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class BaseResponse
    {
        #region "vars"
        protected int httpCode;

        protected Dictionary<String, List<String>> errors;

        protected const String SEPARATOR = " | ";
        #endregion

        #region "constr"
        public BaseResponse(int httpCode)
        {
            this.httpCode = httpCode;
            this.errors = new Dictionary<string, List<string>>();
        }

        public BaseResponse(int httpCode
            , Dictionary<String, List<String>> errors)
        {
            this.httpCode = httpCode;            
            this.errors = errors;
        }
        #endregion
 
        #region "Publics"
        public Dictionary<String, List<String>> getErrors()
        {
            return errors;
        }

        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.httpCode.ToString()).Append(SEPARATOR);

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
