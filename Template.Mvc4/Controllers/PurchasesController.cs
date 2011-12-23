using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Mvc4.Models;
using Template.Mvc4.Repositories;

namespace Template.Mvc4.Controllers
{   
    public class PurchasesController : Controller
    {
		private readonly IProductRepository productRepository;
		private readonly IStoreRepository storeRepository;
		private readonly IPurchaseRepository purchaseRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public PurchasesController() : this(new ProductRepository(), new StoreRepository(), new PurchaseRepository())
        {
        }

        public PurchasesController(IProductRepository productRepository, IStoreRepository storeRepository, IPurchaseRepository purchaseRepository)
        {
			this.productRepository = productRepository;
			this.storeRepository = storeRepository;
			this.purchaseRepository = purchaseRepository;
        }

        //
        // GET: /Purchases/

        public ViewResult Index()
        {
            return View(purchaseRepository.AllIncluding(purchase => purchase.Product, purchase => purchase.Store));
        }

        //
        // GET: /Purchases/Details/5

        public ViewResult Details(int id)
        {
            return View(purchaseRepository.Find(id));
        }

        //
        // GET: /Purchases/Create

        public ActionResult Create()
        {
			ViewBag.PossibleProducts = productRepository.All;
			ViewBag.PossibleStores = storeRepository.All;
            return View();
        } 

        //
        // POST: /Purchases/Create

        [HttpPost]
        public ActionResult Create(Purchase purchase)
        {
            if (ModelState.IsValid) {
                purchaseRepository.InsertOrUpdate(purchase);
                purchaseRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = productRepository.All;
				ViewBag.PossibleStores = storeRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Purchases/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleProducts = productRepository.All;
			ViewBag.PossibleStores = storeRepository.All;
             return View(purchaseRepository.Find(id));
        }

        //
        // POST: /Purchases/Edit/5

        [HttpPost]
        public ActionResult Edit(Purchase purchase)
        {
            if (ModelState.IsValid) {
                purchaseRepository.InsertOrUpdate(purchase);
                purchaseRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = productRepository.All;
				ViewBag.PossibleStores = storeRepository.All;
				return View();
			}
        }

        //
        // GET: /Purchases/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(purchaseRepository.Find(id));
        }

        //
        // POST: /Purchases/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            purchaseRepository.Delete(id);
            purchaseRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

