using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorldHistoryBookStore.Models;

namespace WorldHistoryBookStore.Controllers
{
    public class salesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: sales
        public ActionResult Index()
        {
            var sales = db.sales.Include(s => s.store).Include(s => s.title);
            return View(sales.ToList());
        }

        // GET: sales/Details/5
        public ActionResult Details(string stor_id, string ord_num, string title_id)
        {
            if (stor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ord_num == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            sale sale = db.sales.Find(stor_id, ord_num, title_id);

            if (sale == null)
            {
                return HttpNotFound();
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1");
            ViewBag.ord_num = new SelectList(db.sales, "ord_num", "ord_num");
            return View(sale);
        }

        // GET: sales/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1");
            ViewBag.ord_num = new SelectList(db.sales, "ord_num", "ord_num");
            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sale sale)
        {
            if (ModelState.IsValid)
            {
                var test = db.sales.Find(sale.stor_id, sale.ord_num,sale.title_id); //find if stor_id (prim key's) already exists 

                if (test == null)
                {
                    db.sales.Add(sale);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("../Home/Error");
                    }
                }
                else //value already exists
                    return RedirectToAction("../Home/Error");

                return RedirectToAction("Index");
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            ViewBag.ord_num = new SelectList(db.sales, "ord_num", "ord_num", sale.ord_num);
            return View(sale);
        }

        // GET: sales/Edit/5
        public ActionResult Edit(string stor_id, string ord_num, string title_id)
        {

            if (stor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ord_num == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            sale sale = db.sales.Find(stor_id, ord_num, title_id);

            if (sale == null)
            {
                return HttpNotFound();
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1");
            ViewBag.ord_num = new SelectList(db.sales, "ord_num", "ord_num");
            return View(sale);
           
        }

        // POST: sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return RedirectToAction("../Home/Error");
                }
                return RedirectToAction("Index");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            ViewBag.ord_num = new SelectList(db.sales, "ord_num", "ord_num",sale.ord_num);
            return View(sale);
        }

        // GET: sales/Delete/5
        public ActionResult Delete(string stor_id, string ord_num, string title_id)
        {
            if (stor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (title_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ord_num == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sale sale = db.sales.Find(stor_id, ord_num, title_id);

            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string stor_id, string ord_num, string title_id )
        {
            sale sale = db.sales.Find(stor_id, ord_num, title_id);
            db.sales.Remove(sale);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return RedirectToAction("../Home/Error");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
