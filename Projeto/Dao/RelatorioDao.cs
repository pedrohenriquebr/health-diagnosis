using MySql.Data.MySqlClient;
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
    class RelatorioDao
    {
        public bool add(Relatorio modelo)
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
                sql.CommandText = "insert into relatorio (id, conteudo, datahora, paciente, enfermeiro) values (@id, @conteudo, @datahora, @paciente, @enfermeiro)";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@conteudo", modelo.conteudo);
                sql.Parameters.AddWithValue("@datahora", modelo.datahora);
                sql.Parameters.AddWithValue("@paciente", modelo.paciente);
                sql.Parameters.AddWithValue("@enfermeiro", modelo.enfermeiro);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public bool delete(Relatorio modelo)
        {
            if (this.search("id", modelo.id).Count() > 0)
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "delete from relatorio where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                return sql.ExecuteNonQuery() == 1;
            }
            else
            {
                return false;
            }
        }

        public bool update(Relatorio modelo)
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
                sql.CommandText = "update relatorio set conteudo = @conteudo, datahora = @datahora, paciente = @paciente where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@conteudo", modelo.conteudo);
                sql.Parameters.AddWithValue("@datahora", modelo.datahora);
                sql.Parameters.AddWithValue("@paciente", modelo.paciente);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public List<Relatorio> all()
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from relatorio", conex);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Relatorio> relatorios = new List<Relatorio>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string conteudo = data.Rows[x]["conteudo"].ToString();
                DateTime datahora = Convert.ToDateTime(data.Rows[x]["datahora"]);
                string paciente = data.Rows[x]["paciente"].ToString();
                string enfermeiro = data.Rows[x]["enfermeiro"].ToString();

                Relatorio relatorio = new Relatorio(id, conteudo, datahora, paciente, enfermeiro);
                relatorios.Add(relatorio);
            }
            return relatorios;
        }

        public List<Relatorio> search<T>(string campo, T valor)
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from relatorio where " + campo + " = @value", conex);
            sql.Parameters.AddWithValue("@value", valor);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Relatorio> relatorios = new List<Relatorio>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string conteudo = data.Rows[x]["conteudo"].ToString();
                DateTime datahora = Convert.ToDateTime(data.Rows[x]["datahora"]);
                string paciente = data.Rows[x]["paciente"].ToString();
                string enfermeiro = data.Rows[x]["enfermeiro"].ToString();

                Relatorio relatorio = new Relatorio(id, conteudo, datahora, paciente, enfermeiro);
                relatorios.Add(relatorio);
            }
            return relatorios;
        }
    }
}