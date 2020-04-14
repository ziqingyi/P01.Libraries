using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01.Libraries.Framework.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute:Attribute
    {
        private string _ColumnName = "";
        public ColumnAttribute(string columnName)
        {
            this._ColumnName = columnName;
        }
        public string GetColumnName()
        {
            return this._ColumnName;
        }

    }
}
