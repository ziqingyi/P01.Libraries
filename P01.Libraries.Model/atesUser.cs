using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Libraries.Model
{
    public class atesUser : BaseModel
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public String Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int State { get; set; }
        public int UserType { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int? LastModifierId { get; set; }

        public atesUser(string name, string account, string password,
            string email, string mobile, int? companyId,  string companyName,
            int state,  int usertype, DateTime? lastlogintime,
            DateTime createTime,      int creatorId,  int? lastmodifierId
        )
        {
            Name = name;
            Account = account;
            Password = password;
            Email = email;
            Mobile = mobile;
            CompanyId = companyId;
            CompanyName = companyName;
            State = state;
            UserType = usertype;
            LastLoginTime = lastlogintime;
            CreateTime = createTime;
            CreatorId = creatorId;
            LastModifierId = lastmodifierId;

        }
    }
}
