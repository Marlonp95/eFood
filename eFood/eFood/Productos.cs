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
        {   //VALIDAR FORMULARIO
            bool ValidaFormulario = false;
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox || item is ComboBox || item is DateTimePicker)
                {
                    item.BackColor = Color.White ;
                }
                }
            foreach (Control item in panel1.Controls)
            {
             
                if (item is TextBox || item is ComboBox || item is DateTimePicker ) {
                    if (item.Tag.ToString().ToUpper() == "NO VACIO".ToUpper() && string.IsNullOrEmpty(item.Text.Trim()))
                    {
                        item.BackColor = Color.Red;
                        ValidaFormulario = true; 
                    }
                }
            }

            if (ValidaFormulario)
            {
                MessageBox.Show("Por favor Complete los campos");
                return;
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string vSql = $"EXEC eliminararticulos '{txtcodigo.Text.Trim()}'";

                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Se Elimino El Articulo "); traerArticulo(); }
                else MessageBox.Show("Error Eliminando datos ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtcantidad.Clear();
            txtcodigo.Clear();
            txtdescripcion.Clear();
            txtprecio.Clear();
            txtreorden.Clear();
            txtcodigo.Focus();
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtbuscar.Text.Trim())==false)
            {
                try
                {
                    string vSql = $"SELECT * From productos Where productos Like ('%"+txtbuscar.Text.Trim()+"%') ";

                    DataSet dt = new DataSet();
                    dt.ejecuta(vSql);
                    bool correcto = dt.ejecuta(vSql);
                    if (correcto) {traerArticulo();}
                    dataprueba.DataSource = dt.Tables[0];
                }
                catch(Exception error)
                {
                    MessageBox.Show("Error Al Consultar " + error.Message);
                }
            }

        }
    }
}
