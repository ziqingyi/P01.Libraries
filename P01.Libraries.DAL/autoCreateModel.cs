using System;
using System.Collections.Generic;
using System.Text;

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






    }
}
