using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAV1_AO_2018.GUI
{
    public partial class frmPrestacion : Form
    {
        private BDHelper Helper;
        public frmPrestacion()
        {
            InitializeComponent();
            Helper = new BDHelper();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //frmABMPrestaciones formulario = new frmABMPrestaciones();
            //formulario.ShowDialog();
        }

       
       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void frmPrestacion_Load_1(object sender, EventArgs e)
        {
        
        }

        private void bteditar_Click(object sender, EventArgs e)
        {
        
        }

        private void bteliminar_Click(object sender, EventArgs e)
        {
        
        }

        private void chktodos_CheckedChanged(object sender, EventArgs e)
     
        {
           {
              if (chk_todos.Checked)
              {
                 txtNombre.Enabled = false;

              }
              else
              {
                    txtNombre.Enabled = true;
              }
 
          }
       }

        

        
     
        
    }
}
