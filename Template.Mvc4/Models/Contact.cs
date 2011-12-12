using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Template.Mvc4.Models
{
  public abstract class ModelBase
  {
    public int Id { get; set; }
    public string Title { get; set; }
    [DisplayFormat(DataFormatString = "mm:hh:ss")]
    public DateTime? ModifyDate { get; set; }
  }

  public class Contact : ModelBase
  {
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }


  }

  public class Product : ModelBase
  {
    public string ImageUrl { get; set; }
    public int? CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Store> Stores { get; set; }
    public virtual ICollection<Purchase> Purchases { get; set; }

  }

  public class Purchase : ModelBase
  {
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public int StoreId { get; set; }
    public virtual Store Store { get; set; }
    public double? Price { get; set; }
    public string UnitName { get; set; }
    public double? UnitQuantity { get; set; }
  }

  public class Category: ModelBase
  {
    public virtual ICollection<Product> Products { get; set; }
  }


  public class Store : ModelBase
  {
    public virtual ICollection<Product> Products { get; set; }
  }
}