using MySql.Data.MySqlClient;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Dao
{
    class PacienteDao
    {
        public bool add(Paciente modelo)
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
                sql.CommandText = "insert into paciente (id, nome, nascimento, sexo, enfermeiro, dataEntrada, dataSaida) values (@id, @nome, @nascimento, @sexo, @enfermeiro, @dataEntrada, @dataSaida)";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@nome", modelo.nome);
                sql.Parameters.AddWithValue("@nascimento", modelo.nascimento);
                sql.Parameters.AddWithValue("@sexo", modelo.sexo);
                sql.Parameters.AddWithValue("@enfermeiro", modelo.enfermeiro);
                sql.Parameters.AddWithValue("@dataEntrada", modelo.dataEntrada);
                sql.Parameters.AddWithValue("@dataSaida", modelo.dataSaida);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public bool delete(Paciente modelo)
        {
            if (this.search("id", modelo.id).Count() > 0)
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlCommand sql = new MySqlCommand();
                sql.Connection = conex;
                sql.CommandText = "update paciente set dataSaida = @saida where id = @id";
                sql.Parameters.AddWithValue("@saida", modelo.dataSaida);
                sql.Parameters.AddWithValue("@id", modelo.id);
                return sql.ExecuteNonQuery() == 1;
            }
            else
            {
                return false;
            }
        }

        public bool update(Paciente modelo)
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
                sql.CommandText = "update paciente set nome = @nome, nascimento = @nascimento, sexo = @sexo, enfermeiro = @enfermeiro, dataEntrada = @dataEntrada, dataSaida = @dataSaida where id = @id";
                sql.Parameters.AddWithValue("@id", modelo.id);
                sql.Parameters.AddWithValue("@nome", modelo.nome);
                sql.Parameters.AddWithValue("@nascimento", modelo.nascimento);
                sql.Parameters.AddWithValue("@sexo", modelo.sexo);
                sql.Parameters.AddWithValue("@enfermeiro", modelo.enfermeiro);
                sql.Parameters.AddWithValue("@dataEntrada", modelo.dataEntrada);
                sql.Parameters.AddWithValue("@dataSaida", modelo.dataSaida);
                return sql.ExecuteNonQuery() == 1;
            }
        }

        public List<Paciente> all()
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from paciente", conex);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Paciente> pacientes = new List<Paciente>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string nome = data.Rows[x]["nome"].ToString();
                DateTime nascimento = Convert.ToDateTime(data.Rows[x]["nascimento"]);
                string sexo = data.Rows[x]["sexo"].ToString();
                string enfermeiro = data.Rows[x]["enfermeiro"].ToString();
                DateTime dataEntrada = Convert.ToDateTime(data.Rows[x]["dataEntrada"]);
                DateTime dataSaida = Convert.ToDateTime(null);
                try
                {
                    dataSaida = Convert.ToDateTime(data.Rows[x]["dataSaida"]);
                }
                catch (Exception) { }

                Paciente paciente = new Paciente(id, nome, nascimento, sexo, enfermeiro, dataEntrada, dataSaida);
                pacientes.Add(paciente);
            }
            return pacientes;
        }

        public List<Paciente> search<T>(string campo, T valor)
        {
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            MySqlCommand sql = new MySqlCommand("select * from paciente where " + campo + " = @value", conex);
            sql.Parameters.AddWithValue("@value", valor);
            DataTable data = new DataTable();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql);
            dta.Fill(data);
            List<Paciente> pacientes = new List<Paciente>();

            for (int x = 0; x < data.Rows.Count; x++)
            {
                string id = data.Rows[x]["id"].ToString();
                string nome = data.Rows[x]["nome"].ToString();
                DateTime nascimento = Convert.ToDateTime(data.Rows[x]["nascimento"]);
                string sexo = data.Rows[x]["sexo"].ToString();
                string enfermeiro = data.Rows[x]["enfermeiro"].ToString();
                DateTime dataEntrada = Convert.ToDateTime(data.Rows[x]["dataEntrada"]);
                DateTime dataSaida = Convert.ToDateTime(null);
                try
                {
                    dataSaida = Convert.ToDateTime(data.Rows[x]["dataSaida"]);
                }
                catch (Exception) { }

                Paciente paciente = new Paciente(id, nome, nascimento, sexo, enfermeiro, dataEntrada, dataSaida);
                pacientes.Add(paciente);
            }
            return pacientes;
        }
    }
}