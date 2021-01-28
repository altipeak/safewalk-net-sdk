using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class TokenAssociation
    {
        private String serialNumber ;
        private String deviceType;
        private Boolean confirmed;
        private Boolean passwordRequired;
                
        public TokenAssociation(String deviceType
                              , String serialNumber
                              , Boolean confirmed
                              , Boolean passwordRequired)
        {
            this.serialNumber = serialNumber;
            this.deviceType = deviceType;
            this.confirmed = confirmed;
            this.passwordRequired = passwordRequired;
        }
         
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.deviceType).Append(", ");
            sb.Append(this.serialNumber).Append(", ");
            sb.Append(this.confirmed);
            return sb.ToString();
        }
         
        public String getSerialNumber()
        {
            return serialNumber;
        }
 
        public String getDeviceType()
        {
            return deviceType;
        }

       
        public Boolean getConfirmed()
        {
            return confirmed;
        }

        
        public Boolean getPasswordRequired()
        {
            return passwordRequired;
        }
    }
}
