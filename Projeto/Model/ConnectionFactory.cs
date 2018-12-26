using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Projeto.Model
{
    class ConnectionFactory
    {
        public MySqlConnection database()
        {
            MySqlConnection conex = new MySqlConnection("server=localhost; database=diagnostico_de_saude; uid=root; pwd=root;");
            conex.Open();
            return conex;
        }  
    }
}