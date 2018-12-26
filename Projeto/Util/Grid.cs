using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Projeto.Model;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Projeto
{
    class Grid
    {
        public static void grid(DataGridView dtv, string comando)
        {
            try
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando, conex);
                DataTable datable = new DataTable();
                adapter.Fill(datable);
                dtv.DataSource = datable;
            }
            catch(Exception) {}
        }

        public static void grid(DataGridView dtv, MySqlCommand comando)
        {
            try
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                comando.Connection = conex;
                DataTable datable = new DataTable();
                adapter.Fill(datable);
                dtv.DataSource = datable;
            }
            catch(Exception) {}
        }
    }
}