using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Template.Mvc4.Models;
using Template.Mvc4.Repositories;
using System.Diagnostics;

namespace Template.Mvc4.Controllers
{
  public class TaskController : GenericController<Task, GenericRepository<Task>>
  {
    
    public override ViewResult Index()
    {
      //var list = _repository.All.Where(t => t.ParentId == null);
      var tasks = _repository.All.Where(t => t.ParentId == null);
      populateSubTasks(tasks);



      foreach (var task in tasks)
      {
        foreach (var subTask in task.SubTasks)
        {
          var count = subTask.SubTasks.Count();
          Debug.WriteLine(count);
        }
        
      }
      return View(tasks);
    }

    private void populateSubTasks(IEnumerable<Task> tasks)
    {
      foreach (var task in tasks)
      {
        populateSubTasks(task.SubTasks);
      }
    }

    public ActionResult CreateChild(int id)
    {
      var model = new Task();
      model.ParentId = id;
      return View("Create", model);
    }

    [HttpPost]
    public ActionResult CreateChild(Task model)
    {
      return base.Create(model);
     
    }
  }
}

