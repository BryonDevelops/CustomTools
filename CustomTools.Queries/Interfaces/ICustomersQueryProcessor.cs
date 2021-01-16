using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomTools.Data.Models;

namespace CustomTools.Queries.Interfaces
{
    public interface ICustomersQueryProcessor
    {
        IQueryable<Customer> Get();
        Customer Get(int id);
        Task<Customer> Create(Customer model);
        Task<Customer> Update(int id, Customer model);
        Task Delete(int id);
    }
}
