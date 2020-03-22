using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01.Libraries.Framework
{
    public static class ConsoleExtend
    {
        public static void Show<T>(this T t)
        {
            Type type = t.GetType();
            Console.WriteLine("***********type.Name -- show --- start******************");
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine($" {type.Name}.{prop.Name} = {prop.GetValue(t)} ");
            }

            Console.WriteLine("***********type.Name -- show --- end******************");
        }

        public static void CreateTxt(string fileName, string content)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fileName, false);
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
