using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ConexaoDB
{
    public class db : IDisposable
    {

        private readonly SqlConnection conn;
        public db()
        {
            //CONEXÃO FEITA NA WEB CONFIG ----------------V
            conn = new SqlConnection
            (ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString);
            conn.Open();
        }

        public void ExecutaComando(string strQuery)
        {
            var comando = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = conn
            };
            comando.ExecuteNonQuery();
        }

        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var comandoSelect = new SqlCommand(strQuery, conn);
            return comandoSelect.ExecuteReader();
        }

        public void Dispose()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

    }
}

//vvvvvv Contruindo Banco SQL Server vvvvvvv

//create table Produto
//  (ProdutoID int IDENTITY(1,1) NOT NULL,
//  Nome varchar(20) NOT NULL,
//  Valor int NOT NULL,
//  primary key(ProdutoID));

//create table Pessoa
//  (PessoaID int IDENTITY(1,1) NOT NULL,
//  Nome varchar(20) NOT NULL,
//  Idade int NOT NULL,
//  primary key(PessoaID));

//create table Lance
//  (LanceID int IDENTITY(1,1) NOT NULL,
//  ProdutoID int NOT NULL,
//  PessoaID int NOT NULL,
//  Valor int NOT NULL,
//  primary key(LanceID),
//  foreign key(ProdutoID) references Produto(ProdutoID),
//  foreign key(PessoaID) references Pessoa(PessoaID));

