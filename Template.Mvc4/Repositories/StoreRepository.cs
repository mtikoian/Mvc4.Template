using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Template.Mvc4.Models;

namespace Template.Mvc4.Repositories
{ 
    public class StoreRepository : IStoreRepository
    {
        TemplateMvc4Context context = new TemplateMvc4Context();

        public IQueryable<Store> All
        {
            get { return context.Stores; }
        }

        public IQueryable<Store> AllIncluding(params Expression<Func<Store, object>>[] includeProperties)
        {
            IQueryable<Store> query = context.Stores;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Store Find(int id)
        {
            return context.Stores.Find(id);
        }

        public void InsertOrUpdate(Store store)
        {
            if (store.Id == default(int)) {
                // New entity
                context.Stores.Add(store);
            } else {
                // Existing entity
                context.Entry(store).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var store = context.Stores.Find(id);
            context.Stores.Remove(store);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IStoreRepository
    {
        IQueryable<Store> All { get; }
        IQueryable<Store> AllIncluding(params Expression<Func<Store, object>>[] includeProperties);
        Store Find(int id);
        void InsertOrUpdate(Store store);
        void Delete(int id);
        void Save();
    }
}