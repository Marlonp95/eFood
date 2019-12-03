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
        public string mesatag { get; set; }

        public pedido(string pMesaTag)
        {
            this.mesatag = pMesaTag;
            InitializeComponent();
        }


        private void pedido_Load(object sender, EventArgs e)
        {
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
        bool existe = false;
        int num_fila = 0;
        int contador;
        double total;

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
                        DataPedido.Rows[num_fila].Cells[3].Value = Convert.ToString(Convert.ToInt32(DataPedido.Rows[num_fila].Cells[3].Value) + Convert.ToInt32(txtCantidad.Text));
                        DataPedido.Rows[num_fila].Cells[4].Value = Convert.ToString(Convert.ToInt32(DataPedido.Rows[num_fila].Cells[3].Value) * Convert.ToInt32((DataPedido.Rows[num_fila].Cells[5].Value)));
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
        }
        int cantidad;
        private void button11_Click(object sender, EventArgs e)
        {
            total = 0;
            num_fila = 0;
            num_fila = DataPedido.CurrentRow.Index;
            cantidad = Convert.ToInt32(DataPedido.Rows[num_fila].Cells[3].Value);
            cantidad++;
            DataPedido.Rows[num_fila].Cells[3].Value = Convert.ToString(cantidad);
            DataPedido.Rows[num_fila].Cells[4].Value = Convert.ToString(Convert.ToInt32(DataPedido.Rows[num_fila].Cells[3].Value) * Convert.ToInt32((DataPedido.Rows[num_fila].Cells[5].Value)));

            foreach (DataGridViewRow Fila in DataPedido.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);
            }

            lblTotal.Text = "RD$ " + total.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Seguro desea quitar articulo ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                total = 0;
                num_fila = 0;
                num_fila = DataPedido.CurrentRow.Index;
                cantidad = Convert.ToInt32(DataPedido.Rows[num_fila].Cells[3].Value);
                cantidad--;
                DataPedido.Rows[num_fila].Cells[3].Value = Convert.ToString(cantidad);
                DataPedido.Rows[num_fila].Cells[4].Value = Convert.ToString(Convert.ToInt32(DataPedido.Rows[num_fila].Cells[3].Value) * Convert.ToInt32((DataPedido.Rows[num_fila].Cells[5].Value)));

                foreach (DataGridViewRow Fila in DataPedido.Rows)
                {
                    total += Convert.ToDouble(Fila.Cells[4].Value);
                }

                lblTotal.Text = "RD$ " + total.ToString();
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT id_productos, productos, descripcion, precio_comercial FROM productos WHERE id_categoria = 2");
            dataSeleccionProducto.DataSource = dt;
        }

    }
}
