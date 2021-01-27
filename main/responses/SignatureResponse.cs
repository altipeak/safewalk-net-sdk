using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class SignatureResponse : BaseResponse
    {
        public SignatureResponse(int httpCode) : base(httpCode)
        {

        }

        public SignatureResponse(int httpCode
            , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {

        }

    }
}
