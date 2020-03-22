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

        public readonly static string IBaseDALConfig = "";
        
        static StaticConstraint()
        {
            try
            {
                var test = ConfigurationManager.ConnectionStrings["homedb"].ConnectionString; 
                IBaseDALConfig = test.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

    }
}
