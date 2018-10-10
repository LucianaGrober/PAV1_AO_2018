using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PAV1_AO_2018.BusinessLayer;
using PAV1_AO_2018.BusinessLayer.Services;
using System.Data.Sql;

namespace PAV1_AO_2018.GUILayer
{
    public partial class frmABMPacientes : Form
    {
  
        public frmABMPacientes()
        {
            InitializeComponent();
        }

        public enum Opcion
        {
            insert,
            update,
            delete
        }

        private Opcion _action = Opcion.insert;
        private PacienteService oPacienteService = new PacienteService();
        private Paciente oPacienteSelected;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string str_sql = "";
            Paciente oPaciente;

            switch (_action)
            {
                case Opcion.insert:
                    {
                        if (existe_nombre() == false)
                        {
                            if (validar_campos())
                            {
                                oPaciente = new Paciente();
                                oPaciente.nombre = txtNombre.Text;
                                oPaciente.apellido = txtApellido.Text;
                                //oPaciente.fechaNacimiento =dtpFechaNac.Value().ToString();
                                oPaciente.cod_tipoDoc = cmbTipodoc.SelectedValue.ToString();
                                oPaciente.nroDocumento = txtNrodoc.Text;
                                oPaciente.domicilio = txtDomicilio.Text;
                                oPaciente.telefono = txtTelefono.Text;
                                oPaciente.obraSocial = txtobraSocial.Text;
                                oPaciente.estado = "S";

                               
                                if (oPacienteService.crearPaciente(oPaciente))
                                {
                                    MessageBox.Show("Paciente insertado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                        }
                        else
                            MessageBox.Show("Nombre de paciente encontrado!. Ingrese un nombre diferente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                case Opcion.update:
                    {
                        if (validar_campos())
                        {
                            oPacienteSelected.nombre = txtNombre.Text;
                            oPacienteSelected.apellido = txtApellido.Text;
                            //oPacienteSelected.fechaNacimiento = dtpFechaNac.SelectedValue.ToString();
                            oPacienteSelected.cod_tipoDoc = cmbTipodoc.SelectedValue.ToString();
                            oPacienteSelected.nroDocumento = txtNrodoc.Text;
                            oPacienteSelected.domicilio = txtDomicilio.Text;
                            oPacienteSelected.telefono = txtTelefono.Text;
                            oPacienteSelected.obraSocial = txtobraSocial.Text;
                            oPacienteSelected.estado = "S";
                        
                            if (oPacienteService.actualizarPaciente(oPacienteSelected))
                            {
                                MessageBox.Show("Paciente actualizado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Dispose();
                            }
                            else
                                MessageBox.Show("Error al actualizar el paciente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
                case Opcion.delete:
                    {
                        if (MessageBox.Show("Seguro que desea deshabilitar el Paciente seleccionado?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            oPacienteSelected.nombre = txtNombre.Text;
                            oPacienteSelected.apellido = txtApellido.Text;
                           // oPacienteSelected.fechaNacimiento = dtpFechaNac.DateTimePicker.ToString();
                           
                          
                            oPacienteSelected.cod_tipoDoc = cmbTipodoc.SelectedValue.ToString();
                            oPacienteSelected.nroDocumento = txtNrodoc.Text;
                            oPacienteSelected.domicilio = txtDomicilio.Text;
                            oPacienteSelected.obraSocial = txtobraSocial.Text;
                            oPacienteSelected.telefono = txtTelefono.Text;
                            oPacienteSelected.estado = "N";

                            if (oPacienteService.modificarEstadoPaciente(oPacienteSelected))
                            {
                                MessageBox.Show("Paciente Deshabilitado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Dispose();
                            }
                            else
                                MessageBox.Show("Error al actualizar el Paciente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
           this.Close();
        }

       

        private void frmABMPacientes_Load(object sender, EventArgs e)
        {
            llenarCombo(cmbTipodoc, BDHelper.getBDHelper().ConsultaSQL("SELECT * From TiposDocumento"), "n_tipo", "cod_tipo");
            switch (_action)
            {
                case Opcion.insert:
                    {
                        this.Text = "Nuevo Paciente";
                        break;
                    }
                case Opcion.update:
                    {
                        this.Text = "Actualizar Paciente";
                        // Recuperar Paciente seleccionado en la grilla 
                        mostrar_datos();
                        txtNombre.Enabled = true;
                        txtApellido.Enabled = true;
                        dtpFechaNac.Enabled = true;
                        txtNrodoc.Enabled = true;
                        cmbTipodoc.Enabled = true;
                        txtDomicilio.Enabled = true;
                        txtTelefono.Enabled = true;
                        txtobraSocial.Enabled = true;

                        break;
                    }
                case Opcion.delete:
                    {
                        mostrar_datos();
                        this.Text = "Habilitar/Deshabilitar Paciente";
                        txtNombre.Enabled = false;
                        txtApellido.Enabled = false;
                        dtpFechaNac.Enabled = false;
                        txtNrodoc.Enabled = false;
                        cmbTipodoc.Enabled = false;
                        txtDomicilio.Enabled = false;
                        txtTelefono.Enabled = false;
                        txtobraSocial.Enabled = false;
                        break;
                    }

            }
        }
        public void seleccionar_paciente(Opcion op, Paciente pacienteSelected)
        {
            _action = op;
            oPacienteSelected = pacienteSelected;
        }

        private void mostrar_datos()
        {
            if (oPacienteSelected != null)
            {
                txtNombre.Text = oPacienteSelected.nombre;
                txtApellido.Text = oPacienteSelected.apellido;
                //dtpFechaNac.Text = oPacienteSelected.fechaNacimiento;
                txtNrodoc.Text = oPacienteSelected.nroDocumento;
                cmbTipodoc.Text = oPacienteSelected.cod_tipoDoc;
                txtDomicilio.Text = oPacienteSelected.domicilio;
                txtTelefono.Text = oPacienteSelected.telefono;
                txtobraSocial.Text = oPacienteSelected.obraSocial;

            }
        }

        private bool validar_campos()
        {
            // campos obligatorios
            if (txtNombre.Text == string.Empty)
            {
                txtNombre.BackColor = Color.Red;
                txtNombre.Focus();
                return false;
            }
            else
                txtNombre.BackColor = Color.White;

            if (txtNrodoc.Text == string.Empty)
            {
                txtNrodoc.BackColor = Color.Red;
                txtNrodoc.Focus();
                return false;
            }
            else
                txtNrodoc.BackColor = Color.White;

            if (cmbTipodoc.Text == string.Empty)
            {
                cmbTipodoc.BackColor = Color.Red;
                cmbTipodoc.Focus();
                return false;
            }
            else
                cmbTipodoc.BackColor = Color.White;

            return true;
        }
        private bool existe_nombre()
        {
            return (oPacienteService.validarNombrePaciente(txtNombre.Text) != null);
        }

        private void llenarCombo(ComboBox cmb, Object source, string display, String value)
        {
            cmb.DataSource = source;
            cmb.DisplayMember = display;
            cmb.ValueMember = value;
            cmb.SelectedIndex = -1;
        }

    }
}
