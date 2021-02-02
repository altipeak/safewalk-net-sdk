using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class UpdateUserResponse : BaseResponse
    {
        #region "constr"
        public UpdateUserResponse (int httpCode, JsonObject attributes) :base (httpCode, attributes)
        {

        }

        public UpdateUserResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion
         
    }
}
