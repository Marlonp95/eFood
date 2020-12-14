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

namespace eFood.Vistas
{
    public partial class CuentasPorCobrar : Form
    {
        public CuentasPorCobrar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultaClientes obj = new ConsultaClientes();
            if (obj.ShowDialog() == DialogResult.OK)
            {
                int pos = Convert.ToInt16(obj.dataCliente.CurrentCell.RowIndex);
                txtcodCli.Text = string.Empty;
                txtcodCli.Text = obj.dataCliente.Rows[pos].Cells[0].Value.ToString();
                txtcodCli.Focus();
                SendKeys.Send("{TAB}");
            }
        }

        private void txtcodCli_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtcodCli.Text)) return;

                string vSql = $@"SELECT dbo.cliente.id_cliente ,dbo.persona.nombre1+' '+dbo.persona.apellido1+' '+dbo.persona.apellido2 nombre,dbo.persona.telefono, dbo.persona.documento ,dbo.cliente.rnc, dbo.tipo_ncf.id ,dbo.tipo_ncf.descripcion, dbo.cliente.excento_itbis, dbo.condiciones_pago.id_condicion_pago ,dbo.condiciones_pago.descripcion AS Condicion_pago, dbo.cliente.limite_credito, 
                                    dbo.cliente.porcentaje_mora, dbo.cliente.porcentaje_descuento
                           FROM dbo.cliente left JOIN  dbo.condiciones_pago ON dbo.cliente.id_condicion = dbo.condiciones_pago.id_condicion_pago
		                                    left JOIN  dbo.tipo_ncf ON dbo.cliente.id_tipo_ncf = dbo.tipo_ncf.tipo
		                                    left JOIN  dbo.persona ON dbo.cliente.id_persona = dbo.persona.id_persona 
                            where dbo.cliente.id_cliente  = {txtcodCli.Text}";

                DataSet dt = new DataSet();
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtnomcli.Text = dt.Tables[0].Rows[0]["nombre"].ToString();
                    txtRnc.Text = dt.Tables[0].Rows[0]["rnc"].ToString();
                    txttelefono.Text = dt.Tables[0].Rows[0]["telefono"].ToString();
                    txtdocumento.Text = dt.Tables[0].Rows[0]["documento"].ToString();



                    var data = utilidades.ejecuta($@"select id_factura, fecha_factura,fecha_vencimiento_factura, ncf, dias_acuerdo,total, balance from enc_factura where id_tipo_factura = 2 and id_cliente = {txtcodCli.Text}");
                    dataFacturas.DataSource = null;
                    dataFacturas.DataSource = data;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado");
                }


            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Salir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void CuentasPorCobrar_Load(object sender, EventArgs e)
        {
            var data = utilidades.ejecuta($@"select id_factura, fecha_factura,fecha_vencimiento_factura, ncf, dias_acuerdo,total, balance from enc_factura where id_tipo_factura = 2 ");
            dataFacturas.DataSource = data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string vSql = "select id_factura, fecha_factura,fecha_vencimiento_factura, ncf, dias_acuerdo,total, balance from enc_factura where id_tipo_factura = 2";

            if (string.IsNullOrEmpty(txtbuscar.Text.Trim()) == false)
            {
                vSql += "and nombre_cliente like ('%" + txtbuscar.Text.Trim() + "%')";
                var data = utilidades.ejecuta(vSql);
                dataFacturas.DataSource = data;
            }
        }
    }
}
