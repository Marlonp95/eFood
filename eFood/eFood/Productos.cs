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
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }

      
     

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            { 
                string vSql = $"EXEC actualizaproductos '{txtcodigo.Text.Trim()}','{txtproducto.Text.Trim()}','{combotipo.SelectedValue.ToString()}'," +
                                                      $"'{txtdescripcion.Text.Trim()}','{dateTimePicker2.Value.Date}','{dateTimePicker1.Value.Date}'," +
                                                      $"'{txtcantidad.Text.Trim()}','{ txtreorden.Text.Trim()}','{combounidad.SelectedValue.ToString()}','{txtprecio.Text.Trim()}'";
                
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Se Guardo "); traerArticulo(); }
                else MessageBox.Show("Error Salvando datos ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
            
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'efoodDataSet1.unidad' table. You can move, or remove it, as needed.
            this.unidadTableAdapter.Fill(this.efoodDataSet1.unidad);
            // TODO: This line of code loads data into the 'efoodDataSet.tipo_producto' table. You can move, or remove it, as needed.
            this.tipo_productoTableAdapter.Fill(this.efoodDataSet.tipo_producto);
            traerArticulo();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tipoproductoBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        public void traerArticulo() {
            DataTable dt = new DataTable();
            dt.ejecuta("Select * from productos");
            dataprueba.DataSource = dt;
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void txtdescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
