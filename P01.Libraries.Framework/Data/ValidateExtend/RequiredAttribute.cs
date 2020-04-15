using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01.Libraries.Framework.Data.ValidateExtend
{
    public class RequiredAttribute : AbstractValidateAttribute
    {

        public override ValidateErrorModel Validate(object oValue)
        {
            bool test1 = oValue != null ;
            bool test2 = !string.IsNullOrWhiteSpace(oValue.ToString());
            
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
                    Message = $"{nameof(EmailAttribute)} oValue is empty"
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
