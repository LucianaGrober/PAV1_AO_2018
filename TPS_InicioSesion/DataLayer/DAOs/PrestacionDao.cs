using System;
using System.Collections.Generic;
using System.Data;
using PAV1_AO_2018.BusinessLayer;
using System.Windows.Forms;


namespace PAV1_AO_2018.DataLayer.DAOs
{
    public class PrestacionDao
    {
        // Permite recuperar una prestacion a partir de un nombre 
        public Prestacion getUserByNamePass(string nombre)
        {
            string sql = "Select x.* from Prestaciones x where  x.nombre = '" + nombre + "'";
            DataTable oTabla;
            Prestacion oPrestacion = null/* TODO Change to default(_) if this is not a reference type */;

            oTabla = BDHelper.getBDHelper().ConsultaSQL(sql);
            if (oTabla.Rows.Count > 0)
                oPrestacion = map_user(oTabla.Rows[0]);

            return oPrestacion;
        }

        public Prestacion getUserByName(string nombre)
        {
            string sql = "Select x.* from Prestaciones x where  x.nombre = '" + nombre + "'";
            DataTable oTabla;
            Prestacion oPrestacion = null/* TODO Change to default(_) if this is not a reference type */;

            oTabla = BDHelper.getBDHelper().ConsultaSQL(sql);
            if (oTabla.Rows.Count > 0)
                oPrestacion = map_user(oTabla.Rows[0]);

            return oPrestacion;
        }



        // Permite recuperar todos los prestacions cargados
        public IList<Prestacion> getAll()
        {
            List<Prestacion> lst = new List<Prestacion>();
            string sql = "Select x.* from Prestaciones x ";
            Prestacion oPrestacion = null/* TODO Change to default(_) if this is not a reference type */;

            foreach (DataRow row in BDHelper.getBDHelper().ConsultaSQL(sql).Rows)
                lst.Add(map_user(row));

            return lst;
        }

        // Permite recuperar todos los prestacions cargados para un determinado rango de fechas y/o perfil recibidos como 
        // parámetrosr
        public IList<Prestacion> getByFilters(List<object> @params)
        {
            List<Prestacion> lst = new List<Prestacion>();
            string sql = "Select x.* from Prestaciones x ";
            Prestacion oPrestacion = null/* TODO Change to default(_) if this is not a reference type */;

            if (@params[0] != null)
                sql += " AND x.nombre=@param1 ";
           

            foreach (DataRow row in BDHelper.getBDHelper().ConsultaSQLConParametros(sql, @params).Rows)
                lst.Add(map_user(row));

            return lst;
        }

        // Funciones CRUD
        public bool create(Prestacion oPrestacion)
        {
            string str_sql;

            str_sql = "INSERT INTO Prestacions (nombre, cod_prestacion, descripcion) VALUES('";
            str_sql += oPrestacion.nombre + "','";
            str_sql += oPrestacion.cod_prestacion + "','";
            str_sql += oPrestacion.descripcion + "','";
            MessageBox.Show(str_sql);

            // Si una fila es afectada por la inserción retorna TRUE. Caso contrario FALSE
            return (BDHelper.getBDHelper().EjecutarSQL(str_sql) == 1);
        }
        // Funciones CRUD

        public bool update(Prestacion oPrestacion)
        {
            string str_sql;

            str_sql = "UPDATE Prestacions SET nombre = '";
            str_sql += oPrestacion.nombre + "','";
            str_sql += oPrestacion.cod_prestacion + "','";
            str_sql += oPrestacion.descripcion + "','";
            str_sql += " WHERE id_prestacion = " + oPrestacion.id_prestacion.ToString();
            MessageBox.Show(str_sql);

            // Si una fila es afectada por la actualización retorna TRUE. Caso contrario FALSE
            return (BDHelper.getBDHelper().EjecutarSQL(str_sql) == 1);
        }

        // Función auxiliar responsable de MAPPEAR una fila de Prestacions a un objeto Prestacion
        private Prestacion map_user(DataRow row)
        {
            Prestacion oPrestacion = new Prestacion();

            oPrestacion.id_prestacion = Convert.ToInt32(row["id_prestacion"].ToString());
            oPrestacion.nombre = row["nombre"].ToString();
            oPrestacion.cod_prestacion = row["cod_prestacion"].ToString();
            oPrestacion.descripcion = row["descripcion"].ToString();
            
            return oPrestacion;
        }
    }



}
