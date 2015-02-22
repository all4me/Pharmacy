using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Test_Pharmacy.Models;
using Test_Pharmacy.Models.ViewModels;

namespace Test_Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        Context con = new Context();
        #region Client
        public ActionResult Index(string F_name, string L_Name, string str)
        {

            var repo = from s in con.Clients
                       select s;
            if (!String.IsNullOrWhiteSpace(F_name) && !String.IsNullOrWhiteSpace(L_Name))
            {
                repo = repo.Where(c => c.FName == F_name && c.LName == L_Name);

            }
            return View(repo);
        }

        public ActionResult CreateClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateClient([Bind(Include = "Id,FName,LName,DateOfBirth,Adress,Phone,Email,R_Eye,L_Eye")] Client client)
        {
            {
                if (ModelState.IsValid)
                {
                    con.Clients.Add(client);
                    con.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(client);
            }
        }

        public ActionResult EditClient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = con.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient([Bind(Include = "Id,FName,LName,DateOfBirth,Adress,Phone,Email,R_Eye,L_Eye")] Client client)
        {
            if (ModelState.IsValid)
            {
                con.Entry(client).State = EntityState.Modified;
                con.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(client);
        }

        public ActionResult ClientDetails(int? id, ClientVisitViewModel model)
        {
            model.client = con.Clients.Find(id);
            model.Visits = con.Visits.Where(c => c.ClientId == model.client.Id).ToList();
            return View(model);
        }

        #endregion Client
        #region Visit

        public ActionResult CreateVisit()
        {
            ViewBag.ClientId = new SelectList(con.Clients, "Id", "LName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVisit([Bind(Include = "Id,OrderAmount,Status,VisitDate,ClientId")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                //visit = con.Visits.Where(c => c.ClientId == id);
                con.Visits.Add(visit);
                con.SaveChanges();
                return RedirectToAction("ClientDetails/" + visit.ClientId);
            }

            ViewBag.ClientId = new SelectList(con.Clients, "Id", "LName", visit.ClientId);
            return View(visit);
        }
        public ActionResult VisitEdit(int? vid)
        {
            if (vid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = con.Visits.Find(vid);
            if (visit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(con.Clients, "Id", "FName", visit.ClientId);
            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VisitEdit([Bind(Include = "Id,OrderAmount,Status,VisitDate,ClientId")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                con.Entry(visit).State = EntityState.Modified;
                con.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(con.Clients, "Id", "FName", visit.ClientId);
            return View(visit);
        }

        public ActionResult VisitDetails(int? vid)
        {
            if (vid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = con.Visits.Find(vid);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        public ActionResult VisitDelete(int? vid)
        {
            if (vid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = con.Visits.Find(vid);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        [HttpPost, ActionName("VisitDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult VisitDeleteConfirmed(int vid)
        {
            Client cl = new Client();
            Visit visit = con.Visits.Find(vid);
            con.Visits.Remove(visit);
            con.SaveChanges();
            return RedirectToAction("Index" );
        }

        #endregion Visit

        protected override void Dispose(bool disposing)
        {
            con.Dispose();
            base.Dispose(disposing);
        }
    }
}
