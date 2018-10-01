using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAV1_AO_2018.BusinessLayer;
using PAV1_AO_2018.BusinessLayer.Services;

namespace PAV1_AO_2018.DataLayer.DAOs
{
    public class PacienteDao
    {
        public Paciente getPacienteByName(string nombre)
        {
            string sql = "Select x.* from Pacientes x, where x.nombre = '" + nombre + "'";
            DataTable oTabla;
            Paciente oPaciente = null/* TODO Change to default(_) if this is not a reference type */;

            oTabla = BDHelper.getBDHelper().ConsultaSQL(sql);
            if (oTabla.Rows.Count > 0)
                oPaciente = map_user(oTabla.Rows[0]);

            return oPaciente;
        }

        public IList<Paciente> getByFilters(List<object> @params)
        {
            List<Paciente> lst = new List<Paciente>();
            string sql = "Select x.*,  from Pacientes x, where x.nombre=nombre "; /*revisar esto*/
            Paciente oPaciente = null/* TODO Change to default(_) if this is not a reference type */;

            
            foreach (DataRow row in BDHelper.getBDHelper().ConsultaSQLConParametros(sql, @params).Rows)
                lst.Add(map_user(row));

            return lst;
        }

        // Permite recuperar todos los usuarios cargados
        public IList<Paciente> getAll()
        {
            List<Paciente> lst = new List<Paciente>();
            string sql = "Select x.*, from Pacientes x";
            Usuario oPaciente = null/* TODO Change to default(_) if this is not a reference type */;

            foreach (DataRow row in BDHelper.getBDHelper().ConsultaSQL(sql).Rows)
                lst.Add(map_user(row));

            return lst;
        }

        // Funciones CRUD
        public bool create(Paciente oPaciente)
        {
            string str_sql;

            str_sql = "INSERT INTO Pacientes (nombre, apellido, fechaNac, tipoDocumento, nroDocumento, " +
                "domicilio, telefono, obraSocial, estado) VALUES('";
            str_sql += oPaciente.nombre + "','";
            str_sql += oPaciente.apellido + "','";
            str_sql += oPaciente.fechaNacimiento.ToString() + "','";
            str_sql += oPaciente.tipoDocumento.ToString() + "','";
            str_sql += oPaciente.nroDocumento + "','";
            str_sql += oPaciente.domicilio + "','";
            str_sql += oPaciente.telefono + "',";
            str_sql += oPaciente.obraSocial + ", 'S')";

            // Si una fila es afectada por la inserción retorna TRUE. Caso contrario FALSE
            return (BDHelper.getBDHelper().EjecutarSQL(str_sql) == 1);
        }
        // Funciones CRUD

        public bool update(Paciente oPaciente)
        {
            string str_sql;
            str_sql = "UPDATE Pacientes SET nombre = '";
            str_sql += oPaciente.nombre + "', apellido = '";
            str_sql += oPaciente.apellido + "', fechaNac = '";
            str_sql += oPaciente.fechaNacimiento + "', tipoDocumento = ";
            str_sql += oPaciente.tipoDocumento.ToString() + ", nroDocumento = '";
            str_sql += oPaciente.nroDocumento + "', domicilio = ";
            str_sql += oPaciente.domicilio + "', telefono = ";
            str_sql += oPaciente.telefono + "', obraSocial = ";
            str_sql += oPaciente.obraSocial + "', estado = ";
            str_sql += oPaciente.estado + "'";

            str_sql += " WHERE idPaciente = " + oPaciente.id_paciente.ToString();

            // Si una fila es afectada por la actualización retorna TRUE. Caso contrario FALSE
            return (BDHelper.getBDHelper().EjecutarSQL(str_sql) == 1);
        }

        // Función auxiliar responsable de MAPPEAR una fila de Usuarios a un objeto Usuario
        private Paciente map_user(DataRow row)
        {
            Paciente oPaciente = new Paciente();

            oPaciente.id_paciente = Convert.ToInt32(row["idPaciente"].ToString());

            oPaciente.nombre = row["nombre"].ToString();
            oPaciente.apellido = row["apellido"].ToString();
            oPaciente.fechaNacimiento = Convert.ToDateTime(row["fechaNac"].ToString());
            oPaciente.tipoDocumento = row["tipoDocumento"].ToString();
            oPaciente.nroDocumento = row["nroDocumento"].ToString();
            oPaciente.domicilio = row["domicilio"].ToString();
            oPaciente.telefono = row["telefono"].ToString();
            oPaciente.obraSocial = row["obraSocial"].ToString();
            oPaciente.estado = row["estado"].ToString();
            return oPaciente;
        }


    }
}
