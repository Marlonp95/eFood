using eFood.Utils;
using Ex.OM.Extentions;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using utilidad;

namespace eFood
{
    public partial class Facturacion : Form
    {

        public string mesatag { get; set; }

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
            comboComprobante.DataSource = utilidades.ejecuta("select tipo, convert(varchar(max),tipo)+' - '+descripcion descripcion from tipo_ncf");
            comboComprobante.DisplayMember = "descripcion";
            comboComprobante.ValueMember = "tipo";

            comboFactura.DataSource = utilidades.ejecuta(" select id_mesa, descripcion from mesa");
            comboFactura.DisplayMember = "descripcion";
            comboFactura.ValueMember = "id_mesa";

            comboCondicionpago.DataSource = utilidades.ejecuta(" select id_condicion_pago, descripcion from condiciones_pago");
            comboCondicionpago.DisplayMember = "descripcion";
            comboCondicionpago.ValueMember = "id_condicion_pago";

            comboTipoFactura.DataSource = utilidades.ejecuta(" select id_tipo_factura, tipo_factura from tipo_factura");
            comboTipoFactura.DisplayMember = "tipo_factura";
            comboTipoFactura.ValueMember = "id_tipo_factura";


            comboFactura.DataSource = utilidades.ejecuta("Select descripcion,id_mesa From mesa");
            comboFactura.DisplayMember = "descripcion";
            comboFactura.ValueMember = "id_mesa";

            comboFactura.SelectedValue = 1;

            txtvendedor.Text = Globals.NombreUsuario;

            var row = utilidades.ejecuta($@"select * from vClientes where id_cliente= 6").Rows;
            if (row.Count > 0)
            {
                txtnomcli.Text = row[0].Field<string>("nombre1");
                comboCondicionpago.SelectedValue = row[0].Field<int>("id_condicion_pago");
                comboComprobante.SelectedValue = row[0].Field<int>("id_tipo_ncf");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtnomcli.Text.Trim()) == false)
            {

            }
            
