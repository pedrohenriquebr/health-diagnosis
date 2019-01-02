using Projeto.Controller;
using Projeto.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.View.Enfermeiro
{
    public partial class SenhaEnfermeiro : Form
    {
        bool stt = false;

        public SenhaEnfermeiro()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string senha = textBox1.Text;
            if (Hash.md5(senha) == Session.senha)
            {
                textBox1.ReadOnly = true;
                stt = true;
                button2.Enabled = true;
                MessageBox.Show("Senha verificada. Pode prosseguir!");
            }
            else
            {
                stt = false;
                MessageBox.Show("Senha incorreta!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (stt)
            {
                string s1 = textBox2.Text;
                string s2 = textBox3.Text;

                if (s1 == s2)
                {
                    if (s1.Length > 5)
                    {
                        EnfermeiroController ec = new EnfermeiroController();
                        Model.Enfermeiro enf = ec.search("id", Session.codigo)[0];
                        enf.senha = Hash.md5(s1);
                        if (ec.update(enf))
                        {
                            MessageBox.Show("Senha atualizada com sucesso!");
                        }
                        else
                        {
                            MessageBox.Show("Senha não pôde ser atualizada!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Senha deve ter pelo menos 6 dígitos!");
                    }
                }
                else
                {
                    MessageBox.Show("Novas senhas não batem!");
                }
            }
            else
            {
                MessageBox.Show("Confirme sua senha atual!");
            }
        }
    }
}