using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomTools.Data.Access.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
