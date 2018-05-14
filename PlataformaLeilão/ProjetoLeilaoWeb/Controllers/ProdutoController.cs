using ProjetoLeilaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoLeilaoWeb.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            var app = new ProdutoAplicacao();
            var produtos = app.ListarTodos();
            //ViewBag.ProdutoID = new SelectList(produtos, "ProdutoID", "Nome");
            return View(produtos);
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            var app = new ProdutoAplicacao();
            var produto = app.ListarPorId(id);
            return RedirectToAction("Details", "Lance", produto);
           
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var app = new ProdutoAplicacao();
                    app.Salvar(produto);
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch
            {
                return View(produto);
            }
        }


        public ActionResult DarLance(int id)
        {
            var app = new ProdutoAplicacao();
            var produto = app.ListarPorId(id);
            return RedirectToAction("Create", "Lance", produto);
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            var app = new ProdutoAplicacao();
            var produto = app.ListarPorId(id);

            if(produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                var app = new ProdutoAplicacao();
                app.Salvar(produto);
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
