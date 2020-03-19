using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using P01.Libraries.Framework;
using P01.Libraries.IDAL;

namespace P01.Libraries.Factory
{
    public class SimpleFactory
    {
        private static string DllName = StaticConstraint.IBaseDALConfig.Split(',')[1];
        private static string TypeName = StaticConstraint.IBaseDALConfig.Split(',')[0];


        public static IBaseDAL CreateInstance()
        {
            Assembly assembly = Assembly.Load(DllName);
            Type type = assembly.GetType(TypeName);
            return (IBaseDAL)Activator.CreateInstance(type);
        }

    }
}
