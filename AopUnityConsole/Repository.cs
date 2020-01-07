using System;
using System.Collections.Generic;

namespace AopUnityConsole
{
    public class Repository<T> : IRepository<T>
    {
        public void Add(T entity)
        {
            Console.WriteLine($"Adding {entity}");
        }

        [Dump]
        public void Delete(T entity)
        {
            Console.WriteLine($"Deleting {entity}");
        }
        public void Update(T entity)
        {
            Console.WriteLine($"Updating {entity}");
        }
        public IEnumerable<T> GetAll()
        {
            Console.WriteLine($"Getting entities");
            return null;
        }

        //[Dump]
        public T GetById(int id)
        {
            Console.WriteLine($"Getting entity {id}");
            return default(T);
        }

        public int GetInt()
        {
            Console.WriteLine("Getting integer 999");
            return 999;
        }
    }
}
