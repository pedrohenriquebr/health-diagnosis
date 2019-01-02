using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.View.Relatorio;

namespace Projeto.Model
{
    class Relatorio
    {
        internal string consulta { get; set; }

        public string id { get; set; }
        public string conteudo { get; set; }
        public DateTime datahora { get; set; }
        public string paciente { get; set; }
        public string enfermeiro { get; set; }

        public Relatorio(string id, string conteudo, DateTime datahora, string paciente, string enfermeiro)
        {
            this.id = id;
            this.conteudo = conteudo;
            this.datahora = datahora;
            this.paciente = paciente;
            this.enfermeiro = enfermeiro;
        }

        public Relatorio()
        {
        }

        public static implicit operator Relatorio(View.Relatorio.EdicaoRelatorio v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Relatorio(List<Relatorio> v)
        {
            throw new NotImplementedException();
        }
    }
}