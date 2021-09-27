using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taxcalculator.Database;
using taxcalculator.Models;

namespace taxcalculator.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected TaxContext _db;
        public GenericRepository()
        {
            _db = new TaxContext();
        }

        public virtual TEntity Create(TEntity entity)
        {
            entity.ID = Guid.NewGuid();
            TEntity ret = _db.Set<TEntity>().Add(entity).Entity;
            _db.SaveChanges();

            return ret;
        }

        public virtual void Delete(string id)
        {
            var toDeleteId = Guid.Parse(id);
            var entity = GetById(toDeleteId);
            _db.Set<TEntity>().Remove(entity);
            _db.SaveChanges();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }

        public virtual TEntity GetById(Guid id)
        {
            return _db.Set<TEntity>().AsNoTracking().FirstOrDefault(e => e.ID.Equals(id));
        }

        public virtual TEntity Update(TEntity entity)
        {
            var ret = _db.Set<TEntity>().Update(entity).Entity;
            _db.SaveChanges();

            return ret;
        }
    }
}
