using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Common.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories
{
    public class GenericEFRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;

        public GenericEFRepository(DbContext context)
        {
            this.context = context;
        }

        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public void Create(T item)
        {
            context.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(T item)
        {
            context.Set<T>().Remove(item);
        }
    }
}
