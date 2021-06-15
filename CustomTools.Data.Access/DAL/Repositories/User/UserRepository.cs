using System.Data;
using CustomTools.Data.Access.DAL.Interfaces.User;
using Microsoft.Extensions.Logging;

namespace CustomTools.Data.Access.DAL.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private readonly ILogger<UserRepository> _logger;
        private readonly IDbConnection _dbConnection;

        public UserRepository(ILogger<UserRepository> logger, IDbConnection connection)
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
