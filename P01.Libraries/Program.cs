using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using P01.Libraries;
using P01.Libraries.IDAL;
using P01.Libraries.DAL;
using P01.Libraries.Framework;
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
            //BaseDAL b1 = new BaseDAL();
            //atesUser u1 = b1.FindT<atesUser>(1);
            //Console.WriteLine(u1.Name);
            //1.1 test search by id by delegate
            //BaseDAL b1 = new BaseDAL();
            //atesUser u1 = b1.FindTwithDelegate<atesUser>(1);
            //u1.Show();

            //2 test insert method, 1: how to make sql through type attribute of object
            // new DateTime(2019, 12, 20)   new DateTime(2018, 01, 01)
            //Type type2 = typeof(atesUser); //  u.GetType(); is same
            //atesUser u = new atesUser("user3", "accout3", "password3", "email3@gmail.com", "0433333333", 1,
            //    "microsoft", 1, 1, null, null, 2222, 222);
            //BaseDAL b2 = new BaseDAL();
            //bool testresult = b2.Add(u);
            //Console.WriteLine();

            ////difference between Getproperties.
            //var prop1 = typeof(atesUser).GetProperties();
            //var prop2 = typeof(atesUser).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance |BindingFlags.Public);
            //var test = typeof(atesUser).GetProperties().Select(p => p.Name);
            //var test2 = typeof(atesUser).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p => p.Name);

            //// 3 test FindAll method
            //BaseDAL find = new BaseDAL();
            //var listofOjb = find.FindAll<atesUser>();
            //Console.WriteLine();
            //// 4 test delete method
            //BaseDAL b4 = new BaseDAL();
            //atesUser u4 = b4.FindT<atesUser>(3);
            //bool testresult = b4.Delete(u4);
            //Console.WriteLine();
            ////  5 test modify
            BaseDAL b52 = new BaseDAL();
            Company c1 = b52.FindT<Company>(2);
            c1.name = "dongguan1";
            bool testmodify2 = b52.Update(c1);

            BaseDAL b5 = new BaseDAL();
            atesUser u3 = b5.FindT<atesUser>(2);
            u3.CompanyName = "coke2";
            bool testmodify = b5.Update(u3);

            autoCreateModel.BatchmappingModel();




            Console.ReadKey();

        }
    }
}
