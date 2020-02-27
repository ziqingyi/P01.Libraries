using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Libraries.Model
{
    public class atesCompany : BaseModel
    {
        public string name { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int? LastModifierId { get; set; } 
        //Nullable<int> 

        public DateTime? LastModifyTime { get; set; }




    }
}
