using eFood.Utils;
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
    public partial class clientes : Form
    {
        int codigo_persona;
        int codigo_cliente;
        int excento;
        int credito;

        public clientes()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void clientes_Load(object sender, EventArgs e)
        {

            comboNcf.DataSource = utilidades.ejecuta("select tipo, convert(varchar(max),tipo)+' - '+descripcion descripcion from tipo_ncf");
            comboNcf.DisplayMember = "descripcion";
            comboNcf.ValueMember = "tipo";

            comboCondicionPago.DataSource = utilidades.ejecuta("select id_condicion_pago, descripcion from condiciones_pago;");
            comboCondicionPago.DisplayMember= "descripcion";
            comboCondicionPago.ValueMember = "id_condicion_pago";

            try
            {
                var codPersona = utilidades.ejecuta("select top 1 id_persona from persona  ORDER by id_persona DESC").Rows;
                codigo_persona = codPersona[0].Field<Int32>("id_persona");
                codigo_persona++;

                var codCliente = utilidades.ejecuta("select top 1 id_cliente from cliente  ORDER by id_cliente DESC").Rows;
                codigo_cliente = codCliente[0].Field<Int32>("id_cliente");
                codigo_cliente++;

            }
            catch (Exception error)
            {

                MessageBox.Show("Error" + error);
            }        
          
            traerClientes();
        }

        public void traerClientes()
        {
            DataTable dt = new DataTable();
            dt.ejecuta(@"SELECT dbo.persona.nombre1+' '+dbo.persona.apellido1+' '+dbo.persona.apellido2 nombre, dbo.cliente.rnc, dbo.tipo_ncf.descripcion, dbo.cliente.excento_itbis, dbo.condiciones_pago.descripcion AS Expr1, dbo.cliente.limite_credito, 
                                dbo.cliente.porcentaje_mora, dbo.cliente.porcentaje_descuento, dbo.cliente.credito_activo
                           FROM dbo.cliente left JOIN  dbo.condiciones_pago ON dbo.cliente.id_condicion = dbo.condiciones_pago.id_condicion_pago
                                            left JOIN  dbo.tipo_ncf ON dbo.cliente.id_tipo_ncf = dbo.tipo_ncf.tipo
                                            left JOIN  dbo.persona ON dbo.cliente.id_persona = dbo.persona.id_persona");
            dataCliente.DataSource = dt;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.IsValid())
            {
                MessageBox.Show(Metodos.ErrorMessage);
                return;
            }
            try
            {
                string vSql = $"EXEC actualizapersona '{codigo_persona}','{txtnombre.Text.Trim()}','{txtapellido.Text.Trim()}','{txtapellido2.Text.Trim()}','{txtdireccion.Text.Trim()}','{txtdocumento.OriginalValue}','{null}','{txttelefono.Text.Trim()}','{txtcorreo.Text.Trim()}','{null}'";
                DataSet dt = new DataSet();
                dt.ejecutaTransaccion(vSql);             
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }


            try
            {
                if (checkExcento.Checked == true)
                {
                    excento = 1;
                }
                else
                {
                    excento = 0;
                }

                if (checkCredito.Checked == true)
                {
                    credito = 1;
                }
                else
                {
                    credito = 0;
                }

                string vSql = $"EXEC actualizacliente '{codigo_cliente}','{codigo_persona}','{txtRnc.Text.Trim()}','{comboCondicionPago.SelectedValue}','{excento}','{comboNcf.SelectedValue}','{txtNota.Text}','{txtlimiteCredito.Text.Trim()}','{txtPorcentajeMora.Text.Trim()}','{txtPorcentajeDescuento.Text.Trim()}','{1}','{credito}'";
                DataSet dt = new DataSet();
                dt.ejecutaTransaccion(vSql);               
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
            }

            traerClientes();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbuscar.Text.Trim()) == false)
            {
                DataSet dt = new DataSet();
                string vSql = $@"SELECT dbo.persona.nombre1+' '+dbo.persona.apellido1+' '+dbo.persona.apellido2 nombre, dbo.cliente.rnc, dbo.tipo_ncf.descripcion, dbo.cliente.excento_itbis, dbo.condiciones_pago.descripcion AS Expr1, dbo.cliente.limite_credito, 
                                        dbo.cliente.porcentaje_mora, dbo.cliente.porcentaje_descuento
                               FROM dbo.cliente left JOIN  dbo.condiciones_pago ON dbo.cliente.id_condicion = dbo.condiciones_pago.id_condicion_pago
                                                left JOIN  dbo.tipo_ncf ON dbo.cliente.id_tipo_ncf = dbo.tipo_ncf.tipo
                                                left JOIN  dbo.persona ON dbo.cliente.id_persona = dbo.persona.id_persona";

                if (string.IsNullOrEmpty(txtbuscar.Text.Trim()) == false)
                {
                    vSql += " Where nombre1 like ('%" + txtbuscar.Text.Trim() + "%') or dbo.tipo_ncf.descripcion like ('%" + txtbuscar.Text.Trim() + "%')";
                    dt.ejecuta(vSql);
                    dataCliente.DataSource = dt.Tables[0];
                }

            }
            else
            {
                traerClientes();
            }
        }

        private void txtRnc_Validating(object sender, CancelEventArgs e)
        {
            var rnc = txtRnc.Text;
            var result = Metodos.ValidarDocumento(rnc);
            if (result != true)
            {
                MessageBox.Show("RNC No Valido");
                txtRnc.Text = string.Empty;
            }
        }

        private void txtdocumento_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtdocumento.Text.Trim()))
            {
                return;
            }
            else
            {
                var documento = txtdocumento.Text.Trim();
                var result = Metodos.ValidarDocumento(documento);
                if (result != true)
                {
                    MessageBox.Show("Cedula No Valida");
                    txtdocumento.Text = string.Empty;
                }
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

        private void txtdocumento_KeyPress(object sender, KeyPressEventArgs e)
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
