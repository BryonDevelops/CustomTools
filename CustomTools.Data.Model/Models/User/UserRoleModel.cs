using System;
using System.Collections.Generic;
using System.Text;
using CustomTools.Data.Models.Models;

namespace CustomTools.Data.Models.Models
{
    public class UserRoleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual UserModel User { get; set; }
        public int RoleId { get; set; }
        public virtual RoleModel Role { get; set; }
    }
}
