using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomTools.Contracts.Responses.Customers
{
    public class CustomerResponse
    {
        public int SapCustomerId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool MarkedForDeletion { get; set; }
        public int SalesDistrictId { get; set; }
        public string SalesDistrict { get; set; }
        public string SalesOfficeId { get; set; }
        public string? SalesOffice { get; set; }
        public int SalesVPId { get; set; }
        public string SalesVPName { get; set; }
        public string? ThirdParty { get; set; }
        public string ThirdPartyName { get; set; }
        public string ParentName { get; set; }
    }
}
