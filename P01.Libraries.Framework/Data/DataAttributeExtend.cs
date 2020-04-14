using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace P01.Libraries.Framework.Data
{
    public static class DataAttributeExtend
    {
        //use this method to display more user friendly property's name.
        
        public static string GetDisplayName(this PropertyInfo property)
        {
            if(property.IsDefined(typeof(DisplayAttribute), true))
            {
                DisplayAttribute attribute =
                    (DisplayAttribute) property.GetCustomAttribute(typeof(DisplayAttribute), true);
                return attribute.GetDisplayName();
            }
            else
            {
                // if not define this attribute,just show property's name
                return property.Name;
            }

        }
        /// <summary>
        /// get the db column name of property through its' attribute. 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetColumnName(this PropertyInfo property)
        {
            if (property.IsDefined(typeof(ColumnAttribute), true))
            { 
                ColumnAttribute attribute =
                (ColumnAttribute) property.GetCustomAttribute(typeof(ColumnAttribute), true);
                return attribute.GetColumnAtt_Name();
            }
            else
            {
                return property.Name;
            }

        }



    }
}
