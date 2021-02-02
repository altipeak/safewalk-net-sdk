using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class SetStaticPasswordResponse : BaseResponse
    {
        #region "constr"
        public SetStaticPasswordResponse(int httpCode
                                        , JsonObject attributes) : base(httpCode, attributes)
        {

        }

        public SetStaticPasswordResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion
    }
}
