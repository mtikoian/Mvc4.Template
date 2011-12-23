using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Template.Mvc4.Models
{


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

  public class PurchaseTest : ModelBase
  {
    public string Store { get; set; }
    public float? Price { get; set; }
    public string UnitName { get; set; }
    public float? UnitQuantity { get; set; }
  }



  public class Category : ModelBase
  {
    public virtual ICollection<Product> Products { get; set; }
  }


  public class Store : ModelBase
  {
    public virtual ICollection<Product> Products { get; set; }
  }
}
