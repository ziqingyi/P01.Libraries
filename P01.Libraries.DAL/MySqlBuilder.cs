using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using P01.Libraries.Model;

namespace P01.Libraries.DAL
{
    public class MySqlBuilder<T> where T : BaseModel
    {
        public static string FindSql = "";
        public static string FindAllSql = "";
        public static string AddSql = "";
        //that's how to cache the fixed sql .  Generic method cache. 
        static MySqlBuilder()
        {
            Type type = typeof(T);
            FindSql = FindSqlBuilder<T>();




        }

        private static string FindSqlBuilder<T>()
        {
            // avoid sql injection, so use parameterized query
            Type type = typeof(T);
            var test = type.GetProperties();
            var tlinq = type.GetProperties().Select(p => p.Name);
            //should not have inherted members. 
            String columnString = string.Join(",",
                type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                    .Select(p => $"[{p.Name}]"));

            String valueColumn = String.Join(",",
                type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                    .Select(p => $"@{p.Name}")); //not get value, prepare @value as parameter name
            String sql =
                $"INSERT INTO [{type.Name}]  ({columnString}) values ({valueColumn})";
            return sql;
        }

    }
}
