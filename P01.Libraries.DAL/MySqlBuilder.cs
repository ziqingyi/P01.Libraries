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
        public static string InsertSql = "";

        //that's how to cache the fixed sql .  Generic method cache. 
        //use static construction method, which only run one time.
        static MySqlBuilder()
        {
            Type type = typeof(T);
            InsertSql = InsertSqlBuilder<T>();
            FindAllSql = FindAllSqlBuilder<T>();



        }

        private static string FindAllSqlBuilder<T>()
        {
            Type type = typeof(T);
            var test = type.GetProperties().Select(p => p.Name);
            var test2 = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p=> p.Name);

            String columnString = string.Join(",", type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p=>p.Name));
            String sql = $"Select {columnString} from {type.Name} ";

            return sql;

        }

        private static string InsertSqlBuilder<T>()
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
