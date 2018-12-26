using Projeto.Dao;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    class PacienteController
    {
        private PacienteDao daoPaciente;

        public PacienteController()
        {
            daoPaciente = new PacienteDao();
        }

        public bool add(Paciente pac)
        {
            return daoPaciente.add(pac);
        }

        public bool delete(string id, DateTime saida)
        {
            Paciente tmp = new Paciente();
            tmp.id = id;
            tmp.dataSaida = saida;
            return daoPaciente.delete(tmp);
        }

        public bool update(Paciente pac)
        {
            return daoPaciente.update(pac);
        }

        public List<Paciente> all()
        {
            return daoPaciente.all();
        }

        public List<Paciente> search<T>(string campo, T valor)
        {
            return daoPaciente.search(campo, valor);
        }
    }
}