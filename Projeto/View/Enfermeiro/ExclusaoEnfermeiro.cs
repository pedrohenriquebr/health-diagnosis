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

namespace Projeto.View.Enfermeiro
{
    public partial class Enfermeiro : Form
    {
        public Enfermeiro()
        {
            InitializeComponent();
            MySqlCommand sql = new MySqlCommand();
            if (Session.stt)
            {
                sql.CommandText = "select id as 'Id', nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (dataDemissao is null or dataDemissao = @demissao)";
            }
            else
            {
                sql.CommandText = "select id as 'Id', nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (dataDemissao is null or dataDemissao = @demissao) and stt = false";
            }
            sql.Parameters.AddWithValue("@demissao", Convert.ToDateTime(null));
            Grid.grid(dataGridView1, sql);
        }

        public void habilita()
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            comboBox1.Enabled = true;
        }

        public void desabilita()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            comboBox1.Enabled = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            habilita();
            string nome = "";
            string sexo = "";
            string login = "";
            DateTime nascimento = Convert.ToDateTime(null);
            DateTime dataAdmissao = Convert.ToDateTime(null);

            if (textBox1.Text == "" && textBox2.Text == "  /  /" && textBox3.Text == "  /  /" && textBox4.Text == "" && comboBox1.Text == "")
            {
                MessageBox.Show("Preencha algum campo para pesquisa!");
                MySqlCommand sql = new MySqlCommand();
                if (Session.stt)
                {
                    sql.CommandText = "select id as 'Id', nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (dataDemissao is null or dataDemissao = @demissao)";
                }
                else
                {
                    sql.CommandText = "select id as 'Id', nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (dataDemissao is null or dataDemissao = @demissao) and stt = false";
                }
                sql.Parameters.AddWithValue("@demissao", Convert.ToDateTime(null));
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
                    nascimento = new DateTime(Convert.ToInt32(nasc[2]), Convert.ToInt32(nasc[1]), Convert.ToInt32(nasc[0]));
                }
                catch (Exception) { }
                try
                {
                    string[] entr = textBox3.Text.Split('/');
                    dataAdmissao = new DateTime(Convert.ToInt32(entr[2]), Convert.ToInt32(entr[1]), Convert.ToInt32(entr[0]));
                }
                catch (Exception) { }

                MySqlCommand sql = new MySqlCommand();
                if (Session.stt)
                {
                    sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento, '%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao, '%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (nome = @nome or sexo = @sexo or nascimento = @nascimento or dataAdmissao = @admissao or login = @login) and (dataDemissao is null or dataDemissao = @demissao)";
                }
                else
                {
                    sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento, '%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao, '%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (nome = @nome or sexo = @sexo or nascimento = @nascimento or dataAdmissao = @admissao or login = @login) and (dataDemissao is null or dataDemissao = @demissao) and stt = false";
                }                
                sql.Parameters.AddWithValue("@nome", nome);
                sql.Parameters.AddWithValue("@nascimento", nascimento);
                sql.Parameters.AddWithValue("@sexo", sexo);
                sql.Parameters.AddWithValue("@admissao", dataAdmissao);
                sql.Parameters.AddWithValue("@login", login);
                sql.Parameters.AddWithValue("@demissao", Convert.ToDateTime(null));
                Grid.grid(dataGridView1, sql);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn coluna = dataGridView1.Columns[0];
            coluna.Visible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                desabilita();
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
                textBox4.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            desabilita();
            if (textBox1.Text != "" && textBox2.Text != "  /  /" && textBox3.Text != "  /  /" && comboBox1.Text != "" && textBox4.Text != "" && textBox5.Text != "  /  /")
            {
                try
                {
                    string id = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString();

                    try
                    {
                        string[] dmss = textBox5.Text.Split('/');
                        DateTime demissao = new DateTime(Convert.ToInt32(dmss[2]), Convert.ToInt32(dmss[1]), Convert.ToInt32(dmss[0]));
                        string[] admss = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[4].Value.ToString().Split('/');
                        DateTime admissao = new DateTime(Convert.ToInt32(admss[2]), Convert.ToInt32(admss[1]), Convert.ToInt32(admss[0]));
                        EnfermeiroController ec = new EnfermeiroController();

                        if (Data.saida(admissao, demissao) && ec.delete(id, demissao))
                        {
                            MessageBox.Show("Enfermeiro excluído com sucesso!");
                            MySqlCommand sql = new MySqlCommand();
                            if (Session.stt)
                            {
                                sql.CommandText = "select id as 'Id', nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (dataDemissao is null or dataDemissao = @demissao)";
                            }
                            else
                            {
                                sql.CommandText = "select id as 'Id', nome as 'Nome',  date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataAdmissao,'%d/%m/%Y') as 'Admissão', login as 'Login' from enfermeiro where (dataDemissao is null or dataDemissao = @demissao) and stt = false";
                            }
                            sql.Parameters.AddWithValue("@demissao", Convert.ToDateTime(null));
                            Grid.grid(dataGridView1, sql);
                            Limpar.limpar(this);
                        }
                        else
                        {
                            MessageBox.Show("Enfermeiro não pôde ser excluído!");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Selecione algum enfermeiro para ser excluído em uma data!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Selecione um enfermeiro para ser excluído em uma data!");
                }
            }
            else
            {
                MessageBox.Show("Selecione algum enfermeiro para ser excluído em uma data!");
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }
    }
}