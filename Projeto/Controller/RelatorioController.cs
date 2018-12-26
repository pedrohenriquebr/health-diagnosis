using Projeto.Dao;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    class RelatorioController
    {
        private RelatorioDao daoRelatorio;

        public RelatorioController()
        {
            daoRelatorio = new RelatorioDao();
        }

        public bool add(Relatorio rel)
        {
            return daoRelatorio.add(rel);
        }

        public bool delete(string id)
        {
            Relatorio tmp = new Relatorio();
            tmp.id = id;
            return daoRelatorio.delete(tmp);
        }

        public bool update(Relatorio rel)
        {
             return daoRelatorio.update(rel);
        }

        public List<Relatorio> all()
        {
            return daoRelatorio.all();
        }

        public List<Relatorio> search<T>(string campo, T valor)
        {
            return daoRelatorio.search(campo, valor);
        }
    }
}