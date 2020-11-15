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
using BarcodeLib;
using System.Drawing.Imaging;

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
                    item.BackColor = Color.White;
                }
            }
            foreach (Control item in panel1.Controls)
            {

                if (item is TextBox || item is ComboBox || item is DateTimePicker)
                {
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
                                                      $"'{txtdescripcion.Text.Trim()}'," +
                                                      $"'{txtcantidad.Text.Trim()}','{ txtreorden.Text.Trim()}','{combounidad.SelectedValue.ToString()}','{comboMarca.SelectedValue.ToString()}'";

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
            comboMarca.DataSource = utilidades.ejecuta("select id_marca, marca from marcas");
            comboMarca.DisplayMember = "marca";
            comboMarca.ValueMember = "id_marca";

            combotipo.DataSource = utilidades.ejecuta("select id_tipo, nombre_tipo from tipo_producto");
            combotipo.DisplayMember = "nombre_tipo";
            combotipo.ValueMember = "id_tipo";

            combounidad.DataSource = utilidades.ejecuta("select id_unidad, unidad from unidad");
            combounidad.DisplayMember = "unidad";
            combounidad.ValueMember = "id_unidad";

            traerArticulo();
                             
        }
        int codigo;


        private void tipoproductoBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        public void traerArticulo() 
        {
            DataTable dt = new DataTable();
            dt.ejecuta(@"Select a.id_productos, a.productos, d.descripcion,a.descripcion , b.unidad, c.marca 
                            from productos a inner join unidad b on a.id_unidad = b.id_unidad
                                             inner join marcas c on a.id_marca = c.id_marca 
                                             inner join tipo_producto d on a.id_productos = d.id_tipo");
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
            txtproducto.Clear();
            txtdescripcion.Clear();
            txtreorden.Clear();
            txtcodigo.Focus();

            string vSql = $"SELECT TOP 1 * FROM productos ORDER by id_productos DESC";
            DataSet dt = new DataSet();
            dt.ejecuta(vSql);
            bool correcto = dt.ejecuta(vSql);
            if (utilidades.DsTieneDatos(dt))
            {
                codigo = Convert.ToInt32(dt.Tables[0].Rows[0]["id_productos"]);
                codigo++;
                txtcodigo.Text = Convert.ToString(codigo);

            }
            else

            {
                MessageBox.Show("Producto No existe");
            }
           
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbuscar.Text.Trim()) == false)
            {
                    DataSet dt = new DataSet();
                    string vSql = "Select * From productos ";
                if (string.IsNullOrEmpty(txtbuscar.Text.Trim()) == false)
                {
                    vSql += "Where productos like ('%" + txtbuscar.Text.Trim() + "%')";
                    dt.ejecuta(vSql);
                    dataprueba.DataSource = dt.Tables[0];
                }
            }
            else
            {
                traerArticulo();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string vSql = $"SELECT * From productos Where id_productos Like('%" + txtcodigo.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    string codigobarras = dt.Tables[0].Rows[0]["id_productos"].ToString() + "-" + dt.Tables[0].Rows[0]["productos"].ToString().Trim();
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = false;
                    panelresultado.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, codigobarras.ToString(), Color.Black, Color.White, 300, 100);
                    button5.Enabled = true;
                }
                else
                {
                    MessageBox.Show("ERROR");
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("error" + error);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)panelresultado.BackgroundImage.Clone();

            SaveFileDialog CajaDeDiaologoGuardar = new SaveFileDialog();
            CajaDeDiaologoGuardar.AddExtension = true;
            CajaDeDiaologoGuardar.Filter = "Image PNG (*.png)|*.png";
            CajaDeDiaologoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(CajaDeDiaologoGuardar.FileName))
            {
                imgFinal.Save(CajaDeDiaologoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void txtcodigo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text)) return;

                string vSql = $"SELECT * From productos Where id_productos Like ('%" + txtcodigo.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtcodigo.Text = dt.Tables[0].Rows[0]["id_productos"].ToString();
                    txtproducto.Text = dt.Tables[0].Rows[0]["productos"].ToString();
                    combounidad.SelectedValue = dt.Tables[0].Rows[0]["unidad"].ToString();
                }
                else
                {
                    MessageBox.Show("PRODUCTO NO ENCONTRADO");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.Message);
            }

            try
            {
                string vSql = $"SELECT * From productos Where id_productos Like('%" + txtcodigo.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    string codigobarras = dt.Tables[0].Rows[0]["id_productos"].ToString() + "-" + dt.Tables[0].Rows[0]["productos"].ToString().Trim();
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = false;
                    panelresultado.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, codigobarras.ToString(), Color.Black, Color.White, 290, 90);
                    button5.Enabled = true;
                }
                else
                {
                    MessageBox.Show("ERROR");
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("error" + error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
