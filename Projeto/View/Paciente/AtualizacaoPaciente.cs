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

namespace Projeto.View.Paciente
{
    public partial class AtualizacaoPaciente : Form
    {
        public AtualizacaoPaciente()
        {
            InitializeComponent();
            MySqlCommand sql = new MySqlCommand();
            sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada,'%d/%m/%Y') as 'Entrada' from paciente where (dataSaida is null or dataSaida = @saida)";
            sql.Parameters.AddWithValue("@saida", Convert.ToDateTime(null));
            Grid.grid(dataGridView1, sql);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "  /  /" && textBox3.Text != "  /  /" && comboBox1.Text != "")
            {
                try
                {
                    string id = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString();
                    string sexo = comboBox1.Text[0].ToString();
                    PacienteController pc = new PacienteController();
                    Model.Paciente p = pc.search("id", id)[0];
                    p.nome = textBox1.Text;
                    p.sexo = sexo;

                    try
                    {
                        string[] nasc = textBox2.Text.Split('/');
                        DateTime dataNascimento = new DateTime(Convert.ToInt32(nasc[2]), Convert.ToInt32(nasc[1]), Convert.ToInt32(nasc[0]));
                        string[] entr = textBox3.Text.Split('/');
                        DateTime dataEntrada = new DateTime(Convert.ToInt32(entr[2]), Convert.ToInt32(entr[1]), Convert.ToInt32(entr[0]));
                        p.nascimento = dataNascimento;
                        p.dataEntrada = dataEntrada;

                        if(Data.nascimento(dataNascimento) && pc.update(p))
                        {
                            MessageBox.Show("Paciente atualizado com sucesso!");
                            Limpar.limpar(this);
                            MySqlCommand sql = new MySqlCommand();
                            sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada,'%d/%m/%Y') as 'Entrada' from paciente where (dataSaida is null or dataSaida = @saida) and id = @id";
                            sql.Parameters.AddWithValue("@id", p.id);
                            sql.Parameters.AddWithValue("@saida", Convert.ToDateTime(null));
                            Grid.grid(dataGridView1, sql);
                        }
                        else
                        {
                            MessageBox.Show("Paciente não pôde ser atualizado!");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Preencha tudo corretamente!");
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Selecione um paciente para ser atualizado!");
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int i = dataGridView1.SelectedRows[0].Index;
                textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "F")
                {
                    comboBox1.SelectedItem = "Feminino";
                }
                else if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "M")
                {
                    comboBox1.SelectedItem = "Masculino";
                }
                textBox3.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn coluna = dataGridView1.Columns[0];
            coluna.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = "";
            string sexo = "";
            DateTime dataNascimento = Convert.ToDateTime(null);
            DateTime dataEntrada = Convert.ToDateTime(null);

            if (textBox1.Text == "" && textBox2.Text == "  /  /" && textBox3.Text == "  /  /" && comboBox1.Text == "")
            {
                MessageBox.Show("Preencha algum campo para pesquisa!");
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada,'%d/%m/%Y') as 'Entrada' from paciente where (dataSaida is null or dataSaida = @saida)";
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
                catch(Exception) {}
                try
                {
                    string[] nasc = textBox2.Text.Split('/');
                    dataNascimento = new DateTime(Convert.ToInt32(nasc[2]), Convert.ToInt32(nasc[1]), Convert.ToInt32(nasc[0]));
                }
                catch(Exception) {}
                try
                {
                    string[] entr = textBox3.Text.Split('/');
                    dataEntrada = new DateTime(Convert.ToInt32(entr[2]), Convert.ToInt32(entr[1]), Convert.ToInt32(entr[0]));
                }
                catch (Exception) {}

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

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }
    }
}