using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public bool Delete<T>(T t) where T : BaseModel
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll<T>() where T : BaseModel
        {
            throw new NotImplementedException();
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
