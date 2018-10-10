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

namespace PAV1_AO_2018.GUILayer
{
    public partial class frmNuevoOdontologo : Form
    {
        public frmNuevoOdontologo()
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
        private UsuarioService oUsuarioService = new UsuarioService();
        private Usuario oUsuarioSelected;


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string str_sql = "";
            Usuario oUsuario;

            switch (_action)
                {
                case Opcion.insert:
                    {
                        if (existe_nombre() == false)
                        {
                            if (validar_campos())
                            {
                                oUsuario = new Usuario();
                                oUsuario.nombreUsuario = txtNombreUsuario.Text;
                                oUsuario.password = txtPass.Text;
                                oUsuario.email = txtEmail.Text;
                                oUsuario.id_perfil = cmbPerfil.SelectedValue.ToString();
                                oUsuario.cod_tipoDoc = cmbTipoDocumento.SelectedValue.ToString();
                                oUsuario.nroDocumento = txtNumeroDoc.Text;
                                oUsuario.nroMatricula = txtMatricula.Text;
                                oUsuario.telefono = txtTelefono.Text;
                                oUsuario.estado = "S"; 


                                if (oUsuarioService.crearUsuario(oUsuario))
                                {
                                    MessageBox.Show("Usuario insertado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                        }
                        else
                            MessageBox.Show("Nombre de usuario encontrado!. Ingrese un nombre diferente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                    case Opcion.update:
                    {
                        if (validar_campos())
                        {
                            oUsuarioSelected.nombreUsuario = txtNombreUsuario.Text;
                            oUsuarioSelected.password = txtPass.Text;
                            oUsuarioSelected.email = txtEmail.Text;
                            oUsuarioSelected.id_perfil = cmbPerfil.SelectedValue.ToString();
                            oUsuarioSelected.cod_tipoDoc = cmbTipoDocumento.SelectedValue.ToString();
                            oUsuarioSelected.nroDocumento = txtNumeroDoc.Text;
                            oUsuarioSelected.nroMatricula = txtMatricula.Text;
                            oUsuarioSelected.telefono = txtTelefono.Text;
                            oUsuarioSelected.estado = "S";

                            if (oUsuarioService.actualizarUsuario(oUsuarioSelected))
                            {
                                MessageBox.Show("Usuario actualizado! ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Dispose();
                            }
                            else
                                MessageBox.Show("Error al actualizar el usuario! ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
                    case Opcion.delete:
                    {
                        if (MessageBox.Show("Seguro que desea deshabilitar el usuario seleccionado?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            oUsuarioSelected.nombreUsuario = txtNombreUsuario.Text;
                            oUsuarioSelected.password = txtPass.Text;
                            oUsuarioSelected.email = txtEmail.Text;
                            oUsuarioSelected.id_perfil = cmbPerfil.SelectedValue.ToString();
                            oUsuarioSelected.cod_tipoDoc = cmbTipoDocumento.SelectedValue.ToString();
                            oUsuarioSelected.nroDocumento = txtNumeroDoc.Text;
                            oUsuarioSelected.nroMatricula = txtMatricula.Text;
                            oUsuarioSelected.telefono = txtTelefono.Text;
                            oUsuarioSelected.estado = "N";

                            if (oUsuarioService.modificarEstadoUsuario(oUsuarioSelected))
                            {
                                MessageBox.Show("Usuario Deshabilitado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Dispose();
                            }
                            else
                                MessageBox.Show("Error al actualizar el usuario!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
            }
        }



        private void frmNuevoOdontologo_Load(object sender, EventArgs e)
        {
            llenarCombo(cmbPerfil, BDHelper.getBDHelper().ConsultaSQL("SELECT * From Perfiles"), "n_perfil", "id_perfil");
            llenarCombo(cmbTipoDocumento, BDHelper.getBDHelper().ConsultaSQL("SELECT * From TiposDocumento"), "n_tipo", "cod_tipo");
            switch (_action)
                {
                case Opcion.insert:
                    {
                        this.Text = "Nuevo Usuario";
                        break;
                    }
                    case Opcion.update:
                    {
                        this.Text = "Actualizar Usuario";
                        // Recuperar usuario seleccionado en la grilla 
                        mostrar_datos();
                        txtNombreUsuario.Enabled = true;
                        txtEmail.Enabled = true;
                        txtPass.Enabled = true;
                        txtMatricula.Enabled = true;
                        cmbPerfil.Enabled = true;
                        txtTelefono.Enabled = true;
                        txtNumeroDoc.Enabled = true;
                        cmbTipoDocumento.Enabled = true;
                        break;
                    }
                     case Opcion.delete:
                    {
                        mostrar_datos();
                        this.Text = "Habilitar/Deshabilitar Usuario";
                        txtNombreUsuario.Enabled = false;
                        txtEmail.Enabled = false;
                        txtEmail.Enabled = false;
                        txtPass.Enabled = false;
                        txtMatricula.Enabled = false;
                        cmbPerfil.Enabled = false;
                        txtTelefono.Enabled = false;
                        txtNumeroDoc.Enabled = false;
                        cmbTipoDocumento.Enabled = false;
                        break;
                    }

                }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void seleccionar_usuario(Opcion op, Usuario usuarioSelected)
        {
            _action = op;
            oUsuarioSelected = usuarioSelected;
        }

        private void mostrar_datos()
        {
            if (oUsuarioSelected != null)
            {
                txtNombreUsuario.Text = oUsuarioSelected.nombreUsuario;
                txtEmail.Text = oUsuarioSelected.email;
                txtPass.Text = oUsuarioSelected.password;
                txtNumeroDoc.Text = oUsuarioSelected.nroDocumento;
                txtTelefono.Text = oUsuarioSelected.telefono;
                cmbPerfil.Text = oUsuarioSelected.perfil;
                cmbTipoDocumento.Text = oUsuarioSelected.cod_tipoDoc;
                txtMatricula.Text = oUsuarioSelected.nroMatricula;

                
            }
        }

        private bool validar_campos()
        {
            // campos obligatorios
            if (txtNombreUsuario.Text == string.Empty)
            {
                txtNombreUsuario.BackColor = Color.Red;
                txtNombreUsuario.Focus();
                return false;
            }
            else
                txtNombreUsuario.BackColor = Color.White;

            if (txtPass.Text == string.Empty)
            {
                txtPass.BackColor = Color.Red;
                txtPass.Focus();
                return false;
            }
            else
                txtPass.BackColor = Color.White;

            if (cmbPerfil.Text == string.Empty)
            {
                cmbPerfil.BackColor = Color.Red;
                cmbPerfil.Focus();
                return false;
            }
            else
                cmbPerfil.BackColor = Color.White;

            if (txtNumeroDoc.Text == string.Empty)
            {
                txtNumeroDoc.BackColor = Color.Red;
                txtNumeroDoc.Focus();
                return false;
            }
            else
                txtNumeroDoc.BackColor = Color.White;

            if (cmbTipoDocumento.Text == string.Empty)
            {
                cmbTipoDocumento.BackColor = Color.Red;
                cmbTipoDocumento.Focus();
                return false;
            }
            else
                cmbTipoDocumento.BackColor = Color.White;

            return true;
        }

        private bool existe_nombre()
        {
            return (oUsuarioService.validarNombreUsuario(txtNombreUsuario.Text) != null);
        }

        private void llenarCombo(ComboBox cmb, Object source, string display, String value)
        {
            cmb.DataSource = source;
            cmb.DisplayMember = display;
            cmb.ValueMember = value;
            cmb.SelectedIndex = -1;
        }

        private void cmbPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbPerfil != 2)
            //{
            //    txtMatricula.Enabled = false;
            //}
        }
    }
}
