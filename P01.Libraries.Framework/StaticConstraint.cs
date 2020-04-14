using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P01.Libraries.Framework
{
    //read configuration files, static class
    public class StaticConstraint
    {
        public readonly static string DBconnection = "";
        public readonly static string IBaseDALConfig = "";
        public readonly static string ErrorFilePath = "";
        public readonly static string ModelFilePath = "";

        static StaticConstraint()
        {
            try
            {
                string home = "server=.;uid=sa;pwd=123;database=RPracticeDB";
                //ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
                string work = @"server=netcrmau;uid=dev;pwd='';database=Backup";
                DBconnection = home;//test.Tostring

                ModelFilePath = @"C:\Users\adrian\Desktop";//ConfigurationManager ? 
                ErrorFilePath = @"C:\Users\adrian\Desktop";//ConfigurationManager ? 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
