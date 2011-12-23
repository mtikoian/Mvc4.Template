using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Template.Mvc4.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Template.Mvc4.Models
{
  public class TaskRepository : GenericRepository<Task>
  {
  }

  public class Task: ModelBase
  {
    [ForeignKey("ParentTask")]
    public int? ParentId { get; set; }
    public Task ParentTask { get; set; }
    public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    public virtual ICollection<Task> SubTasks { get; set; }
  }

  public class TimeEntry : ModelBase
  {
    public int TaskId { get; set; }
    public virtual Task Task { get; set; }
    
    public virtual DateTime Start { get; set; }
    public virtual DateTime End { get; set; }
    public virtual string Notes { get; set; }
  }
}