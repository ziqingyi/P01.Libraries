using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
            //BaseDAL b1 = new BaseDAL();
            //atesUser u1 = b1.FindT<atesUser>(1);

            /*
            //2 test insert method, 1: how to make sql through type attribute of object
                     // new DateTime(2019, 12, 20)   new DateTime(2018, 01, 01)
                     //Type type2 = typeof(atesUser); //  u.GetType(); is same
            atesUser u = new atesUser("user5", "accout5", "password5", "email5@gmail.com", "0455555555", 2,
                "google", 1, 1, null, null, 1111, 111);
            BaseDAL b2 = new BaseDAL();
            bool testresult = b2.Add(u);
            */

            ////difference between Getproperties.
            //var prop1 = typeof(atesUser).GetProperties();
            //var prop2 = typeof(atesUser).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance |BindingFlags.Public);
            //var test = typeof(atesUser).GetProperties().Select(p => p.Name);
            //var test2 = typeof(atesUser).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p => p.Name);

            //// 3 test FindAll method
            //BaseDAL find = new BaseDAL();
            //var listofOjb = find.FindAll<atesUser>();

            //// 4 test delete method
            //BaseDAL b4 = new BaseDAL();
            //atesUser u4 = b4.FindT<atesUser>(5);
            //bool testresult = b4.Delete(u4);
            ////  5 test modify
            //BaseDAL b5 = new BaseDAL();
            //atesUser u3 = b5.FindT<atesUser>(3);
            //u3.LastModifierId = 3;
            //bool testmodify = b5.Update(u3);

            autoCreateModel.BatchmappingModel();




            Console.ReadKey();

        }
    }
}
