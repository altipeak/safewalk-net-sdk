using System;
using System.Collections.Generic;
using System.Json;
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

        protected Dictionary<String, String> attributes;
        #endregion

        #region "constr"
       
        public BaseResponse(int httpCode, Dictionary<String, String> attributes)
        {
            this.httpCode = httpCode;
            this.errors = new Dictionary<string, List<string>>();
            this.attributes = attributes;
        }

        public BaseResponse(int httpCode
            , Dictionary<String, List<String>> errors)
        {
            this.httpCode = httpCode;            
            this.errors = errors;
            this.attributes = new Dictionary<String, String>();
        }
        public BaseResponse(int httpCode
            , Dictionary<String, String> attributes
            , Dictionary<String, List<String>> errors)
        {
            this.httpCode = httpCode;
            this.errors = errors;
            this.attributes = attributes;
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
        
        public Dictionary<String, String> getAttributes()
        {
            return this.attributes;
        }
        #endregion

    }
}
