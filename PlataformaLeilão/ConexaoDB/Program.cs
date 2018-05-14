using System;
using System.Data.SqlClient;
using ProjetoLeilaoWeb;

namespace ConexaoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ProdutoAplicacao app = new ProdutoAplicacao();

            SqlConnection conn = new SqlConnection(@"data source = DESKTOP-CQTE0GH\SQLEXPRESS; Integrated Security = SSPI; Initial Catalog =LeilaoDB");
            conn.Open();

            Console.WriteLine("Nome:");
            string nome = Console.ReadLine();

            Console.WriteLine("Valor:");
            string valor = Console.ReadLine();
            

            Produto produto = new Produto()
            {
                Nome = nome,
                Valor = int.Parse(valor)
            };

            app.Inserir(produto);
            
            var dados = app.ListarTodos();


            foreach(var usuario in dados)
            {
                Console.WriteLine("iD: {0}, Nome: {1}, Valor: {2}", 
                            usuario.ProdutoID,usuario.Nome,usuario.Valor);
            }
        }
    }
}
