using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AS_Licence.Data.Interface.GenericRepository
{
  public interface IGenericRepository<TEntity> where TEntity : class, new()
  {
    /*
     *dal.Get(x=> x.Property == )
     */
    List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      string includeProperties = "");
    TEntity GetById(object id);
    void Insert(TEntity entityToInsert);
    void Update(TEntity entityToUpdate);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
  }
}