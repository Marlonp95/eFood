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
    public partial class Facturacion : Form
    {
        public Facturacion()
        {
            InitializeComponent();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar Facturacion", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            try
            {
                string vSql = $"SELECT * From persona Where id_persona=" + login.codigo;
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto)
                {
                  
                    txtvendedor.Text = dt.Tables[0].Rows[0]["nombre1"].ToString() + " " + dt.Tables[0].Rows[0]["apellido1"].ToString().Trim(); ;
                }
            }
            catch(Exception error)
            {
                MessageBox.Show("error" + error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string cmd = "Select * From cliente ";
            //if (string.IsNullOrEmpty(textBox1.Text.Trim()) == false) ;
            //cmd += "Where Nom_Art like ('%" + textBox1.Text.Trim() + "%')";
            //DataSet ds = Utilidades.Utilidad.ejecuta(cmd);
            //dataGridView1.DataSource = ds.Tables[0];
        }
    }
    
}
