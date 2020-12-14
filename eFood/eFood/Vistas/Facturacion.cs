using eFood.Utils;
using Ex.OM.Extentions;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using utilidad;
using System.Linq;
using eFood.Vistas;
using eFood.Reportes;

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
            if (Globals.idAperturaCaja == 0)
            {
                MessageBox.Show("Debe tener caja aperturada para poder cobrar.", "Recordatorio");
            }

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
                txtcodcli.Text = row[0].Field<int>("id_cliente").ToString();
                comboCondicionpago.SelectedValue = row[0].Field<int>("id_condicion_pago");
                comboComprobante.SelectedValue = row[0].Field<int>("id_tipo_ncf");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultaClientes obj = new ConsultaClientes();
            if (obj.ShowDialog() == DialogResult.OK)
            {
                int pos = Convert.ToInt16(obj.dataCliente.CurrentCell.RowIndex);
                txtcodcli.Text = string.Empty;
                txtcodcli.Text = obj.dataCliente.Rows[pos].Cells[0].Value.ToString();
                txtcodcli.Focus();
                SendKeys.Send("{TAB}");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcodigo.Text))
            {
                MessageBox.Show("Seleccione articulo a facturar.");
                txtcodigo.Focus();
                return;
            }

            try
            {
                if (DataFactura.RowCount <= 0)
                {
                    var importe = Convert.ToDecimal(txtcantidad.Text) * Convert.ToDecimal(txtprecio.Text);
                    var itbis = Convert.ToDecimal(importe) * Convert.ToDecimal(txtItbis.Text);
                    DataFactura.Rows.Add(txtcodigo.Text, txtdescripcion.Text, txtcantidad.Text,  txtprecio.Text,  importe.ToString().Decimals(), itbis.ToString().Decimals());
                }
                //PARA DETERMINAR EXISTENCIA Y LA POSICION DE DICHO ARTICULO           
                else
                {
                    var dataRow = DataFactura.Rows.Cast<DataGridViewRow>().Where(x => x.Cells[0].Value.ToString() == txtcodigo.Text);

                    if (dataRow.Count() > 0)
                    {
                        var num_fila = dataRow.FirstOrDefault().Index;
                        DataFactura.Rows[num_fila].Cells[3].Value = (Convert.ToDecimal(txtcantidad.Text) + Convert.ToDecimal(DataFactura.Rows[num_fila].Cells[3].Value)).ToString().Decimals();
                        var importe = Convert.ToString(Convert.ToDecimal(DataFactura.Rows[num_fila].Cells[2].Value) * Convert.ToDecimal(DataFactura.Rows[num_fila].Cells[3].Value)).Decimals();
                        DataFactura.Rows[num_fila].Cells[4].Value = importe;
                        var itbis = Convert.ToString(Convert.ToDecimal(DataFactura.Rows[num_fila].Cells[4].Value) * Convert.ToDecimal(txtItbis.Text)).Decimals();
                        DataFactura.Rows[num_fila].Cells[5].Value = itbis;
                    }
                    else
                    {
                        var importe = Convert.ToDecimal(txtcantidad.Text) * Convert.ToDecimal(txtprecio.Text);
                        var itbis = Convert.ToDecimal(importe) * Convert.ToDecimal(txtItbis.Text);
                        DataFactura.Rows.Add(txtcodigo.Text, txtdescripcion.Text, txtcantidad.Text.Decimals(), txtprecio.Text.Decimals(), importe.ToString().Decimals(), itbis.ToString().Decimals());
                    }
                }

                //ME REALIZA LA SUMA QUE MUESTRO PARA EL TOTAL QUE VOY A FACTURAR
                CalcTotal();

                txtcodigo.Clear();
                txtdescripcion.Clear();
                txtprecio.Clear();
                txtcantidad.Clear();
                txtItbis.Clear();
                txtcodigo.Focus();
            }
            catch
            {
                MessageBox.Show("Error");
            }

        }

        void CalcTotal()
        {
            var total = 0m;

            foreach (DataGridViewRow Fila in DataFactura.Rows)
            {
                total += Convert.ToDecimal(Fila.Cells[4].Value);
            }

            lblTotal.Text = total.ToString().MoneyDecimal("RD$ ");


            var subTotal = total + (total * 0.18m) + (total * 0.10m);
            var itbis = total * 0.18m;
            var ley = total * 0.10m;

            lblLey.Text = ley.ToString().MoneyDecimal("RD$ ");
            lblSubTotal.Text = total.ToString().MoneyDecimal("RD$ ");
            lblItbis.Text = itbis.ToString().MoneyDecimal("RD$ ");
            lblTotal.Text = subTotal.ToString().MoneyDecimal("RD$ ");
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
                    txtItbis.Text = dt.Tables[0].Rows[0]["itbis"].ToString();
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

            if (DataFactura.Rows.Count > 0)
            {

                DataFactura.Rows.RemoveAt(DataFactura.CurrentRow.Index);
                CalcTotal();

                //contador--;
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
            int? idCobro;
            int idfact = 0;
            try
            {
                if (DataFactura.Rows.Count == 0 || NumFaact.Text == string.Empty) throw new Exception("Debe Cargar o Crear factura para procesar el cobro");


                cobro obj = new cobro(Convert.ToInt32(NumFaact.Text), Convert.ToInt32(comboFactura.SelectedValue), 1);
                obj.ShowDialog();

                var data = utilidades.ejecuta($@"select b.id
                                                 from temp_enc_factura a inner join enc_cobros b on a.id_factura = b.id_factura 
                                                where a.id_factura = {NumFaact.Text}
                                                  and a.total = b.monto
                                                  and a.id_tipo_factura = 1");

                if (data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    idCobro = row.Field<int>("id");

                    string vSql = $@"EXEC SetSecuenciaNCF {comboComprobante.SelectedValue}";
                    DataTable dt = new DataTable();
                    dt.ejecutaTransaccion(vSql);

                    string secuencia;
                    DateTime? vencimientoNCF = null;

                    if (dt.Rows.Count > 0)
                    {
                        secuencia = dt.Rows[0].Field<string>("secuencia_generada");
                    }
                    else throw new Exception("No hay secuencia para este tipo de comprobante");

                    vSql = $@"Select * from GetSecuenciaNCF ({comboComprobante.SelectedValue})";

                    var d = utilidades.ExecuteSQL(vSql);
                    if (d != null) vencimientoNCF = d.Rows[0].Field<DateTime?>("fecha_vencimiento");

                    using (var tran = utilidades.BeginTransation())
                    {
                        try
                        {
                            vSql = $@"EXEC actualiza_enc_factura  {comboFactura.SelectedValue}, {txtcodcli.Text.Trim()}, {1}, {1}, '{System.DateTime.Today}', '{txtRnc.Text.Trim()}',{comboComprobante.SelectedValue},'{secuencia}',{lblItbis.Text.Decimals()},
                                                                  {lblTotal.Text.Decimals()},{lblLey.Text.Decimals()}, {lblSubTotal.Text.Decimals()}, {0}, {Globals.IdUsuario},'{txtnomcli.Text}','{txtDireccion.Text}',{txttelefoo.Text.Nvl<string>("NULL")}, {0} ,'{vencimientoNCF}','{System.DateTime.Now}','{null}'";
                            var x = utilidades.ExecuteSQL(vSql);
                            var id = x.GetIdentity();
                            idfact = id;

                            foreach (DataGridViewRow fila in DataFactura.Rows)
                            {
                                string vSql2 = $"EXEC actualiza_det_factura {id}, {fila.Cells[0].Value},{2},{fila.Cells[2].Value},{fila.Cells[3].Value},{fila.Cells[4].Value},{1},{1},{0.18},{fila.Cells[5].Value},{Globals.IdUsuario},'{null}','{0}'";
                                utilidades.ExecuteSQL(vSql2);
                            }

                            string vSql3 = $"EXEC cerrar_factura_temporal {NumFaact.Text.Trim()},'{'C'}'";
                            utilidades.ExecuteSQL(vSql3);


                            var estadoMesa = utilidades.ExecuteSQL($@"select * from temp_enc_factura a inner join mesa b on a.id_mesa = b.id_mesa where a.estado IN('A', 'S') and a.id_mesa = {comboFactura.SelectedValue}");

                            if (estadoMesa.Rows.Count > 0)
                            {
                                MessageBox.Show("Esta mesa tiene facturas pendientes", "Mensaje");

                            }
                            else
                            {
                                utilidades.ExecuteSQL($@"update mesa set estado = 'D' where id_mesa = {comboFactura.SelectedValue}");
                            }

                            tran.Commit();
                            MessageBox.Show("Cobro efectuado correctamente.", "Mensaje.");
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            MessageBox.Show(ex.Message);
                        }


                        tran.ConectionClose();

                        dataCuentas.Rows.Clear();
                        DataFactura.Rows.Clear();
                        comboFactura.Focus();
                        lblItbis.Text = "RD$ 0.00";
                        lblLey.Text = "RD$ 0.00";
                        lblTotal.Text = "RD$ 0.00";
                        lblSubTotal.Text = "RD$ 0.00";
               
                    }
                }

                else throw new Exception("Cobro cancelado o no aplicado.");

                string vSqlFactura = $"EXEC datos_factura {idfact}";
                DataSet ds = new DataSet();
                ds.ejecuta(vSqlFactura);

                ReporteFactura reporte = new ReporteFactura();
                reporte.reportViewer1.LocalReport.DataSources[0].Value = ds.Tables[0];
                reporte.ShowDialog();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
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
                CalcTotal();
            }
            catch (NullReferenceException ex)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void comboFactura_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                dataCuentas.Rows.Clear();

                DataTable dt = new DataTable();
                string vSQL2 = $@"SELECT  temp_enc_factura.id_factura, persona.nombre1, persona.apellido1, temp_enc_factura.fecha_factura
                                  FROM temp_enc_factura INNER JOIN
                                     cliente ON temp_enc_factura.id_cliente = cliente.id_cliente INNER JOIN
                                     persona ON cliente.id_persona = persona.id_persona
                                     where id_mesa = {comboFactura.SelectedValue}
                                       and temp_enc_factura.estado = 'S'";

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

                string Vsql = $@"SELECT    temp_det_factura.id_plato, platos.plato, temp_det_factura.cantidad, temp_det_factura.precio, platos.itbis 
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
                        (Convert.ToInt32(Fila["precio"]) * Convert.ToInt32(Fila["cantidad"])).ToString("F"),
                        (Convert.ToInt32(Fila["precio"]) * Convert.ToInt32(Fila["itbis"])).ToString("F")
                    );
                }
            }

            CalcTotal();
        }

        private void dataCuentas_SelectionChanged(object sender, EventArgs e)
        {

            if (dataCuentas.Rows.Count > 0)
            {
                if (dataCuentas.CurrentRow == null) return;
                lblTotal.Text = "RD$0.00";
                DataFactura.Rows.Clear(); var a = dataCuentas;

                DataTable dt = new DataTable();
                string Vsql = $@"SELECT  temp_det_factura.id_plato, platos.plato, temp_det_factura.cantidad, temp_det_factura.precio, platos.itbis 
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
                       (Convert.ToInt32(Fila["precio"]) * Convert.ToInt32(Fila["cantidad"])).ToString("F"),
                       (Convert.ToDecimal(Fila["precio"]) * Convert.ToDecimal(Fila["itbis"])).ToString("F")
                    );
                }
            }
            CalcTotal();
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
            if (txtRnc.Text == string.Empty)
            {
                return;
            }
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

        private void button11_Click(object sender, EventArgs e)
        {
        
            int idfact = 0;

            if (DataFactura.Rows.Count <= 0)
            {
                MessageBox.Show("Debe selecionar articulos a facturar", "Mensaje");
                return;
            }

            if (Convert.ToInt16(comboCondicionpago.SelectedValue) == 1)
            {
                MessageBox.Show("Debe especificar dias de condicion de pago.", "Mensaje");
                return;
            }


            string vSql = $@"EXEC SetSecuenciaNCF {comboComprobante.SelectedValue}";
            DataTable dt = new DataTable();
            dt.ejecutaTransaccion(vSql);

            string secuencia;
            DateTime? vencimientoNCF = null;

            if (dt.Rows.Count > 0)
            {
                secuencia = dt.Rows[0].Field<string>("secuencia_generada");
            }
            else throw new Exception("No hay secuencia para este tipo de comprobante");


            var vencimientoFact = utilidades.ejecuta($"select dias from condiciones_pago where id_condicion_pago ={comboCondicionpago.SelectedValue}").Rows;
            int dias = vencimientoFact[0].Field<int>("dias");

            DateTime vencimientoFactura  = DateTime.Now.AddDays(dias);

            using (var tran = utilidades.BeginTransation())
            {
                try
                {
                    vSql = $@"Select * from GetSecuenciaNCF ({comboComprobante.SelectedValue})";

                    var d = utilidades.ExecuteSQL(vSql);
                    if (d != null) vencimientoNCF = d.Rows[0].Field<DateTime?>("fecha_vencimiento");

                    vSql = $@"EXEC actualiza_enc_factura  NULL ,  {txtcodcli.Text.Trim()}, {1}, {2}, '{System.DateTime.Today}', '{txtRnc.Text.Trim()}',{comboComprobante.SelectedValue},'{secuencia}',{lblItbis.Text.Decimals()},
                                                                  {lblTotal.Text.Decimals()},{lblLey.Text.Decimals()}, {lblSubTotal.Text.Decimals()}, {0}, {Globals.IdUsuario},'{txtnomcli.Text}','{txtDireccion.Text}',{txttelefoo.Text.Nvl<string>("NULL")}, {dias} ,'{vencimientoNCF}','{vencimientoFactura}','{null}'";
                    var x = utilidades.ExecuteSQL(vSql);
                    var id = x.GetIdentity();
                    idfact = id;

                    foreach (DataGridViewRow fila in DataFactura.Rows)
                    {
                        string vSql2 = $"EXEC actualiza_det_factura {id}, {fila.Cells[0].Value},{2},{fila.Cells[2].Value},{fila.Cells[3].Value},{fila.Cells[4].Value},{1},{1},{0.18},{fila.Cells[5].Value},{Globals.IdUsuario},'{null}','{0}'";
                        utilidades.ExecuteSQL(vSql2);
                    }
                    tran.Commit();
                    MessageBox.Show("Factura Generada.", "Mensaje.");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.Message);
                }

                tran.ConectionClose();

                dataCuentas.Rows.Clear();
                DataFactura.Rows.Clear();
                comboFactura.Focus();
                lblItbis.Text = "RD$ 0.00";
                lblLey.Text = "RD$ 0.00";
                lblTotal.Text = "RD$ 0.00";
                lblSubTotal.Text = "RD$ 0.00";
            }

            string vSqlFactura = $"EXEC datos_factura {idfact}";
            DataSet ds = new DataSet();
            ds.ejecuta(vSqlFactura);

            ReportefacturaCredito reporte = new ReportefacturaCredito();
            reporte.reportViewer1.LocalReport.DataSources[0].Value = ds.Tables[0];
            reporte.ShowDialog();


        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void txtRnc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ConsultaPlatos obj = new ConsultaPlatos();
            if (obj.ShowDialog() == DialogResult.OK)
            {
                int pos = Convert.ToInt16(obj.dataGridView1.CurrentCell.RowIndex);
                txtcodigo.Text = string.Empty;
                txtcodigo.Text = obj.dataGridView1.Rows[pos].Cells[0].Value.ToString();               
                txtcodigo.Focus();
                SendKeys.Send("{TAB}");
            }
        }

        private void txtcodcli_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcodcli.Text)) return;

                string vSql = $@"SELECT dbo.cliente.id_cliente ,dbo.persona.nombre1+' '+dbo.persona.apellido1+' '+dbo.persona.apellido2 nombre,dbo.persona.telefono, dbo.persona.documento ,dbo.cliente.rnc, dbo.tipo_ncf.id ,dbo.tipo_ncf.descripcion, dbo.cliente.excento_itbis, dbo.condiciones_pago.id_condicion_pago ,dbo.condiciones_pago.descripcion AS Condicion_pago, dbo.cliente.limite_credito, 
                                    dbo.cliente.porcentaje_mora, dbo.cliente.porcentaje_descuento
                           FROM dbo.cliente left JOIN  dbo.condiciones_pago ON dbo.cliente.id_condicion = dbo.condiciones_pago.id_condicion_pago
		                                    left JOIN  dbo.tipo_ncf ON dbo.cliente.id_tipo_ncf = dbo.tipo_ncf.tipo
		                                    left JOIN  dbo.persona ON dbo.cliente.id_persona = dbo.persona.id_persona 
                            where dbo.cliente.id_cliente = {txtcodcli.Text}";

                DataSet dt = new DataSet();
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtnomcli.Text = dt.Tables[0].Rows[0]["nombre"].ToString();
                    txtRnc.Text = dt.Tables[0].Rows[0]["rnc"].ToString();
                    if(dt.Tables[0].Rows[0]["id"].ToString() != string.Empty) comboComprobante.SelectedValue = dt.Tables[0].Rows[0]["id"].ToString();
                    comboCondicionpago.SelectedValue = dt.Tables[0].Rows[0]["id_condicion_pago"].ToString();
                    txttelefoo.Text = dt.Tables[0].Rows[0]["telefono"].ToString();
                    txtCedula.Text = dt.Tables[0].Rows[0]["documento"].ToString();
                }
                else
                {
                    MessageBox.Show("Cliente no existe, favor registrar", "Mensaje");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.Message);
            }
        }
    }

}
