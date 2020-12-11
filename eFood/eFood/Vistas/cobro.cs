using eFood.Utils;
using Ex.OM.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using utilidad;

namespace eFood.Vistas
{
    public partial class cobro : Form
    {
        int facturaNo;
        int mesaNo;
        int idApertura;
        decimal tasa;
        decimal totalFactura;
        decimal devolver;
        int tipoFactura;

        public cobro(int pFctura, int pMesa, int ptipoFactura)
        {
            facturaNo = pFctura;
            mesaNo = pMesa;
            tipoFactura = ptipoFactura;
            InitializeComponent();
        }

        private void cobro_Load(object sender, EventArgs e)
        {
            comboTipoPago.DataSource = utilidades.ejecuta("select id, descripcion from tipo_cobro where desactivado = 'N'");
            comboTipoPago.DisplayMember = "descripcion";
            comboTipoPago.ValueMember = "id";


            comboMoneda.DataSource = utilidades.ejecuta("select id, abreviatura from monedas where desactivado = 'N'");
            comboMoneda.DisplayMember = "abreviatura";
            comboMoneda.ValueMember = "id";

            txtTarjetaNo.Enabled = false;
            comboBanco.Enabled = false;

            var data = utilidades.ejecuta($@"select total from temp_enc_factura where id_factura = {facturaNo}").Rows;
            totalFactura = data[0].Field<decimal>("total");
            lblTotal.Text = totalFactura.ToString().MoneyDecimal();
            NumFact.Text = facturaNo.ToString();

            txtMonto.Focus();
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                this.Close();
            }
        }

        //Agrerar Monto Y Metodo Pago
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == string.Empty)
            {
                MessageBox.Show("Digite el monto a cobrar.");
                return;
            }

            var data = utilidades.ejecuta($@"select tasa_del_dia from monedas where id = {comboMoneda.SelectedValue}").Rows;

            tasa = data[0].Field<decimal>("tasa_del_dia");

            dataPago.Rows.Add(new string[]
            {
                comboTipoPago.SelectedValue.ToString(),
                comboTipoPago.Text.ToString(),
                txtMonto.Text.Decimals(),
                tasa.ToString(),
                Convert.ToString(tasa*Convert.ToDecimal(txtMonto.Text)),
                comboMoneda.SelectedValue.ToString(),
                comboMoneda.Text
            });

            CalcTotal();

        }

        void CalcTotal()
        {
            var totalPagado = 0m;
            foreach (DataGridViewRow Fila in dataPago.Rows)
            {
                totalPagado += Convert.ToDecimal(Fila.Cells[4].Value);
            }

            lblPagado.Text = totalPagado.ToString().MoneyDecimal();
            devolver = (totalFactura - totalPagado);
            lblDevolver.Text = devolver.ToString().MoneyDecimal();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboTipoPago_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboTipoPago.SelectedValue.ToString() == "2")
            {
                txtTarjetaNo.Enabled = true;
                comboBanco.Enabled = true;
                comboMoneda.Enabled = false;
            }
            else
            {
                txtTarjetaNo.Enabled = false;
                comboBanco.Enabled = false;
                comboMoneda.Enabled = true;
            }
        }

        //APLICAR COBROS
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Globals.idAperturaCaja == 0)
                {
                    MessageBox.Show("La caja no esta aperturada para este usuario", "Alerta!");
                    return;
                }

                if (dataPago.Rows.Count <= 0)
                {
                    MessageBox.Show("No hay datos para realizar pago.");
                    return;
                }

                var row = utilidades.ejecuta($@"select a.id_factura, a.total, b.monto
                                             from temp_enc_factura a inner join enc_cobros b on a.id_factura = b.id_factura 
                                            where a.id_factura = {NumFact.Text.Trim()}
                                              and a.total = b.monto
                                              and a.id_tipo_factura = 1");

                if (row.Rows.Count > 0)
                {
                    throw new Exception("Esta factura ya fue cobrada");
                }

                if (tipoFactura == 1)
                {
                    if (totalFactura > Convert.ToDecimal(lblPagado.Text.Decimals()))
                    {
                        MessageBox.Show("El monto pagado es menor al monto total de la factura","Factura Contado");
                        return;
                    }
                }
              
                string vSql = $"EXEC actualiza_enc_cobro {Globals.idAperturaCaja},{facturaNo},{totalFactura},{0},{devolver},{0},'{System.DateTime.Now}','{null}',{Globals.IdUsuario}";

                using (var tran = utilidades.BeginTransation())
                {
                    try
                    {
                        var data = utilidades.ExecuteSQL(vSql);
                        var id = data.GetIdentity();

                        foreach (DataGridViewRow fila in dataPago.Rows)
                        {
                            string vSql2 = $"EXEC actualiza_det_cobros {id},{fila.Cells[0].Value},{fila.Cells[4].Value},'{null}','{null}','{null}','{null}','{null}',{fila.Cells[5].Value},{fila.Cells[3].Value},{Globals.IdUsuario},'{'N'}','{System.DateTime.Now}','{null}'";
                            utilidades.ExecuteSQL(vSql2);
                        }

                        tran.Commit();
                        //MessageBox.Show("Cobro efectuado correctamente.", "Mensaje.");

                        dataPago.Rows.Clear();
                        txtMonto.Text = string.Empty;

                        lblDevolver.Text = "RD$  00.00";
                        lblPagado.Text = "RD$  00.00";
                        lblTotal.Text = "RD$  00.00";

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.Message);
                    }

                    tran.ConectionClose();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataPago.Rows.Count > 0)
            {
                dataPago.Rows.RemoveAt(dataPago.CurrentRow.Index);
                CalcTotal();

                if (dataPago.Rows.Count <= 0)
                {
                    lblDevolver.Text = "RD$  00.00";
                    lblPagado.Text = "RD$  00.00";
                    lblTotal.Text = "RD$  00.00";
                }
            }
            else
            {
                MessageBox.Show("No hay pagos para eliminar.");
            }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTarjetaNo_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
