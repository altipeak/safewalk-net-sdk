using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class SetStaticPasswordResponse : BaseResponse
    {
        #region "constr"
        public SetStaticPasswordResponse(int httpCode) : base(httpCode)
        {

        }

        public SetStaticPasswordResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion
    }
}
