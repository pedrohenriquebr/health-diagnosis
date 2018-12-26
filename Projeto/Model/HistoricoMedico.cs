using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Model
{
    public class HistoricoMedico
    {
        public string[] id { get; set; }
        public string[] enfermeiro { get; set; }
        public DateTime[] datahora { get; set; }
        public string[] paciente { get; set; }
        public double[] massaCorporal { get; set; }
        public double[] circAbdominal { get; set; }
        public double[] altura { get; set; }
        public bool[] jejum { get; set; }
        public double[] glicemia { get; set; }
        public string[] pressaoArterial { get; set; }
        public int[] respiracao { get; set; }
        public double[] temperatura { get; set; }
        public int[] batimentoCardio { get; set; }

        public HistoricoMedico() {}

        public HistoricoMedico(string[] id, string[] enfermeiro, DateTime[] datahora, string[] paciente, double[] massaCorporal, double[] circAbdominal, double[] altura, bool[] jejum, double[] glicemia, string[] pressaoArterial, int[] respiracao, double[] temperatura, int[] batimentoCardio)
        {
            this.id = id;
            this.enfermeiro = enfermeiro;
            this.datahora = datahora;
            this.paciente = paciente;
            this.massaCorporal = massaCorporal;
            this.circAbdominal = circAbdominal;
            this.altura = altura;
            this.jejum = jejum;
            this.glicemia = glicemia;
            this.pressaoArterial = pressaoArterial;
            this.respiracao = respiracao;
            this.temperatura = temperatura;
            this.batimentoCardio = batimentoCardio;
        }
	}
}