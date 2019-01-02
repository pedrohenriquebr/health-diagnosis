using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Model
{
    public class Enfermeiro
    {
        public string id { get; set; }
        public string nome { get; set; }
        public DateTime nascimento { get; set; }
        public string sexo { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public bool stt { get; set; }
        public DateTime dataAdmissao { get; set; }
        public DateTime dataDemissao { get; set; }

        public Enfermeiro() { }

        public Enfermeiro(string id, string nome, DateTime nascimento, string sexo, string login, string senha, bool stt, DateTime dataAdmissao, DateTime dataDemissao)
        {
            this.id = id;
            this.nome = nome;
            this.nascimento = nascimento;
            this.sexo = sexo;
            this.login = login;
            this.senha = senha;
            this.stt = stt;
            this.dataAdmissao = dataAdmissao;
            this.dataDemissao = dataDemissao;
        }
    }
}