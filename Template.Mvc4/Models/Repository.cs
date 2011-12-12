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
    private TemplateMvc4Context context = new TemplateMvc4Context();

    public IQueryable<T> All
    {
      get { return context.Set<T>(); }
    }

    public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
      IQueryable<T> query = context.Set<T>();
      foreach (var includeProperty in includeProperties)
      {
        query = query.Include(includeProperty);
      }
      return query;
    }

    public T Find(int id)
    {
      return context.Set<T>().Find(id);
    }

    public void InsertOrUpdate(T entity)
    {
      entity.ModifyDate = DateTime.Now;
      if (entity.Id == default(int))
      {
        // New entity
        context.Set<T>().Add(entity);
      }
      else
      {
        // Existing entity
        context.Entry(entity).State = EntityState.Modified;
      }
    }

    public void Delete(int id)
    {
      var entity = context.Set<T>().Find(id);
      context.Set<T>().Remove(entity);
    }

    public void Save()
    {
      context.SaveChanges();
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