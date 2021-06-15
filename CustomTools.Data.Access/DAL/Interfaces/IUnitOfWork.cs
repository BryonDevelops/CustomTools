using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomTools.Data.Access.DAL.Interfaces.Customer;
using CustomTools.Data.Access.DAL.Interfaces.Sustainability;

namespace CustomTools.Data.Access.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        ISustainabilityRepository Sustainabilities { get; }
    }
}
