using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using P01.Libraries.IDAL;
using P01.Libraries.Model;

namespace P01.Libraries.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseDAL : IBaseDAL
    {
        //private static string ConnectionStringCustomers = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;
        private static string ConnectionStringCustomers = @"server=netcrmau;uid=dev;pwd='';database=Backup";
        public bool Add<T>(T t) where T : BaseModel
        {
            #region method 1
            //Type type = t.GetType();
            //var test = type.GetProperties();
            //var tlinq = type.GetProperties().Select(p => p.Name);
            ////should not have inherted members. 
            //String columnString = string.Join(",",
            //    type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
            //        .Select(p => $"[{p.Name}]"));

            //String valueColumn = String.Join(",",
            //    type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p => $"'{p.GetValue(t)}'"));

            //String sql =
            //    $"INSERT INTO [{type.Name}]  ({columnString}) values ({valueColumn})";
            //using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            //{
            //    SqlCommand cmd = new SqlCommand(sql,conn);
            //    conn.Open();
            //    return cmd.ExecuteNonQuery()==1;
            //}
            #endregion

            #region method 2
            Type type = typeof(T);
            String sql = MySqlBuilder<T>.FindSql;
            // static construct method be called only once before first use of static element or new instance



            var parameterList = type
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                .Select(p => new SqlParameter($"@{p.Name}",p.GetValue(t)?? "")); 
            //can use DBNull.Value, but some column not null


           
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameterList.ToArray());
                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
            #endregion
        }

        public bool Delete<T>(T t) where T : BaseModel
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll<T>() where T : BaseModel
        {
            Type type = typeof(T);

            return null;

        }

        public T FindT<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            String Sql = $"SELECT {string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"))}" +
                         $"FROM [{type.Name}]" +
                         $"WHERE ID= {id}";
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(Sql, conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        object obj = Activator.CreateInstance(type);

                        foreach (var prop in type.GetProperties())
                        {
                            // notice the null from database 
                            prop.SetValue(obj, reader[prop.Name] is DBNull? null: reader[prop.Name]);
                            
                        }
                        reader.Close();
                        return (T) obj;
                    }
                    else
                    {
                        return null; //not exist in database 
                    }

                    
                }


            }


        }

        public bool Update<T>(T t) where T : BaseModel
        {
            throw new NotImplementedException();
        }
    }
}
