﻿using System;
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
        int id_factura ;
        bool correcto;

        public string mesatag { get; set; }
         
        public pedido(string pMesaTag)
        {
            this.mesatag = pMesaTag;
            InitializeComponent();
        }
      
        

        public void traerPedido()
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"select a.id_productos, b.productos, b.descripcion, a.cantidad, a.cantidad * b.precio_comercial importe, a.precio from temp_det_factura a inner join productos b on a.id_productos = b.id_productos inner join temp_enc_factura c on a.id_factura = c.id_mesa where c.id_mesa = {mesatag};");

            foreach (DataRow Fila in dt.Rows)
            {
                DataPedido.Rows.Add
                (
                    Convert.ToString(Fila["id_productos"]),
                    Convert.ToString(Fila["productos"]),
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
            total =sub_total + itbis + porciento_ley;
            lblTotal.Text = "RD$ " + total.ToString();

        }
        string vSql;
        DataTable dt;
        private void pedido_Load(object sender, EventArgs e)
        {
            MessageBox.Show("fecha" + fecha);
            try
            {
                dt = new DataTable();
                vSql = $"SELECT nombre1, apellido1 From persona Where id_persona= {login.codigo} ";
                dt.ejecuta(vSql);
                lblusuario.Text =dt.Rows[0]["nombre1"].ToString() + " " + dt.Rows[0]["apellido1"].ToString();

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
                id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));
                id_factura++;
            }
            else
            {
                id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));
            }
            traerPedido();
            MessageBox.Show("Factura#"+id_factura);
          
            lblmesa.Text = mesatag;
            txtCantidad.Text = "1";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_productos, productos, descripcion, precio_comercial FROM productos WHERE id_categoria = 1");
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
                if(DataPedido.Rows.Count == 0)
                {
                    DataPedido.Rows.Add(new string[]
                   {
                        Convert.ToString(dataSeleccionProducto[0, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[1, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(dataSeleccionProducto[2, dataSeleccionProducto.CurrentRow.Index].Value),
                        Convert.ToString(txtCantidad.Text),
                        Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value)),
                        Convert.ToString(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value),
                        
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
                        Convert.ToString(dataSeleccionProducto[3, dataSeleccionProducto.CurrentRow.Index].Value)
                        
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
            sub_total     = total;
            itbis         = total * 0.18;
            porciento_ley = total * 0.10;
            lblSubTotal.Text = sub_total.ToString();
            lblItbis.Text    = itbis.ToString();
            lblLey.Text      = porciento_ley.ToString();
            total = sub_total+ itbis + porciento_ley;
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
                cantidad--;
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
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_productos, productos, descripcion, precio_comercial FROM productos WHERE id_categoria = 2");
            dataSeleccionProducto.DataSource = dt;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //total = Convert.ToDouble(lblTotal.Text);
            int rnc;
            sub_total = total;
            itbis         = total * 0.18;
            porciento_ley = total * 0.10;
            total  =+ itbis + porciento_ley;

            dt = new DataTable();
            vSql = $"SELECT id_factura FROM temp_enc_factura WHERE estado ='A' and id_mesa = {mesatag} ";
            dt.ejecuta(vSql);
            correcto = dt.ejecuta(vSql);

            if (dt.Rows.Count == 0)
            {
                dt = new DataTable();
                dt.ejecuta("SELECT max(id_factura) id_factura FROM temp_enc_factura");
                id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));
                id_factura++;
               
                try
                {
                    string vSql = $"EXEC actuaiza_temp_enc_fact '{id_factura}','{mesatag}','{fecha}','{6}','{login.codigo}','{null}','{itbis}','{total}','{porciento_ley}','{sub_total}','{'A'}','{null}'";
                    DataSet dt = new DataSet();
                    dt.ejecuta(vSql);
                    correcto = dt.ejecuta(vSql);

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error" + error.ToString());
                }

                if (correcto)
                {
                    MessageBox.Show("Se Guardo ");
                }
                else MessageBox.Show("Error Salvando datos ");
            }
            else
            {
                id_factura = Convert.ToInt32((dt.Rows[0]["id_factura"]));

                foreach (DataGridViewRow fila in DataPedido.Rows)
                {
                    try
                    {
                        string vSql = $"EXEC actuaiza_temp_det_fact '{id_factura}','{fila.Cells[0].Value}','{fila.Cells[3].Value}','{fila.Cells[5].Value}','{fila.Cells[0].Value}'";
                        DataSet dt = new DataSet();
                        dt.ejecuta(vSql);
                        correcto = dt.ejecuta(vSql);

                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error" + error.ToString());
                    }

                    if (correcto)
                    {
                        MessageBox.Show("Se Guardo ");
                    }
                    else MessageBox.Show("Error Salvando datos ");
                }
    }
            if (DataPedido.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para guardar");
            }

        }
    }
}
