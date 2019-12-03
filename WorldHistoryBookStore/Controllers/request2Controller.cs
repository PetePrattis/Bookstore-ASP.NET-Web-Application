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
    public class request2Controller : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: request2
        public ActionResult Index()
        {
            var sales = db.sales.Include(s => s.store).Include(s => s.title);
            return View(sales.ToList());
        }

        public ActionResult Submit()
        {
            ViewBag.stor_name = new SelectList(db.stores,"stor_name","stor_name");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string start, string end, string stor_name)
        {
            DateTime date1;
            DateTime date2;

            if (start == "")
                start = "01/01/1992";

            if (end == "")
                end = "01/01/1995";


            date1 = Convert.ToDateTime(start);
            date2 = Convert.ToDateTime(end);

           

            date1 = DateTime.Parse(start);

            date2 = DateTime.Parse(end);

            if (date1 == date2 || date1 > date2)
            {
                date1 = DateTime.Parse("01/01/1992");
                start = "01/01/1992";
                date2 = DateTime.Parse("01/01/1995");
                end = "01/01/1995";
            }

            if (!stor_name.Equals("")) //when  certain store was selected
            {
                if (!stor_name.Contains("'"))
                {
                    var r = db.Database.SqlQuery<Request2>("Select s.ord_num, st.stor_name, t.title From titles as t, stores as st, authors as a, sales as s, titleauthor as ta " +
                    "Where s.ord_date >= '" + start + "' And s.ord_date <= '" + end + "' And st.stor_name like '" + stor_name + "' and st.stor_id = s.stor_id " +
                    "And t.title_id = s.title_id And ta.title_id = t.title_id And ta.au_id = a.au_id").ToList();
                    return View(r.ToList());
                }
                else //when there's an apostrophe in stor_name, replace it with 2 apostrophes in order to solve this problem and don't get an sql error
                {

                    var r = db.Database.SqlQuery<Request2>("Select s.ord_num, st.stor_name, t.title From titles as t, stores as st, authors as a, sales as s, titleauthor as ta " +
                   "Where s.ord_date >= '" + start + "' And s.ord_date <= '" + end + "' And st.stor_name like '" + stor_name.Replace("'", "''") + "' and st.stor_id = s.stor_id " +
                   "And t.title_id = s.title_id And ta.title_id = t.title_id And ta.au_id = a.au_id").ToList();

                    return View(r.ToList());
                }

            }

            else
            {
                var r = db.Database.SqlQuery<Request2>("Select s.ord_num, st.stor_name, t.title From titles as t, stores as st, authors as a, sales as s, titleauthor as ta " +
                    "Where s.ord_date >= '" + start + "' And s.ord_date <= '" + end + "' And st.stor_id = s.stor_id " +
                    "And t.title_id = s.title_id And ta.title_id = t.title_id And ta.au_id = a.au_id").ToList();

                return View(r.ToList());
            }
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
