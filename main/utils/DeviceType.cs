using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk
{
    public class DeviceType
    {
        public static readonly DeviceType VIRTUAL = new DeviceType("Virtual");
        public static readonly DeviceType SESAMI_MOBILE = new DeviceType("SESAMI:Mobile");
        public static readonly DeviceType SESAMI_MOBILE_HYBRID = new DeviceType("SESAMI:Mobile:Hybrid");
        public static readonly DeviceType TOTP_MOBILE = new DeviceType("TOTP:Mobile");
        public static readonly DeviceType TOTP_MOBILE_HYBRID = new DeviceType("TOTP:Mobile:Hybrid");
         
        public string code { get; private set; }
        DeviceType(string name)  =>
            code = name;

        public static IEnumerable<DeviceType> Values
        {
            get
            {
                yield return VIRTUAL;
                yield return SESAMI_MOBILE;
                yield return SESAMI_MOBILE_HYBRID;
                yield return TOTP_MOBILE;
                yield return TOTP_MOBILE_HYBRID; 
            }
        }

        public String getCode()
        {
            return code;
        }

        public override string ToString() => code;
    }

}
