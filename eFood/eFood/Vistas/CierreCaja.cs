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
    public partial class CierreCaja : Form
    {
        public CierreCaja()
        {
            InitializeComponent();
        }
        decimal total = 0;

        private void CierreCaja_Load(object sender, EventArgs e)
        {
            var data = utilidades.ejecuta($@"select descripcion, valor from denominaciones where desactivado ='N' AND moneda = 'S'");
            dataDenominaciones.DataSource = data;

            var data1 = utilidades.ejecuta($@"select id, descripcion, valor from denominaciones where desactivado ='N' AND moneda = 'N'");
            dataDenomina.DataSource = data1;

            traerAperturas();

            txtTarjeta.Text = "0.00";
            txtCheques.Text = "0.00";
            txtNotaCredito.Text = "0.00";
            txtDeposito.Text = "0.00";
            txtEfectivo.Text = "0.00";
            txtDolares.Text = "0.00";
            txtDiferenciaDolar.Text = "0.00";
        }

        void traerAperturas()
        {
            DataTable dt = new DataTable();
            dt.ejecuta($@"select a.id, a.fecha_inicio, c.nombre1+' '+c.apellido1+' '+c.apellido2 Cajero, monto_inicial ,sum(d.monto) total
                              from enc_apertura_caja a inner join usuarios b   on a.id_usuario = b.id_usuario
						                               inner join persona c    on b.id_persona = c.id_persona
						                               inner join enc_cobros d on a.id = d.id_apertura
                             where  a.estado ='A'
	                            group by a.id, a.fecha_inicio, c.nombre1+' '+c.apellido1+' '+c.apellido2,  monto_inicial ");

            dataApertura.DataSource = dt;

            txtEfectivoSistema.Text = dataApertura.CurrentRow.Cells[4].Value.ToString();
        }

        private void dataDenominaciones_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataDenominaciones_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataDenominaciones.Rows.Count > 0)
            {

                if (dataDenominaciones.CurrentCell.ColumnIndex == 0)
                {
                    var valor = dataDenominaciones.CurrentRow.Cells.GetCellValueFromColumnHeader("valor") == null ? 0 : Convert.ToDecimal(dataDenominaciones.CurrentRow.Cells.GetCellValueFromColumnHeader("valor"));
                    var cantidad = dataDenominaciones.CurrentRow.Cells.GetCellValueFromColumnHeader("cantidad") == null ? 0 : Convert.ToDecimal(dataDenominaciones.CurrentRow.Cells.GetCellValueFromColumnHeader("cantidad"));
                    dataDenominaciones.CurrentRow.Cells.SetCellValueFromColumnHeader("totales", (valor * cantidad).ToString().Decimals());
                }
                total = 0;
                foreach (DataGridViewRow item in dataDenominaciones.Rows)
                {
                    if (item.Cells.GetCellValueFromColumnHeader("cantidad") != null)
                        total += Convert.ToDecimal(item.Cells[1].Value);
                }

                txtEfectivo.Text = total.ToString();
                txtDiferenciaDOP.Text = (Convert.ToDecimal(txtEfectivo.Text) - Convert.ToDecimal(txtEfectivoSistema.Text)).ToString();

                if (Convert.ToDecimal(txtDiferenciaDOP.Text) < 0)
                {
                    txtDiferenciaDOP.ForeColor = Color.Red;
                }
                else
                {
                    txtDiferenciaDOP.ForeColor = Color.Green;
                }
            }       
        }

        private void dataApertura_SelectionChanged(object sender, EventArgs e)
        {
            if (dataApertura.Rows.Count > 0)
            {
                if (txtEfectivoSistema.Text != string.Empty && txtEfectivo.Text != string.Empty)
                {
                    txtDiferenciaDOP.Text = (Convert.ToDecimal(txtEfectivo.Text) - Convert.ToDecimal(txtEfectivoSistema.Text)).ToString();
                    if (Convert.ToDecimal(txtDiferenciaDOP.Text) < 0)
                    {
                        txtDiferenciaDOP.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtDiferenciaDOP.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void dataApertura_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (txtEfectivoSistema.Text != string.Empty && txtEfectivo.Text != string.Empty)
            {
                txtDiferenciaDOP.Text = (Convert.ToDecimal(txtEfectivo.Text) - Convert.ToDecimal(txtEfectivoSistema.Text)).ToString();

                if (Convert.ToDecimal(txtDiferenciaDOP.Text) < 0)
                {
                    txtDiferenciaDOP.ForeColor = Color.Red;
                }
                else
                {
                    txtDiferenciaDOP.ForeColor = Color.Black;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var tran = utilidades.BeginTransation())
            {
                try
                {
                    string vSql = $@"EXEC actualiza_enc_cierre_caja '{dataApertura.CurrentRow.Cells[1].Value.ToString()}','{System.DateTime.Now}',{dataApertura.CurrentRow.Cells[0].Value.ToString()}, {dataApertura.CurrentRow.Cells[3].Value.ToString()}, {dataApertura.CurrentRow.Cells[4].Value.ToString()},{txtEfectivo.Text}, {txtDolares.Text},{txtDolares.Text},'{txtNota.Text}',{Globals.IdUsuario},'{'N'}',{1},'{System.DateTime.Today}',{txtDiferenciaDOP.Text},{txtDiferenciaDolar.Text}";
                    var x = utilidades.ExecuteSQL(vSql);
                    var id = x.GetIdentity();

                    foreach (DataGridViewRow fila in dataDenominaciones.Rows)
                    {
                        if (fila.Cells[0].Value != null)
                        {
                            string vSql2 = $"EXEC actualiza_det_cierre_caja {fila.Cells.GetCellValueFromColumnHeader("id")}, {id} , {fila.Cells[0].Value.ToString()}, {fila.Cells.GetCellValueFromColumnHeader("valor")} ";
                            utilidades.ExecuteSQL(vSql2);
                        }
                    }

                    utilidades.ExecuteSQL($@"update enc_apertura_caja set estado = 'C' where id = {dataApertura.CurrentRow.Cells[0].Value.ToString()} and estado = 'A'");

                    tran.Commit();
                    MessageBox.Show("Cierre de caja realizado.", "Mensaje.");


                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error cerrando caja " + ex.Message, "Alerta ");
                }

                tran.ConectionClose();
                traerAperturas();
            }
        }

        private void dataDenomina_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataDenomina.Rows.Count > 0)
            {

                if (dataDenomina.CurrentCell.ColumnIndex == 0 && Convert.ToInt16(dataDenomina.CurrentRow.Cells["id"].Value.ToString()) == 17)
                {
                    var valor = dataDenomina.CurrentRow.Cells.GetCellValueFromColumnHeader("valor") == null ? 0 : Convert.ToDecimal(dataDenomina.CurrentRow.Cells.GetCellValueFromColumnHeader("valor"));
                    var cantidad = dataDenomina.CurrentRow.Cells.GetCellValueFromColumnHeader("cantidad") == null ? 0 : Convert.ToDecimal(dataDenomina.CurrentRow.Cells.GetCellValueFromColumnHeader("cantidad"));
                    dataDenomina.CurrentRow.Cells.SetCellValueFromColumnHeader("totales", (valor * cantidad).ToString().Decimals());
                }
                total = 0;
                foreach (DataGridViewRow item in dataDenomina.Rows)
                {
                    if (item.Cells.GetCellValueFromColumnHeader("cantidad") != null)
                        total += Convert.ToDecimal(item.Cells[1].Value);
                }

                txtDolares.Text = total.ToString();
                //txtDiferenciaDolar.Text = (Convert.ToDecimal(txtEfectivo.Text) - Convert.ToDecimal(txtEfectivoSistema.Text)).ToString();

                if (Convert.ToDecimal(txtDiferenciaDOP.Text) < 0)
                {
                    txtDiferenciaDOP.ForeColor = Color.Red;
                }
                else
                {
                    txtDiferenciaDOP.ForeColor = Color.Green;
                }
            }
        }
    }
}
