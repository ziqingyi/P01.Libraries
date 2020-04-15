using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace P01.Libraries.Framework.Data.ValidateExtend
{
    public abstract class AbstractValidateAttribute: System.Attribute
    {
        public abstract bool Validate(object oValue);
    }
}
