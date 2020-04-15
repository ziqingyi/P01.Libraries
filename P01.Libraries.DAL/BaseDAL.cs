using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using P01.Libraries.Framework;
using P01.Libraries.Framework.Data;
using P01.Libraries.IDAL;
using P01.Libraries.Model;

namespace P01.Libraries.DAL
{
    /// <summary>
    /// limit the type of T,
    /// all T passed must have ID, so defined in BaseModel
    /// </summary>
    public class BaseDAL : IBaseDAL
    {
        //private static string ConnectionStringCustomers = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;
        private static string ConnectionStringCustomers = StaticConstraint.DBconnection;
        //private static string ConnectionStringCustomers = @"server=.;uid=sa;pwd=123;database=RPracticeDB";
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
            String sql = MySqlBuilder<T>.InsertSql;
            // static construct method be called only once before first use of static element or new instance

            //use same list from builder. 
            var parameterList = MySqlBuilder<T>.propList
                .Select(p => new SqlParameter($"@{p.GetColumnName()}",p.GetValue(t)?? "")); 
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
            String Sql = MySqlBuilder<T>.DeleteSql;
            SqlParameter p = new SqlParameter("@id", SqlDbType.Int);
            p.Value = t.Id;
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                SqlCommand cmd = new SqlCommand(Sql,conn);
                cmd.Parameters.Add(p);
                conn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public List<T> FindAll<T>() where T : BaseModel
        {
            Type type = typeof(T);
            List<T> allObj = new List<T>();
            String Sql = MySqlBuilder<T>.FindAllSql;

            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(Sql, conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object obj = Activator.CreateInstance(type);

                        obj = MySqlBuilder<T>.CreateObjectFromSqlDataReader<T>(reader);

                        allObj.Add( (T)obj);
                    }
                    reader.Close();
                }
            }

            return allObj;

        }
        public T FindT<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            String Sql = MySqlBuilder<T>.FindSql;
            SqlParameter p = new SqlParameter("@id", SqlDbType.Int);
            p.Value = id;
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(Sql, conn))
                {
                    command.Parameters.Add(p);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        object obj = Activator.CreateInstance(type);
                        obj = MySqlBuilder<T>.CreateObjectFromSqlDataReader<T>(reader);

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
            Type type = typeof(T);

            String Sql = MySqlBuilder<T>.ModifySql;
            List<SqlParameter> list = new List<SqlParameter>();
            //then build your sql parameters. need to have id.
            foreach (var prop in MySqlBuilder<T>.propListAllPub)
            {
                //test parameter type
                string pname = "@" + prop.GetColumnName();
                SqlParameter para = new SqlParameter(pname,prop.PropertyType.Name);
                para.Value = prop.GetValue(t);
                list.Add(para);
            }

            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(Sql, conn))
                {
                    command.Parameters.AddRange(list.ToArray());
                    return command.ExecuteNonQuery() == 1;

                }
            }

        }
    }
}


