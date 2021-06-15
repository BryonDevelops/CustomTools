using System;

namespace CustomTools.Data.Models.Models.Supplement
{
    public class ManufacturingLocationModel
    {
        public int ManufacturingLocationID { get; set; }
        public string ManufacturingLocation { get; set; }
        public int SAPNo { get; set; }
        public bool International { get; set; }
        public bool Active { get; set; }
        public int ApplicableApplication { get; set; }
        public int ApplicationLocationCapability { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string ModifiedBy { get; set; }
    }
}