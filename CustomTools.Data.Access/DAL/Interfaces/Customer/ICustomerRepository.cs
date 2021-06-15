using System.Collections.Generic;
using System.Threading.Tasks;
using CustomTools.Data.Access.DAL.DTOs.Customers;

namespace CustomTools.Data.Access.DAL.Interfaces.Customer
{
    public interface ICustomerRepository: IRepository<CustomerDto>
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
    }
}