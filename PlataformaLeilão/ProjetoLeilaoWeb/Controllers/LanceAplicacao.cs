using ConexaoDB;
using ProjetoLeilaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLeilaoWeb.Controllers
{
    public class LanceAplicacao
    {
        private db db;
        public void Inserir(Lance lance)
        {
            var strQuery = "";
            strQuery += "INSERT INTO Lance(ProdutoID, PessoaID, Valor)";
            strQuery += string.Format(" VALUES ('{0}', '{1}', '{2}')", lance.ProdutoID, lance.PessoaID, lance.Valor
                );
            using (db = new db())
            {
                db.ExecutaComando(strQuery);
            }
        }

        public List<Lance> ListarTodos()
        {
            using (db = new db())
            {
                var strQuery = "SELECT * FROM Lance";
                var retorno = db.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno);
            }
        }

        private List<Lance> ReaderEmLista(SqlDataReader reader)
        {
            var lances = new List<Lance>();
            while (reader.Read())
            {
                var lance = new Lance()
                {
                    LanceID = int.Parse(reader["LanceID"].ToString()),
                    ProdutoID = int.Parse(reader["ProdutoID"].ToString()),
                    PessoaID = int.Parse(reader["PessoaID"].ToString()),
                    Valor = int.Parse(reader["Valor"].ToString())
                };

                lances.Add(lance);
            }

            reader.Close();
            return lances;
        }

        public Lance ListarPorId(int id)
        {
            using (db = new db())
            {
                var strQuery = string.Format("SELECT * FROM Lance WHERE LanceID = {0}", id);
                var retorno = db.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }

        public List<Lance> ListarPorProduto(int id)
        {
            using (db = new db())
            {
                var strQuery = string.Format("SELECT * FROM Lance WHERE ProdutoID = {0}", id);
                var retorno = db.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno);
            }
        }

        public bool Salvar(Lance lance)
        {
            var app = new ProdutoAplicacao();
            var produto = app.ListarPorId(lance.ProdutoID);
            var retorno = ListarPorProduto(lance.ProdutoID);
            

            foreach (var lanceLista in retorno)
            {
                if(lanceLista.Valor <= lance.Valor || produto.Valor < lance.Valor)
                {
                    return false;
                }
            }

            Inserir(lance);
            return true;
        }

    }
}