using System;
using System.Collections.Generic;

namespace safewalk
{
    public class DeleteUserResponse : BaseResponse
    {
        #region "constr"
        public DeleteUserResponse(int httpCode
                                , Dictionary<String, String> attributes) : base(httpCode, attributes)
        {

        }

        public DeleteUserResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion

    }
}
