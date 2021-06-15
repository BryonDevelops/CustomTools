using System;

namespace CustomTools.Data.Models.Models
{
    public class UtilityTypeModel
    {
        public int UtilityTypeID { get; set; }
        public string UtilityType { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string ModifiedBy { get; set; }
    }
}