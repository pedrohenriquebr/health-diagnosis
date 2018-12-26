using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Model
{
    class Solucao
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }

        public Solucao() { }

        public Solucao(string id, string nome, string descricao)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
        }
    }
}