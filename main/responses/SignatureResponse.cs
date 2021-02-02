using System;
using System.Collections.Generic;

namespace safewalk
{
    public class SignatureResponse : BaseResponse
    {
        public SignatureResponse(int httpCode, Dictionary<String, String> attributes) : base(httpCode, attributes)
        {

        }

        public SignatureResponse(int httpCode
            , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {

        }

    }
}
