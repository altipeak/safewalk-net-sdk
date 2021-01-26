using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class DeleteUserResponse : BaseResponse
    {
        #region "constr"
        public DeleteUserResponse(int httpCode) : base(httpCode)
        {

        }

        public DeleteUserResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion

    }
}
