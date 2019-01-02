using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Dao
{
    class SolucaoDao
    {
        public bool add(Solucao modelo)
        {
            if (this.search("id", modelo.id).Count() > 0)
            {
                return false;
            }
            else
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "insert into solucao (id, nome, descricao) values (@id, @nome, @descricao)";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@nome", modelo.nome);
                sql.Parameters.AddWithValue("@descricao", modelo.descricao);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public bool delete(Solucao modelo)
        {
            if (this.search("id", modelo.id).Count() > 0)
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "delete from solucao where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                return sql.ExecuteNonQuery() == 1;
            }
            else
            {
                return false;
            }
        }

        public bool update(Solucao modelo)
        {
            if (this.search("id", modelo.id).Count() <= 0)
            {
                return false;
            }
            else
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "update solucao set nome = @nome, descricao = @descricao where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@nome", modelo.nome);
                sql.Parameters.AddWithValue("@descricao", modelo.descricao);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public List<Solucao> all()
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from solucao", conex);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Solucao> solucoes = new List<Solucao>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string nome = data.Rows[x]["nome"].ToString();
                string descricao = data.Rows[x]["descricao"].ToString();

                Solucao solucao = new Solucao(id, nome, descricao);
                solucoes.Add(solucao);
            }
            return solucoes;
        }

        public List<Solucao> search<T>(string campo, T valor)
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from solucao where " + campo + " = @value", conex);
            sql.Parameters.AddWithValue("@value", valor);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Solucao> solucoes = new List<Solucao>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string nome = data.Rows[x]["nome"].ToString();
                string descricao = data.Rows[x]["descricao"].ToString();

                Solucao solucao = new Solucao(id, nome, descricao);
                solucoes.Add(solucao);
            }
            return solucoes;
        }

        public bool problemaSolucao(string problema, string solucao)
        {
            ProblemaController pc = new ProblemaController();
            if (this.search("id", solucao).Count() <= 0 || pc.search("id", problema).Count() <= 0)
            {
                return false;
            }
            else
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "insert into problemaSolucao values (@problema, @solucao)";
                sql.Parameters.AddWithValue("@problema", problema);
                sql.Parameters.AddWithValue("@solucao", solucao);
                return sql.ExecuteNonQuery() == 1;
            }
        }
    }
}