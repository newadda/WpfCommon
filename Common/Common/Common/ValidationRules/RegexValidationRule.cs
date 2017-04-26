using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFCommon.Common.ValidationRules
{
    public class RegexValidationRule : ValidationRule
    {
        private string _pattern;
        private Regex _regex;
        private string _errorMesg = "";

        public string Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                _regex = new Regex(_pattern, RegexOptions.IgnoreCase);
            }
        }

        public string ErrorMesg
        {
            get { return _errorMesg; }
            set { this._errorMesg = value; }
        }


        public RegexValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo ultureInfo)
        {
            if (value == null || !_regex.Match(value.ToString()).Success)
            {
                return new ValidationResult(false, _errorMesg);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
