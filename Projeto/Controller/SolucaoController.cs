using Projeto.Dao;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    class SolucaoController
    {
        private SolucaoDao daoSolucao;

        public SolucaoController()
        {
            daoSolucao = new SolucaoDao();
        }

        public bool add(Solucao sol)
        {
            return daoSolucao.add(sol);
        }

        public bool delete(string id)
        {
            Solucao tmp = new Solucao();
            tmp.id = id;
            return daoSolucao.delete(tmp);
        }

        public bool update(Solucao sol)
        {
            return daoSolucao.update(sol);
        }

        public List<Solucao> all()
        {
             return daoSolucao.all();
        }

        public List<Solucao> search<T>(string campo, T valor)
        {
             return daoSolucao.search(campo, valor);
        }

        public bool problemaSolucao(string problema, string solucao)
        {
            return daoSolucao.problemaSolucao(problema, solucao);
        }
    }
}