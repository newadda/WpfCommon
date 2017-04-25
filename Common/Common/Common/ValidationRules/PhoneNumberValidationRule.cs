using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common.ValidationRules
{
    public class PhoneNumberValidationRule : RegexValidationRule
    {
        public PhoneNumberValidationRule()
        {
            base.Pattern = "^[0-9]{0,11}$";
        }
    }
}
