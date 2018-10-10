using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using PAV1_AO_2018.BusinessLayer;
using PAV1_AO_2018.BusinessLayer.Services;

namespace PAV1_AO_2018.GUILayer
{
    public partial class frmOdontologos : Form
    {
        private UsuarioService oUsuarioService;
        public frmOdontologos()
        {
            InitializeComponent();
            oUsuarioService = new UsuarioService();
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void frmOdontologos_Load(object sender, EventArgs e)
        {
            llenarCombo(cmbPerfil, BDHelper.getBDHelper().ConsultaSQL("SELECT * From Perfiles"), "n_perfil", "id_perfil");
        }

        private void llenarCombo(ComboBox cmb, Object source, string display, String value)
        {
            cmb.DataSource = source;
            cmb.DisplayMember = display;
            cmb.ValueMember = value;
            cmb.SelectedIndex = -1;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmNuevoOdontologo formulario = new frmNuevoOdontologo();
            formulario.ShowDialog();
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (chkTodos.Checked)
                {
                    txtNombre.Enabled = false;
                    cmbPerfil.Enabled = false;
                }
                else
                {
                    txtNombre.Enabled = true;
                    cmbPerfil.Enabled = true;
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Confirmación de salida.
            if (MessageBox.Show("Seguro que desea salir?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                this.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            List<object> filters = new List<object>();
            bool flag_filtros = false;

            if (!chkTodos.Checked)
            {
                // Validar si el combo 'Perfil' esta seleccionado.
                if (cmbPerfil.Text != string.Empty)
                {
                    // Si el cmb tiene un texto no vacìo entonces recuperamos el valor de la propiedad ValueMember
                    filters.Add(cmbPerfil.SelectedValue);
                    flag_filtros = true;
                }
                else
                    filters.Add(null);

                // Validar si el combo 'Perfil' esta seleccionado.
                if (txtNombre.Text != string.Empty)
                {
                    // Si el cmb tiene un texto no vacìo entonces recuperamos el valor de la propiedad ValueMember
                    filters.Add(txtNombre.Text);
                    flag_filtros = true;
                    btnEditar.Enabled = true;
                    btnEliminar.Enabled = true;
                }
                else
                    filters.Add(null);

                if (flag_filtros)
                    llenarGrilla(oUsuarioService.consultarUsuariosConFiltros(filters));
                else
                    MessageBox.Show("Debe ingresar al menos un criterio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                llenarGrilla(oUsuarioService.consultarUsuarios());
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
        }

        private void llenarGrilla(IList<Usuario> source)
        {
            dgvUsers.Rows.Clear();
            foreach (Usuario oUsuario in source)
                dgvUsers.Rows.Add(new string[] { oUsuario.id_usuario.ToString(), oUsuario.password, oUsuario.nombreUsuario,  oUsuario.perfil, oUsuario.tipoDocumento, oUsuario.nroDocumento,  oUsuario.nroMatricula, oUsuario.telefono, oUsuario.email ,oUsuario.id_perfil, oUsuario.estado});
        }

        private void btnEditar_Click(System.Object sender, System.EventArgs e)
        {
            frmNuevoOdontologo formulario = new frmNuevoOdontologo();
            formulario.seleccionar_usuario(frmNuevoOdontologo.Opcion.update, map_usuario_fila());
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        //private void dgvUsers_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    btnEditar.Enabled = true;
        //    btnEliminar.Enabled = true;
        //}

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            frmNuevoOdontologo formulario = new frmNuevoOdontologo();
            formulario.seleccionar_usuario(frmNuevoOdontologo.Opcion.delete, map_usuario_fila());
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        private Usuario map_usuario_fila()
        {
            Usuario oUsuario = new Usuario();
            oUsuario.id_usuario = int.Parse(dgvUsers.CurrentRow.Cells["col_id"].Value.ToString());
            oUsuario.password = dgvUsers.CurrentRow.Cells["col_pass"].Value.ToString();
            oUsuario.nombreUsuario = dgvUsers.CurrentRow.Cells["col_nombre"].Value.ToString();
            oUsuario.perfil = dgvUsers.CurrentRow.Cells["col_perfil"].Value.ToString();
            oUsuario.cod_tipoDoc = dgvUsers.CurrentRow.Cells["col_tipoDoc"].Value.ToString();
            oUsuario.nroDocumento = dgvUsers.CurrentRow.Cells["col_documento"].Value.ToString();
            oUsuario.nroMatricula = dgvUsers.CurrentRow.Cells["col_nroMatricula"].Value.ToString();
            oUsuario.telefono = dgvUsers.CurrentRow.Cells["col_telefono"].Value.ToString();
            oUsuario.email = dgvUsers.CurrentRow.Cells["col_email"].Value.ToString();
            oUsuario.estado = dgvUsers.CurrentRow.Cells["col_estado"].Value.ToString();
            

            //oUsuario.id_perfil = dgvUsers.CurrentRow.Cells["id_perfil_col"].Value.ToString();
            return oUsuario;
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
