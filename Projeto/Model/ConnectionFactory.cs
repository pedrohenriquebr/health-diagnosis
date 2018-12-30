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
        private string server = Environment.GetEnvironmentVariable("DB_HOST");
        private string database_name = Environment.GetEnvironmentVariable("DB_NAME");
        private string user = Environment.GetEnvironmentVariable("DB_USER");
        private string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        public MySqlConnection database()
        {
            server = (server ==  null ? "localhost" : server);
            System.Console.WriteLine("Variável server carregada:" + server);
            database_name = (database_name == null ? "diagnostico_de_saude" : database_name);
            user = (user == null ? "root" : user);
            password = (password == null ? "root" : password);

            MySqlConnection conn = new MySqlConnection(String.Format("server={0}; database={1}; uid={2}; pwd={3};",server,database_name,user,password));
            conn.Open();
            return conn;
        }  
    }
}