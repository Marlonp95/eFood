using Ex.OM.Extentions;
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
    public partial class cobro : Form
    {
        public cobro()
        {
            InitializeComponent();
        }

        private void cobro_Load(object sender, EventArgs e)
        {
            comboTipoPago.DataSource = utilidades.ejecuta("select id, descripcion from tipo_cobro where desactivado = 'N'");
            comboTipoPago.DisplayMember = "descripcion";
            comboTipoPago.ValueMember = "id";

            txtTarjetaNo.Enabled = false;
            comboBanco.Enabled = false;
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataPago.Rows.Count <= 0)
            {
                dataPago.Rows.Add(new string[]
                {
                comboTipoPago.SelectedValue.ToString(),
                comboTipoPago.Text.ToString(),
                txtMonto.Text.Decimals()
                });
            }
            else
            {
                var dataRow = dataPago.Rows.Cast<DataGridViewRow>().Where(x => x.Cells[0].Value.ToString() == comboTipoPago.SelectedValue.ToString());
                if (dataRow.Count() > 0)
                {
                    var num_fila = dataRow.FirstOrDefault().Index;
                    dataPago.Rows[num_fila].Cells[2].Value = Convert.ToDecimal(dataPago.Rows[num_fila].Cells[2].Value) + Convert.ToDecimal(txtMonto.Text);
                }
            }
           
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
            }
            else
            {
                txtTarjetaNo.Enabled = false;
                comboBanco.Enabled = false;
            }
        }
    }
}
