using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CustomTools.Data.Access.DAL.DTOs.Sustainability;
using CustomTools.Data.Access.DAL.Interfaces.Sustainability;
using CustomTools.Data.Access.DAL.Repositories.Sustainability;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CustomTools.Data.Access.DAL.Repositories.Sustainability
{
    public class SustainabilityRepository: ISustainabilityRepository
    {
        private readonly ILogger<SustainabilityRepository> _logger;
        private readonly IDbConnection _dbConnection;

        public SustainabilityRepository(ILogger<SustainabilityRepository> logger, IDbConnection connection)
        {
            _logger = logger;
            _dbConnection = connection;
        }

        public async Task<IEnumerable<SustainabilityDto>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<SustainabilityDto>("GetAllSustainabilities", commandType: CommandType.StoredProcedure);
        }
    }
}