using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P01.Libraries.Framework.Data.ValidateExtend
{
    public class PhoneAttribute : AbstractValidateAttribute
    {
        private  string _PhoneRegular = @"`[1]+[3,5]+\d[9]";
        public override bool Validate(object oValue)
        {
            bool test1 = oValue != null;
            bool test2 = !string.IsNullOrWhiteSpace(oValue.ToString());
            bool test3 = Regex.IsMatch(oValue.ToString(), this._PhoneRegular);

            return test1 && test2 && test3;
        }
    }
}
