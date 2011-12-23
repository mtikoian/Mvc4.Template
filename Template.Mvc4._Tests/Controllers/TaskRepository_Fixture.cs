using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Template.Mvc4.Controllers;
using Template.Mvc4.Models;
using System.Diagnostics;
using System.Data.Entity;

namespace Template.Mvc4._Tests.Controllers
{
  [TestFixture]
  public class TaskRepository_Fixture
  {
    TemplateMvc4Context _context = new TemplateMvc4Context();
    TaskRepository _repo = new TaskRepository();
    
    private void SetDbInit()
    {
      Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TemplateMvc4Context>());
      //Database.SetInitializer(new CreateDatabaseIfNotExists<TemplateMvc4Context>());
    }
    public TaskRepository_Fixture()
    {
      SetDbInit();  
    }
    [Test]
    public void Create()
    {
      // Arrange
      var initialCount = _context.Tasks.Count();
      Debug.WriteLine("Initial Count: " + initialCount);
      var task = new Task()
                   {
                     ModifyDate = DateTime.Now,
                     Title = "TestTask"
                   };
      // Act
      _repo.InsertOrUpdate(task);
      _repo.Save();

      // Assert
      var finalCount = _context.Tasks.Count();
      Debug.WriteLine("Final Count: " + finalCount);
      Assert.That(finalCount, Is.EqualTo(initialCount + 1), "Final count.");
    }


    [Test]
    public void CreateSubTask()
    {
      // Arrange
      var initialCount = _context.Tasks.Count();
      Debug.WriteLine("Initial Count: " + initialCount);
      var task = CreateDefaultTask("parent"); 
      var subTask = CreateDefaultTask("child");
      var childTitle = subTask.Title;

      task.SubTasks.Add(subTask);

      // Act
      _repo.InsertOrUpdate(task);
      _repo.Save();

      // Assert
      //  - task.Id is set
      Assert.That(task.Id, Is.GreaterThan(0), "Task Id");
      
      //  - database task count
      var finalCount = _context.Tasks.Count();
      Debug.WriteLine("Final Count: " + finalCount);
      Assert.That(finalCount, Is.EqualTo(initialCount + 2), "Final count.");
      
      //  - task.ModifyDate
      Assert.That(task.ModifyDate, Is.GreaterThan(DateTime.Now.AddMinutes(-1)), "ModifyDate more than 1 minute ago.");
      Assert.That(task.ModifyDate, Is.LessThan(DateTime.Now), "ModifyDate more than now.");


      //  - re-fetch task and count subtasks
      var fetchedTask = _context.Tasks.Find(task.Id);
      Assert.That(task.SubTasks.Count(), Is.EqualTo(1), "SubTask count");

      //  - refetch child task and check parent
      var fetchedSubTask = _context.Tasks.Find(task.SubTasks.First().Id);
      //Assert.That(fetchedSubTask.ParentId, Is.EqualTo(task.Id));
      Assert.That(fetchedSubTask.ParentTask, Is.Not.Null,"Parent task is null.");
    }





    public string UniqueString(int length = 5)
    {
      return Guid.NewGuid().ToString().Substring(0, length);
    }
    
    public Task CreateDefaultTask(string prefix = "")
    {
      var task = _context.Tasks.Create();
      task.Title = prefix + "-Test" + UniqueString();
      task.SubTasks = new List<Task>();
      return task;
    }
  }
}
