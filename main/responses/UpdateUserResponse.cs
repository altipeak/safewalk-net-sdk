using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class UpdateUserResponse : BaseResponse
    {
        #region "constr"
        public UpdateUserResponse (int httpCode) :base (httpCode)
        {

        }

        public UpdateUserResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion
         
    }
}
