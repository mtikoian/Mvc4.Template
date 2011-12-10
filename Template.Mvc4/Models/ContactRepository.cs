using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Template.Mvc4.Models
{ 
    public class ContactRepository : IContactRepository
    {
        TemplateMvc4Context context = new TemplateMvc4Context();

        public IQueryable<Contact> All
        {
            get { return context.Contacts; }
        }

        public IQueryable<Contact> AllIncluding(params Expression<Func<Contact, object>>[] includeProperties)
        {
            IQueryable<Contact> query = context.Contacts;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Contact Find(int id)
        {
            return context.Contacts.Find(id);
        }

        public void InsertOrUpdate(Contact contact)
        {
          contact.ModifyDate = DateTime.Now;
            if (contact.Id == default(int)) {
                // New entity
                context.Contacts.Add(contact);
            } else {
                // Existing entity
                context.Entry(contact).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var contact = context.Contacts.Find(id);
            context.Contacts.Remove(contact);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IContactRepository
    {
        IQueryable<Contact> All { get; }
        IQueryable<Contact> AllIncluding(params Expression<Func<Contact, object>>[] includeProperties);
        Contact Find(int id);
        void InsertOrUpdate(Contact contact);
        void Delete(int id);
        void Save();
    }
}