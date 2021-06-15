using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTools.Data.Models.Models
{
    public class UOMModel
    {
        public int UOMID { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string ModifiedBy { get; set; }
    }
}
