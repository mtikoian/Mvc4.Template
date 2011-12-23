using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Template.Mvc4.Models
{
  public abstract class ModelBase
  {
    public int Id { get; set; }
    public string Title { get; set; }
    [DisplayFormat(DataFormatString = "mm:hh:ss")]
    public DateTime? ModifyDate { get; set; }
  }
}