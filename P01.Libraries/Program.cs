using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using P01.Libraries;
using P01.Libraries.IDAL;
using P01.Libraries.DAL;
using P01.Libraries.Model;
namespace P01.Libraries
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("update database with different layers");

            //String tt = ConfigurationManager.ConnectionStrings["myCustomConfig"].ConnectionString;//"server=netcrmau;uid=dev;pwd='';database=Backup";
                

            BaseDAL b = new BaseDAL();

            atesUser u1 = b.FindT<atesUser>(1);


        }
    }
}
