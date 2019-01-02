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

namespace Projeto.View.Relatorio
{
    public partial class EdicaoRelatorio : Form
    {
        private Model.Consulta consulta;
        private string nome_paciente;
        private string html;
        private Dictionary<Model.Problema, Model.Solucao> modelo;

        private string conteudoRelatorio(string paciente, string consultor, string data, string hora, Dictionary<Model.Problema, Model.Solucao> probSol)
        {
            html =
            "<html> " +
                "<head>" +
                    "<meta charset='utf-8'>" +
                "</head>" +
                "<body>" +
                    "<table cellspacing='20' width='100%' style='font-size: 16pt; font-weight: bold;'>" +
                        "<tr>" +
                            "<td colspan='80'>Paciente: " + nome_paciente + " </td>" +
                            "<td>Consultor: " + Session.nome + "</td> " +
                        "</tr>" +
                        "<tr>" +
                            "<td colspan='80'>Data do Exame: " + data + "</td>" +
                            "<td>Hora: " + hora + "</td>" +
                        "</tr>" +
                    "</table>" +
                    "<center>" +
                        "<h2>Relatório</h2>" +
                    "</center>" +
                    "<br>" +
                    "<h3>";

            foreach (KeyValuePair<Model.Problema, Model.Solucao> tt in probSol)
            {
                try
                {
                    html += "\n" + tt.Key.nome + "\t\n";
                    html += tt.Value.descricao + "\n";
                }
                catch (Exception)
                {
                    continue;
                }
            }

            html = html.Replace("\n", "<br>");
            html += "</p> </h3> </body> </html>";
            return "";
        }

        public EdicaoRelatorio(Model.Consulta con, string pacNome)
        {
            InitializeComponent();
            this.consulta = con;
            this.nome_paciente = pacNome;

            ConsultaController conCont = new ConsultaController();

            if (conCont.add(con))
            {
                MessageBox.Show("Consulta cadastrada com sucesso!");

                ListaProblemas lp = new ListaProblemas();
                List<Model.Problema> problemas = new List<Model.Problema>();
                problemas = lp.problematizar(con);
                List<Model.Solucao> solucoes = new List<Model.Solucao>();
                ProblemaController pC = new ProblemaController();

                string data = con.datahora.ToShortDateString();
                string hora = con.datahora.ToShortTimeString();

                try
                {
                    modelo = new Dictionary<Model.Problema, Model.Solucao>();

                    foreach (Model.Problema problema in problemas)
                    {
                        try
                        {
                            solucoes = pC.listaSolucao(problema.id);
                            for (int x = 0; x < solucoes.Count; x++)
                            {
                                modelo.Add(problema, solucoes[x]);
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    textBox1.Text = nome_paciente;
                    textBox2.Text = Session.nome;
                    maskedTextBox1.Text = consulta.datahora.ToShortDateString();
                    maskedTextBox2.Text = consulta.datahora.ToShortTimeString();
                    Combobox.combobox(comboBox1, problemas);
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível realizar a análise dos problemas para o relatório!");
                }
            }
            else
            {
                MessageBox.Show("Consulta não pôde ser cadastrada!");
            }
        }

        private void problematizarSolucionar(string file_name)
        {
            try
            {
                String destHtml = System.IO.Path.GetTempFileName() + ".html";
                String destPdf = file_name;
                System.IO.File.WriteAllText(destHtml, html);

                var Renderer = new IronPdf.HtmlToPdf();
                var PDF = Renderer.RenderHTMLFileAsPdf(destHtml);
                var OutputPath = destPdf;
                PDF.SaveAs(OutputPath);
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível gerar o relatório!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conteudoRelatorio(nome_paciente, Session.nome, this.consulta.datahora.ToShortDateString(), this.consulta.datahora.ToShortTimeString(), modelo);
            Model.Relatorio rel = new Model.Relatorio(consulta.id, html, consulta.datahora, consulta.paciente, consulta.enfermeiro);
            RelatorioController rc = new RelatorioController();

            if (rc.add(rel))
            {
                MessageBox.Show("Relatório cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show("Relatório não pôde ser cadastrado!");
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Document|*.pdf";
            saveFileDialog.Title = "Salvar relatório em PDF";
            DialogResult resultado = saveFileDialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                string file_name = saveFileDialog.FileName;
                problematizarSolucionar(file_name);
                MessageBox.Show("Relatório gerado com sucesso!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Relatório não pôde ser gerado!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (richTextBox1.Text != "")
            {
                ProblemaController pc = new ProblemaController();

                Model.Problema problemaId = pc.search("id", comboBox1.SelectedItem.ToString())[0];
                Model.Solucao solucaoId = pc.listaSolucao(comboBox1.SelectedItem.ToString())[0];

                solucaoId.descricao = richTextBox1.Text;
                foreach (Model.Problema atraso in modelo.Keys)
                {
                    if (problemaId.nome == atraso.nome)
                    {
                        problemaId = atraso;
                    }
                }
                modelo[problemaId] = solucaoId;

                MessageBox.Show("Recomendação atualizada!");
                conteudoRelatorio(nome_paciente, Session.nome, this.consulta.datahora.ToShortDateString(), this.consulta.datahora.ToShortTimeString(), modelo);
            }
            else
            {
                MessageBox.Show("Preencha o campo requisitado!");
            }
        }

        private void comboBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            ProblemaController pc = new ProblemaController();
            e.Value = pc.search("id", e.Value.ToString())[0].nome;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ProblemaController pc = new ProblemaController();
                foreach (Model.Problema atraso in modelo.Keys)
                {
                    if (comboBox1.SelectedItem.ToString() == atraso.id)
                    {
                        Model.Solucao solu = modelo[atraso];
                        richTextBox1.Text = solu.descricao;
                    }
                }
            }
            catch (Exception) { }
        }
    }
}