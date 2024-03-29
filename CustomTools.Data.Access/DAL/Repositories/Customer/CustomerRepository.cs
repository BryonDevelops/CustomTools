﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CustomTools.Data.Access.DAL.DTOs.Customers;
using CustomTools.Data.Access.DAL.Interfaces.Customer;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CustomTools.Data.Access.DAL.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(ILogger<CustomerRepository> logger, IDbConnection connection)
        {
            _logger = logger;
            _dbConnection = connection;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<CustomerDto>("GetAllCustomers", commandType: CommandType.StoredProcedure);
        }

    }
}
