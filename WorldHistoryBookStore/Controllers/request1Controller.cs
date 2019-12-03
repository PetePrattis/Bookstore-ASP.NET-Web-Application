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
    public class request1Controller : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: request1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Submit()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(string start, string end, int number)
        {
            DateTime date1;
            DateTime date2;

            if (start == "")
                start = "01/01/1992";

            if (end == "")
                end = "01/01/1995";

            date1 = DateTime.Parse(start);

            date2 = DateTime.Parse(end);

            if (date1 == date2 || date1 > date2)
            {
                date1 = DateTime.Parse("01/01/1992");
                date2 = DateTime.Parse("01/01/1995");
            }

            var sales = db.sales.ToList();

            sales = sales.OrderByDescending(q => q.qty).ToList();
            sales = sales.FindAll(d => d.ord_date >= date1);
            sales = sales.FindAll(d => d.ord_date <= date2);

            if (number != 0)
                sales = sales.Take(number).ToList();


            var titleauthors = db.titleauthors.ToList();
            var authors = db.authors.ToList();


            titleauthors = titleauthors.Where(id => sales.Any(i => i.title_id == id.title_id)).ToList();
            authors = authors.Where(id => titleauthors.Any(i => i.au_id == id.au_id)).ToList();

            return View(authors);

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
