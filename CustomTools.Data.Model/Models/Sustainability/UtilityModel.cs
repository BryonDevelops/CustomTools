using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTools.Data.Models.Models
{
    public class UtilityModel
    {
        public int UtilityID { get; set; }
        public string UtilityName { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string ModifiedBy { get; set; }
    }
}
