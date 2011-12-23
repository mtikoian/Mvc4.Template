using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Mvc4.Models;

namespace Template.Mvc4.Controllers
{   
    public class PurchaseTestsController : Controller
    {
        private TemplateMvc4Context context = new TemplateMvc4Context();

        //
        // GET: /PurchaseTests/

        public ViewResult Index()
        {
            return View(context.PurchaseTests.ToList());
        }

        public ActionResult AutoCompleteStore(string term)
        {
          if (term == null)
          {
            term = string.Empty;
          }

          var model = (from p in context.PurchaseTests
                       where p.Store.Contains(term)
                      select p.Store).Distinct() ;
          return Json(model.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteTitle(string term)
        {
          if (term == null)
          {
            term = string.Empty;
          }

          var model = (from p in context.PurchaseTests
                       where p.Title.Contains(term)
                       select p.Title).Distinct();
          return Json(model.ToArray(), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /PurchaseTests/Details/5

        public ViewResult Details(int id)
        {
            PurchaseTest purchasetest = context.PurchaseTests.Single(x => x.Id == id);
            return View(purchasetest);
        }

        //
        // GET: /PurchaseTests/Create

        public ActionResult Create()
        {
            return View(new PurchaseTest());
        } 

        //
        // POST: /PurchaseTests/Create

        [HttpPost]
        public ActionResult Create(PurchaseTest purchasetest)
        {
            if (ModelState.IsValid)
            {
                purchasetest.ModifyDate = DateTime.Now;
                context.PurchaseTests.Add(purchasetest);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(purchasetest);
        }
        
        //
        // GET: /PurchaseTests/Edit/5
 
        public ActionResult Edit(int id)
        {
            PurchaseTest purchasetest = context.PurchaseTests.Single(x => x.Id == id);
            return View(purchasetest);
        }

        //
        // POST: /PurchaseTests/Edit/5

        [HttpPost]
        public ActionResult Edit(PurchaseTest purchasetest)
        {
            if (ModelState.IsValid)
            {
                purchasetest.ModifyDate = DateTime.Now;
                context.Entry(purchasetest).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchasetest);
        }

        //
        // GET: /PurchaseTests/Delete/5
 
        public ActionResult Delete(int id)
        {
            PurchaseTest purchasetest = context.PurchaseTests.Single(x => x.Id == id);
            return View(purchasetest);
        }

        //
        // POST: /PurchaseTests/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseTest purchasetest = context.PurchaseTests.Single(x => x.Id == id);
            context.PurchaseTests.Remove(purchasetest);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}