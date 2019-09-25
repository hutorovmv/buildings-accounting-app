using System.Collections.Generic;

namespace Common.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
