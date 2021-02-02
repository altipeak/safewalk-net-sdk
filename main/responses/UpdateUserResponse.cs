using System;
using System.Collections.Generic;

namespace safewalk
{
    public class UpdateUserResponse : BaseResponse
    {
        #region "constr"
        public UpdateUserResponse (int httpCode, Dictionary<String, String> attributes) :base (httpCode, attributes)
        {

        }

        public UpdateUserResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion
         
    }
}
