using System.Collections.Generic;
using System.Threading.Tasks;
using CustomTools.Data.Access.DAL.DTOs.Sustainability;

namespace CustomTools.Data.Access.DAL.Interfaces.Sustainability
{
    public interface ISustainabilityRepository
    {
        Task<IEnumerable<SustainabilityDto>> GetAllAsync();
    }
}