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
    public DateTime ModifyDate { get; private set; }
  }

  public class Contact : ModelBase
  {
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}