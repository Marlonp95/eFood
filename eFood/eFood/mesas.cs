using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using utilidad;

namespace eFood
{
    public partial class mesas : Form
    {
        public mesas()
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
                formulario.BringToFront(); ;
            }
            else
            {
                formulario.BringToFront();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
          
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Abrir Mesa 1 ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                
                 char estado;
                string vSql = $"SELECT estado  FROM mesa where  id_mesa =1 ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);

                if (utilidades.DsTieneDatos(dt))
                {
                    estado = Convert.ToChar(dt.Tables[0].Rows[0]["estado"]);
                    if (estado == 'O') {
                        MessageBox.Show("Mesa Ocupada");

                    }
                       else  if (estado == 'D')
                    {
                        vSql = $"UPDATE mesa SET estado = 'O'  WHERE  id_mesa = 1 ";
                        dt = new DataSet();
                        dt.ejecuta(vSql);
                        button1.BackColor = Color.Red;
                        AbrirFormulario<pedido>();
                    }
                }
               
             
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test");
            AbrirFormulario<pedido>();
        }

        private void mesas_Load(object sender, EventArgs e)
        {

        }
    }
}
