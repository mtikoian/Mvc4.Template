using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data;

namespace Template.Mvc4.Models
{
  public class Repository<T> : IRepository<T> where T: ModelBase
  {
    private readonly TemplateMvc4Context _context = new TemplateMvc4Context();

    public IQueryable<T> All
    {
      get { return _context.Set<T>(); }
    }

    public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
      IQueryable<T> query = _context.Set<T>();
      foreach (var includeProperty in includeProperties)
      {
        query = query.Include(includeProperty);
      }
      return query;
    }

    public T Find(int id)
    {
      return _context.Set<T>().Find(id);
    }

    public void InsertOrUpdate(T entity)
    {
      entity.ModifyDate = DateTime.Now;
      if (entity.Id == default(int))
      {
        // New entity
        _context.Set<T>().Add(entity);
      }
      else
      {
        // Existing entity
        _context.Entry(entity).State = EntityState.Modified;
      }
    }

    public void Delete(int id)
    {
      var entity = _context.Set<T>().Find(id);
      _context.Set<T>().Remove(entity);
    }

    public void Save()
    {
      _context.SaveChanges();
    }
  }

  public interface IRepository<T>
  {
    IQueryable<T> All { get; }
    IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
    T Find(int id);
    void InsertOrUpdate(T contact);
    void Delete(int id);
    void Save();
  }
}