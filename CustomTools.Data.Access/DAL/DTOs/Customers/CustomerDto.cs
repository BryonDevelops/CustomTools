using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTools.Data.Access.DAL.DTOs.Customers
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string? MarkedForDeletion { get; set; }
        public string SalesDistrictID { get; set; }
        public string SalesDistrict { get; set; }
        public string SalesOfficeID { get; set; }
        public string? SalesOffice { get; set; }
        public string SalesVPID { get; set; }
        public string SalesVPName { get; set; }
        public string ParentName { get; set; }
    }
}
