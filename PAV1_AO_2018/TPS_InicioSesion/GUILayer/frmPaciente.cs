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
    public partial class frmPaciente : Form
    {
        private PacienteService oPacienteService;

        public frmPaciente()
        {
            InitializeComponent();
            oPacienteService = new PacienteService();
        }

        private void frmPaciente_Load(object sender, EventArgs e)
        {
            //llenarCombo(cmbPerfil, BDHelper.getBDHelper().ConsultaSQL("SELECT * From Perfiles"), "n_perfil", "id_perfil");
        }

        //private void llenarCombo(ComboBox cmb, Object source, string display, String value)
        //{
        //    cmb.DataSource = source;
        //    cmb.DisplayMember = display;
        //    cmb.ValueMember = value;
        //    cmb.SelectedIndex = -1;
        //}

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmABMPacientes formulario = new frmABMPacientes();
            formulario.ShowDialog();
        }

              
        //private void chkTodos_CheckedChanged(object sender, EventArgs e)
        //{
        //    {
        //        if (chkTodos.Checked)
        //        {
        //            txtNombre.Enabled = false;
        //            txtApellido.Enabled = false;
                    
        //        }
        //        else
        //        {
        //            txtNombre.Enabled = true;
        //            txtApellido.Enabled = true;
        //        }
        //    }
        //}

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
                
                if (flag_filtros)
                    llenarGrilla(oPacienteService.consultarPacientesConFiltros(filters));
                else
                    MessageBox.Show("Debe ingresar al menos un criterio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                llenarGrilla(oPacienteService.consultarPacientes());
        }

        private void llenarGrilla(IList<Paciente> source)
        {
            dgvPaciente.Rows.Clear();
            foreach (Paciente oPaciente in source)
                dgvPaciente.Rows.Add(new string[] { oPaciente.nombre, oPaciente.apellido, oPaciente.nroDocumento, oPaciente.tipoDocumento, oPaciente.id_paciente.ToString(), oPaciente.nroDocumento, oPaciente.domicilio, oPaciente.telefono, oPaciente.obraSocial });
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmABMPacientes formulario = new frmABMPacientes();
            formulario.seleccionar_paciente(frmABMPacientes.Opcion.update, map_paciente_fila());
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        private void dgvPaciente_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            frmABMPacientes formulario = new frmABMPacientes();
            formulario.seleccionar_paciente(frmABMPacientes.Opcion.delete, map_paciente_fila());
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        private Paciente map_paciente_fila()
        {
            Paciente oPaciente = new Paciente();
            oPaciente.nombre = dgvPaciente.CurrentRow.Cells["col_nombre"].Value.ToString();
            oPaciente.apellido = dgvPaciente.CurrentRow.Cells["col_perfil"].Value.ToString();
            oPaciente.telefono = dgvPaciente.CurrentRow.Cells["col_telefono"].Value.ToString();
            oPaciente.fechaNacimiento = Convert.ToDateTime(dgvPaciente.CurrentRow.Cells["col_fechaNac"].Value.ToString());
            oPaciente.nroDocumento = dgvPaciente.CurrentRow.Cells["col_documento"].Value.ToString();
            oPaciente.tipoDocumento = dgvPaciente.CurrentRow.Cells["col_tipoDoc"].Value.ToString();
            oPaciente.id_paciente = int.Parse(dgvPaciente.CurrentRow.Cells["col_id"].Value.ToString());
            oPaciente.obraSocial = dgvPaciente.CurrentRow.Cells["col_obraSocial"].Value.ToString();
            oPaciente.domicilio = dgvPaciente.CurrentRow.Cells["col_domicilio"].Value.ToString();
            return oPaciente;
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que desea salir?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                this.Close();
        }

        private void chkTodos_CheckedChanged_1(object sender, EventArgs e)
        {
            {
                if (chkTodos.Checked)
                {
                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtDocumento.Enabled = false;

                }
                else
                {
                    txtNombre.Enabled = true;
                    txtApellido.Enabled = true;
                    txtDocumento.Enabled = true;
                }
            }
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            List<object> filters = new List<object>();
            bool flag_filtros = false;

            if (!chkTodos.Checked)
            {

                if (flag_filtros)
                    llenarGrilla(oPacienteService.consultarPacientesConFiltros(filters));
                else
                    MessageBox.Show("Debe ingresar al menos un criterio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                llenarGrilla(oPacienteService.consultarPacientes());
        }
    }
}
