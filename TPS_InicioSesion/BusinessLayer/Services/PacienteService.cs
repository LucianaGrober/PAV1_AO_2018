using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAV1_AO_2018.DataLayer.DAOs;

namespace PAV1_AO_2018.BusinessLayer.Services
{
    public class PacienteService
    {
        private PacienteDao oPacienteDao;

        public PacienteService()
        {
            oPacienteDao = new PacienteDao();
        }
        public Paciente validarPacienre(string nombre)
        {
            return oPacienteDao.getUserByNamePass(nombre);
        }

        public Paciente validarNombrePaciente(string nombre)
        {
            return oPacienteDao.getPacienteByName(nombre);
        }

        public IList<Paciente> consultarPacientesConFiltros(List<object> @params)
        {
            return oPacienteDao.getByFilters(@params);
        }

        public IList<Paciente> consultarPacientes()
        {
            return oPacienteDao.getAll();
        }

      

        public bool crearPaciente(Paciente oPaciente)
        {
            return oPacienteDao.create(oPaciente);
        }

        public bool actualizarPaciente(Paciente oPaciente)
        {
            return oPacienteDao.update(oPaciente);
        }

        public bool modificarEstadoPaciente(Paciente oPaciente)
        {
            return oPacienteDao.update(oPaciente);
        }
    }
}
