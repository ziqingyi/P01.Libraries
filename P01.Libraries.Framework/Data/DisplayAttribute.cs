using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01.Libraries.Framework.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayAttribute : Attribute
    {
        private string _DisplayName = "";

        public DisplayAttribute(string displayName)
        {
            this._DisplayName = displayName;
        }

        public string GetDisplayName()
        {
            return this._DisplayName;
        }
    }
}
