using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01.Libraries.Framework.Data.ValidateExtend
{
    public class StringLengthAttribute:AbstractValidateAttribute
    {
        private int _Min = 0;
        private int _Max = 0;

        public StringLengthAttribute(int min, int max)
        {
            this._Min = min;
            this._Max = max;
        }

        public override ValidateErrorModel Validate(object oValue)
        {
            bool test1 = oValue != null;
            bool test2 = oValue.ToString().Length >= this._Min;
            bool test3 = oValue.ToString().Length <= this._Max;

            if (!test1)
            {
                return new ValidateErrorModel()
                {
                    Result = false,
                    Message = $"{nameof(EmailAttribute)} oValue is null"
                };
            }
            if (!test2)
            {
                return new ValidateErrorModel()
                {
                    Result = false,
                    Message = $"{nameof(EmailAttribute)} oValue is less than min"
                };
            }
            if (!test3)
            {
                return new ValidateErrorModel()
                {
                    Result = false,
                    Message = $"{nameof(EmailAttribute)} oValue is more than max"
                };
            }
            else
            {
                return new ValidateErrorModel()
                {
                    Result = true,
                    Message = $"{nameof(EmailAttribute)} all good"
                };
            }

        }
    }
}
