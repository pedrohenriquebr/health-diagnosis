using Projeto.Controller;
using Projeto.Model;
using Projeto.Util;
using Projeto.View.Enfermeiro;
using Projeto.View.Paciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using dotenv.net;

namespace Projeto
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DotEnv.Config(throwOnError: false);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Projeto.View.Enfermeiro.LoginEnfermeiro());
        }
    }
}