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

namespace Projeto.View.Solucao
{
    public partial class CadastroSolucao : Form
    {
        public CadastroSolucao()
        {
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Combobox.combobox(comboBox1);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == ""))
                { 
                    string nome = textBox1.Text;
                    string descricao = textBox2.Text;
                    string problema = Hash.md5(comboBox1.Text);
                    string id = Hash.md5(nome);

                    SolucaoController solC = new SolucaoController();
                    Model.Solucao sol = new Model.Solucao(id, nome, descricao);
                    string msg = "";
                    if (solC.add(sol))
                    {
                        msg += "Solução cadastrada com sucesso";
                        Limpar.limpar(this);
                        dataGridView1.DataSource = null;
                        Grid.grid(dataGridView1, "select id as 'Id', nome as 'Nome', descricao as 'Descrição' from solucao where id = '" + id + "'");
                        if (solC.problemaSolucao(problema, id))
                        {
                            MessageBox.Show(msg + " e devidamente associada ao problema!");
                        }
                        else
                        {
                            MessageBox.Show(msg + ", mas não devidamente associada ao problema!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Solução não pôde ser cadastrada!");
                    }
                }
                else
                {
                    MessageBox.Show("Informe tudo que for necessário!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Solução não pôde ser cadastrada!");
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn coluna = dataGridView1.Columns[0];
            coluna.Visible = false;
        }
    }
}