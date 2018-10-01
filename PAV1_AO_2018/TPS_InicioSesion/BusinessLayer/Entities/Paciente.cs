using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV1_AO_2018.BusinessLayer
{
    public class Paciente
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string obraSocial { get; set; }
        public string telefono { get; set; }
        public string nroDocumento { get; set; }
        public string tipoDocumento { get; set; }
        public string estado { get; set; }
        public string domicilio { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int id_paciente { get; set; }
    }
}
