using System;
using System.Collections.Generic;
using System.Text;
using DOfficeCore.Data.Entities.Core;

namespace DOfficeCore.Data.Storage
{
    public interface IStorage<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetByID(int ID);
        T Add(T Item);
        void Update(T Item);
        bool Delete(int ID);
        bool Delete(T item);
    }
}
