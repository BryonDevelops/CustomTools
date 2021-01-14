using System;
using System.Linq;
using System.Threading.Tasks;
using CustomTools.Data.Models;
using CustomTools.Queries.Interfaces;

namespace CustomTools.Queries
{
    public class CustomersQueryProcessor : ICustomersQueryProcessor
    {
        public IQueryable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Create(Customer model)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Update(int id, Customer model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
