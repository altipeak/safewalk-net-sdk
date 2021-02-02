using System;
using System.Collections.Generic;
using System.Text;

namespace safewalk
{
    public class AssociateTokenResponse : BaseResponse
    {
        #region "vars"
        private Boolean? failToSendRegistrationCode;
        private Boolean? failToSendDownloadLinks;
        #endregion

        #region "constr"
        public AssociateTokenResponse(int httpCode
            , Dictionary<String, String> attributes
            , Boolean failToSendRegistrationCode
            , Boolean failToSendDownloadLinks) : base(httpCode, attributes)
        {
            this.failToSendDownloadLinks = failToSendDownloadLinks;
            this.failToSendRegistrationCode = failToSendRegistrationCode;
        }

        public AssociateTokenResponse(int httpCode
           , Dictionary<String, List<String>> errors) : base(httpCode, errors)
        {
            this.failToSendDownloadLinks = null;
            this.failToSendRegistrationCode = null;
        }

        #endregion

        #region "publics"
        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.httpCode.ToString()).Append(SEPARATOR);

            if (this.httpCode == 200)
            {
                sb.Append(this.failToSendRegistrationCode).Append(SEPARATOR);
                sb.Append(this.failToSendDownloadLinks).Append(SEPARATOR);
            }

            foreach (KeyValuePair<String, List<String>> error in this.errors)
            {
                sb.Append(error.Key).Append(" [");
                foreach (String e in error.Value)
                {
                    sb.Append(e).Append(", ");
                }
                sb.Append("]").Append(SEPARATOR);
            }

            return sb.ToString();
        }
        #endregion
    }
}
