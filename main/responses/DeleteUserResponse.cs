using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class DeleteUserResponse : BaseResponse
    {
        #region "constr"
        public DeleteUserResponse(int httpCode
                                , JsonObject attributes) : base(httpCode, attributes)
        {

        }

        public DeleteUserResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion

    }
}
