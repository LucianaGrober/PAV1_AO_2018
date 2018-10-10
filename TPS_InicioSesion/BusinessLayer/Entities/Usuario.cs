using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV1_AO_2018.BusinessLayer

{
    public class Usuario
    {
        public string nombreUsuario { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string nroDocumento { get; set; }
        public string tipoDocumento { get; set; }
        public string estado { get; set; }
        public string cod_tipoDoc { get; set; }
        public string nroMatricula { get; set; }
        public int id_usuario { get; set; }
        // Relación con Perfil
        public string id_perfil { get; set; }
        public string perfil { get; set; }
    }
}
