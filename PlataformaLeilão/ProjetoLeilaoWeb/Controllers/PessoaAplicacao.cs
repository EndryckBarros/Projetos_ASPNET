using ConexaoDB;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ProjetoLeilaoWeb.Models;

namespace ProjetoLeilaoWeb
{
    public class PessoaAplicacao
    {
        private db db;
        public void Inserir(Pessoa pessoa)
        {
            var strQuery = "";
            strQuery += "INSERT INTO Pessoa(Nome, Idade)";
            strQuery += string.Format(" VALUES ('{0}', '{1}')", pessoa.Nome, pessoa.Idade
                );
            using (db = new db())
            {
                db.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Pessoa pessoa)
        {
            var strQuery = "";
            strQuery += "UPDATE Pessoa SET ";
            strQuery += string.Format("Nome = '{0}',", pessoa.Nome);
            strQuery += string.Format("Idade = '{0}' ", pessoa.Idade);
            strQuery += string.Format("WHERE PessoaID = {0} ", pessoa.PessoaID);

            using (db = new db())
            {
                db.ExecutaComando(strQuery);
            }
        }

        public List<Pessoa> ListarTodos()
        {
            using (db = new db())
            {
                var strQuery = "SELECT * FROM Pessoa";
                var retorno = db.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno);
            }
        }

        private List<Pessoa> ReaderEmLista(SqlDataReader reader)
        {
            var pessoas = new List<Pessoa>();
            while (reader.Read())
            {
                var pessoa = new Pessoa()
                {
                    PessoaID = int.Parse(reader["PessoaID"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Idade = int.Parse(reader["Idade"].ToString())
                };

                pessoas.Add(pessoa);
            }

            reader.Close();
            return pessoas;
        }

        public Pessoa ListarPorId(int id)
        {
            using (db = new db())
            {
                var strQuery = string.Format("SELECT * FROM Pessoa WHERE PessoaID = {0}", id);
                var retorno = db.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }

        public void Salvar(Pessoa pessoa)
        {
            if (pessoa.PessoaID > 0)
            {
                Alterar(pessoa);
            }
            else
            {
                Inserir(pessoa);
            }
        }
    }
}
