using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Dao
{
    class ProblemaDao
    {
        public bool add(Problema modelo)
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
                sql.CommandText = "insert into problema (id, nome, stt, detalhes, descricao) values (@id, @nome, @stt, @detalhes, @descricao)";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@nome", modelo.nome);
                sql.Parameters.AddWithValue("@stt", modelo.stt);
                sql.Parameters.AddWithValue("@detalhes", modelo.detalhes);
                sql.Parameters.AddWithValue("@descricao", modelo.descricao);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public bool delete(Problema modelo)
        {
            if (this.search("id", modelo.id).Count() > 0)
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "delete from problema where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                return sql.ExecuteNonQuery() == 1;
            }
            else
            {
                return false;
            }
        }

        public bool update(Problema modelo)
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
                sql.CommandText = "update problema set nome = @nome, detalhes = @detalhes, stt = @stt, descricao = @descricao where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@nome", modelo.nome);
                sql.Parameters.AddWithValue("@stt", modelo.stt);
                sql.Parameters.AddWithValue("@detalhes", modelo.detalhes);
                sql.Parameters.AddWithValue("@descricao", modelo.descricao);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public List<Model.Problema> all()
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from problema", conex);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Problema> problemas = new List<Problema>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string nome = data.Rows[x]["nome"].ToString();
                bool stt = Convert.ToBoolean(data.Rows[x]["stt"]);
                string detalhes = data.Rows[x]["detalhes"].ToString();
                string descricao = data.Rows[x]["descricao"].ToString();

                Problema problema = new Problema(id, nome, stt, detalhes, descricao);
                problemas.Add(problema);
            }
            return problemas;
        }

        public List<Model.Problema> search<T>(string campo, T valor)
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from problema where " + campo + " = @value", conex);
            sql.Parameters.AddWithValue("@value", valor);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Problema> problemas = new List<Problema>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string nome = data.Rows[x]["nome"].ToString();
                bool stt = Convert.ToBoolean(data.Rows[x]["stt"]);
                string detalhes = data.Rows[x]["detalhes"].ToString();
                string descricao = data.Rows[x]["descricao"].ToString();

                Problema problema = new Problema(id, nome, stt, detalhes, descricao);
                problemas.Add(problema);
            }
            return problemas;
        }

        public List<Solucao> listaSolucao(string problema)
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select solucao from problemaSolucao where problema = @problema", conex);
            sql.Parameters.AddWithValue("@problema", problema);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Solucao> solucoes = new List<Solucao>();
            SolucaoController solC = new SolucaoController();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                Solucao solucao = solC.search("id", data.Rows[x]["solucao"].ToString())[0];
                solucoes.Add(solucao);
            }
            return solucoes;
        }
    }
}