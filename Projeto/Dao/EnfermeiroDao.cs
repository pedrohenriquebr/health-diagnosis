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
    class EnfermeiroDao
    {
        public bool add(Enfermeiro modelo)
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
                sql.CommandText = "insert into enfermeiro (id, nome, nascimento, sexo, login, senha, stt, dataAdmissao, dataDemissao) values (@id, @nome, @nascimento, @sexo, @login, @senha, @stt, @dataAdmissao, @dataDemissao)";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@nome", modelo.nome);
                sql.Parameters.AddWithValue("@nascimento", modelo.nascimento);
                sql.Parameters.AddWithValue("@sexo", modelo.sexo);
                sql.Parameters.AddWithValue("@login", modelo.login);
                sql.Parameters.AddWithValue("@senha", modelo.senha);
                sql.Parameters.AddWithValue("@stt", modelo.stt);
                sql.Parameters.AddWithValue("@dataAdmissao", modelo.dataAdmissao);
                sql.Parameters.AddWithValue("@dataDemissao", modelo.dataDemissao);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public bool delete(Enfermeiro modelo)
        {
            if (this.search("id", modelo.id).Count() > 0)
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "update enfermeiro set dataDemissao = @demissao where id = @id";
                sql.Parameters.AddWithValue("@demissao", modelo.dataDemissao);
                sql.Parameters.AddWithValue("@id", modelo.id);
                return sql.ExecuteNonQuery() == 1;
            }
            else
            {
                return false;
            }
        }

        public bool update(Enfermeiro modelo)
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
                sql.CommandText = "update enfermeiro set nome = @nome, nascimento = @nascimento, sexo = @sexo, login = @login, senha = @senha, dataAdmissao = @dataAdmissao, dataDemissao = @dataDemissao where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@nome", modelo.nome);
                sql.Parameters.AddWithValue("@nascimento", modelo.nascimento);
                sql.Parameters.AddWithValue("@sexo", modelo.sexo);
                sql.Parameters.AddWithValue("@login", modelo.login);
                sql.Parameters.AddWithValue("@senha", modelo.senha);
                sql.Parameters.AddWithValue("@dataAdmissao", modelo.dataAdmissao);
                sql.Parameters.AddWithValue("@dataDemissao", modelo.dataDemissao);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public List<Enfermeiro> all()
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from enfermeiro", conex);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Enfermeiro> enfermeiros = new List<Enfermeiro>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string nome = data.Rows[x]["nome"].ToString();
                DateTime nascimento = Convert.ToDateTime(data.Rows[x]["nascimento"]);
                string sexo = data.Rows[x]["sexo"].ToString();
                string login = data.Rows[x]["login"].ToString();
                string senha = data.Rows[x]["senha"].ToString();
                bool stt = Convert.ToBoolean(data.Rows[x]["stt"].ToString());
                DateTime dataAdmissao = Convert.ToDateTime(data.Rows[x]["dataAdmissao"]);
                DateTime dataDemissao = Convert.ToDateTime(null);
                try
                {
                    dataDemissao = Convert.ToDateTime(data.Rows[x]["dataDemissao"]);
                }
                catch (Exception) { }

                Enfermeiro enfermeiro = new Enfermeiro(id, nome, nascimento, sexo, login, senha, stt, dataAdmissao, dataDemissao);
                enfermeiros.Add(enfermeiro);
            }
            return enfermeiros;
        }

        public List<Enfermeiro> search<T>(string campo, T valor)
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from enfermeiro where " + campo + " = @value", conex);
            sql.Parameters.AddWithValue("@value", valor);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Enfermeiro> enfermeiros = new List<Enfermeiro>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string nome = data.Rows[x]["nome"].ToString();
                DateTime nascimento = Convert.ToDateTime(data.Rows[x]["nascimento"]);
                string sexo = data.Rows[x]["sexo"].ToString();
                string login = data.Rows[x]["login"].ToString();
                string senha = data.Rows[x]["senha"].ToString();
                bool stt = Convert.ToBoolean(data.Rows[x]["stt"].ToString());
                DateTime dataAdmissao = Convert.ToDateTime(data.Rows[x]["dataAdmissao"]);
                DateTime dataDemissao = Convert.ToDateTime(null);
                try
                {
                    dataDemissao = Convert.ToDateTime(data.Rows[x]["dataDemissao"]);
                }
                catch(Exception) {}

                Enfermeiro enfermeiro = new Enfermeiro(id, nome, nascimento, sexo, login, senha, stt, dataAdmissao, dataDemissao);
                enfermeiros.Add(enfermeiro);
            }
            return enfermeiros;
        }

        public Enfermeiro login(string login, string senha)
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from enfermeiro where login = @login and senha = @senha", conex);
            sql.Parameters.AddWithValue("@login", login);
            sql.Parameters.AddWithValue("@senha", senha);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Enfermeiro> enfermeiros = new List<Enfermeiro>();

            if(data.Rows.Count > 0)
            {
                string id = data.Rows[0]["id"].ToString();
                string nome = data.Rows[0]["nome"].ToString();
                DateTime nascimento = Convert.ToDateTime(data.Rows[0]["nascimento"]);
                string sexo = data.Rows[0]["sexo"].ToString();
                bool stt = Convert.ToBoolean(data.Rows[0]["stt"].ToString());
                DateTime dataAdmissao = Convert.ToDateTime(data.Rows[0]["dataAdmissao"]);
                DateTime dataDemissao = Convert.ToDateTime(null);
                try
                {
                    dataDemissao = Convert.ToDateTime(data.Rows[0]["dataDemissao"]);
                }
                catch(Exception) {}

                Enfermeiro enf = new Enfermeiro(id, nome, nascimento, sexo, login, senha, stt, dataAdmissao, dataDemissao);
                return enf;
            }
            else
            {
                return null;
            }
        }
    }
}