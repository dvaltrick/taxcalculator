using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taxcalculator.Models;

namespace taxcalculator.Repositories
{
    interface IGenericRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(Guid id);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(string id);
    }
}
