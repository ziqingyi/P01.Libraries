using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
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

            //1 test search by id method 
            //BaseDAL b = new BaseDAL();
            //atesUser u1 = b.FindT<atesUser>(1);

            //2 test insert method, 1: how to make sql through type attribute of object
            // new DateTime(2019, 12, 20)   new DateTime(2018, 01, 01)
            //Type type2 = typeof(atesUser); //  u.GetType(); is same
            atesUser u = new atesUser("user3", "accout2", "password2", "email@gmail.com", "0466666666", 1,
                "google", 1, 1,null, null, 1111, 111);
            BaseDAL b = new BaseDAL();
            bool testresult = b.Add(u);

            Console.ReadKey();
        }
    }
}
