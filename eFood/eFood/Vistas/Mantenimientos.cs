using eFood.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eFood
{
    public partial class Mantenimientos : Form
    {
        public Mantenimientos()
        {
            InitializeComponent();
        }

        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panelFormulario.Controls.OfType<MiForm>().FirstOrDefault();

            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                panelFormulario.Controls.Add(formulario);
                panelFormulario.Tag = formulario;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                formulario.Show();
                formulario.BringToFront(); 
            }
            else
            {
                formulario.BringToFront();
            }
        }


            private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Usuarios>();
        }

        private void Mantenimientos_Load(object sender, EventArgs e)
        {

        }

        private void panelFormulario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Productos>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Suplidores>();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AbrirFormulario<empleados>();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Categorias>();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirFormulario<clientes>();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AbrirFormulario<secuencia_ncf>();
        }
    }
}
