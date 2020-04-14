using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using P01.Libraries.Framework;


namespace P01.Libraries.DAL
{
    //generate C# code for creating single object based on table name and table structure info

    public static class autoCreateModel
    {
        private static string GetALLTableSql = "SELECT name FROM sys.tables WHERE type = 'U'";
        private static String GetTableInfoSql = @"
                                                  select a.TABLE_NAME,a.COLUMN_NAME,a.DATA_TYPE typename, a.IS_NULLABLE isnullable,
                                                         B.CONSTRAINT_NAME
                                                  from information_schema.columns a
                                                  left join information_schema.key_column_usage b
                                                  on(a.TABLE_NAME = b.TABLE_NAME and a.COLUMN_NAME = b.COLUMN_NAME)";
        public static void BatchmappingModel()
        {
            using (SqlConnection conn = new SqlConnection(StaticConstraint.DBconnection))
            {
                using(SqlCommand cmd = new SqlCommand(GetALLTableSql,conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MappingModel(reader["name"].ToString());
                    }
                }
            }
        }

        public static void MappingModel(string tablename)
        {
            string sql = $" {GetTableInfoSql} where a.table_name = '{tablename}'";
            using (SqlConnection conn = new SqlConnection(StaticConstraint.DBconnection))
            {
                SqlCommand sqlCommand = new SqlCommand(sql,conn);
                conn.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                StringBuilder sb = new StringBuilder();
                sb.Append($"public class {tablename}  \r\n {{\r\n");
                while (reader.Read())
                {
                    string typeOfColumn = GetTypeOfColumn(reader["typename"].ToString(),reader["isnullable"].ToString());
                    sb.Append($"\r        {typeOfColumn} {reader["COLUMN_NAME"]} {{ get; set;}} \r\n");
                }

                sb.Append("}");
                Console.WriteLine(sb.ToString());
                string modelFileLoc = StaticConstraint.ModelFilePath + @"\" + tablename + ".txt";

                ConsoleExtend.CreateTxt(modelFileLoc,sb.ToString());

            }
        }

        private static string GetTypeOfColumn(string type, string nullable)
        {
            if (type.Equals("int") && nullable.Equals("NO"))
                return "int";
            else if (type.Equals("int") && nullable.Equals("YES"))
                return "int?";
            else if (type.Equals("datetime") && nullable.Equals("YES"))
                return "DateTime?";
            else if (type.Equals("datetime") && nullable.Equals("NO"))
                return "DateTime";
            else if (type.Equals("nvarchar") || type.Equals("varchar") || type.Equals("text"))
                return "string";
            else throw new Exception("not supported type");
        }
    }
}
