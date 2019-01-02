using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.View.HistoricoMedico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.View.Paciente
{
    public partial class ConsultaPaciente : Form
    {
        List<Model.Consulta> historicoConsulta;

        public ConsultaPaciente()
        {
            InitializeComponent();
            MySqlCommand sql = new MySqlCommand();
            sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento, '%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada, '%d/%m/%Y') as 'Entrada' from paciente where (dataSaida is null or dataSaida = @saida)";
            sql.Parameters.AddWithValue("@saida", Convert.ToDateTime(null));
            Grid.grid(dataGridView1, sql);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            btnHistorico.Enabled = false;
            string nome = "";
            string sexo = "";
            DateTime dataNascimento = Convert.ToDateTime(null);
            DateTime dataEntrada = Convert.ToDateTime(null);

            if (textBox1.Text == "" && textBox2.Text == "  /  /" && textBox3.Text == "  /  /" && comboBox1.Text == "")
            {
                MessageBox.Show("Preencha algum campo para pesquisa!");
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento, '%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada, '%d/%m/%Y') as 'Entrada' from paciente where (dataSaida is null or dataSaida = @saida)";
                sql.Parameters.AddWithValue("@saida", Convert.ToDateTime(null));
                Grid.grid(dataGridView1, sql);
            }
            else
            {
                nome = textBox1.Text;
                try
                {
                    sexo = comboBox1.Text[0].ToString();
                }
                catch (Exception) { }
                try
                {
                    string[] nasc = textBox2.Text.Split('/');
                    dataNascimento = new DateTime(Convert.ToInt32(nasc[2]), Convert.ToInt32(nasc[1]), Convert.ToInt32(nasc[0]));
                }
                catch (Exception) { }
                try
                {
                    string[] entr = textBox3.Text.Split('/');
                    dataEntrada = new DateTime(Convert.ToInt32(entr[2]), Convert.ToInt32(entr[1]), Convert.ToInt32(entr[0]));
                }
                catch (Exception) { }

                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento, '%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada, '%d/%m/%Y') as 'Entrada' from paciente where (nome = @nome or sexo = @sexo or nascimento = @nascimento or dataEntrada = @entrada) and (dataSaida is null or dataSaida = @saida)";
                sql.Parameters.AddWithValue("@nome", nome);
                sql.Parameters.AddWithValue("@nascimento", dataNascimento);
                sql.Parameters.AddWithValue("@sexo", sexo);
                sql.Parameters.AddWithValue("@entrada", dataEntrada);
                sql.Parameters.AddWithValue("@saida", Convert.ToDateTime(null));
                Grid.grid(dataGridView1, sql);
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn coluna = dataGridView1.Columns[0];
            coluna.Visible = false;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            btnHistorico.Enabled = false;
            Limpar.limpar(this);
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            ConsultaController cc = new ConsultaController();
            historicoConsulta = cc.search("paciente", dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString());
            if (historicoConsulta.Count > 0)
            {
                if (Application.OpenForms.OfType<GraficoGeral>().Count() <= 0)
                {
                    GraficoGeral gg = new GraficoGeral(historicoConsulta);
                    gg.Show();
                }
                else
                {
                    MessageBox.Show("Formulário já está aberto!");
                }
                if (Application.OpenForms.OfType<GraficoPressao>().Count() <= 0)
                {
                    GraficoPressao gp = new GraficoPressao(historicoConsulta);
                    gp.Show();
                }
                else
                {
                    MessageBox.Show("Formulário já está aberto!");
                }
            }
            else
            {
                MessageBox.Show("Paciente não possui dados em seu histórico!");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int i = dataGridView1.SelectedRows[0].Index;
                ConsultaController cc = new ConsultaController();
                historicoConsulta = cc.search("id", dataGridView1.Rows[i].Cells[0].Value.ToString());
                if (historicoConsulta.Count > 0)
                {
                    btnHistorico.Enabled = true;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnHistorico.Enabled = false;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnHistorico.Enabled = true;
        }
    }
}