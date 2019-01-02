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
    public partial class CadastroEnfermeiro : Form
    {
        public CadastroEnfermeiro()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(textBox1.Text == "" || textBox2.Text == "  /  /" || textBox3.Text == "  /  /" || comboBox1.Text == "" || textBox4.Text == "" || textBox5.Text == ""))
                {
                    string nome = textBox1.Text;
                    string nasci = textBox2.Text;
                    string[] nascimento = nasci.Split('/');
                    string sexo = comboBox1.Text[0].ToString();
                    string entra = textBox3.Text;
                    string[] entrada = entra.Split('/');
                    EnfermeiroController ec = new EnfermeiroController();
                    DateTime nasc = new DateTime(Convert.ToInt32(nascimento[2]), Convert.ToInt32(nascimento[1]), Convert.ToInt32(nascimento[0]));
                    DateTime admss = new DateTime(Convert.ToInt32(entrada[2]), Convert.ToInt32(entrada[1]), Convert.ToInt32(entrada[0]));
                    string login = textBox4.Text;
                    string senha = Hash.md5(textBox5.Text);
                    string id = Hash.md5(login);

                    Model.Enfermeiro enf = new Model.Enfermeiro(id, nome, nasc, sexo, login, senha, false, admss, Convert.ToDateTime(null));
                    if (Data.nascimento(nasc) && ec.add(enf))
                    {
                        MessageBox.Show("Enfermeiro cadastrado com sucesso!");
                        Limpar.limpar(this);
                        dataGridView1.DataSource = null;
                        Grid.grid(dataGridView1, "select nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where id = '" + id + "'");
                    }
                    else
                    {
                        MessageBox.Show("Enfermeiro não pôde ser cadastrado!");
                    }
                }
                else
                {
                    MessageBox.Show("Informe tudo que for necessário!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Enfermeiro não pôde ser cadastrado!");
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }
    }
}