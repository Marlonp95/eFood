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
    public partial class pedido : Form
    {
        DateTime fecha = DateTime.Now;
        double itbis;
        double porciento_ley;
        double sub_total;
        bool existe = false;
        int num_fila = 0;
        int contador;
        double total;
        int id_factura;
        bool correcto;
        int id_plato;


        public string mesatag { get; set; }

        public pedido(string pMesaTag)
        {
            this.mesatag = pMesaTag;
            InitializeComponent();
        }


        #region Metodos
        public void traerPedido()
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"select a.id_plato, b.plato, b.descripcion, a.cantidad, a.cantidad * b.precio importe, a.precio from temp_det_factura a inner join platos b on a.id_plato = b.id_plato inner join temp_enc_factura c on a.id_factura = c.id_mesa where c.id_mesa = {mesatag};");

            foreach (DataRow Fila in dt.Rows)
            {
                DataPedido.Rows.Add
                (
                    Convert.ToString(Fila["id_plato"]),
                    Convert.ToString(Fila["plato"]),
                    Convert.ToString(Fila["descripcion"]),
                    Convert.ToString(Fila["cantidad"]),
                    Convert.ToDecimal(Fila["importe"]).ToString("F"),
                    Convert.ToString(Fila["precio"])
                );
            }

            foreach (DataGridViewRow Rows in DataPedido.Rows)
            {
                total += Convert.ToDouble(Rows.Cells[4].Value);
            }

            sub_total = total;
            itbis = total * 0.18;
            porciento_ley = total * 0.10;
            lblSubTotal.Text = sub_total.ToString();
            lblItbis.Text = itbis.ToString();
            lblLey.Text = porciento_ley.ToString();
            total = sub_total + itbis + porciento_ley;
            lblTotal.Text = "RD$ " + total.ToString();
        }

        string vSql;
        DataTable dt;

        public void traerFacturas()
        {
            try
            {
                dataCuentas.Rows.Clear();
                DataTable dt = new DataTable();
                string vSQL2 = $@"SELECT    temp_enc_factura.id_factura, persona.nombre1, persona.apellido1, temp_enc_factura.fecha_factura
                                       FROM temp_enc_factura INNER JOIN
                                 cliente ON temp_enc_factura.id_cliente = cliente.id_cliente INNER JOIN
                                 persona ON cliente.id_persona = persona.id_persona
                                 where id_mesa = {mesatag}";

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
        private void pedido_Load(object sender, EventArgs e)
        {
            //TRAER CUENTAS
            dataSeleccionProducto.Rows.Clear();

            traerFacturas();

            try
            {
                dt = new DataTable();
                vSql = $"SELECT nombre1, apellido1 From persona Where id_persona= {login.codigo} ";
                dt.ejecuta(vSql);
                lblusuario.Text = dt.Rows[0]["nombre1"].ToString() + " " + dt.Rows[0]["apellido1"].ToString();
            }
            catch (Exception error)
            {
                MessageBox.Show("error" + error);
            }

            dt = new DataTable();
            vSql = $"SELECT id_factura FROM temp_enc_factura WHERE estado ='A' and id_mesa = {mesatag} ";
            dt.ejecuta(vSql);
            correcto = dt.ejecuta(vSql);

            if (dt.Rows.Count == 0)
            {
                dt = new DataTable();
                dt.ejecuta("SELECT max(id_factura) id_factura FROM temp_enc_factura");

                if (Convert.ToString(dt.Rows[0]["id_factura"]) == "")
                {
                    id_factura = 1;
                }
                else
                {
                    id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));
                    id_factura++;
                }
            }
            else
            {
                id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));
            }
            lblmesa.Text = mesatag;
            txtCantidad.Text = "1";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //dataSeleccionProducto.Rows.Clear();
                DataPedido.Rows.Clear();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio, itbis FROM platos WHERE id_categoria = 4");
            dataSeleccionProducto.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio, itbis FROM platos WHERE id_categoria = 1");
            dataSeleccionProducto.DataSource = dt;
        }


        private void button14_Click(object sender, EventArgs e)
        {
            existe = false;

            if (dataSeleccionProducto.Rows.Count == 0)
            {
                MessageBox.Show("seleccionar articulos");
            }

            else
            {
                if (DataPedido.Rows.Count == 0)
                {
                    DataPedido.Rows.Add(new string[]
                   {
                        Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[1, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[2, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(txtCantidad.Text),
                        Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value)),
                        Convert.ToString(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[4, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(Convert.ToDouble(dataSeleccionProducto[4, dataSeleccionProducto.CurrentRow.Index].Value) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value))
                   });

                    contador++;
                }
                else
                {
                    foreach (DataGridViewRow Fila in DataPedido.Rows)
                    {
                        if (Fila.Cells[0].Value.ToString() == Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value))
                        {
                            existe = true;
                            num_fila = Fila.Index;
                        }
                    }
                    if (existe == true)
                    {
                        DataPedido.Rows[num_fila].Cells[3].Value = Convert.ToString(Convert.ToDecimal(DataPedido.Rows[num_fila].Cells[3].Value) + Convert.ToDecimal(txtCantidad.Text));
                        DataPedido.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(DataPedido.Rows[num_fila].Cells[3].Value) * Convert.ToDecimal((DataPedido.Rows[num_fila].Cells[5].Value))).ToString("F");
                    }
                    else
                    {
                        DataPedido.Rows.Add(
                        Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[1, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[2, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(txtCantidad.Text),
                        Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value)),
                        Convert.ToString(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[4, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(Convert.ToDouble(dataSeleccionProducto[4, dataSeleccionProducto.CurrentRow.Index].Value) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value))

                         );
                        contador++;
                    }
                }
            }
            total = 0;

            foreach (DataGridViewRow Fila in DataPedido.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);
            }

            lblTotal.Text = "RD$ " + total.ToString();
            sub_total = total;
            itbis = total * 0.18;
            porciento_ley = total * 0.10;
            lblSubTotal.Text = sub_total.ToString();
            lblItbis.Text = itbis.ToString();
            lblLey.Text = porciento_ley.ToString();
            total = sub_total + itbis + porciento_ley;
            lblTotal.Text = "RD$ " + total.ToString();

        }

        decimal cantidad;

        private void button11_Click(object sender, EventArgs e)
        {
            total = 0;
            num_fila = 0;
            num_fila = DataPedido.CurrentRow.Index;
            cantidad = Convert.ToDecimal(DataPedido.Rows[num_fila].Cells[3].Value);
            cantidad++;
            DataPedido.Rows[num_fila].Cells[3].Value = Convert.ToString(cantidad);
            DataPedido.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(DataPedido.Rows[num_fila].Cells[3].Value) * Convert.ToDecimal((DataPedido.Rows[num_fila].Cells[5].Value))).ToString("F");

            foreach (DataGridViewRow Fila in DataPedido.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);
            }
            sub_total = total;
            itbis = total * 0.18;
            porciento_ley = total * 0.10;
            lblSubTotal.Text = sub_total.ToString();
            lblItbis.Text = itbis.ToString();
            lblLey.Text = porciento_ley.ToString();
            total = sub_total + itbis + porciento_ley;
            lblTotal.Text = "RD$ " + total.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Seguro desea quitar articulo ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                total = 0;
                num_fila = 0;
                num_fila = DataPedido.CurrentRow.Index;
                cantidad = Convert.ToDecimal(DataPedido.Rows[num_fila].Cells[3].Value);
                id_plato = Convert.ToInt32(DataPedido.Rows[num_fila].Cells[0].Value);
                cantidad--;
                if (cantidad == 0)
                {
                    DataPedido.Rows.Remove(DataPedido.CurrentRow);
                    dt = new DataTable();
                    dt.ejecuta($"DELETE FROM temp_det_factura where id_plato = {id_plato}");
                }
                else
                {

                    DataPedido.Rows[num_fila].Cells[3].Value = Convert.ToString(cantidad);
                    DataPedido.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(DataPedido.Rows[num_fila].Cells[3].Value) * Convert.ToDecimal((DataPedido.Rows[num_fila].Cells[5].Value))).ToString("F");
                }

                foreach (DataGridViewRow Fila in DataPedido.Rows)
                {
                    total += Convert.ToDouble(Fila.Cells[4].Value);
                }

                sub_total = total;
                itbis = total * 0.18;
                porciento_ley = total * 0.10;
                lblSubTotal.Text = sub_total.ToString();
                lblItbis.Text = itbis.ToString();
                lblLey.Text = porciento_ley.ToString();
                total = sub_total + itbis + porciento_ley;
                lblTotal.Text = "RD$ " + total.ToString();
            }

        }

       

        private void button15_Click(object sender, EventArgs e)
        {
            //Guardar Temporales Facturas.

            if (dataCuentas.Rows.Count <= 0)
            {
                dt = new DataTable();
                vSql = $"SELECT id_factura FROM temp_enc_factura WHERE estado ='A' and id_mesa = {mesatag} ";
                correcto = dt.ejecuta(vSql);

                if (dt.Rows.Count == 0)
                {
                    dt = new DataTable();
                    dt.ejecuta("SELECT max(id_factura) id_factura FROM temp_enc_factura");

                    if (Convert.ToString(dt.Rows[0]["id_factura"]) == "")
                    {
                        id_factura = 1;
                        num_fact = id_factura;
                    }
                    else
                    {
                        id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));
                        id_factura++;
                        num_fact = id_factura;
                        MessageBox.Show(num_fact.ToString());
                        try
                        {
                            MessageBox.Show("Factura" + num_fact.ToString());

                            string vSql = $"EXEC actuaiza_temp_enc_fact {num_fact},'{mesatag}','{fecha}','{6}','{login.codigo}','{null}',{itbis.ToString().Replace(',', '.')},{total.ToString().Replace(',', '.')},{porciento_ley.ToString().Replace(',', '.')},{sub_total.ToString().Replace(',', '.')},'{'A'}','{null}'";
                            DataSet dt = new DataSet();
                            correcto = dt.ejecuta(vSql);

                            foreach (DataGridViewRow fila in DataPedido.Rows)
                            {
                                vSql = $"EXEC actuaiza_temp_det_fact '{num_fact}','{fila.Cells[0].Value}','{fila.Cells[3].Value}','{Convert.ToDouble(fila.Cells[5].Value)}','{Convert.ToDouble(fila.Cells[6].Value)}'";
                                dt = new DataSet();
                                correcto = dt.ejecuta(vSql);
                            }

                            MessageBox.Show("Datos Guardados");

                            traerFacturas();

                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error Guardando Datos" + error.ToString());
                        }
                       
                    }
                }
                else
                {
                    id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));
                    id_factura++;
                    num_fact = id_factura;
                }  
            }
            else
            {               
                int rnc;

                int cuenta = Convert.ToInt32(dataCuentas.CurrentRow.Cells["id_fact"].Value);

                dt = new DataTable();
                vSql = $"SELECT id_factura FROM temp_enc_factura WHERE estado ='A' and id_mesa = {mesatag} and id_factura = {cuenta} ";
                correcto = dt.ejecuta(vSql);

                if (dt.Rows.Count == 0)
                {
                    try
                    {
                        MessageBox.Show("Factura" + num_fact.ToString());

                        string vSql = $"EXEC actuaiza_temp_enc_fact {num_fact},'{mesatag}','{fecha}','{6}','{login.codigo}','{null}',{itbis.ToString().Replace(',', '.')},{total.ToString().Replace(',', '.')},{porciento_ley.ToString().Replace(',', '.')},{sub_total.ToString().Replace(',', '.')},'{'A'}','{null}'";
                        DataSet dt = new DataSet();
                        correcto = dt.ejecuta(vSql);

                        foreach (DataGridViewRow fila in DataPedido.Rows)
                        {
                            vSql = $"EXEC actuaiza_temp_det_fact '{num_fact}','{fila.Cells[0].Value}','{fila.Cells[3].Value}','{Convert.ToDouble(fila.Cells[5].Value)}','{Convert.ToDouble(fila.Cells[6].Value)}'";
                            dt = new DataSet();
                            correcto = dt.ejecuta(vSql);
                        }

                        MessageBox.Show("Datos Guardados");
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error Guardando Datos" + error.ToString());
                    }

                }
                else
                {
                    id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));

                    foreach (DataGridViewRow fila in DataPedido.Rows)
                    {
                        try
                        {
                            string vSql = $"EXEC actuaiza_temp_det_fact '{id_factura}','{fila.Cells[0].Value}','{fila.Cells[3].Value}','{Convert.ToDouble(fila.Cells[5].Value)}','{fila.Cells[6].Value.ToString().Replace(',', '.')}'";
                            DataSet dt = new DataSet();
                            correcto = dt.ejecuta(vSql);
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error 2" + error.ToString());
                        }
                    }

                    MessageBox.Show("Datos Guardados");

                }

                if (DataPedido.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para guardar");
                }

                traerFacturas();
            }
   
        }


        #region Platos
        private void button10_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio, itbis FROM platos WHERE id_categoria = 2");
            dataSeleccionProducto.DataSource = dt;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio, itbis FROM platos WHERE id_categoria = 5");
            dataSeleccionProducto.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio, itbis FROM platos WHERE id_categoria = 6");
            dataSeleccionProducto.DataSource = dt;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio, itbis FROM platos WHERE id_categoria = 7");
            dataSeleccionProducto.DataSource = dt;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio,itbis FROM platos WHERE id_categoria = 8");
            dataSeleccionProducto.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio, itbis FROM platos WHERE id_categoria = 9");
            dataSeleccionProducto.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_plato, plato, descripcion, precio, itbis FROM platos WHERE id_categoria = 3");
            dataSeleccionProducto.DataSource = dt;
        }

        #endregion

        private void button19_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"UPDATE mesa SET estado = 'S' WHERE id_mesa  = {mesatag} ");
            MessageBox.Show("Pasar por caja");
        }
        int num_fact;

        private void dataCuentas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataCuentas.CurrentRow == null) return;

            num_fact = Convert.ToInt32(dataCuentas.CurrentRow.Cells["id_fact"].Value);

            if (dataCuentas.Rows.Count > 0)
            {

                DataPedido.Rows.Clear();
                DataTable dt = new DataTable();
                dt.ejecuta($@"select a.id_plato, b.plato, b.descripcion, a.cantidad, a.cantidad * b.precio importe, a.precio, a.itbis, Cast (Round(a.precio*a.itbis,2) as decimal(10,2)) totalitbis  
                                from temp_det_factura a inner join platos b on a.id_plato = b.id_plato 
                                                        inner join temp_enc_factura c on a.id_factura = c.id_factura    
                                where c.id_factura = {num_fact};");

                foreach (DataRow Fila in dt.Rows)
                {
                    DataPedido.Rows.Add
                    (
                        Convert.ToString(Fila["id_plato"]),
                        Convert.ToString(Fila["plato"]),
                        Convert.ToString(Fila["descripcion"]),
                        Convert.ToString(Fila["cantidad"]),
                        Convert.ToDecimal(Fila["importe"]).ToString("F"),
                        Convert.ToString(Fila["precio"]),
                        Convert.ToString(Fila["itbis"]),
                        Convert.ToString(Fila["totalitbis"])


                    );
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dividir_cuentas obj = new dividir_cuentas(Convert.ToInt32(mesatag));
            obj.ShowDialog();
         

            traerFacturas();

        }

        private void dataCuentas_SelectionChanged(object sender, EventArgs e)
        {
            if (dataCuentas.CurrentRow == null) return;

            num_fact = Convert.ToInt32(dataCuentas.CurrentRow.Cells["id_fact"].Value);

            if (dataCuentas.Rows.Count > 0)
            {

                DataPedido.Rows.Clear();
                DataTable dt = new DataTable();
                sub_total = 0;
                itbis = 0;
                porciento_ley = 0;
                total = 0;
                dt.ejecuta($@"select a.id_plato, b.plato, b.descripcion, a.cantidad, a.cantidad * b.precio importe, a.precio, a.itbis, Cast (Round(a.precio*a.itbis,2) as decimal(10,2)) totalitbis  
                                from temp_det_factura a inner join platos b on a.id_plato = b.id_plato
                                                        inner join temp_enc_factura c on a.id_factura = c.id_factura
                                where c.id_factura = {num_fact};");

                foreach (DataRow Fila in dt.Rows)
                {
                    DataPedido.Rows.Add
                    (
                        Convert.ToString(Fila["id_plato"]),
                        Convert.ToString(Fila["plato"]),
                        Convert.ToString(Fila["descripcion"]),
                        Convert.ToString(Fila["cantidad"]),
                        Convert.ToDecimal(Fila["importe"]).ToString("F"),
                        Convert.ToString(Fila["precio"]),
                        Convert.ToString(Fila["itbis"]),
                         Convert.ToString(Fila["totalitbis"])
                    );
                }

                foreach (DataGridViewRow Rows in DataPedido.Rows)
                {
                    total += Convert.ToDouble(Rows.Cells[4].Value);
                }

                sub_total = total;
                itbis = total * 0.18;
                porciento_ley = total * 0.10;
                lblSubTotal.Text = sub_total.ToString();
                lblItbis.Text = itbis.ToString();
                lblLey.Text = porciento_ley.ToString();
                total = sub_total + itbis + porciento_ley;
                lblTotal.Text = "RD$ " + total.ToString();
            }
        }

        private void txtMask1_TextChanged(object sender, EventArgs e)
        {
            var data = txtMask1.OriginalValue;
        }

        //  private void dataCuentas_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dataCuentas.CurrentRow == null) return;
        //    num_fact = Convert.ToInt32(dataCuentas.CurrentRow.Cells["id_factura"].Value);
        //    if (dataCuentas.Rows.Count > 0)
        //    {
        //        if (dataCuentas.CurrentRow == null) return;
        //        DataPedido.Rows.Clear();
        //        DataTable dt = new DataTable();
        //        dt.ejecuta($"select a.id_plato, b.plato, b.descripcion, a.cantidad, a.cantidad * b.precio importe, a.precio from temp_det_factura a inner join platos b on a.id_plato = b.id_plato inner join temp_enc_factura c on a.id_factura = c.id_mesa where c.id_mesa = {num_fact};");

        //        foreach (DataRow Fila in dt.Rows)
        //        {
        //            DataPedido.Rows.Add
        //            (
        //                Convert.ToString(Fila["id_plato"]),
        //                Convert.ToString(Fila["plato"]),
        //                Convert.ToString(Fila["descripcion"]),
        //                Convert.ToString(Fila["cantidad"]),
        //                Convert.ToDecimal(Fila["importe"]).ToString("F"),
        //                Convert.ToString(Fila["precio"])
        //            );
        //        }
        //    }
        //}

    }

    #endregion 
}
