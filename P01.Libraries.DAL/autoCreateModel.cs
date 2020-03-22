using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using P01.Libraries.Framework;


namespace P01.Libraries.DAL
{
    public static class autoCreateModel
    {
        private static string GetALLTableSql = "SELECT name FROM sys.tables WHERE type = 'U'";
        private static String GetTableInfoSql = @"
                                                  select a.TABLE_NAME,a.COLUMN_NAME,B.CONSTRAINT_NAME
                                                  from information_schema.columns a
                                                  left join information_schema.key_column_usage b
                                                  on(a.TABLE_NAME = b.TABLE_NAME and a.COLUMN_NAME = b.COLUMN_NAME)";

        public static void BatchmappingModel()
        {
            using (SqlConnection conn = new SqlConnection(StaticConstraint.IBaseDALConfig))
            {
                using(SqlCommand cmd = new SqlCommand(GetALLTableSql,conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MappingModel(reader["name"].ToString());
                    }
                }
            }
        }

        //generate single object
        public static void MappingModel(string tablename)
        {
            string sql = $" {GetTableInfoSql} where a.table_name = '{tablename}'";
            using (SqlConnection conn = new SqlConnection(StaticConstraint.IBaseDALConfig))
            {



            }



        }




    }
}
