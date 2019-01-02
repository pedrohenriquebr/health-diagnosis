using MySql.Data.MySqlClient;
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
    public partial class ExclusaoSolucao : Form
    {
        public ExclusaoSolucao()
        {
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Grid.grid(dataGridView1, "select id as 'Id', nome as 'Nome', descricao as 'Descrição' from solucao");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;

            string nome = "";
            string descricao = "";

            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Preencha algum campo para pesquisa!");
                Grid.grid(dataGridView1, "select id as 'Id', nome as 'Nome', descricao as 'Descrição' from solucao");
            }
            else
            {
                nome = textBox1.Text;
                descricao = textBox2.Text;

                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "select id as 'Id', nome as 'Nome', descricao as 'Descrição' from solucao where nome like concat('%',@nome,'%') or descricao like concat('%',@descricao,'%')";
                if (nome != "")
                {
                    sql.Parameters.AddWithValue("@nome", nome);
                }
                else
                {
                    sql.Parameters.AddWithValue("@nome", Hash.md5(nome));
                }
                if (descricao != "")
                {
                    sql.Parameters.AddWithValue("@descricao", descricao);
                }
                else
                {
                    sql.Parameters.AddWithValue("@descricao", Hash.md5(descricao));
                }
                Grid.grid(dataGridView1, sql);
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn coluna = dataGridView1.Columns[0];
            coluna.Visible = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            if (textBox1.Text != "")
            {
                try
                {
                    string id = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString();

                    try
                    {
                        SolucaoController pc = new SolucaoController();

                        if (pc.delete(id))
                        {
                            MessageBox.Show("Solução excluída com sucesso!");
                            Grid.grid(dataGridView1, "select id as 'Id', nome as 'Nome', descricao as 'Descrição' from solucao");
                            Limpar.limpar(this);
                        }
                        else
                        {
                            MessageBox.Show("Solução não pôde ser excluída!");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Selecione alguma solução para ser excluída!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Selecione uma solução para ser excluída!");
                }
            }
            else
            {
                MessageBox.Show("Selecione alguma solução para ser excluída!");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                int i = dataGridView1.SelectedRows[0].Index;
                textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            }
        }
    }
}