using Projeto.Dao;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    class EnfermeiroController
    {
        private EnfermeiroDao daoEnfermeiro;

        public EnfermeiroController()
        {
            daoEnfermeiro = new EnfermeiroDao();
        }

        public bool add(Enfermeiro enf)
        {
            return daoEnfermeiro.add(enf);
        }

        public bool delete(string id, DateTime demissao)
        {
            Enfermeiro tmp = new Enfermeiro();
            tmp.id = id;
            tmp.dataDemissao = demissao;
            return daoEnfermeiro.delete(tmp);
        }

        public bool update(Enfermeiro enf)
        {
            return daoEnfermeiro.update(enf);
        }

        public List<Enfermeiro> all()
        {
            return daoEnfermeiro.all();
        }

        public List<Enfermeiro> search<T>(string campo, T valor)
        {
            return daoEnfermeiro.search(campo, valor);
        }
        
        public Enfermeiro login(string login, string senha)
        {
            return daoEnfermeiro.login(login, senha);
        }
    }
}