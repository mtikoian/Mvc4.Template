using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Template.Mvc4.Models
{


  public class Contact : ModelBase
  {
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }


  }

}