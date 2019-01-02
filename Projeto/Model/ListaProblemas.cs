using Projeto.Controller;
using Projeto.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Model
{
    class ListaProblemas
    {
        public List<Problema> problematizar(Consulta consulta)
        {
            PacienteController pacC = new PacienteController();
            List<string> problemas = new List<string>();
            string sexo = pacC.search("id", consulta.paciente)[0].sexo;
            double imc = 0;
            if (consulta.massaCorporal != 0 && consulta.altura != 0)
            {
                imc = consulta.massaCorporal / Math.Pow(consulta.altura, 2);
            }            
            double temperatura = consulta.temperatura;
            int pulso = consulta.batimentoCardio;
            int respiracao = consulta.respiracao;
            double circAbdominal = consulta.circAbdominal;
            double glicemia = consulta.glicemia;
            string[] pressaoArterial = consulta.pressaoArterial.Split('/');
            int pas = Convert.ToInt32(pressaoArterial[0]);
            int pad = Convert.ToInt32(pressaoArterial[1]);
            bool jejum = consulta.jejum;
            
            if (imc == 0) {}
            else if (imc <= 18) problemas.Add(Hash.md5("Baixo Peso"));
            else if (imc < 25) problemas.Add(Hash.md5("Peso normal"));
            else if (imc < 30) problemas.Add(Hash.md5("Sobrepeso"));
            else if (imc < 35) problemas.Add(Hash.md5("Obesidade Grau I"));
            else if (imc < 40) problemas.Add(Hash.md5("Obesidade Grau II"));
            else problemas.Add(Hash.md5("Obesidade Grau III"));

            if (temperatura == 0) {}
            else if (temperatura <= 27) problemas.Add(Hash.md5("Hipotermia Profunda"));
            else if (temperatura <= 30) problemas.Add(Hash.md5("Hipotermia Grave"));
            else if (temperatura <= 33) problemas.Add(Hash.md5("Hipotermia Moderada"));
            else if (temperatura < 36) problemas.Add(Hash.md5("Hipotermia Leve"));
            else if (temperatura <= 37) problemas.Add(Hash.md5("Temperatura Normal"));
            else if (temperatura < 39) problemas.Add(Hash.md5("Febre"));
            else if (temperatura <= 40) problemas.Add(Hash.md5("Pirexia"));
            else problemas.Add(Hash.md5("Hiperpirexia"));

            if (pulso == 0) {}
            else if (pulso <= 59) problemas.Add(Hash.md5("Bradisfigmia"));
            else if (pulso <= 100) problemas.Add(Hash.md5("Normocardia"));
            else problemas.Add(Hash.md5("Taquisfigmia"));

            if (respiracao == 0) {}
            else if (respiracao <= 11) problemas.Add(Hash.md5("Bradipneia"));
            else if (respiracao <= 20) problemas.Add(Hash.md5("Eupneia"));
            else problemas.Add(Hash.md5("Taquipneia"));

            if (circAbdominal == 0) {}
            else if ((sexo == "F" && circAbdominal <= 70) || (sexo == "M" && circAbdominal <= 60)) problemas.Add(Hash.md5("Sem Risco de Complicações Metabólicas"));
            else if ((sexo == "F" && circAbdominal > 70 && circAbdominal <= 81) || (sexo == "M" && circAbdominal > 60 && circAbdominal <= 95)) problemas.Add(Hash.md5("Risco de Complicações Metabólicas Aumentado"));
            else if ((sexo == "F" && circAbdominal > 70) || (sexo == "M" && circAbdominal > 95)) problemas.Add(Hash.md5("Risco de Complicações Metabólicas Aumentado Substancialmente"));

            if (glicemia == 0) {}
            else if ((jejum == true && glicemia <= 50) || (jejum == false && glicemia <= 50)) problemas.Add(Hash.md5("Hipoglicemia"));
            else if ((jejum == true && glicemia > 50 && glicemia < 110) || (jejum == false && glicemia > 50 && glicemia < 140)) problemas.Add(Hash.md5("Glicemia Normal"));
            else if ((jejum == true && glicemia > 110 && glicemia <= 126) || (jejum == false && glicemia > 140 && glicemia <= 200)) problemas.Add(Hash.md5("Pré-diabetes"));
            else if ((jejum == true && glicemia > 126) || (jejum == false && glicemia > 200)) problemas.Add(Hash.md5("Diabetes"));

            if (pas == 0 && pad == 0) {}
            else if (pas <= 90 && pad <= 60) problemas.Add(Hash.md5("Hipotensão"));
            else if (pas > 90 && pas <= 130 && pad > 60 && pad <= 85) problemas.Add(Hash.md5("Pressão Arterial Normal"));
            else if (pas > 130 && pas <= 139 && pad > 85 && pad <= 89) problemas.Add(Hash.md5("Pressão Arterial Normal Limítrofe"));
            else if (pas > 139 && pas <= 159 && pad > 89 && pad <= 99) problemas.Add(Hash.md5("Hipertensão Leve"));
            else if (pas > 159 && pas <= 179 && pad > 99 && pad <= 109) problemas.Add(Hash.md5("Hipertensão Moderada"));
            else if (pas >= 180 && pad >= 110) problemas.Add(Hash.md5("Hipertensão Grave"));
            else if (pas >= 140 && pad < 90) problemas.Add(Hash.md5("Hipertensão Sistólica Isolada"));

            ConsultaController cc = new ConsultaController();
            List<Problema> prob = cc.listaProblema(problemas);
            List<Problema> probReal = new List<Problema>();
            try
            {
                foreach (Problema problema in prob)
                {
                    if (problema.stt)
                    {
                        probReal.Add(problema);
                    }
                }
                return probReal;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}