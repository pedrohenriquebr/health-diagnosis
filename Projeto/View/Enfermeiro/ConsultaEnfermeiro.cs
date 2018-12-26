using MySql.Data.MySqlClient;
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
    public partial class ConsultaEnfermeiro : Form
    {
        public ConsultaEnfermeiro()
        {
            InitializeComponent();
            MySqlCommand sql = new MySqlCommand();
            sql.CommandText = "select id as 'Id', nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (dataDemissao is null or dataDemissao = @demissao)";
            sql.Parameters.AddWithValue("@demissao", Convert.ToDateTime(null));
            Grid.grid(dataGridView1, sql);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string nome = "";
            string sexo = "";
            string login = "";
            DateTime dataNascimento = Convert.ToDateTime(null);
            DateTime dataAdmissao = Convert.ToDateTime(null);

            if (textBox1.Text == "" && textBox2.Text == "  /  /" && textBox3.Text == "  /  /" && comboBox1.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Preencha algum campo para pesquisa!");
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "select id as 'Id', nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (dataDemissao is null or dataDemissao = @demissao)";
                sql.Parameters.AddWithValue("@demissao", Convert.ToDateTime(null));
                Grid.grid(dataGridView1, sql);
            }
            else
            {
                nome = textBox1.Text;
                login = textBox4.Text;
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
                    dataAdmissao = new DateTime(Convert.ToInt32(entr[2]), Convert.ToInt32(entr[1]), Convert.ToInt32(entr[0]));
                }
                catch (Exception) { }

                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento, '%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao, '%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (nome = @nome or sexo = @sexo or nascimento = @nascimento or dataAdmissao = @admissao or login = @login) and (dataDemissao is null or dataDemissao = @demissao)";
                sql.Parameters.AddWithValue("@nome", nome);
                sql.Parameters.AddWithValue("@nascimento", dataNascimento);
                sql.Parameters.AddWithValue("@sexo", sexo);
                sql.Parameters.AddWithValue("@admissao", dataAdmissao);
                sql.Parameters.AddWithValue("@login", login);
                sql.Parameters.AddWithValue("@demissao", Convert.ToDateTime(null));
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
            Limpar.limpar(this);
        }
    }
}