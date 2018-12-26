using MySql.Data.MySqlClient;
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
    public partial class ConsultaProblema : Form
    {
        public ConsultaProblema()
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

        private void btnConsultar_Click(object sender, EventArgs e)
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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn coluna = dataGridView1.Columns[0];
            coluna.Visible = false;
            coluna = dataGridView1.Columns[1];
            coluna.Visible = false;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != "")
            {
                if(dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "False")
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Nome"].Style.ForeColor = Color.Blue;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Nome"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }
    }
}