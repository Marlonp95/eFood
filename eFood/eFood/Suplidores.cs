using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utilidad;
using System.Windows.Forms;

namespace eFood
{
    public partial class Suplidores : Form
    {
        public Suplidores()
        {
            InitializeComponent();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtcodigo_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text)) return;

                string vSql = $"SELECT * From proveedor Where ficha Like ('%" + txtcodigo.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtcodigo.Text = dt.Tables[0].Rows[0]["id_proveedor"].ToString();
                }
                else
                {
                    MessageBox.Show("USUARIO NO ENCONTRADO");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error " + error);
                
            }
            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text)) return;

                string vSql = $"SELECT * From proveedor Where ficha Like ('%" + txtcodigo.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtcodigo.Text = dt.Tables[0].Rows[0]["id_proveedor"].ToString();
                }
                else
                {
                    MessageBox.Show("USUARIO NO ENCONTRADO");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error " + error);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string vSql = $"SELECT TOP 1 * FROM persona ORDER by id_persona DESC";
            DataSet dt = new DataSet();
            dt.ejecuta(vSql);
            bool correcto = dt.ejecuta(vSql);
            if (utilidades.DsTieneDatos(dt))
            {
                int codigo = Convert.ToInt32(dt.Tables[0].Rows[0]["id_persona"]);
                codigo++;
                txtcodigo.Text = Convert.ToString(codigo);

            }
            else
            {
                MessageBox.Show("CREAR EMPLEADO");
            }
        }
    }
}
