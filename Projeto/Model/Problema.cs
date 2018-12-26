using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Model
{
    class Problema
    {
        public string id { get; set; }
        public string nome { get; set; }
        public bool stt { get; set; }
        public string detalhes { get; set; }
        public string descricao { get; set; }

        public Problema() { }

        public Problema(string id, string nome, bool stt, string detalhes, string descricao)
        {
            this.id = id;
            this.nome = nome;
            this.stt = stt;
            this.detalhes = detalhes;
            this.descricao = descricao;
        }
    }
}