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
    class ConsultaDao
    {
        public bool add(Consulta modelo)
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
                sql.CommandText = "insert into consulta (id, enfermeiro, paciente, datahora, massacorporal, circabdominal, altura, jejum, glicemia, pressaoarterial, respiracao, temperatura, batimentocardio) values (@id, @enfermeiro, @paciente, @datahora, @massacorporal, @circabdominal, @altura, @jejum, @glicemia, @pressaoarterial, @respiracao, @temperatura, @batimentocardio)";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@enfermeiro", modelo.enfermeiro);
                sql.Parameters.AddWithValue("@paciente", modelo.paciente);
                sql.Parameters.AddWithValue("@datahora", modelo.datahora);
                sql.Parameters.AddWithValue("@massacorporal", modelo.massaCorporal);
                sql.Parameters.AddWithValue("@circabdominal", modelo.circAbdominal);
                sql.Parameters.AddWithValue("@altura", modelo.altura);
                sql.Parameters.AddWithValue("@jejum", modelo.jejum);
                sql.Parameters.AddWithValue("@glicemia", modelo.glicemia);
                sql.Parameters.AddWithValue("@pressaoarterial", modelo.pressaoArterial);
                sql.Parameters.AddWithValue("@respiracao", modelo.respiracao);
                sql.Parameters.AddWithValue("@temperatura", modelo.temperatura);
                sql.Parameters.AddWithValue("@batimentocardio", modelo.batimentoCardio);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public bool delete(Consulta modelo)
        {
            if (this.search("id", modelo.id).Count() > 0)
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "delete from consulta where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                return sql.ExecuteNonQuery() == 1;
            }
            else
            {
                return false;
            }        
        }

        public bool update(Consulta modelo)
        {
            if (this.search("id", modelo.id).Count() > 0)
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "update consulta set enfermeiro = @enfermeiro, paciente = @paciente, datahora = @datahora, massacorporal = @massacorporal, circabdominal = @circabdominal, altura = @altura, jejum = @jejum, glicemia = @glicemia, pressaoarterial = @pressaoarterial, respiracao = @respiracao, temperatura = @temperatura, batimentocardio = @batimentocardio where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@enfermeiro", modelo.enfermeiro);
                sql.Parameters.AddWithValue("@paciente", modelo.paciente);
                sql.Parameters.AddWithValue("@datahora", modelo.datahora);
                sql.Parameters.AddWithValue("@massacorporal", modelo.massaCorporal);
                sql.Parameters.AddWithValue("@circabdominal", modelo.circAbdominal);
                sql.Parameters.AddWithValue("@altura", modelo.altura);
                sql.Parameters.AddWithValue("@jejum", modelo.jejum);
                sql.Parameters.AddWithValue("@glicemia", modelo.glicemia);
                sql.Parameters.AddWithValue("@pressaoarterial", modelo.pressaoArterial);
                sql.Parameters.AddWithValue("@respiracao", modelo.respiracao);
                sql.Parameters.AddWithValue("@temperatura", modelo.temperatura);
                sql.Parameters.AddWithValue("@batimentocardio", modelo.batimentoCardio);
                return sql.ExecuteNonQuery() == 1;
            }
            else
            {
                return false;
            }
        }

        public List<Consulta> all()
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from consulta order by datahora", conex);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Consulta> consultas = new List<Consulta>();

            for (int x = 0; x < data.Rows.Count; x++)
            {           
                string id = data.Rows[x]["id"].ToString();
                string enfermeiro = data.Rows[x]["enfermeiro"].ToString();
                string paciente = data.Rows[x]["paciente"].ToString();
                DateTime datahora = Convert.ToDateTime(data.Rows[x]["datahora"]);
                double massaCorporal = Convert.ToDouble(data.Rows[x]["massacorporal"]);
                double circAbdominal = Convert.ToDouble(data.Rows[x]["circabdominal"]);
                double altura = Convert.ToDouble(data.Rows[x]["altura"]);
                bool jejum = Convert.ToBoolean(data.Rows[x]["jejum"]);
                double glicemia = Convert.ToDouble(data.Rows[x]["glicemia"]);
                string pressaoArterial = data.Rows[x]["pressaoarterial"].ToString();
                int respiracao = Convert.ToInt32(data.Rows[x]["respiracao"]);
                double temperatura = Convert.ToDouble(data.Rows[x]["temperatura"]);
                int batimentoCardio = Convert.ToInt32(data.Rows[x]["batimentocardio"]);
                Consulta b = new Consulta(id, enfermeiro, paciente, datahora, massaCorporal, circAbdominal, altura, jejum, glicemia, pressaoArterial, respiracao, temperatura, batimentoCardio);
                consultas.Add(b);
            }
            return consultas;
        }

        public List<Consulta> search<T> (string campo, T valor)
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from consulta where " + campo + " = @value order by datahora", conex);
            sql.Parameters.AddWithValue("@value", valor);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Consulta> consultas = new List<Consulta>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string enfermeiro = data.Rows[x]["enfermeiro"].ToString();
                string paciente = data.Rows[x]["paciente"].ToString();
                DateTime datahora = Convert.ToDateTime(data.Rows[x]["datahora"]);
                double massaCorporal = Convert.ToDouble(data.Rows[x]["massacorporal"]);
                double circAbdominal = Convert.ToDouble(data.Rows[x]["circabdominal"]);
                double altura = Convert.ToDouble(data.Rows[x]["altura"]);
                bool jejum = Convert.ToBoolean(data.Rows[x]["jejum"]);
                double glicemia = Convert.ToDouble(data.Rows[x]["glicemia"]);
                string pressaoArterial = data.Rows[x]["pressaoarterial"].ToString();
                int respiracao = Convert.ToInt32(data.Rows[x]["respiracao"]);
                double temperatura = Convert.ToDouble(data.Rows[x]["temperatura"]);
                int batimentoCardio = Convert.ToInt32(data.Rows[x]["batimentocardio"]);
                Consulta b = new Consulta(id, enfermeiro, paciente, datahora, massaCorporal, circAbdominal, altura, jejum, glicemia, pressaoArterial, respiracao, temperatura, batimentoCardio);
                consultas.Add(b);
            }
            return consultas;
        }

        public List<Problema> listaProblema(List<string> ids)
        {
            ProblemaController probC = new ProblemaController();
            List<Problema> problema = new List<Problema>();
            try
            {
                for (int x = 0; x < ids.Count; x++)
                {
                    problema.Add(probC.search("id", ids[x])[0]);
                }
                return problema;
            }
            catch(Exception)
            {
                return null;
            }   
        }
    }
}