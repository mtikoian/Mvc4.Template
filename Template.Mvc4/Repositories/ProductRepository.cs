using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Template.Mvc4.Models;

namespace Template.Mvc4.Repositories
{ 
    public class ProductRepository : IProductRepository
    {
        TemplateMvc4Context context = new TemplateMvc4Context();

        public IQueryable<Product> All
        {
            get { return context.Products; }
        }

        public IQueryable<Product> AllIncluding(params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = context.Products;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Product Find(int id)
        {
            return context.Products.Find(id);
        }

        public void InsertOrUpdate(Product product)
        {
            if (product.Id == default(int)) {
                // New entity
                context.Products.Add(product);
            } else {
                // Existing entity
                context.Entry(product).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var product = context.Products.Find(id);
            context.Products.Remove(product);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IProductRepository
    {
        IQueryable<Product> All { get; }
        IQueryable<Product> AllIncluding(params Expression<Func<Product, object>>[] includeProperties);
        Product Find(int id);
        void InsertOrUpdate(Product product);
        void Delete(int id);
        void Save();
    }
}