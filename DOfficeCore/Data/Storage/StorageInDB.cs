using System.Collections.Generic;
using System.Linq;
using DOfficeCore.Data.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace DOfficeCore.Data.Storage
{
    public class StorageInDB<T> : IStorage<T> where T : Entity
    {
        private readonly HospitalDb _db;
        private readonly DbSet<T> _set;
        public StorageInDB(HospitalDb db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public T Add(T Item)
        {
            _set.Add(Item);
            _db.SaveChanges();
            return Item;
        }

        public bool Delete(int ID)
        {
            var item = GetByID(ID);
            return Delete(item);
        }

        public bool Delete(T item)
        {
            if (item is null) return false;

            _set.Remove(item);
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<T> GetAll() => _set.ToArray();

        public T GetByID(int ID) => _set.SingleOrDefault(t => t.Id == ID);
        public void Update(T Item)
        {
            _set.Update(Item);
            _db.SaveChanges();
        }
    }
}