            if (string.IsNullOrEmpty(txtcodcli.Text.Trim()) == false)
            {

            }
            
 
        }
        int contador;
        double total;

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NumFaact.Text.Trim()) == false)
            {
                try
                {
                    bool existe = false;
                    int num_fila = 0;
                    #region Control Reorden
                    //string vSql = $"Select cantidad, productos, reorden From productos Where id_productos = " + txtcodigo.Text.Trim();
                    //DataSet DS = new DataSet();
                    //DS.ejecuta(vSql);
                    ////CONTROLAR SI HAY SUFICIENTES ARTICULOS PARA LA VENTA 
                    //if (Convert.ToDouble(txtcantidad.Text.Trim()) > Convert.ToDouble(DS.Tables[0].Rows[0][0]))
                    //{
                    //    MessageBox.Show("NO HAY INVENTARIO PARA EL SIGUIENTE ARTICULO: " + DS.Tables[0].Rows[0][1] + "!", "Advertencia", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //else
                    //{
                    //    if (Convert.ToDouble(DS.Tables[0].Rows[0][0]) - Convert.ToDouble(txtcantidad.Text.Trim()) < 0)
                    //    {
                    //        MessageBox.Show("NO HAY INVENTARIO PARA EL SIGUIENTE ARTICULO: " + DS.Tables[0].Rows[0][1] + "!", "Advertencia", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //}
                    ////CONTROLAR EL PUNTO DE REORDEN
                    //if (Convert.ToDouble(DS.Tables[0].Rows[0][0]) - Convert.ToDouble(txtcantidad.Text.Trim()) <= Convert.ToDouble(DS.Tables[0].Rows[0][2]))
                    //    MessageBox.Show("REALICE UN PEDIDO PARA: " + DS.Tables[0].Rows[0][1], "SE ESTA AGOTANDO EL SIGUIENTE PRODUCTO" + "!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ////AGREGO EL ARTICULO
                    #endregion

                    if (DataFactura.RowCount <= 0)
                    {
                        DataFactura.Rows.Add(txtcodigo.Text, txtdescripcion.Text, txtprecio.Text, txtcantidad.Text);
                        double importe = Convert.ToDouble(DataFactura.Rows[contador].Cells[2].Value) * Convert.ToDouble(DataFactura.Rows[contador].Cells[3].Value);
                        DataFactura.Rows[contador].Cells[4].Value = importe;
                        contador++;
                    }
                    //PARA DETERMINAR EXISTENCIA Y LA POSICION DE DICHO ARTICULO           
                    else
                    {
                        foreach (DataGridViewRow Fila in DataFactura.Rows)
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
                            #region Reorden
                            //string vSql1 = $"Select cantidad, productos, reorden From productos Where id_productos = " + txtcodigo.Text.Trim();
                            //DataSet Dt = new DataSet();
                            //Dt.ejecuta(vSql1);
                            ////DataSet Dt = utilidades.ejecuta("Select Can_existente, Nom_Art, Pan_Reorden From Articulo Where Cod_Art = " + txtcodigo.Text.Trim());
                            ////CONTROLAR SI HAY SUFICIENTES ARTICULOS PARA LA VENTA 

                            //if (Convert.ToDouble(txtcantidad.Text.Trim()) > Convert.ToDouble(Dt.Tables[0].Rows[0][0]) || Convert.ToDouble(DataFactura.Rows[0].Cells[3].Value) > Convert.ToDouble(Dt.Tables[0].Rows[0][0]))
                            //{
                            //    MessageBox.Show("NO HAY INVENTARIO PARA EL SIGUIENTE ARTICULO: " + Dt.Tables[0].Rows[0][1] + "!", "Advertencia", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            //    return;
                            //}
                            //else
                            //{
                            //    if (Convert.ToDouble(Dt.Tables[0].Rows[0][0]) - Convert.ToDouble(txtcantidad.Text.Trim()) < 0)
                            //    {
                            //        MessageBox.Show("NO HAY INVENTARIO PARA EL SIGUIENTE ARTICULO: " + Dt.Tables[0].Rows[0][1] + "!", "Advertencia", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            //        return;
                            //    }
                            //}
                            //CONTROLAR EL PUNTO DE REORDEN
                            //if (Convert.ToDouble(DS.Tables[0].Rows[0][0]) - Convert.ToDouble(txtcantidad.Text.Trim()) <= Convert.ToDouble(DS.Tables[0].Rows[0][2]))
                            //MessageBox.Show("REALICE UN PEDIDO PARA: " + DS.Tables[0].Rows[0][1], "SE ESTA AGOTANDO EL SIGUIENTE PRODUCTO" + "!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            #endregion
                            DataFactura.Rows[num_fila].Cells[2].Value = (Convert.ToDecimal(txtcantidad.Text) + Convert.ToDecimal(DataFactura.Rows[num_fila].Cells[2].Value)).ToString().Decimals();
                            var importe = Convert.ToString(Convert.ToDecimal(DataFactura.Rows[num_fila].Cells[2].Value) * Convert.ToDecimal(DataFactura.Rows[num_fila].Cells[3].Value)).Decimals();
                            DataFactura.Rows[num_fila].Cells[4].Value = importe;
                        }

                        else
                        {
                            DataFactura.Rows.Add(txtcodigo.Text, txtdescripcion.Text, txtcantidad.Text.Decimals(),txtprecio.Text.Decimals()) ;
                            var importe = Convert.ToString(Convert.ToDecimal(DataFactura.Rows[contador].Cells[2].Value) * Convert.ToDecimal(DataFactura.Rows[contador].Cells[3].Value)).Decimals();
                            DataFactura.Rows[contador].Cells[4].Value = importe;

                            contador++;

                        }
                    }

                    //ME REALIZA LA SUMA QUE MUESTRO PARA EL TOTAL QUE VOY A FACTURAR
                    total = 0;
                    foreach (DataGridViewRow Fila in DataFactura.Rows)
                    {
                        total += Convert.ToDouble(Fila.Cells[4].Value);
                    }

                    lblTotal.Text = "RD$ " + total.ToString().Decimals();

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
            else
            {
                MessageBox.Show("Debe cargar Factura");
            }
        } 

        private void txtcodigo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text)) return;

                string vSql = $"SELECT * From platos Where id_plato Like ('%" + txtcodigo.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtcodigo.Text = dt.Tables[0].Rows[0]["id_plato"].ToString();
                    txtdescripcion.Text = dt.Tables[0].Rows[0]["plato"].ToString();
                    txtprecio.Text = dt.Tables[0].Rows[0]["precio"].ToString();
                    txtcantidad.Focus();
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
            lblTotal.Text = "RD$ 0.00";
            DataFactura.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (contador > 0)
            {
                total = total - (Convert.ToDouble(DataFactura.Rows[DataFactura.CurrentRow.Index].Cells[4].Value));
                lblTotal.Text = "RD$ " + total.ToString();

                DataFactura.Rows.RemoveAt(DataFactura.CurrentRow.Index);
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

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string vSql = $"EXEC datos_factura {NumFaact.Text}";
            DataSet dt = new DataSet();
            dt.ejecuta(vSql);

            reporte rp = new reporte();
            rp.reportViewer1.LocalReport.DataSources[0].Value = dt.Tables[0];
            rp.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

          
        }

        private void comboBox1_VisibleChanged(object sender, EventArgs e)
        {
            //lblTotal.Text = "RD$0.00";
            //DataPedido.Rows.Clear();

            //DataTable dt = new DataTable();

         
            //dt.ejecuta($"SELECT a.id_usuario, CONCAT( d.nombre1,' ',d.apellido1,' ',d.apellido2 ) as Nombre, a.id_cliente ,b.id_plato ,c.plato, c.precio, b.cantidad, b.cantidad * c.precio importe FROM temp_enc_factura a INNER JOIN temp_det_factura b on a.id_factura = b.id_factura INNER JOIN platos c on b.id_plato = c.id_plato INNER JOIN persona d on a.id_usuario = d.id_persona WHERE a.id_mesa  = {comboBox1.SelectedValue.ToString()}");


            //foreach (DataRow Fila in dt.Rows)
            //{
            //    DataPedido.Rows.Add
            //    (
            //        Convert.ToString(Fila["id_plato"]),
            //        Convert.ToString(Fila["plato"]),
            //        Convert.ToString(Fila["precio"]),
            //        Convert.ToString(Fila["cantidad"]),
            //        Convert.ToDecimal(Fila["importe"]).ToString("F"),
            //        Convert.ToString(Fila["precio"])
            //    );
            //}

            //foreach (DataGridViewRow Rows in DataPedido.Rows)
            //{
            //    total += Convert.ToDouble(Rows.Cells[4].Value);
            //}

            //lblTotal.Text = Convert.ToString(total);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }


        private void comboBox1_SelectedValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                var select = comboFactura.SelectedValue.ToString();
                lblTotal.Text = "RD$0.00";
                DataFactura.Rows.Clear();

                DataTable dt = new DataTable();


                dt.ejecuta($"SELECT a.id_usuario, CONCAT( d.nombre1,' ',d.apellido1,' ',d.apellido2 ) as Nombre, a.id_cliente ,b.id_plato ,c.plato, c.precio, b.cantidad, b.cantidad * c.precio importe FROM temp_enc_factura a INNER JOIN temp_det_factura b on a.id_factura = b.id_factura INNER JOIN platos c on b.id_plato = c.id_plato INNER JOIN persona d on a.id_usuario = d.id_persona WHERE a.id_mesa  = {comboFactura.SelectedValue};");


                foreach (DataRow Fila in dt.Rows)
                {
                    DataFactura.Rows.Add
                    (
                        Convert.ToString(Fila["id_plato"]),
                        Convert.ToString(Fila["plato"]),
                        Convert.ToString(Fila["precio"]),
                        Convert.ToString(Fila["cantidad"]),
                        Convert.ToDecimal(Fila["importe"]).ToString("F"),
                        Convert.ToString(Fila["precio"])
                    );
                }

                foreach (DataGridViewRow Rows in DataFactura.Rows)
                {
                    total += Convert.ToDouble(Rows.Cells[4].Value);
                }

                lblTotal.Text = Convert.ToString(total);
                total = 0;
            }
            catch (NullReferenceException ex)
            {
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());

            }
        }

        private void comboFactura_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                dataCuentas.Rows.Clear();

                DataTable dt = new DataTable();
                string vSQL2= $@"SELECT  temp_enc_factura.id_factura, persona.nombre1, persona.apellido1, temp_enc_factura.fecha_factura
                                  FROM temp_enc_factura INNER JOIN
                                     cliente ON temp_enc_factura.id_cliente = cliente.id_cliente INNER JOIN
                                     persona ON cliente.id_persona = persona.id_persona
                                     where id_mesa = {comboFactura.SelectedValue}";

                dt.ejecuta(vSQL2);
                foreach (DataRow Fila in dt.Rows)
                {
                    dataCuentas.Rows.Add
                    (
                        Convert.ToString(Fila["id_factura"]),
                        Convert.ToString(Fila["nombre1"]),
                        Convert.ToString(Fila["fecha_factura"])
                    );
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataCuentas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataCuentas.Rows.Count > 0)
            {
                if (dataCuentas.CurrentRow == null) return;
                lblTotal.Text = "RD$0.00";
                DataFactura.Rows.Clear();

                DataTable dt = new DataTable();
                var a = dataCuentas;

                string Vsql = $@"SELECT    temp_det_factura.id_plato, platos.plato, temp_det_factura.cantidad, temp_det_factura.precio
                                    FROM   temp_det_factura INNER JOIN
                             platos ON temp_det_factura.id_plato = platos.id_plato where id_factura ={dataCuentas.CurrentRow.Cells["id_factura"].Value}";
                dt.ejecuta(Vsql);
                foreach (DataRow Fila in dt.Rows)
                {
                    DataFactura.Rows.Add
                    (
                        Convert.ToString(Fila["id_plato"]),
                        Convert.ToString(Fila["plato"]),
                        Convert.ToString(Fila["cantidad"]),
                        Convert.ToString(Fila["precio"]),
                        (Convert.ToInt32(Fila["precio"]) * Convert.ToInt32(Fila["cantidad"])).ToString("F")
                    );
                }
            }

            foreach (DataGridViewRow Rows in DataFactura.Rows)
            {
                total += Convert.ToDouble(Rows.Cells[4].Value);
            }

            lblTotal.Text = Convert.ToString(total);
            total = 0;
        }

        private void dataCuentas_SelectionChanged(object sender, EventArgs e)
        {

            if (dataCuentas.Rows.Count > 0)
            {
                if (dataCuentas.CurrentRow == null) return;
                lblTotal.Text = "RD$0.00";
                DataFactura.Rows.Clear();

                DataTable dt = new DataTable();
                var a = dataCuentas;

                string Vsql = $@"SELECT  temp_det_factura.id_plato, platos.plato, temp_det_factura.cantidad, temp_det_factura.precio
                                    FROM temp_det_factura INNER JOIN
                               platos ON temp_det_factura.id_plato = platos.id_plato where id_factura = {dataCuentas.CurrentRow.Cells["id_factura"].Value}";
                dt.ejecuta(Vsql);

                    NumFaact.Text = dataCuentas.CurrentRow.Cells["id_factura"].Value.ToString();

                foreach (DataRow Fila in dt.Rows)
                {
                    DataFactura.Rows.Add
                    (
                        Convert.ToString(Fila["id_plato"]),
                        Convert.ToString(Fila["plato"]),
                        Convert.ToString(Fila["cantidad"]),
                        Convert.ToString(Fila["precio"]),
                        (Convert.ToInt32(Fila["precio"]) * Convert.ToInt32(Fila["cantidad"])).ToString("F")
                    );
                }
            }

            foreach (DataGridViewRow Rows in DataFactura.Rows)
            {
                total += Convert.ToDouble(Rows.Cells[4].Value);
            }
            double subTotal = total + (total * 0.18) + (total * 0.10) ;
            double itbis = total * 0.18;

            lblSubTotal.Text =total.ToString().MoneyDecimal("RD$ ");
            lblItbis.Text = itbis.ToString().MoneyDecimal("RD$ ");
            lblTotal.Text = subTotal.ToString().MoneyDecimal("RD$ ");

            total = 0;
    }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void NumFaact_TextChanged(object sender, EventArgs e)
        {

        }

        private void Facturacion_Click_1(object sender, EventArgs e)
        {
            if (TipoPago.Visible == true) utilidades.Animate(TipoPago, utilidades.Effect.roll, 100, 50);
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            var rnc = txtRnc.Text;
            var result = Metodos.ValidarDocumento(rnc);
            if (result != true)
            {
                MessageBox.Show("RNC No Valido");
                txtRnc.Text = string.Empty;
            }
            
        }

        private void txtCedula_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCedula.OriginalValue))
            {
                return;
            }
            else
            {
                var documento = txtCedula.OriginalValue;
                var result = Metodos.ValidarDocumento(documento);
                if (result != true)
                {
                    MessageBox.Show("Cedula No Valida");
                    txtRnc.Text = string.Empty;
                }
            }
        
        }
    }
    
}
