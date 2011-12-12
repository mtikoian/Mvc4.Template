using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Template.Mvc4.Models
{ 
    public class PurchaseRepository : IPurchaseRepository
    {
        TemplateMvc4Context context = new TemplateMvc4Context();

        public IQueryable<Purchase> All
        {
            get { return context.Purchases; }
        }

        public IQueryable<Purchase> AllIncluding(params Expression<Func<Purchase, object>>[] includeProperties)
        {
            IQueryable<Purchase> query = context.Purchases;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Purchase Find(int id)
        {
            return context.Purchases.Find(id);
        }

        public void InsertOrUpdate(Purchase purchase)
        {
            if (purchase.Id == default(int)) {
                // New entity
                context.Purchases.Add(purchase);
            } else {
                // Existing entity
                context.Entry(purchase).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var purchase = context.Purchases.Find(id);
            context.Purchases.Remove(purchase);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IPurchaseRepository
    {
        IQueryable<Purchase> All { get; }
        IQueryable<Purchase> AllIncluding(params Expression<Func<Purchase, object>>[] includeProperties);
        Purchase Find(int id);
        void InsertOrUpdate(Purchase purchase);
        void Delete(int id);
        void Save();
    }
}