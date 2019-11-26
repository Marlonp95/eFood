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

        public void mesa(Button btn)
        {
                char estado;
                string vSql = $"SELECT estado  FROM mesa where  id_mesa = " + btn.Tag.ToString();
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                estado = Convert.ToChar(dt.Tables[0].Rows[0]["estado"]);

            if (utilidades.DsTieneDatos(dt))
                {              
                    
                    if (MessageBox.Show("Desea Abrir Mesa " + btn.Tag.ToString(), "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        vSql = $"UPDATE mesa SET estado = 'O'  WHERE  id_mesa = " +btn.Tag.ToString(); ;
                        dt = new DataSet();
                        dt.ejecuta(vSql);
                        btn.BackColor = Color.Red;
                        AbrirFormulario<pedido>();
                    }
            }
        }
   

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            mesa(button2);
        }

        private void mesas_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mesa(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mesa(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mesa(button5);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mesa(button6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mesa(button7);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mesa(button8);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mesa(button9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mesa(button10);
        }


        private void button16_Click(object sender, EventArgs e)
        {
            mesa(button16);
        }
    }
}
