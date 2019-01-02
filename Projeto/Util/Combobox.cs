using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Projeto.Model;

namespace Projeto
{
    class Combobox
    {
        public static void combobox(ComboBox c)
        {
            try
            {
                ConnectionFactory cf = new ConnectionFactory();
                MySqlConnection conex = cf.database();
                MySqlDataAdapter da = new MySqlDataAdapter("select nome from problema order by nome", conex);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    c.Items.Add(dt.Rows[i]["nome"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro no preenchimento dos dados!");
            }
        }

        public static void combobox(ComboBox c, MySqlCommand sql)
        {
            c.Items.Clear();
            ConnectionFactory cf = new ConnectionFactory();
            MySqlConnection conex = cf.database();
            sql.Connection = conex;
            MySqlDataAdapter da = new MySqlDataAdapter(sql);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c.Items.Add(dt.Rows[i]["id"]);
            }
        }

        public static void combobox(ComboBox c, List<Model.Problema> problemas)
        {
            c.Items.Clear();
            foreach (Model.Problema prob in problemas)
            {
                c.Items.Add(prob.id);
            }
        }
    }
}