using ProjetoLeilaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjetoLeilaoWeb.Controllers
{
    public class LanceController : Controller
    {
        // GET: Lance
        public ActionResult Index()
        {
            var app = new LanceAplicacao();
            var lances = app.ListarTodos();
            return View(lances);
        }

        
        public ActionResult Details(Produto produto)
        {         
            var app = new LanceAplicacao();
            var lances = app.ListarPorProduto(produto.ProdutoID);
            return View(lances);
        }

        // GET: Lance/Details/5
        public ActionResult Invalido()
        {
            return View();
        }

        // GET: Lance/Create
        public ActionResult Create(Produto produto)
        {
            //CARREGA SELECT DE PRODUTOS
            var app = new ProdutoAplicacao();
            var produtos = app.ListarTodos();
            ViewBag.ProdutoID = new SelectList(produtos, "ProdutoID", "Nome");

            //CARREGA SELECT DE PESSOAS
            var app2 = new PessoaAplicacao();
            var pessoas = app2.ListarTodos();
            ViewBag.PessoaID = new SelectList(pessoas, "PessoaID", "Nome");
            return View();
        }

        // POST: Lance/Create
        [HttpPost]
        public ActionResult Create(Lance lance)
        {
            if (ModelState.IsValid)
            {
                var app = new LanceAplicacao();
                bool aux = app.Salvar(lance);
                if (aux == true)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Invalido");
            }
            return View(lance);
        }

        public ActionResult Produtos()
        {
            return RedirectToAction("Index", "Produto");
        }

        // GET: Lance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lance/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lance/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lance/Delete/5
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
