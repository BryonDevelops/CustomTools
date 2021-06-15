﻿using System;

namespace CustomTools.Data.Access.DAL.DTOs.Sustainability
{
    public class SustainabilityDto
    {
        public int SustainabilityID { get; set; }
        public int? ManufacturingLocationID { get; set; }
        public string? Utility { get; set; }
        public int? UtilityID { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Usage { get; set; }
        public decimal? Cost { get; set; }
        public string? UOM { get; set; }
        public int? UOMID { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string? ModifiedBy { get; set; }
    }
}