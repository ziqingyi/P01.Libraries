using System;
using System.Collections.Generic;
using System.Text;
using P01.Libraries.Framework.Data;

namespace P01.Libraries.Model
{
    public class atesCompany : BaseModel
    {
        [Display("company name")]
        public string name { get; set; }

        [Display("create time")]
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int? LastModifierId { get; set; } 
        //Nullable<int> 

        public DateTime? LastModifyTime { get; set; }
    }

}
