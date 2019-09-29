using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using AS_Licence.Data.Interface.GenericRepository;
using AS_Licence.Data.Repository.Host.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AS_Licence.Data.Repository.Infrastracture.EntityFramework
{
  public class EfEntityRepositoryBase<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
  {
    private readonly DbSet<TEntity> _dbSet;
    private readonly EfAsLicenceContext _context;

    public EfEntityRepositoryBase(EfAsLicenceContext context)
    {
      _context = context;
      _dbSet = _context.Set<TEntity>();
    }

    public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
    {
      IQueryable<TEntity> query = _dbSet;

      if (filter != null)
      {
        query = query.Where(filter);
      }

      if (!string.IsNullOrEmpty(includeProperties))
      {
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
          query = query.Include(includeProperty);
        }
      }

      if (orderBy != null)
      {
        return orderBy(query).ToList();
      }

      return query.ToList();
    }

    public virtual TEntity GetById(object id)
    {
      return _dbSet.Find(id);
    }

    public virtual void Insert(TEntity entityToInsert)
    {
      _dbSet.Add(entityToInsert);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
      _dbSet.Attach(entityToUpdate);
      _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual void Delete(object id)
    {
      TEntity entityToDelete = _dbSet.Find(id);
      Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
      if (_context.Entry(entityToDelete).State == EntityState.Detached)
      {
        _dbSet.Attach(entityToDelete);
      }

      _dbSet.Remove(entityToDelete);
    }
  }
}
