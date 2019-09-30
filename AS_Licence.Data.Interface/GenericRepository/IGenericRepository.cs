using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AS_Licence.Data.Interface.GenericRepository
{
  public interface IGenericRepository<TEntity> where TEntity : class, new()
  {
    /*
     *dal.Get(x=> x.Property == )
     */
    Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      string includeProperties = "");
    Task<TEntity> GetById(object id);
    Task Insert(TEntity entityToInsert);
    Task Update(TEntity entityToUpdate);
    Task Delete(object id);
    Task Delete(TEntity entityToDelete);
  }
}