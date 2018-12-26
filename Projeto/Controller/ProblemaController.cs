using Projeto.Dao;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    class ProblemaController
    {
        private ProblemaDao daoProblema;

        public ProblemaController()
        {
            daoProblema = new ProblemaDao();
        }

        public bool add(Problema prob)
        {
            return daoProblema.add(prob);
        }

        public bool delete(string id)
        {
            Problema tmp = new Problema();
            tmp.id = id;
            return daoProblema.delete(tmp);
        }

        public bool update(Problema enf)
        {
            return daoProblema.update(enf);
        }

        public List<Problema> all()
        {
            return daoProblema.all();
        }

        public List<Problema> search<T>(string campo, T valor)
        {
            return daoProblema.search(campo, valor);
        }

        public List<Solucao> listaSolucao(string idProblema)
        {
            return daoProblema.listaSolucao(idProblema);
        }
    }
}