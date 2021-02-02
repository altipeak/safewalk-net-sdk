using System;
using System.Collections.Generic;

namespace safewalk
{
    public class SetStaticPasswordResponse : BaseResponse
    {
        #region "constr"
        public SetStaticPasswordResponse(int httpCode
                                        , Dictionary<String, String> attributes) : base(httpCode, attributes)
        {

        }

        public SetStaticPasswordResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
        }
        #endregion
    }
}
