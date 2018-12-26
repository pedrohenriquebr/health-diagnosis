using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Projeto.View.HistoricoMedico
{
    public partial class GraficoPressao : Form
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

        public GraficoPressao(List<Model.Consulta> consultas)
        {
            InitializeComponent();
            Model.HistoricoMedico historico = GerarHistoricoMedico(consultas);

            // global
            chartPrincipal.Series.Clear();
            chartPrincipal.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartPrincipal.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // barra para exibir pressão sistólica
            Series serie_pressao_s = new Series()
            {
                IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LegendText = "Pressão Sistólica",
                ChartType = SeriesChartType.Column,
                Color = Color.Red
            };

            // barra para exibir pressão assistólica
            Series serie_pressao_d = new Series()
            {
                IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                LegendText = "Pressão Assistólica",
                ChartType = SeriesChartType.Column,
                Color = Color.Blue
            };

            string[] consultas_datas = new string[historico.id.Length];

            int k = 0;
            foreach (DateTime datahora in historico.datahora)
            {
                consultas_datas[k++] = datahora.ToString();
            }
            string[] pressao = historico.pressaoArterial;
            int[] pressao_s_formatada = new int[historico.id.Length];
            int[] pressao_d_formatada = new int[historico.id.Length];

            int i = 0;
            foreach (String valor in pressao)
            {
                int pressao_s = int.Parse(valor.Split('/')[0]);
                int pressao_d = int.Parse(valor.Split('/')[1]);
                pressao_s_formatada[i] = pressao_s;
                pressao_d_formatada[i++] = pressao_d;

            }

            serie_pressao_s.Points.DataBindXY(consultas_datas, pressao_s_formatada);
            serie_pressao_d.Points.DataBindXY(consultas_datas, pressao_d_formatada);
            chartPrincipal.Series.Add(serie_pressao_s);
            chartPrincipal.Series.Add(serie_pressao_d);
            chartPrincipal.ChartAreas[0].AxisX.ScrollBar.Size = 15;
            chartPrincipal.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chartPrincipal.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartPrincipal.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            chartPrincipal.ChartAreas[0].AxisX.ScaleView.Zoom(0, 3);
            chartPrincipal.Invalidate();
        }
    }
}