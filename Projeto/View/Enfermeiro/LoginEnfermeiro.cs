using Projeto.Controller;
using Projeto.Dao;
using Projeto.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.View.Enfermeiro
{
    public partial class LoginEnfermeiro : Form
    {
        public LoginEnfermeiro()
        {
            InitializeComponent();
            System.Console.WriteLine(Directory.GetCurrentDirectory());
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                EnfermeiroController ec = new EnfermeiroController();
                string login = textBox1.Text;
                string senha = Hash.md5(textBox2.Text);
                Model.Enfermeiro enf = ec.login(login, senha);
                if (enf is null)
                {
                    MessageBox.Show("Enfermeiro não encontrado!");
                }
                else
                {
                    MessageBox.Show("Login efetuado com sucesso!");
                    Session.codigo = enf.id;
                    Session.senha = enf.senha;
                    Session.stt = enf.stt;
                    Session.nome = enf.nome;
                    Hide();
                    MenuEnfermeiro form = new MenuEnfermeiro();
                    form.Show();
                }
            }
            else
            {
                MessageBox.Show("Preencha tudo corretamente!");
            }
        }
    }
}