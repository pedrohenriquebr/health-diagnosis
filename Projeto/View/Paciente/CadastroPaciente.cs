using Projeto.Model;
using Projeto.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projeto.Util;

namespace Projeto.View.Paciente
{
    public partial class CadastroPaciente : Form
    {
        public CadastroPaciente()
        {
            InitializeComponent();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(textBox1.Text == "" || textBox2.Text == "  /  /" || textBox3.Text == "  /  /" || comboBox1.Text == ""))
                {
                    string nome = textBox1.Text;
                    string nasci = textBox2.Text;
                    string[] nascimento = nasci.Split('/');
                    string sexo = comboBox1.Text[0].ToString();
                    string enfermeiro = Session.codigo;
                    string entra = textBox3.Text;
                    string[] entrada = entra.Split('/');
                    PacienteController pc = new PacienteController();
                    DateTime nasc = new DateTime(Convert.ToInt32(nascimento[2]), Convert.ToInt32(nascimento[1]), Convert.ToInt32(nascimento[0]));
                    DateTime entr = new DateTime(Convert.ToInt32(entrada[2]), Convert.ToInt32(entrada[1]), Convert.ToInt32(entrada[0]));
                    string id = Hash.md5(nome+nascimento);
                    enfermeiro = Session.codigo;

                    Projeto.Model.Paciente p = new Projeto.Model.Paciente(id, nome, nasc, sexo, enfermeiro, entr, Convert.ToDateTime(null));
                    if (Data.nascimento(nasc) && pc.add(p))
                    {
                        MessageBox.Show("Paciente cadastrado com sucesso!");
                        Limpar.limpar(this);
                        dataGridView1.DataSource = null;
                        Grid.grid(dataGridView1, "select nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada,'%d/%m/%Y') as 'Entrada' from paciente where id = '" + id + "'");
                    }
                    else
                    {
                        MessageBox.Show("Paciente não pôde ser cadastrado!");
                    }
                }
                else
                {
                    MessageBox.Show("Informe tudo que for necessário!");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Paciente não pôde ser cadastrado!");
            }
        }

        private void btnLimpar_Click_1(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }
    }
}