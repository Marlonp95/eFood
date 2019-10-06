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
            catch (Exception error)
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
        int contador;
        double total;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bool existe = false;
                int num_fila = 0;
                string vSql = $"Select cantidad, productos, reorden From productos Where id_productos = " + txtcodigo.Text.Trim();
                DataSet DS = new DataSet();
                DS.ejecuta(vSql);
             
                //CONTROLAR SI HAY SUFICIENTES ARTICULOS PARA LA VENTA 

                if (Convert.ToDouble(txtcantidad.Text.Trim()) > Convert.ToDouble(DS.Tables[0].Rows[0][0]))
                {
                    MessageBox.Show("NO HAY INVENTARIO PARA EL SIGUIENTE ARTICULO: " + DS.Tables[0].Rows[0][1] + "!", "Advertencia", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (Convert.ToDouble(DS.Tables[0].Rows[0][0]) - Convert.ToDouble(txtcantidad.Text.Trim()) < 0)
                    {
                        MessageBox.Show("NO HAY INVENTARIO PARA EL SIGUIENTE ARTICULO: " + DS.Tables[0].Rows[0][1] + "!", "Advertencia", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //CONTROLAR EL PUNTO DE REORDEN
                if (Convert.ToDouble(DS.Tables[0].Rows[0][0]) - Convert.ToDouble(txtcantidad.Text.Trim()) <= Convert.ToDouble(DS.Tables[0].Rows[0][2]))
                    MessageBox.Show("REALICE UN PEDIDO PARA: " + DS.Tables[0].Rows[0][1], "SE ESTA AGOTANDO EL SIGUIENTE PRODUCTO" + "!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //AGREGO EL ARTICULO
                if (contador == 0)
                {
                    dataGridView1.Rows.Add(txtcodigo.Text, txtdescripcion.Text, txtprecio.Text, txtcantidad.Text);
                    double importe = Convert.ToDouble(dataGridView1.Rows[contador].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contador].Cells[3].Value);
                    dataGridView1.Rows[contador].Cells[4].Value = importe;

                    contador++;
                }
                //PARA DETERMINAR EXISTENCIA Y LA POSICION DE DICHO ARTICULO

                else
                {
                    foreach (DataGridViewRow Fila in dataGridView1.Rows)
                    {
                        if (Fila.Cells[0].Value.ToString() == txtcodigo.Text)
                        {
                            existe = true;
                            num_fila = Fila.Index;
                        }
                    }
                    //SI ESTA AGREGADO ACTUALIZO CANTIDAD
                

                    if (existe == true)
                    {
                        string vSql1 = $"Select cantidad, productos, reorden From productos Where id_productos = " + txtcodigo.Text.Trim();
                        DataSet Dt = new DataSet();
                        Dt.ejecuta(vSql1);
                        //DataSet Dt = utilidades.ejecuta("Select Can_existente, Nom_Art, Pan_Reorden From Articulo Where Cod_Art = " + txtcodigo.Text.Trim());
                        //CONTROLAR SI HAY SUFICIENTES ARTICULOS PARA LA VENTA 

                        if (Convert.ToDouble(txtcantidad.Text.Trim()) > Convert.ToDouble(Dt.Tables[0].Rows[0][0]) || Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value) > Convert.ToDouble(Dt.Tables[0].Rows[0][0]))
                        {
                            MessageBox.Show("NO HAY INVENTARIO PARA EL SIGUIENTE ARTICULO: " + Dt.Tables[0].Rows[0][1] + "!", "Advertencia", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            if (Convert.ToDouble(Dt.Tables[0].Rows[0][0]) - Convert.ToDouble(txtcantidad.Text.Trim()) < 0)
                            {
                                MessageBox.Show("NO HAY INVENTARIO PARA EL SIGUIENTE ARTICULO: " + Dt.Tables[0].Rows[0][1] + "!", "Advertencia", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        //CONTROLAR EL PUNTO DE REORDEN
                        if (Convert.ToDouble(DS.Tables[0].Rows[0][0]) - Convert.ToDouble(txtcantidad.Text.Trim()) <= Convert.ToDouble(DS.Tables[0].Rows[0][2]))
                            MessageBox.Show("REALICE UN PEDIDO PARA: " + DS.Tables[0].Rows[0][1], "SE ESTA AGOTANDO EL SIGUIENTE PRODUCTO" + "!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.Rows[num_fila].Cells[3].Value = (Convert.ToDouble(txtcantidad.Text) + Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[3].Value)).ToString();
                        double importe = Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[3].Value);
                        dataGridView1.Rows[num_fila].Cells[4].Value = importe;
                    }

                    else
                    {
                        dataGridView1.Rows.Add(txtcodigo.Text, txtdescripcion.Text, txtprecio.Text, txtcantidad.Text);
                        double importe = Convert.ToDouble(dataGridView1.Rows[contador].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contador].Cells[3].Value);
                        dataGridView1.Rows[contador].Cells[4].Value = importe;

                        contador++;
                    }
                }
                //ME REALIZA LA SUMA QUE MUESTRO PARA EL TOTAL QUE VOY A FACTURAR
                total = 0;
                foreach (DataGridViewRow Fila in dataGridView1.Rows)
                {
                    total += Convert.ToDouble(Fila.Cells[4].Value);
                }

                label9.Text = "RD$ " + total.ToString();

                txtcodigo.Clear();
                txtdescripcion.Clear();
                txtprecio.Clear();
                txtcantidad.Clear();
                txtcodigo.Focus();


            }
            catch
            {
                MessageBox.Show("Error");
            }
        } 

        private void txtcodigo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text)) return;

                string vSql = $"SELECT * From productos Where id_productos Like ('%" + txtcodigo.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtcodigo.Text = dt.Tables[0].Rows[0]["id_productos"].ToString();
                    txtdescripcion.Text = dt.Tables[0].Rows[0]["productos"].ToString();
                    txtcantidad.Text = dt.Tables[0].Rows[0]["cantidad"].ToString();
                    txtprecio.Text = dt.Tables[0].Rows[0]["precio_comercial"].ToString();
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
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtnomcli.Clear();
            txtprecio.Clear();
            txttelefoo.Clear();
            txtdescripcion.Clear();
            txtcantidad.Clear();
            label9.Text = "RD$ 0.00";
            dataGridView1.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (contador > 0)
            {
                total = total - (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value));
                label9.Text = "RD$ " + total.ToString();

                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                contador--;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {
                TipoPago.Width = 130;
                TipoPago.Height = 135;
                if (TipoPago.Visible == false) utilidades.Animate(TipoPago, utilidades.Effect.roll, 100, 50);
            }
        }

        private void Facturacion_Click(object sender, EventArgs e)
        {

            if (TipoPago.Visible == true) utilidades.Animate(TipoPago, utilidades.Effect.roll, 100, 600);

        }
    }
    
}
