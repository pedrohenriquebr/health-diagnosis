using System;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Projeto.Controller;
using Projeto.Model;
using Projeto;
using System.Collections.Generic;

namespace Projeto.View.HistoricoMedico
{
    public partial class GraficoGeral : Form
    {
        public Model.HistoricoMedico GerarHistoricoMedico(List<Model.Consulta> consultas)
        {
            Model.HistoricoMedico historico = new Model.HistoricoMedico();

            int qtde = consultas.Count;
            double[] valores_massa = new double[qtde];
            double[] valores_glicemia = new double[qtde];
            double[] valores_circAbdominal = new double[qtde];
            int[] valores_respiracao = new int[qtde];
            int[] valores_batimentoCardio = new int[qtde];
            double[] valores_temperatura = new double[qtde];
            string[] valores_id = new string[qtde];
            string[] valores_pressao = new string[qtde];
            string[] valores_enfermeiro = new string[qtde];
            bool[] valores_jejum = new bool[qtde];
            DateTime[] valores_datahora = new DateTime[qtde];


            // insere os dados 
            int i = 0;
            foreach (Model.Consulta item in consultas)
            {
                valores_massa[i] = item.massaCorporal;
                valores_glicemia[i] = item.glicemia;
                valores_circAbdominal[i] = item.circAbdominal;
                valores_respiracao[i] = item.respiracao;
                valores_batimentoCardio[i] = item.batimentoCardio;
                valores_temperatura[i] = item.temperatura;
                valores_id[i] = item.id;
                valores_pressao[i] = item.pressaoArterial;
                valores_enfermeiro[i] = item.enfermeiro;
                valores_jejum[i] = item.jejum;
                valores_datahora[i] = item.datahora;
                i++;
            }

            historico.glicemia = valores_glicemia;
            historico.respiracao = valores_respiracao;
            historico.temperatura = valores_temperatura;
            historico.massaCorporal = valores_massa;
            historico.batimentoCardio = valores_batimentoCardio;
            historico.circAbdominal = valores_circAbdominal;
            historico.id = valores_id;
            historico.pressaoArterial = valores_pressao;
            historico.enfermeiro = valores_enfermeiro;
            historico.jejum = valores_jejum;
            historico.datahora = valores_datahora;
           
            return historico;
        }

        public GraficoGeral(List<Model.Consulta> consultas)
        {
            InitializeComponent();
            Model.HistoricoMedico historico = GerarHistoricoMedico(consultas);

            // global
            chartPrincipal.Series.Clear();
            chartPrincipal.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartPrincipal.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // linha para exibir o peso 
            Series serie_peso = new Series()
            {
                //IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LegendText = "Massa Corporal (KG)",
                ChartType = SeriesChartType.Column,
                Color = Color.Red
            };

            // linha para exibir glicemia
            Series serie_glicemia = new Series()
            {
                //IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LegendText = "Glicemia (mg/dl)",
                ChartType = SeriesChartType.Column,
                Color = Color.Green
            };

            // linha para exibir circunferência abdominal 
            Series serie_circAbdominal = new Series()
            {
                //IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LegendText = "Circunferência Abdominal (cm)",
                ChartType = SeriesChartType.Column,
                Color = Color.Blue
            };

            // linha para exibir circunferência abdominal 
            Series serie_respiracao = new Series()
            {
                //IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LegendText = "Respiração (rpm)",
                ChartType = SeriesChartType.Column,
                Color = Color.Brown
            };

            // linha para exibir circunferência abdominal 
            Series serie_batimentoCardio = new Series()
            {
                //IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LegendText = "Pulso (bpm)",
                ChartType = SeriesChartType.Column,
                Color = Color.DimGray
            };

            // linha para exibir temperatura
            Series serie_temperatura = new Series()
            {
               //IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LegendText = "Temperatura (ºC)",
                ChartType = SeriesChartType.Column,
                Color = Color.Purple

            };


            string[] consultas_datas = new string[historico.id.Length];

            int k = 0;
            foreach (DateTime datahora in historico.datahora)
            {
                consultas_datas[k++] = datahora.ToString();
            }

            serie_peso.Points.DataBindXY(consultas_datas, historico.massaCorporal);
            serie_glicemia.Points.DataBindXY(consultas_datas, historico.glicemia);
            serie_circAbdominal.Points.DataBindXY(consultas_datas, historico.circAbdominal);
            serie_respiracao.Points.DataBindXY(consultas_datas, historico.respiracao);
            serie_batimentoCardio.Points.DataBindXY(consultas_datas, historico.batimentoCardio);
            serie_temperatura.Points.DataBindXY(consultas_datas, historico.temperatura);

            chartPrincipal.Series.Add(serie_glicemia);
            chartPrincipal.Series.Add(serie_peso);
            chartPrincipal.Series.Add(serie_circAbdominal);
            chartPrincipal.Series.Add(serie_respiracao);
            chartPrincipal.Series.Add(serie_batimentoCardio);
            chartPrincipal.Series.Add(serie_temperatura);

            chartPrincipal.Click += ChartPrincipal_Click;
            chartPrincipal.ChartAreas[0].AxisX.ScrollBar.Size = 15;
            chartPrincipal.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chartPrincipal.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartPrincipal.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            chartPrincipal.ChartAreas[0].AxisX.ScaleView.Zoom(0, 3);
            chartPrincipal.Invalidate();
        }

        private void ChartPrincipal_Click(object sender, EventArgs e)
        {

        }
    }
}