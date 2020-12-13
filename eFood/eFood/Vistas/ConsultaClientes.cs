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
    public partial class ConsultaClientes : Form
    {
        public ConsultaClientes()
        {
            InitializeComponent();
        }

        private void ConsultaClientes_Load(object sender, EventArgs e)
        {
            traerClientes();
        }

        public void traerClientes()
        {
            DataTable dt = new DataTable();

            dt.ejecuta(@"SELECT dbo.cliente.id_cliente, dbo.persona.nombre1+' '+dbo.persona.apellido1+' '+dbo.persona.apellido2 nombre, dbo.cliente.rnc, dbo.tipo_ncf.descripcion, dbo.cliente.excento_itbis, dbo.condiciones_pago.descripcion AS Expr1, dbo.cliente.limite_credito, 
                                dbo.cliente.porcentaje_mora, dbo.cliente.porcentaje_descuento, dbo.cliente.credito_activo
                           FROM dbo.cliente left JOIN  dbo.condiciones_pago ON dbo.cliente.id_condicion = dbo.condiciones_pago.id_condicion_pago
                                            left JOIN  dbo.tipo_ncf ON dbo.cliente.id_tipo_ncf = dbo.tipo_ncf.tipo
                                            left JOIN  dbo.persona ON dbo.cliente.id_persona = dbo.persona.id_persona");
            dataCliente.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet dt = new DataSet();
            string vSql = $@"SELECT dbo.persona.nombre1+' '+dbo.persona.apellido1+' '+dbo.persona.apellido2 nombre, dbo.cliente.rnc, dbo.tipo_ncf.descripcion, dbo.cliente.excento_itbis, dbo.condiciones_pago.descripcion AS Expr1, dbo.cliente.limite_credito, 
                                        dbo.cliente.porcentaje_mora, dbo.cliente.porcentaje_descuento
                               FROM dbo.cliente left JOIN  dbo.condiciones_pago ON dbo.cliente.id_condicion = dbo.condiciones_pago.id_condicion_pago
                                                left JOIN  dbo.tipo_ncf ON dbo.cliente.id_tipo_ncf = dbo.tipo_ncf.tipo
                                                left JOIN  dbo.persona ON dbo.cliente.id_persona = dbo.persona.id_persona";

            if (string.IsNullOrEmpty(txtBuscar.Text.Trim()) == false)
            {
                vSql += " Where nombre1 like ('%" + txtBuscar.Text.Trim() + "%') or dbo.tipo_ncf.descripcion like ('%" + txtBuscar.Text.Trim() + "%')";
                dt.ejecuta(vSql);
                dataCliente.DataSource = dt.Tables[0];
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataCliente.Rows.Count <= 0)
            return;
            DialogResult = DialogResult.OK;
        }
    }
}
