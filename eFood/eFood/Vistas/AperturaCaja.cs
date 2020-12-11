using eFood.Utils;
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
            var data = utilidades.ejecuta($@"select id, descripcion, valor from denominaciones where desactivado ='N' AND moneda = 'S'");
            dataDenominaciones.DataSource = data;

            comboCajero.DataSource = utilidades.ejecuta($@"select  id_usuario ,e.nombre1+' '+e.apellido1+' '+e.apellido2 Nombre   from usuarios a inner join empleado b on a.ficha = b.ficha 
						                                     inner join departamento c on b.id_departamento = c.id_departamento
					                                         inner join cargo d on b.id_cargo = d.id_cargo
						                                     inner join persona e on a.id_persona = e.id_persona
	                                                        where b.id_departamento = 2 
	                                                          and b.id_cargo        = 1");
            comboCajero.DisplayMember = "Nombre";
            comboCajero.ValueMember = "id_usuario";

            comboCaja.DataSource = utilidades.ejecuta($@"select id, caja from cajas where desactivado = 'N'");
            comboCaja.DisplayMember = "caja";
            comboCaja.ValueMember = "id";

            traerAperturas();
        }


        public void traerAperturas()
        {
            DataTable dt = new DataTable();
            dt.ejecuta($@"select convert(varchar(max),b.id)+' - '+b.caja Caja, a.monto_inicial Monto_Inicial , d.nombre1+' '+d.apellido1+' '+apellido2 Cajero, cast(a.fecha_inicio as date) Fecha_apertura, a.estado Estado
                            from enc_apertura_caja a inner join cajas b on a.id_caja = b.id
						  inner join usuarios c on a.id_usuario = c.id_usuario 
						  inner join persona d on c.id_persona = d.id_persona");

            dataCajas.DataSource = dt;
        }

        private void dataDenominaciones_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        
        
        }

        private void dataDenominaciones_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
            if (dataDenominaciones.CurrentCell != null)
                if (dataDenominaciones.CurrentCell.ColumnIndex == 0)
                {
                    var valor = dataDenominaciones.CurrentRow.Cells.GetCellValueFromColumnHeader("valor") == null?0 : Convert.ToDecimal(dataDenominaciones.CurrentRow.Cells.GetCellValueFromColumnHeader("valor"));
                    var cantidad = dataDenominaciones.CurrentRow.Cells.GetCellValueFromColumnHeader("cantidad") == null?0: Convert.ToDecimal(dataDenominaciones.CurrentRow.Cells.GetCellValueFromColumnHeader("cantidad"));
                    dataDenominaciones.CurrentRow.Cells.SetCellValueFromColumnHeader("total", (valor * cantidad).ToString().Decimals());
                }

            decimal total = 0;
            foreach (DataGridViewRow item in dataDenominaciones.Rows)
            {
                if(item.Cells.GetCellValueFromColumnHeader("cantidad") != null)
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
            var HayApertura = utilidades.ejecuta("select * from enc_apertura_caja where id_caja = 1 and estado = 'A'").Rows;
            if (HayApertura.Count > 0)
            {
                MessageBox.Show("Esta caja tiene apertura realizada");
                return;
            }

            using (var tran = utilidades.BeginTransation())
            {
                try
                {
                   string  vSql = $@"EXEC actualiza_enc_apertura_caja  '{System.DateTime.Today}','{System.DateTime.Today}',{lbltotal.Text.Decimals()}, {0} , {comboCaja.SelectedValue}, {Globals.IdUsuario},'{'N'}','{'A'}','{System.DateTime.Today}','{null}'";
                    var x = utilidades.ExecuteSQL(vSql);
                    var id = x.GetIdentity();

                    foreach (DataGridViewRow fila in dataDenominaciones.Rows)
                    {
                        if (fila.Cells[0].Value != null)
                        {
                            string vSql2 = $"EXEC actualiza_det_apertura_caja {id},{fila.Cells.GetCellValueFromColumnHeader("id")},{fila.Cells.GetCellValueFromColumnHeader("valor")},{fila.Cells[0].Value}";
                            utilidades.ExecuteSQL(vSql2);
                        }
                    }

                    tran.Commit();
                    MessageBox.Show("Apertura de caja realizada.", "Mensaje.");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error aperturando caja " + ex.Message, "Alerta ");
                }

                tran.ConectionClose();
                traerAperturas();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
