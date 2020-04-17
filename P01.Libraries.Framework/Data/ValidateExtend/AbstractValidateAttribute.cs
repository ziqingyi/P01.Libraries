using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace P01.Libraries.Framework.Data.ValidateExtend
{
    public abstract class AbstractValidateAttribute: System.Attribute
    {
        private Func<object, ValidateErrorModel> _Func;

        public AbstractValidateAttribute(Func<object, ValidateErrorModel> func)
        {
            this._Func = func;
        }
        public AbstractValidateAttribute()
        {}
        public ValidateErrorModel ValidateSelf(object oValue)
        {
            return this._Func.Invoke(oValue);
        }
        // sub class don't need to override parent class, 
        // use delegate to pass to constructor, 
        public abstract ValidateErrorModel Validate(object oValue);


    }
}
