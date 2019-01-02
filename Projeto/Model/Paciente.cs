using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Model
{
    public class Paciente
    {
        public string id { get; set; }
        public string nome { get; set; }
        public DateTime nascimento { get; set; }
        public string sexo { get; set; }
        public string enfermeiro { get; set; }
        public DateTime dataEntrada { get; set; }
        public DateTime dataSaida { get; set; }

        public Paciente() { }

        public Paciente(string id, string nome, DateTime nascimento, string sexo, string enfermeiro, DateTime dataEntrada, DateTime dataSaida)
        {
            this.id = id;
            this.nome = nome;
            this.nascimento = nascimento;
            this.sexo = sexo;
            this.enfermeiro = enfermeiro;
            this.dataEntrada = dataEntrada;
            this.dataSaida = dataSaida;
        }
    }
}