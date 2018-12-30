using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using dotenv.net;
namespace Projeto.Model
{
    class ConnectionFactory
    {
        private string server = "localhost";
        private string database_name = "diagnostico_de_saude";
        private string user = "root";
        private string password = "root";
        public MySqlConnection database()
        {
            DotEnv.Config(throwOnError: false, filePath: ".config");
            server = Environment.GetEnvironmentVariable("DB_HOST");
            server = (server ==  null ? "localhost" : server);

            database_name = Environment.GetEnvironmentVariable("DB_NAME");
            database_name = (database_name == null ? "diagnostico_de_saude" : database_name);

            user = Environment.GetEnvironmentVariable("DB_USER");
            user = (user == null ? "root" : user);

            password  = Environment.GetEnvironmentVariable("DB_PASSWORD");
            password = (password == null ? "root" : password);

            MySqlConnection conn = new MySqlConnection(String.Format("server={0}; database={1}; uid={2}; pwd={3};",server,database_name,user,password));
            conn.Open();
            return conn;
        }  
    }
}