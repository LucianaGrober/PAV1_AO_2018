using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAV1_AO_2018.BusinessLayer;
using System.Windows.Forms;


namespace PAV1_AO_2018.DataLayer.DAOs
{
    public class UsuarioDao
    {
        // Permite recuperar un usuario a partir de un nombre y password
        public Usuario getUserByNamePass(string nombre, string password)
        {
            string sql = "Select x.*, y.n_perfil, y.id_perfil as perfil_usuario, z.n_tipo, z.cod_tipo as tipoDoc_usuario from Usuarios x, Perfiles y, TiposDocumento z where x.id_perfil=y.id_perfil AND x.tipoDocumento = z.cod_tipo AND x.nombreUsuario = '" + nombre + "' AND password = '" + password + "'";
            DataTable oTabla;
            Usuario oUsuario = null/* TODO Change to default(_) if this is not a reference type */;

            oTabla = BDHelper.getBDHelper().ConsultaSQL(sql);
            if (oTabla.Rows.Count > 0)
                oUsuario = map_user(oTabla.Rows[0]);

            return oUsuario;
        }

        public Usuario getUserByName(string nombre)
        {
            string sql = "Select x.*, y.n_perfil, y.id_perfil as perfil_usuario, z.n_tipo, z.cod_tipo as tipoDoc_usuario from Usuarios x, Perfiles y, TiposDocumento z where x.id_perfil=y.id_perfil AND x.tipoDocumento = z.cod_tipo AND x.nombreUsuario = '" + nombre + "'";
            DataTable oTabla;
            Usuario oUsuario = null/* TODO Change to default(_) if this is not a reference type */;

            oTabla = BDHelper.getBDHelper().ConsultaSQL(sql);
            if (oTabla.Rows.Count >  0)
                oUsuario = map_user(oTabla.Rows[0]);

            return oUsuario;
        }



        // Permite recuperar todos los usuarios cargados
        public IList<Usuario> getAll()
        {
            List<Usuario> lst = new List<Usuario>();
            string sql = "Select x.*, y.n_perfil, y.id_perfil as perfil_usuario, z.n_tipo, z.cod_tipo as tipoDoc_usuario from Usuarios x, Perfiles y, TiposDocumento z where x.id_perfil=y.id_perfil AND x.tipoDocumento = z.cod_tipo";
            Usuario oUsuario = null/* TODO Change to default(_) if this is not a reference type */;

            foreach (DataRow row in BDHelper.getBDHelper().ConsultaSQL(sql).Rows)
                lst.Add(map_user(row));

            return lst;
        }

        // Permite recuperar todos los usuarios cargados para un determinado rango de fechas y/o perfil recibidos como 
        // parámetrosr
        public IList<Usuario> getByFilters(List<object> @params)
        {
            List<Usuario> lst = new List<Usuario>();
            string sql = "Select x.*, y.n_perfil, y.id_perfil as perfil_usuario, z.n_tipo, z.cod_tipo as tipoDoc_usuario from Usuarios x, Perfiles y, TiposDocumento z where x.id_perfil=y.id_perfil AND x.tipoDocumento = z.cod_tipo";
            Usuario oUsuario = null/* TODO Change to default(_) if this is not a reference type */;

            if (@params[0] != null)
                sql += " AND x.id_perfil=@param1 ";
            if (@params[1] != null)
                sql += " AND x.nombreUsuario LIKE '%' + @param2 + '%' ";
            

            foreach (DataRow row in BDHelper.getBDHelper().ConsultaSQLConParametros(sql, @params).Rows)
                lst.Add(map_user(row));

            return lst;
        }

        // Funciones CRUD
        public bool create(Usuario oUsuario)
        {
            string str_sql;

            str_sql = "INSERT INTO Usuarios (nombreUsuario, password, tipoDocumento, nroDocumento, nroMatricula, telefono, email, id_perfil, estado) VALUES('";
            str_sql += oUsuario.nombreUsuario + "','";
            str_sql += oUsuario.password + "','";
            str_sql += oUsuario.cod_tipoDoc + "','";
            str_sql += oUsuario.nroDocumento + "','";
            str_sql += oUsuario.nroMatricula + "',";
            if (oUsuario.telefono == "")
            {                
                str_sql += "NULL" + ",'";
            }
            else
            {                
                str_sql += "'" + oUsuario.telefono + "','";
                
            }
            str_sql += oUsuario.email + "',";
            str_sql += oUsuario.id_perfil.ToString() + ", 'S')";
            MessageBox.Show(str_sql);

            // Si una fila es afectada por la inserción retorna TRUE. Caso contrario FALSE
            return (BDHelper.getBDHelper().EjecutarSQL(str_sql) == 1);
        }
        // Funciones CRUD

        public bool update(Usuario oUsuario)
        {
            string str_sql;

            str_sql = "UPDATE Usuarios SET nombreUsuario = '";
            str_sql += oUsuario.nombreUsuario + "', password = '";
            str_sql += oUsuario.password + "', tipoDocumento = '";
            str_sql += oUsuario.cod_tipoDoc + "', nroDocumento = '";
            str_sql += oUsuario.nroDocumento + "', nroMatricula = '";
            str_sql += oUsuario.nroMatricula + "', telefono = ";
            if (oUsuario.telefono == "")
            {
                str_sql += "NULL" + ",";
            }
            else
            {
                str_sql += "'" + oUsuario.telefono + "',";
            }
            str_sql += " email = '";
            str_sql += oUsuario.email + "', id_perfil = '";
            str_sql += oUsuario.id_perfil + "', estado = '";
            str_sql += oUsuario.estado + "'";

            str_sql += " WHERE id_usuario = " + oUsuario.id_usuario.ToString();
            MessageBox.Show(str_sql);

            // Si una fila es afectada por la actualización retorna TRUE. Caso contrario FALSE
            return (BDHelper.getBDHelper().EjecutarSQL(str_sql) == 1);
        }

        // Función auxiliar responsable de MAPPEAR una fila de Usuarios a un objeto Usuario
        private Usuario map_user(DataRow row)
        {
            Usuario oUsuario = new Usuario();

            oUsuario.id_usuario = Convert.ToInt32(row["id_usuario"].ToString());
            oUsuario.nombreUsuario = row["nombreUsuario"].ToString();
            oUsuario.password = row["password"].ToString();
            oUsuario.tipoDocumento = row["n_tipo"].ToString();
            oUsuario.nroDocumento = row["nroDocumento"].ToString();
            oUsuario.nroMatricula = row["nroMatricula"].ToString();
            oUsuario.telefono = row["telefono"].ToString();
            oUsuario.email = row["email"].ToString();
            oUsuario.perfil = row["n_perfil"].ToString();
            oUsuario.estado = row["estado"].ToString();
            oUsuario.id_perfil = row["id_perfil"].ToString();
            //oUsuario.cod_tipoDoc = row["cod_tipo"].ToString();
            return oUsuario;
        }
    }



}
