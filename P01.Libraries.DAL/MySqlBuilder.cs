using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public static string InsertSql = "";

        //should be public, for add method
        public static PropertyInfo[] propList;
        //that's how to cache the fixed sql .  Generic method cache. 
        //use static construction method, which only run one time.
        //note: the properties in sql should be same to the properties you create obj(eg. for each get more prop?!)
        static MySqlBuilder()
        {
            Type type = typeof(T);
            propList = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            InsertSql = InsertSqlBuilder<T>();
            FindAllSql = FindAllSqlBuilder<T>();
            FindSql = FindSqlBuilder<T>();


        }
        public static T CreateObjectFromSqlDataReader<T>(SqlDataReader reader)
        {
            object obj = Activator.CreateInstance(typeof(T));
            foreach (var prop in propList)
            {
                // notice the null from database 
                prop.SetValue(obj, reader[prop.Name] is DBNull ? null : reader[prop.Name]);
            }
            return (T)obj;
        }

        private static string FindSqlBuilder<T>()
        {
            Type type = typeof(T);
            //id is assigned by DAL
            String Sql = $"SELECT {string.Join(",", propList.Select(p => $"[{p.Name}]"))}" +
                         $"FROM [{type.Name}]" +
                         "WHERE ID=  @id";
            return Sql;
        }
        private static string FindAllSqlBuilder<T>()
        {
            Type type = typeof(T);
            String columnString = string.Join(",", propList.Select(p=>p.Name));
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
            String columnString = string.Join(",", propList.Select(p => $"[{p.Name}]"));

            String valueColumn = String.Join(",", propList.Select(p => $"@{p.Name}")); //not get value, prepare @value as parameter name
            String sql =
                $"INSERT INTO [{type.Name}]  ({columnString}) values ({valueColumn})";
            return sql;
        }

    }
}
