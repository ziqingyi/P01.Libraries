using System;
using System.Collections.Generic;
using System.Text;
using P01.Libraries.Model;

namespace P01.Libraries.IDAL
{
    public interface IBaseDAL
    {
        T FindT<T>(int id) where T : BaseModel;
        List<T> FindAll<T>() where T : BaseModel;
        bool Add<T>(T t) where T : BaseModel;
        bool Update<T>(T t) where T : BaseModel;
        bool Delete<T>(T t) where T : BaseModel;

    }
}
