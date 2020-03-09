using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace P01.Libraries.Model
{
    class ImportFromExcel
    {
        public void import()
        {

            string path = Path.Combine(Environment.CurrentDirectory, @"files\");

            String[] files = System.IO.Directory.GetFiles(path, "*.xlsx");

            foreach (var file in files)
            {
                FileInfo existingFile = new FileInfo(file);
                string filename = GetFileName(file);

                using (var package = new ExcelPackage(existingFile))
                {
                    foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                    {
                        worksheet.Column(14).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        if (worksheet.Index == 0)
                        {

                            Stopwatch sw2 = new Stopwatch();
                            sw2.Start();
                            string conncetionString = @"server=netcrmau;uid=dev;pwd='';database=netcrm";

                            using (System.Data.SqlClient.SqlConnection conn = new SqlConnection(conncetionString))
                            {
                                int totalrow = worksheet.Dimension.End.Row;
                                conn.Open();
                                //reuse SqlCommand, create different transactions and commit every 100 times
                                using (SqlCommand cmd = conn.CreateCommand())
                                {
                                    SqlTransaction tran = conn.BeginTransaction();
                                    cmd.Transaction = tran;

                                    //begin from the second row
                                    for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                                    {
                                        List<SqlParameter> paras = new List<SqlParameter>();
                                        SqlParameter p = new SqlParameter("@filenames", SqlDbType.NVarChar, 32);
                                        p.Value = filename;
                                        paras.Add(p);
                                        for (int j = worksheet.Dimension.Start.Column;
                                            j <= worksheet.Dimension.End.Column;
                                            j++)
                                        {
                                            SqlParameter para = null;
                                            if (j != 14)
                                            {
                                                para = GenerateParam(
                                                worksheet.Cells[i, j].Value == null ? String.Empty : worksheet.Cells[i, j].Value.ToString()
                                                , j);

                                            }
                                            else if (j == 14 && worksheet.Cells[i, j].Value != null)
                                            {
                                                double d = double.Parse(worksheet.Cells[i, j].Value.ToString());
                                                DateTime conv = DateTime.FromOADate(d);
                                                para = GenerateDateParam(conv);
                                            }

                                            if (para != null)
                                            {
                                                paras.Add(para);
                                            }

                                        }
                                        cmd.CommandText = PrepareInsertSql();
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.AddRange(paras.ToArray());
                                        try
                                        {
                                            cmd.ExecuteNonQuery();
                                            if (i > 0 && (i % 100 == 0 || i == totalrow))
                                            {
                                                tran.Commit();
                                                tran = conn.BeginTransaction();
                                                cmd.Transaction = tran;
                                            }

                                        }
                                        catch (Exception e)
                                        {
                                            tran.Rollback();
                                            using (System.IO.StreamWriter file2 =
                                                new System.IO.StreamWriter(
                                                    @"C:\Users\adrian_sun\Desktop\errorlog\test2.txt", true))
                                            {
                                                file2.WriteLine(paras[0].Value.ToString() + "   " + paras[1].Value.ToString());
                                            }

                                            Console.WriteLine(
                                                paras[0].Value.ToString() + "   " + paras[1].Value.ToString());
                                            throw new Exception(e.Message);
                                        }
                                    }
                                }
                            }
                            sw2.Stop();
                            Console.WriteLine("total runing time : {0} seconds", sw2.ElapsedMilliseconds / 1000);

                        }

                    }

                }

            }


        }
        private static string PrepareInsertSql()
        {
            return @"insert into tblreturnmaterial(CampCode, descr,slsRep,affinity,Returned, filenames,contno)
                     values(@campcode,@desc,@slsrep,@affinity,@return,@filenames,@contno)";
        }
        private static SqlParameter GenerateParam(string cellvalue, int columnno)
        {
            if (columnno == 1)
            {
                SqlParameter p1 = new SqlParameter("@campcode", SqlDbType.VarChar, 100);
                p1.Value = cellvalue;
                return p1;
            }
            if (columnno == 2)
            {
                SqlParameter p1 = new SqlParameter("@desc", SqlDbType.VarChar, 2000);
                p1.Value = cellvalue;
                return p1;
            }
            if (columnno == 4)
            {
                SqlParameter p1 = new SqlParameter("@contno", SqlDbType.VarChar, 12);
                p1.Value = cellvalue;
                return p1;
            }
            if (columnno == 9)
            {
                SqlParameter p1 = new SqlParameter("@affinity", SqlDbType.VarChar, 5);
                p1.Value = cellvalue;
                return p1;
            }
            if (columnno == 10)
            {
                SqlParameter p1 = new SqlParameter("@slsrep", SqlDbType.VarChar, 3);
                p1.Value = cellvalue;
                return p1;
            }

            else
            {
                return null;
            }
        }

        private static SqlParameter GenerateDateParam(DateTime cellvalue)
        {
            SqlParameter p1 = new SqlParameter("@return", SqlDbType.DateTime, 100);
            p1.Value = cellvalue;
            return p1;
        }

        private static string GetFileName(string path)
        {
            int len = path.Length;
            int index = path.LastIndexOf(@"\");
            return path.Substring(index + 1);
        }


    }
}