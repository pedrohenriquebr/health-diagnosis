using Projeto.Dao;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    class ConsultaController
    {
        private ConsultaDao daoConsulta;

        public ConsultaController()
        {
            daoConsulta = new ConsultaDao();
        }

        public bool add(Consulta consulta)
        {
            return daoConsulta.add(consulta);
        }

        public bool delete(string id)
        {
            Consulta tmp = new Consulta();
            tmp.id = id;
            return daoConsulta.delete(tmp);
        }

        public bool update(Consulta consulta)
        {
             return daoConsulta.update(consulta);
        }

        public List<Consulta> all()
        {
            return daoConsulta.all();
        }

        public List<Consulta> search<T>(string campo, T valor)
        {
            return daoConsulta.search(campo, valor);
        }

        public List<Problema> listaProblema(List<string> idProblemas)
        {
            return daoConsulta.listaProblema(idProblemas);
        }
    }
}