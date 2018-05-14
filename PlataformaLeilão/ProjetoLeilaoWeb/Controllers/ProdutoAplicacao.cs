using ConexaoDB;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;
using ProjetoLeilaoWeb.Models;

namespace ProjetoLeilaoWeb
{
    public class ProdutoAplicacao
    {
        private db db;
        public void Inserir(Produto produto)
        {
            var strQuery = "";
            strQuery += "INSERT INTO Produto(Nome, Valor)";
            strQuery += string.Format(" VALUES ('{0}', '{1}')", produto.Nome, produto.Valor
                );
            using (db = new db())
            {
                db.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Produto produto)
        {
            var strQuery = "";
            strQuery += "UPDATE Produto SET ";
            strQuery += string.Format("Nome = '{0}',", produto.Nome);
            strQuery += string.Format("Valor = '{0}' ", produto.Valor);
            strQuery += string.Format("WHERE ProdutoID = {0} ", produto.ProdutoID);

            using (db = new db())
            {
                db.ExecutaComando(strQuery);
            }
        }

        public List<Produto> ListarTodos()
        {
            using (db = new db())
            {
                var strQuery = "SELECT * FROM Produto";
                var retorno = db.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno);
            }
        }

        private List<Produto> ReaderEmLista(SqlDataReader reader)
        {
            var produtos = new List<Produto>();
            while (reader.Read())
            {
                var produto = new Produto()
                {
                    ProdutoID = int.Parse(reader["ProdutoID"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Valor = int.Parse(reader["Valor"].ToString())
                };

                produtos.Add(produto);
            }

            reader.Close();
            return produtos;
        }

        public Produto ListarPorId(int id)
        {
            using (db = new db())
            {
                var strQuery = string.Format("SELECT * FROM Produto WHERE ProdutoID = {0}", id);
                var retorno = db.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }

        public void Salvar(Produto produto)
        {
            if (produto.ProdutoID > 0)
            {
                Alterar(produto);
            }
            else
            {
                Inserir(produto);
            }
        }

        public void DarLance(Lance lance)
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
    }
}
