using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTools.Data.Access.DAL.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NTID { get; set; }
    }
}
