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

namespace Projeto.View.Problema
{
    public partial class AtualizacaoProblema : Form
    {
        public AtualizacaoProblema()
        {
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Grid.grid(dataGridView1, "select id as 'Id', stt as 'Status', nome as 'Nome', descricao as 'Descrição' from problema");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string nome = "";
            string descricao = "";

            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Preencha algum campo para pesquisa!");
                Grid.grid(dataGridView1, "select id as 'Id', stt as 'Status', nome as 'Nome', descricao as 'Descrição' from problema");
            }
            else
            {
                nome = textBox1.Text;
                descricao = textBox2.Text;

                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "select id as 'Id', stt as 'Status', nome as 'Nome', descricao as 'Descrição' from problema where nome = @nome or descricao like concat('%',@descricao,'%')";
                sql.Parameters.AddWithValue("@nome", nome);
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != "")
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "False")
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Nome"].Style.ForeColor = Color.Blue;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Nome"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn coluna = dataGridView1.Columns[0];
            coluna.Visible = false;
            coluna = dataGridView1.Columns[1];
            coluna.Visible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int i = dataGridView1.SelectedRows[0].Index;
                textBox1.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    string id = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString();
                    string nome = textBox1.Text;
                    string descricao = textBox2.Text;

                    ProblemaController pc = new ProblemaController();
                    Model.Problema prob = pc.search("id", id)[0];
                    prob.nome = nome;
                    prob.descricao = descricao;

                    try
                    {
                        if (pc.update(prob))
                        {
                            MessageBox.Show("Problema atualizado com sucesso!");
                            Limpar.limpar(this);

                            MySqlCommand sql = new MySqlCommand();
                            sql.CommandText = "select id as 'Id', stt as 'Status', nome as 'Nome', descricao as 'Descrição' from problema where id = @id or descricao like concat('%',@descricao,'%')";
                            sql.Parameters.AddWithValue("@id", id);
                            sql.Parameters.AddWithValue("@descricao", descricao);
                            Grid.grid(dataGridView1, sql);
                        }
                        else
                        {
                            MessageBox.Show("Problema não pôde ser atualizado!");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Preencha tudo corretamente!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Selecione um problema para ser atualizado!");
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!");
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }
    }
}