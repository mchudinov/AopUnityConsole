using System.Collections.Generic;

namespace AopUnityConsole
{
    public interface IRepository<T>
    {
        [Dump]
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        int GetInt();
    }
}
