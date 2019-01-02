using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Model;
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
using System.IO;

namespace Projeto.View.Consulta
{
    public partial class RealizacaoConsulta : Form
    {
        public RealizacaoConsulta()
        {

            InitializeComponent();
            MySqlCommand sql = new MySqlCommand();
            sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada,'%d/%m/%Y') as 'Entrada' from paciente where (dataSaida is null or dataSaida = @saida)";
            sql.Parameters.AddWithValue("@saida", Convert.ToDateTime(null));
            Combobox.combobox(comboBox2, sql);
        }

        public void allowOnlyNumbers(TextBox tb, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.KeyChar = ',';

                if (tb.Text.Contains(","))
                {
                    e.Handled = true;
                }
            }
            else if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            DateTime datahora = Convert.ToDateTime(null);
            string enfermeiro = Session.codigo;
            string paciente = "";

            try
            {
                if (comboBox2.Text != "")
                {
                    paciente = comboBox2.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show("Selecione um paciente!");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione um paciente!");
                return;
            }

            double massaCorporal = 0;
            double circAbdominal = 0;
            double altura = 0;
            bool jejum = false;
            double glicemia = 0;
            string pressaoArterial = "0/0";
            int respiracao = 0;
            double temperatura = 0;
            int batimentos = 0;

            if (maskedTextBox1.Text != "  /  /" && maskedTextBox2.Text != "  :")
            {
                try
                {
                    string[] dt = maskedTextBox1.Text.Split('/');
                    string[] hr = maskedTextBox2.Text.Split(':');
                    datahora = new DateTime(Convert.ToInt32(dt[2]), Convert.ToInt32(dt[1]), Convert.ToInt32(dt[0]), Convert.ToInt32(hr[0]), Convert.ToInt32(hr[1]), 0);
                    if (Data.consulta(datahora))
                    {
                        string id = Hash.md5(enfermeiro + paciente + datahora);
                        if (textBox1.Text != "")
                        {
                            if (Double.Parse(textBox1.Text) > 600)
                            {
                                MessageBox.Show("Massa corporal inválida!");
                                return;
                            }
                            else
                            {
                                massaCorporal = Double.Parse(textBox1.Text);
                            }
                        }
                        if (textBox2.Text != "")
                        {
                            circAbdominal = Double.Parse(textBox2.Text);
                        }
                        if (textBox3.Text != " ,")
                        {
                            if (textBox3.MaskCompleted)
                            {
                                if (Double.Parse(textBox3.Text) >= 3)
                                {
                                    MessageBox.Show("Altura inválida!");
                                    return;
                                }
                                else
                                {
                                    altura = Double.Parse(textBox3.Text);
                                }       
                            }
                            else
                            {
                                MessageBox.Show("Altura inválida!");
                                return;
                            }
                        }
                        if (textBox4.Text != "")
                        {
                            glicemia = Double.Parse(textBox4.Text);
                        }
                        if (textBox5.Text != "   /")
                        {
                            if (textBox5.MaskCompleted)
                            {
                                pressaoArterial = textBox5.Text;
                            }
                            else
                            {
                                MessageBox.Show("Pressão Arterial inválida!");
                                return;
                            }
                        }
                        if (textBox6.Text != "")
                        {
                            respiracao = Convert.ToInt32(textBox6.Text);
                        }
                        if (textBox7.Text != "  ,")
                        {
                            if (textBox7.MaskCompleted)
                            {
                                temperatura = Double.Parse(textBox7.Text);
                            }
                            else
                            {
                                MessageBox.Show("Temperatura inválida!");
                                return;
                            }
                            
                        }
                        if (textBox8.Text != "")
                        {
                            if (textBox8.MaskCompleted)
                            {
                                batimentos = Convert.ToInt32(textBox8.Text);
                            }
                            else
                            {
                                MessageBox.Show("Pulso inválido!");
                                return;
                            }
                        }
                        if (checkBox1.CheckState.ToString() != "Indeterminate")
                        {
                            jejum = checkBox1.Checked;
                        }

                        Model.Consulta con = new Model.Consulta(id, enfermeiro, paciente, datahora, massaCorporal, circAbdominal, altura, jejum, glicemia, pressaoArterial, respiracao, temperatura, batimentos);
                        PacienteController pc = new PacienteController();

                        Model.Paciente pac = pc.search("id", comboBox2.SelectedItem.ToString())[0];

                        Relatorio.EdicaoRelatorio form = new Relatorio.EdicaoRelatorio(con, pac.nome);
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("É necessário o registro de uma data e de um horário válidos!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Todos os dados precisam ser válidos!");
                }
            }
            else
            {
                MessageBox.Show("É necessário o registro de uma data e de um horário válidos!");
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox5.Text[0].ToString()) >= 3)
                {
                    textBox5.Mask = "00/000";
                    if (Convert.ToInt32(textBox5.Text[3].ToString()) >= 3)
                    {
                        textBox5.Mask = "00/00";
                    }
                    else
                    {
                        textBox5.Mask = "00/000";
                    }
                }
                else
                {
                    textBox5.Mask = "000/000";
                    if (Convert.ToInt32(textBox5.Text[4].ToString()) >= 3)
                    {
                        textBox5.Mask = "000/00";
                    }
                    else
                    {
                        textBox5.Mask = "000/000";
                    }
                }
            }
            catch (Exception) {}
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox8.Text[0].ToString()) >= 3)
                {
                    textBox8.Mask = "00";
                }
                else
                {
                    textBox8.Mask = "000";
                }
            }
            catch(Exception) {}
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar.limpar(this);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            allowOnlyNumbers(textBox1, e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            allowOnlyNumbers(textBox2, e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            allowOnlyNumbers(textBox4, e);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string nome = "";
            string sexo = "";
            DateTime dataNascimento = Convert.ToDateTime(null);
            DateTime dataEntrada = Convert.ToDateTime(null);

            if (textBox9.Text == "" && maskedTextBox4.Text == "  /  /" && maskedTextBox3.Text == "  /  /" && comboBox1.Text == "")
            {
                MessageBox.Show("Preencha algum campo para pesquisa!");
                MySqlCommand sql = new MySqlCommand();
                sql.CommandText = "select id as 'Id', nome as 'Nome', date_format(nascimento,'%d/%m/%Y') as 'Nascimento', sexo as 'Sexo', date_format(dataEntrada,'%d/%m/%Y') as 'Entrada' from paciente where (dataSaida is null or dataSaida = @saida)";
                sql.Parameters.AddWithValue("@saida", Convert.ToDateTime(null));
                Combobox.combobox(comboBox2, sql);
            }
            else
            {
                nome = textBox9.Text;
                try
                {
                    sexo = comboBox1.Text[0].ToString();
                }
                catch (Exception) { }
                try
                {
                    string[] nasc = maskedTextBox4.Text.Split('/');
                    dataNascimento = new DateTime(Convert.ToInt32(nasc[2]), Convert.ToInt32(nasc[1]), Convert.ToInt32(nasc[0]));
                }
                catch (Exception) { }
                try
                {
                    string[] entr = maskedTextBox3.Text.Split('/');
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
                Combobox.combobox(comboBox2, sql);
            }
        }

        private void comboBox2_Format(object sender, ListControlConvertEventArgs e)
        {
            PacienteController pc = new PacienteController();
            e.Value = pc.search("id", e.Value.ToString())[0].nome;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                PacienteController pc = new PacienteController();
                Model.Paciente pac = pc.search("id", comboBox2.SelectedItem.ToString())[0];
                textBox9.Text = pac.nome;
                maskedTextBox4.Text = pac.nascimento.ToString("dd/MM/yyyy");
                if (pac.sexo == "F")
                {
                    comboBox1.SelectedItem = "Feminino";
                }
                else if (pac.sexo == "M")
                {
                    comboBox1.SelectedItem = "Masculino";
                }
                maskedTextBox3.Text = pac.dataEntrada.ToString("dd/MM/yyyy");
            }
            catch (Exception) {}
        }
    }
}