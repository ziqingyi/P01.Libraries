using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        private static string ConnectionStringCustomers = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;
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
            string sql = $"SELECT {string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"))} FROM [{type.Name}] WHERE ID={id}";
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    object oObject = Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        //prop.SetValue(oObject, reader[prop.Name]]);
                        //Eleven 可空类型，如果数据库存储的是null，直接SetValue会报错的
                        prop.SetValue(oObject, reader[prop.Name] is DBNull ? null : reader[prop.Name]);
                    }
                    //return this.Trans<T>(type, reader);
                    return (T) oObject;
                }
                else
                {
                    return null;//Eleven  数据库没有，应该返回null  而不是一个默认对象
                }
            }
        }

        public bool Update<T>(T t) where T : BaseModel
        {
            throw new NotImplementedException();
        }
    }
}
