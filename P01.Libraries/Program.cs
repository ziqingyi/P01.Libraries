using System;
using P01.Libraries;
using P01.Libraries.IDAL;
using P01.Libraries.DAL;
namespace P01.Libraries
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("update database with different layers");

            String name = "Eleven";
            String sql = $"SELECT * FROM [TESTUSER] WHER ENAME = '{name}' and PASSWORD='12345'";
            BaseDAL b = new BaseDAL();
            
            b.FindT<>()


        }
    }
}
