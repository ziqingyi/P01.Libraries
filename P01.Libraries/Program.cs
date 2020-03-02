using System;
using System.Configuration;
using System.Linq;
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

            ////test search by id method 
            //BaseDAL b = new BaseDAL();
            //atesUser u1 = b.FindT<atesUser>(1);

            //test insert method, 1: how to make sql through type attribute of object
            Type type = typeof(atesUser);

            var test = type.GetProperties();

            var tlinq = type.GetProperties().Select(p => p.Name);



            String sql =
                $"INSERT INTO ([{type.Name}] ({string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"))} )";
            
            atesUser u = new atesUser("user2", "accout2", "password2", "email@gmail.com", "0466666666", 1,
                "google", 1, 1, new DateTime(2019,12,20), new DateTime(2018,01,01), 1111,111);


            var temp = u.GetType().GetProperties();

            //String sql2 = $"Values( {string.Join(",",u.GetType().GetProperties().Select(p => $"[{p.}]") )}             )";

            Console.ReadKey();
        }
    }
}
