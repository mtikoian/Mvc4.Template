using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Mvc4.Models;

namespace Template.Mvc4.Controllers
{
  public class ContactsController : Controller
  {
    private readonly IContactRepository contactRepository;

    #region -- Constructor --
    
    public ContactsController()
      : this(new ContactRepository())
    {
      // If you are using Dependency Injection, you can delete the following constructor
    }

    public ContactsController(IContactRepository contactRepository)
    {
      this.contactRepository = contactRepository;
    }

    #endregion //-- Constructor --

    public ViewResult Knockout()
    {
      return View();
    }

    public JsonResult KnockoutIndex()
    {
      var contacts = from c in contactRepository.All
                     select new {c.Id, c.FirstName, c.LastName, c.Title};

      var jsonResult = new JsonResult()
                         {JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = contacts};
      return jsonResult;
    }

    public ViewResult Index()
    {
      return View(contactRepository.All);
    }

    public ViewResult Details(int id)
    {
      return View(contactRepository.Find(id));
    }

    #region -- Create --

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Contact contact)
    {
      if (ModelState.IsValid)
      {
        contactRepository.InsertOrUpdate(contact);
        contactRepository.Save();
        if (Request.IsAjaxRequest())
        {
          return Json(contact, JsonRequestBehavior.AllowGet);
        }
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    #endregion //-- Create --

    #region -- Edit --

    public ActionResult Edit(int id)
    {
      return View(contactRepository.Find(id));
    }

    [HttpPost]
    public ActionResult Edit(Contact contact)
    {
      TryUpdateModel(contact);
      if (ModelState.IsValid)
      {
        contactRepository.InsertOrUpdate(contact);
        contactRepository.Save();
        if (Request.IsAjaxRequest())
        {
          return Json(contact, JsonRequestBehavior.AllowGet);
        }
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    #endregion // -- Edit --

    #region -- Delete --
    
    public ActionResult Delete(int id)
    {
      return View(contactRepository.Find(id));
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      contactRepository.Delete(id);
      contactRepository.Save();

      return RedirectToAction("Index");
    }

    #endregion // -- Delete --
  }
}

