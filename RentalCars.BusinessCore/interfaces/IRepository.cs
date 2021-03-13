using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.interfaces
{
    /// <summary>
    /// Generic interface to be implemented by different interfaces, provides contract for basic function implementation of other interfaces (of class/model entities).
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(dynamic id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(dynamic id, TEntity updatedEntity);
    }
}
