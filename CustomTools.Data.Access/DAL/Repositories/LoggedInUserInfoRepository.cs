using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomTools.Data.Access.DAL;
using CustomTools.Data.Access.DAL.DTOs.Customers;
using CustomTools.Data.Access.DAL.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CustomTools.Data.Access.DAL.Repositories
{
    public class LoggedInUserInfoRepository : ILoggedInUserInfoRepository
    {

        private readonly ILogger<LoggedInUserInfoRepository> _logger;
        private readonly IDbConnection _dbConnection;

        public LoggedInUserInfoRepository(ILogger<LoggedInUserInfoRepository> logger, IDbConnection connection)
        {
            _logger = logger;
            _dbConnection = connection;
        }

        //public async Task<List<LoggedInUserInfo>> Get(string impersonateUser, string loggedInUser)
        //{
        //   // return await DbQuerySingleAsync<LoggedInUserInfoDto>("SELECT * FROM Customer WHERE CustomerId = @ID", new { cus });
        //}

    }
}
