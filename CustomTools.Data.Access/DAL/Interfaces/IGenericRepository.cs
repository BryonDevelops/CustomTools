using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomTools.Data.Access.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Get(string searchText);
        Task<List<T>> GetAll();

        Task<int> Create(T entity);
        Task<int> Delete(int id);
        Task<int> Update(int entity);
    }
}
