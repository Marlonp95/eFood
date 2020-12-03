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
    public partial class AperturaCaja : Form
    {
        public AperturaCaja()
        {
            InitializeComponent();
        }

        private void AperturaCaja_Load(object sender, EventArgs e)
        {
            var data = utilidades.ejecuta($@"select descripcion, valor from denominaciones where desactivado ='N' AND moneda = 'S'");
            dataDenominaciones.DataSource = data;

            comboCajero.DataSource = utilidades.ejecuta($@"select  id_usuario ,e.nombre1+' '+e.apellido1+' '+e.apellido2 Nombre   from usuarios a inner join empleado b on a.ficha = b.ficha 
						                                     inner join departamento c on b.id_departamento = c.id_departamento
					                                         inner join cargo d on b.id_cargo = d.id_cargo
						                                     inner join persona e on a.id_persona = e.id_persona
	                                                        where b.id_departamento = 2 
	                                                          and b.id_cargo        = 1");
            comboCajero.DisplayMember = "Nombre";
            comboCajero.ValueMember = "id_usuario";
        }

      

        private void dataDenominaciones_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        
        
        }

        private void dataDenominaciones_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
            if (dataDenominaciones.CurrentCell != null)
                if (dataDenominaciones.CurrentCell.ColumnIndex == 0)
                {   
                    var valor = dataDenominaciones.CurrentRow.Cells[3].Value != null ? Convert.ToDecimal(dataDenominaciones.CurrentRow.Cells[3].Value) : 0;
                    var cantidad = dataDenominaciones.CurrentRow.Cells[0].Value != null ? Convert.ToDecimal(dataDenominaciones.CurrentRow.Cells[0].Value) : 0;
                    dataDenominaciones.CurrentRow.Cells[1].Value = (valor * cantidad).ToString().Decimals();
                }

            decimal total = 0;
            foreach (DataGridViewRow item in dataDenominaciones.Rows)
            {
                total += Convert.ToDecimal(item.Cells[1].Value);

            }
            lbltotal.Text = total.ToString().MoneyDecimal();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
