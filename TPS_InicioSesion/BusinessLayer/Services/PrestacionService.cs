using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAV1_AO_2018.DataLayer.DAOs;

namespace PAV1_AO_2018.BusinessLayer.Services
{
    public class PrestacionService
    {
        private PrestacionDao oPrestacionDao;

        public PrestacionService()
        {
           oPrestacionDao = new PrestacionDao();
        }
        

        public Prestacion validarPrestacion(string nombre)
        {
            return oPrestacionDao.getUserByNamePass(nombre);
        }

        public Prestacion validarNombrePrestacion(string nombre)
        {
            return oPrestacionDao.getUserByName(nombre);
        }

        public IList<Prestacion> consultarPrestacionesConFiltros(List<object> @params)
        {
            return oPrestacionDao.getByFilters(@params);        }

        public IList<Prestacion> consultarUsuarios()
        {
            return oPrestacionDao.getAll();
        }



        public bool crearPrestacion(Prestacion oPrestacion)
        {
            return oPrestacionDao.create(oPrestacion);
        }

        public bool actualizarPrestacion(Prestacion oPrestacion)
        {
            return oPrestacionDao.update(oPrestacion);
        }

        public bool modificarEstadoPrestacion(Prestacion oPrestacion)
        {
            return oPrestacionDao.update(oPrestacion);
        }
    }

}
