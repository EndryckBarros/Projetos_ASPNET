using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaseModels;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservas
        public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r._Cliente).Include(r => r._Servico);
            return View(reservas.ToList());
        }

        // GET: Reservas/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome");
            ViewBag.ServicoID = new SelectList(db.Servicoes, "ServicoID", "Nome");
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservaID,Data,ServicoID,ClienteID")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", reserva.ClienteID);
            ViewBag.ServicoID = new SelectList(db.Servicoes, "ServicoID", "Nome", reserva.ServicoID);
            return View(reserva);
        }

        // GET: Reservas/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", reserva.ClienteID);
            ViewBag.ServicoID = new SelectList(db.Servicoes, "ServicoID", "Nome", reserva.ServicoID);
            return View(reserva);
        }

        // POST: Reservas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservaID,Data,ServicoID,ClienteID")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", reserva.ClienteID);
            ViewBag.ServicoID = new SelectList(db.Servicoes, "ServicoID", "Nome", reserva.ServicoID);
            return View(reserva);
        }

        // GET: Reservas/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
            db.SaveChanges();
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
