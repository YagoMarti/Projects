using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SisRep.Models;

namespace SisRep.Controllers
{
    public class ReposicionController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();

        // GET: /Reposicion/
        public ActionResult Index()
        {
            var reposiciones = db.reposiciones.Include(r => r.herramienta).Include(r => r.puesto).Include(r => r.usuario).Include(r => r.usuario1);
            return View(reposiciones.ToList());
        }

        // GET: /Reposicion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reposicione reposicione = db.reposiciones.Find(id);
            if (reposicione == null)
            {
                return HttpNotFound();
            }
            return View(reposicione);
        }

        // GET: /Reposicion/Create
        public ActionResult Create()
        {
            ViewBag.idHerramienta = new SelectList(db.herramientas, "idHerramienta", "herramienta1");
            ViewBag.idPuesto = new SelectList(db.puestos, "idPuesto", "puesto1");
            ViewBag.idOperario = new SelectList(db.usuarios, "idUsuario", "nombre");
            ViewBag.idSolicitante = new SelectList(db.usuarios, "idUsuario", "nombre");
            return View();
        }

        // POST: /Reposicion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idReposicion,idOperario,idSolicitante,idPuesto,idHerramienta,comentario,horaCreacion,horaAsignacion,horaResolucion")] reposicione reposicione)
        {
            if (ModelState.IsValid)
            {
                db.reposiciones.Add(reposicione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idHerramienta = new SelectList(db.herramientas, "idHerramienta", "herramienta1", reposicione.idHerramienta);
            ViewBag.idPuesto = new SelectList(db.puestos, "idPuesto", "puesto1", reposicione.idPuesto);
            ViewBag.idOperario = new SelectList(db.usuarios, "idUsuario", "nombre", reposicione.idOperario);
            ViewBag.idSolicitante = new SelectList(db.usuarios, "idUsuario", "nombre", reposicione.idSolicitante);
            return View(reposicione);
        }

        // GET: /Reposicion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reposicione reposicione = db.reposiciones.Find(id);
            if (reposicione == null)
            {
                return HttpNotFound();
            }
            ViewBag.idHerramienta = new SelectList(db.herramientas, "idHerramienta", "herramienta1", reposicione.idHerramienta);
            ViewBag.idPuesto = new SelectList(db.puestos, "idPuesto", "puesto1", reposicione.idPuesto);
            ViewBag.idOperario = new SelectList(db.usuarios, "idUsuario", "nombre", reposicione.idOperario);
            ViewBag.idSolicitante = new SelectList(db.usuarios, "idUsuario", "nombre", reposicione.idSolicitante);
            return View(reposicione);
        }

        // POST: /Reposicion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idReposicion,idOperario,idSolicitante,idPuesto,idHerramienta,comentario,horaCreacion,horaAsignacion,horaResolucion")] reposicione reposicione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reposicione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idHerramienta = new SelectList(db.herramientas, "idHerramienta", "herramienta1", reposicione.idHerramienta);
            ViewBag.idPuesto = new SelectList(db.puestos, "idPuesto", "puesto1", reposicione.idPuesto);
            ViewBag.idOperario = new SelectList(db.usuarios, "idUsuario", "nombre", reposicione.idOperario);
            ViewBag.idSolicitante = new SelectList(db.usuarios, "idUsuario", "nombre", reposicione.idSolicitante);
            return View(reposicione);
        }

        // GET: /Reposicion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reposicione reposicione = db.reposiciones.Find(id);
            if (reposicione == null)
            {
                return HttpNotFound();
            }
            return View(reposicione);
        }

        // POST: /Reposicion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            reposicione reposicione = db.reposiciones.Find(id);
            db.reposiciones.Remove(reposicione);
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
