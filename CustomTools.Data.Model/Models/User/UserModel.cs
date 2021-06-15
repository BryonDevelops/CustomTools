using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTools.Data.Models.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string NtId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Ext { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual IList<UserRoleModel> Roles { get; set; }
    }
}
