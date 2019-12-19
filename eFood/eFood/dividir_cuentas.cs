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
    public partial class dividir_cuentas : Form
    {
        public int IdMesa { get; set; } = 0;
        public int num_fact { get; set; }
        public dividir_cuentas(int pId_mesa)
        {

            InitializeComponent();

            IdMesa = pId_mesa;

            id_mesa.Text =$"Mesa {IdMesa.ToString()}";

            dataCuentas.Rows.Clear();

            try { 
            DataTable dt = new DataTable();
            string vSQL2 = $@"SELECT    temp_enc_factura.id_factura, persona.nombre1, persona.apellido1, temp_enc_factura.fecha_factura
FROM temp_enc_factura INNER JOIN
                         cliente ON temp_enc_factura.id_cliente = cliente.id_cliente INNER JOIN
                         persona ON cliente.id_persona = persona.id_persona

                         where id_mesa = {IdMesa}";

            //dt.ejecuta($"SELECT a.id_usuario, CONCAT( d.nombre1,' ',d.apellido1,' ',d.apellido2 ) as Nombre, a.id_cliente ,b.id_plato ,c.plato, c.precio, b.cantidad, b.cantidad * c.precio importe FROM temp_enc_factura a INNER JOIN temp_det_factura b on a.id_factura = b.id_factura INNER JOIN platos c on b.id_plato = c.id_plato INNER JOIN persona d on a.id_usuario = d.id_persona WHERE a.id_mesa  = {comboFactura.SelectedValue};");

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

        private void dividir_cuentas_Load(object sender, EventArgs e)
        {
            string vSql = $"select a.id_plato, b.plato, a.cantidad  from temp_det_factura a inner join platos b on a.id_plato = b.id_plato inner join temp_enc_factura c on a.id_factura = c.id_factura where c. ={IdMesa}";
            txtCantidad.Text = "1";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataCuentas_SelectionChanged(object sender, EventArgs e)
        {
            if (dataCuentas.CurrentRow == null) return;

            num_fact = Convert.ToInt32(dataCuentas.CurrentRow.Cells[0].Value);

            if (dataCuentas.Rows.Count > 0)
            {

                dataSeleccionProducto.Rows.Clear();
                DataTable dt = new DataTable();
                dt.ejecuta($"select a.id_plato, b.plato, b.descripcion, a.cantidad, a.cantidad * b.precio importe, a.precio from temp_det_factura a inner join platos b on a.id_plato = b.id_plato inner join temp_enc_factura c on a.id_factura = c.id_factura where c.id_factura = {dataCuentas.CurrentRow.Cells[0].Value}");

                foreach (DataRow Fila in dt.Rows)
                {
                    dataSeleccionProducto.Rows.Add
                    (
                        Convert.ToString(Fila["id_plato"]),
                        Convert.ToString(Fila["plato"]),
                       // Convert.ToString(Fila["descripcion"]),
                        Convert.ToString(Fila["cantidad"]),
                        Convert.ToString(Fila["precio"]),
                        Convert.ToDecimal(Fila["importe"]).ToString("F")
                    );
                }
            }
        }
        bool existe;
        int contador;
        int num_fila;
        double total;
        decimal cantidad;
        int id_plato;
         double itbis;
        double porciento_ley;
        double sub_total;

        private void button1_Click(object sender, EventArgs e)
        {
            num_fila = 0;
            num_fila = dataSeleccionProducto.CurrentRow.Index;
            existe = false;

            if (dataSeleccionProducto.Rows.Count == 0)
            {
                MessageBox.Show("seleccionar articulos");
            }

            else
            {
                if (dataPedido.Rows.Count == 0)
                {
                    dataPedido.Rows.Add(new string[]
                   {
                        Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[1, dataSeleccionProducto.CurrentRow.Index].Value),                
                        Convert.ToString(txtCantidad.Text),
                        Convert.ToString(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value)),
                   });
                    dataSeleccionProducto.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value) - Convert.ToDecimal(txtCantidad.Text));
                    cantidad = Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value);
                    id_plato = Convert.ToInt32(dataSeleccionProducto.Rows[num_fila].Cells[0].Value);

                    if (cantidad == 0)
                    {
                        dataSeleccionProducto.Rows.Remove(dataSeleccionProducto.CurrentRow);
                        DataTable dt = new DataTable();
                        // dt.ejecuta($"DELETE FROM temp_det_factura where id_plato = {id_plato}");
                    }
                    contador++;


                }
                else
                {
                    foreach (DataGridViewRow Fila in dataPedido.Rows)
                    {
                        if (Fila.Cells[0].Value.ToString() == Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value))
                        {
                            existe = true;
                            num_fila = Fila.Index;
                        }
                    }
                    if (existe == true)
                    {
                        dataPedido.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value) + Convert.ToDecimal(txtCantidad.Text));
                        dataPedido.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value) * Convert.ToDecimal((dataPedido.Rows[num_fila].Cells[3].Value))).ToString("F");
                        dataSeleccionProducto.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value) - Convert.ToDecimal(txtCantidad.Text));
                        cantidad = Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value);
                        contador++;
                        id_plato = Convert.ToInt32(dataSeleccionProducto.Rows[num_fila].Cells[0].Value);
                        if (cantidad == 0)
                        {
                            dataSeleccionProducto.Rows.Remove(dataSeleccionProducto.CurrentRow);
                            DataTable dt = new DataTable();
                            // dt.ejecuta($"DELETE FROM temp_det_factura where id_plato = {id_plato}");
                        }
                    }
                    else
                    {
                       dataPedido.Rows.Add
                        (
                            Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value),
                            Convert.ToString(dataSeleccionProducto[1, dataSeleccionProducto.CurrentRow.Index].Value),
                            Convert.ToString(txtCantidad.Text),
                            Convert.ToString(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value),
                            Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value))
                         );
                        contador++;
                    }
                }
            }

            total = 0;

            foreach (DataGridViewRow Fila in dataPedido.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);
            }
            lbltotal.Text = "RD$ " + total.ToString();
            sub_total = total;
            itbis = total * 0.18;
            porciento_ley = total * 0.10;
            lblSubTotal.Text = sub_total.ToString();
            lblItbis.Text = itbis.ToString();
            lblLey.Text = porciento_ley.ToString();

            total = sub_total + itbis + porciento_ley;
            lbltotal.Text = "RD$ " + total.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            num_fila = 0;
            num_fila = dataPedido.CurrentRow.Index;
            existe = false;

            if (dataSeleccionProducto.Rows.Count == 0)
            {
                MessageBox.Show("seleccionar articulos");
            }

            else
            {
                if (dataSeleccionProducto.Rows.Count == 0)
                {
                    dataSeleccionProducto.Rows.Add(new string[]
                   {
                        Convert.ToString(dataPedido[0, dataPedido.CurrentRow.Index].Value),
                        Convert.ToString(dataPedido[1, dataPedido.CurrentRow.Index].Value),
                        Convert.ToString(txtCantidad.Text),
                        Convert.ToString(dataPedido[3, dataPedido.CurrentRow.Index].Value),
                        Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataPedido[3, dataPedido.CurrentRow.Index].Value)),
                   });
                    dataPedido.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value) - Convert.ToDecimal(txtCantidad.Text));
                    cantidad = Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value);
                    id_plato = Convert.ToInt32(dataPedido.Rows[num_fila].Cells[0].Value);
                    if (cantidad == 0)
                    {
                        dataPedido.Rows.Remove(dataPedido.CurrentRow);
                        DataTable dt = new DataTable();
                        // dt.ejecuta($"DELETE FROM temp_det_factura where id_plato = {id_plato}");
                    }
                    contador++;
                }
                else
                {
                    foreach (DataGridViewRow Fila in dataPedido.Rows)
                    {
                        if (Fila.Cells[0].Value.ToString() == Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value))
                        {
                            existe = true;
                            num_fila = Fila.Index;
                        }
                    }
                    if (existe == true)
                    {
                        dataSeleccionProducto.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value) + Convert.ToDecimal(txtCantidad.Text));
                        dataSeleccionProducto.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value) * Convert.ToDecimal((dataSeleccionProducto.Rows[num_fila].Cells[3].Value))).ToString("F");
                        dataPedido.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value) - Convert.ToDecimal(txtCantidad.Text));
                        cantidad = Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value);
                        contador++;
                        id_plato = Convert.ToInt32(dataPedido.Rows[num_fila].Cells[0].Value);
                        if (cantidad == 0)
                        {
                            dataPedido.Rows.Remove(dataPedido.CurrentRow);
                            DataTable dt = new DataTable();
                            // dt.ejecuta($"DELETE FROM temp_det_factura where id_plato = {id_plato}");
                        }
                    }
                    else
                    {
                        dataSeleccionProducto.Rows.Add
                         (
                             Convert.ToString(dataPedido[0, dataPedido.CurrentRow.Index].Value),
                             Convert.ToString(dataPedido[1, dataPedido.CurrentRow.Index].Value),
                             Convert.ToString(txtCantidad.Text),
                             Convert.ToString(dataPedido[3, dataPedido.CurrentRow.Index].Value),
                             Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataPedido[3, dataPedido.CurrentRow.Index].Value))
                          );
                        contador++;
                    }
                }
            }
            total = 0;
            foreach (DataGridViewRow Fila in dataPedido.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);
            }


            lbltotal.Text = "RD$ " + total.ToString();
            sub_total = total;
            itbis = total * 0.18;
            porciento_ley = total * 0.10;
            lblSubTotal.Text = sub_total.ToString();
            lblItbis.Text = itbis.ToString();
            lblLey.Text = porciento_ley.ToString();
            total = sub_total + itbis + porciento_ley;
            lbltotal.Text = "RD$ " + total.ToString();
        }
    }
}
