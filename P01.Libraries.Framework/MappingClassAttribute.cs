using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01.Libraries.Framework
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Property)]
    public class MappingClassAttribute : Attribute
    {
        public string MappingName { get; private set; }
        public MappingClassAttribute(string mappingName)
        {
            this.MappingName = mappingName;
        }
    }
}
