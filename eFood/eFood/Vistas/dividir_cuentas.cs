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
    public partial class dividir_cuentas : Form
    {
        public int IdMesa { get; set; } = 0;
        public int num_fact { get; set; }
        bool existe;
        int contador;
        int num_fila;
        double total;
        double total1;
        decimal cantidad;
        int id_plato;
        double itbis;
        double porciento_ley;
        double sub_total;
        double itbis1;
        double porciento_ley1;
        double sub_total1;



        DateTime fecha = DateTime.Now;
        public dividir_cuentas(int pId_mesa)
        {

            InitializeComponent();

            IdMesa = pId_mesa;
            id_mesa.Text =$"Mesa {IdMesa.ToString()}";
            dataCuentas.Rows.Clear();

            traerCuentas();
        }

        void traerCuentas()
        {
            try
            {
                DataTable dt = new DataTable();
                string vSQL2 = $@"SELECT    temp_enc_factura.id_factura, persona.nombre1, persona.apellido1, temp_enc_factura.fecha_factura
                                    FROM temp_enc_factura INNER JOIN cliente ON temp_enc_factura.id_cliente = cliente.id_cliente 
                                                          INNER JOIN persona ON cliente.id_persona = persona.id_persona
                                where id_mesa = {IdMesa}
                                  and temp_enc_factura.estado  ='A'";
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

            try
            {
                DataTable dt = new DataTable();
                vSql = $"SELECT nombre1, apellido1 From persona Where id_persona= {login.codigo} ";
                dt.ejecuta(vSql);
                lblusuario.Text = dt.Rows[0]["nombre1"].ToString() + " " + dt.Rows[0]["apellido1"].ToString();
            }
            catch (Exception error)
            {
                MessageBox.Show("error" + error);
            }
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
                        Convert.ToString(Fila["cantidad"]),
                        Convert.ToString(Fila["precio"]),
                        Convert.ToDecimal(Fila["importe"]).ToString("F")
                    );
                }
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {

            if (dataSeleccionProducto.Rows.Count <= 0) return;

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
                    cantidad = Convert.ToDecimal(txtCantidad.Text);
    
                    if (cantidad == Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value))
                    {
                        dataPedido.Rows.Add(new string[]
                        {
                        Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[1, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(txtCantidad.Text),
                        Convert.ToString(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value)),
                        });

                        dataSeleccionProducto.Rows.Remove(dataSeleccionProducto.CurrentRow);
                    }
                    else
                    {
                        dataPedido.Rows.Add(new string[]
                        {
                            Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value),
                            Convert.ToString(dataSeleccionProducto[1, dataSeleccionProducto.CurrentRow.Index].Value),
                            Convert.ToString(txtCantidad.Text),
                            Convert.ToString(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value),
                            Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value)),
                        });

                        cantidad = Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value);
                        cantidad = cantidad - 1;

                        id_plato = Convert.ToInt32(dataSeleccionProducto.Rows[num_fila].Cells[0].Value);

                        if (cantidad == 0)
                        {
                            dataSeleccionProducto.Rows.Remove(dataSeleccionProducto.CurrentRow);
                            DataTable dt = new DataTable();
                        }
                        else
                        {
                            dataSeleccionProducto.Rows[num_fila].Cells[2].Value = Convert.ToString(cantidad);
                        }

                        dataSeleccionProducto.Refresh();                       
                    }
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
                        cantidad = Convert.ToDecimal(txtCantidad.Text);

                        if (cantidad == Convert.ToDecimal(dataSeleccionProducto.CurrentRow.Cells[2].Value))
                        {
                            dataPedido.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value) + Convert.ToDecimal(txtCantidad.Text));
                            dataPedido.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value) * Convert.ToDecimal((dataPedido.Rows[num_fila].Cells[3].Value))).ToString("F");
                            dataSeleccionProducto.Rows.Remove(dataSeleccionProducto.CurrentRow);
                        }
                        else
                        {
                            dataPedido.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value) + Convert.ToDecimal(txtCantidad.Text));
                            dataPedido.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value) * Convert.ToDecimal((dataPedido.Rows[num_fila].Cells[3].Value))).ToString("F");
                            dataSeleccionProducto.Rows[num_fila].Cells[2].Value = Convert.ToString(cantidad);
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
                             cantidad = Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value);
                             cantidad = cantidad - 1;
                             id_plato = Convert.ToInt32(dataSeleccionProducto.Rows[num_fila].Cells[0].Value);
                        if (cantidad == 0)
                        {
                            dataSeleccionProducto.Rows.Remove(dataSeleccionProducto.CurrentRow);
                            DataTable dt = new DataTable();
                            // dt.ejecuta($"DELETE FROM temp_det_factura where id_plato = {id_plato}");
                        }
                        else
                        {
                            dataSeleccionProducto.Rows[num_fila].Cells[2].Value = Convert.ToString(cantidad);
                        }
                    }
                }
            }

            total = 0;
            total1 = 0;

            foreach (DataGridViewRow Fila in dataPedido.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);
            }

            totalCuentaNueva(total);

            foreach (DataGridViewRow Fila in dataSeleccionProducto.Rows)
            {
                total1 += Convert.ToDouble(Fila.Cells[4].Value);
            }

            totalCuentaVieja(total1);

            if (dataSeleccionProducto.Rows.Count <= 0)
            {
                lblSubtotal1.Text = "RD$ 00.00";
                lblITBIS1.Text = "RD$ 00.00";
                lblLey1.Text = "RD$ 00.00";
                lblTotal1.Text = "RD$ 00.00";
            }
        }

        void totalCuentaNueva(double ptotal)
        {
            lbltotal.Text = "RD$ " + ptotal.ToString();
            sub_total = ptotal;
            itbis = ptotal * 0.18;
            porciento_ley = total * 0.10;

            lblSubTotal.Text = sub_total.ToString();
            lblItbis.Text = itbis.ToString();
            lblLey.Text = porciento_ley.ToString();

            ptotal = sub_total + itbis + porciento_ley;
            lbltotal.Text = "RD$ " + ptotal.ToString();
        }

        void totalCuentaVieja(double ptotal)
        {
            lblTotal1.Text = "RD$ " + ptotal.ToString();
            sub_total1 = ptotal;
            itbis1 = ptotal * 0.18;
            porciento_ley1 = total1 * 0.10;

            lblSubtotal1.Text = sub_total1.ToString();
            lblITBIS1.Text = itbis1.ToString();
            lblLey1.Text = porciento_ley1.ToString();

            ptotal = sub_total1 + itbis1 + porciento_ley1;
            lblTotal1.Text = "RD$ " + ptotal.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataPedido.Rows.Count <= 0)
            {
                return;
            }

            num_fila = 0;
            num_fila = dataPedido.CurrentRow.Index;
            existe = false;

            if (dataPedido.Rows.Count == 0)
            {
                MessageBox.Show("seleccionar articulos");
                return;
            }    
            
            if (dataSeleccionProducto.Rows.Count == 0)
            {
                cantidad = Convert.ToDecimal(txtCantidad.Text);

                if (cantidad == Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value))
                {
                    dataSeleccionProducto.Rows.Add(new string[]
                    {
                    Convert.ToString(dataPedido[0, dataPedido.CurrentRow.Index].Value),
                    Convert.ToString(dataPedido[1, dataPedido.CurrentRow.Index].Value),
                    Convert.ToString(txtCantidad.Text),
                    Convert.ToString(dataPedido[3, dataPedido.CurrentRow.Index].Value),
                    Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataPedido[3, dataPedido.CurrentRow.Index].Value)),
                    });

                    dataPedido.Rows.Remove(dataPedido.CurrentRow);
                }
                else
                {
                    dataSeleccionProducto.Rows.Add(new string[]
                    {
                    Convert.ToString(dataPedido[0, dataPedido.CurrentRow.Index].Value),
                    Convert.ToString(dataPedido[1, dataPedido.CurrentRow.Index].Value),
                    Convert.ToString(txtCantidad.Text),
                    Convert.ToString(dataPedido[3, dataPedido.CurrentRow.Index].Value),
                    Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataPedido[3, dataPedido.CurrentRow.Index].Value)),
                    });

                    cantidad = Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value);
                    cantidad = cantidad - 1;
                    id_plato = Convert.ToInt32(dataPedido.Rows[num_fila].Cells[0].Value);

                    if (cantidad == 0)
                    {
                        dataPedido.Rows.Remove(dataPedido.CurrentRow);
                    }
                    else
                    {
                        dataPedido.Rows[num_fila].Cells[2].Value = Convert.ToString(cantidad);
                    }

                    dataPedido.Refresh();
                }
            }
            else
            {
                foreach (DataGridViewRow Fila in dataSeleccionProducto.Rows)
                {
                    if (Fila.Cells[0].Value.ToString() == Convert.ToString(dataPedido[0, dataPedido.CurrentRow.Index].Value))
                    {
                        existe = true;
                        num_fila = Fila.Index;
                    }
                }
                if (existe == true)
                {
                    cantidad = Convert.ToDecimal(txtCantidad.Text);

                    if (cantidad == Convert.ToDecimal(dataPedido.CurrentRow.Cells[2].Value))
                    {
                        dataSeleccionProducto.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value) + Convert.ToDecimal(txtCantidad.Text));
                        dataSeleccionProducto.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value) * Convert.ToDecimal((dataSeleccionProducto.Rows[num_fila].Cells[3].Value))).ToString("F");
                        dataPedido.Rows.Remove(dataPedido.CurrentRow);
                    }
                    else
                    {
                        dataSeleccionProducto.Rows[num_fila].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value) + Convert.ToDecimal(txtCantidad.Text));
                        dataSeleccionProducto.Rows[num_fila].Cells[4].Value = (Convert.ToDecimal(dataSeleccionProducto.Rows[num_fila].Cells[2].Value) * Convert.ToDecimal((dataSeleccionProducto.Rows[num_fila].Cells[3].Value))).ToString("F");
                        dataPedido.Rows[num_fila].Cells[2].Value = Convert.ToString(cantidad);
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

                    cantidad = Convert.ToDecimal(dataPedido.Rows[num_fila].Cells[2].Value);
                    cantidad = cantidad - 1;
                    id_plato = Convert.ToInt32(dataPedido.Rows[num_fila].Cells[0].Value);

                    if (cantidad == 0)
                    {
                        dataPedido.Rows.Remove(dataPedido.CurrentRow);
                        DataTable dt = new DataTable();
                    }
                    else
                    {
                        dataPedido.Rows[num_fila].Cells[2].Value = Convert.ToString(cantidad);
                    }
                }
            }

            total = 0;
            total1 = 0;

            foreach (DataGridViewRow Fila in dataPedido.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);
            }

            totalCuentaNueva(total);

            foreach (DataGridViewRow Fila in dataSeleccionProducto.Rows)
            {
                total1 += Convert.ToDouble(Fila.Cells[4].Value);
            }

            totalCuentaVieja(total1);

            if (dataSeleccionProducto.Rows.Count <= 0)
            {
                lblSubtotal1.Text = "RD$ 00.00";
                lblITBIS1.Text = "RD$ 00.00";
                lblLey1.Text = "RD$ 00.00";
                lblTotal1.Text = "RD$ 00.00";
            }

            if (dataPedido.Rows.Count <= 0)
            {
                lblSubTotal.Text = "RD$ 00.00";
                lblItbis.Text = "RD$ 00.00";
                lblLey.Text = "RD$ 00.00";
                lbltotal.Text = "RD$ 00.00";
            }
        }
        

        private void button4_Click(object sender, EventArgs e)
        {   
            num_fact = Convert.ToInt32(dataCuentas.CurrentRow.Cells["id_factura"].Value);
            bool correcto;

            int fact_nueva;
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT max(id_factura) id_factura FROM temp_enc_factura");

            if (Convert.ToString(dt.Rows[0]["id_factura"]) == "")
            {
                fact_nueva = 1;
            }
            else
            {
                fact_nueva = Convert.ToInt32((dt.Rows[0]["id_factura"]));
                fact_nueva++;
            }

            using (var tran = utilidades.BeginTransation())
            {
                try
                {
                    string vsql = $"delete from temp_det_factura where  id_factura = {num_fact} ";
                    utilidades.ExecuteSQL(vsql);

                    vsql = $"delete from temp_enc_factura where  id_factura = {num_fact} ";
                    utilidades.ExecuteSQL(vsql);

                    vsql = $"EXEC actuaiza_temp_enc_fact {num_fact},{IdMesa},'{fecha}',{6} ,{Globals.IdUsuario},'{null}',{itbis1.ToString().Replace(',', '.')}, {total1.ToString().Replace(',', '.')}, {porciento_ley1.ToString().Replace(',', '.')}, {sub_total1.ToString().Replace(',', '.')},'{'A'}','{null}', {1}";
                    utilidades.ExecuteSQL(vsql);

                    foreach (DataGridViewRow fila in dataSeleccionProducto.Rows)
                    {
                        vsql = $"EXEC actuaiza_temp_det_fact '{num_fact}','{fila.Cells[0].Value}','{fila.Cells[2].Value}','{Convert.ToDouble(fila.Cells[3].Value)}',{0.18}";
                        utilidades.ExecuteSQL(vsql);
                    }
  

                    string vSql = $"EXEC actuaiza_temp_enc_fact {fact_nueva}, {IdMesa}, '{fecha}', {6} , {Globals.IdUsuario} ,'{null}',{itbis.ToString().Replace(',', '.')},{total.ToString().Replace(',', '.')},{porciento_ley.ToString().Replace(',', '.')},{sub_total.ToString().Replace(',', '.')},'{'A'}','{null}',{1}";
                    utilidades.ExecuteSQL(vSql);
               
                    foreach (DataGridViewRow fila in dataPedido.Rows)
                    {
                        vSql = $"EXEC actuaiza_temp_det_fact {fact_nueva},'{fila.Cells[0].Value}','{fila.Cells[2].Value}','{Convert.ToDouble(fila.Cells[3].Value)}',{0.18}";
                        utilidades.ExecuteSQL(vSql);
                    }             
                    
                    tran.Commit();
                    MessageBox.Show("Cuenta dividida", "Mensaje");
                    dataCuentas.Rows.Clear();
                    dataPedido.Rows.Clear();
                    traerCuentas();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
            
        }
    }
}
