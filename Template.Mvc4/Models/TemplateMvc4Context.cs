using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Template.Mvc4.Models
{
    public class TemplateMvc4Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Template.Mvc4.Models.TemplateMvc4Context>());

       public DbSet<Template.Mvc4.Models.Contact> Contacts { get; set; }

       public DbSet<Template.Mvc4.Models.Product> Products { get; set; }

       public DbSet<Template.Mvc4.Models.Category> Categories { get; set; }

       public DbSet<Template.Mvc4.Models.Purchase> Purchases { get; set; }

       public DbSet<Template.Mvc4.Models.Store> Stores { get; set; }

       public DbSet<Template.Mvc4.Models.PurchaseTest> PurchaseTests { get; set; }

       public DbSet<Template.Mvc4.Models.Task> Tasks { get; set; }
    }
}