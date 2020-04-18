using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Libraries.Framework
{
    //put into framwork because Model need to refer Framework for using attribute
    //while Framework need to use BaseModel to limit T 
    // but they cannot refer each other
    //so put BaseModel into Framework
    public class BaseModel
    {
        public int Id { set; get; }
    }
}
