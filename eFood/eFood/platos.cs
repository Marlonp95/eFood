﻿using System;
using System.Data;
using System.Windows.Forms;
using utilidad;

namespace eFood
{
    public partial class platos : Form
    {
        public platos()
        {
            InitializeComponent();
        }
        int id_plato;

        public void traerPlatos()
        {
            DataTable dt = new DataTable();
            dt.ejecuta("select a.id_plato, a.plato, a.descripcion, a.precio, a.itbis, b.receta, c.DESCRIPCION from platos a inner join recetas b on a.id_receta = b.id_receta inner join categoria c on a.id_categoria = c.ID_CATEGORIA");
            foreach (DataRow Fila in dt.Rows)
            {
                dataGridView1.Rows.Add
                (
                    Convert.ToString(Fila["id_plato"]),
                    Convert.ToString(Fila["plato"]),
                    Convert.ToString(Fila["descripcion"]),
                    Convert.ToString(Fila["precio"]),
                    Convert.ToString(Fila["itbis"]),
                    Convert.ToString(Fila["receta"]),
                    Convert.ToString(Fila["DESCRIPCION"])
                );
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.Equals("") == false)
                {
                    pictureBox1.Load(openFileDialog1.FileName);
                    txturl.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error Guardando Imagen");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void platos_Load(object sender, EventArgs e)
        {
            traerPlatos();
            // TODO: This line of code loads data into the 'efoodDataSet11.recetas' table. You can move, or remove it, as needed.
            this.recetasTableAdapter.Fill(this.efoodDataSet11.recetas);
            // TODO: This line of code loads data into the 'efoodDataSet10.categoria' table. You can move, or remove it, as needed.
            this.categoriaTableAdapter.Fill(this.efoodDataSet10.categoria);
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT MAX(id_plato) id_plato FROM platos");
            if (Convert.ToString(dt.Rows[0]["id_plato"])=="")
            {
                id_plato = 1;
            }
            else
            {
                id_plato = Convert.ToInt32(dt.Rows[0]["id_plato"]);
                id_plato++;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {              
                string vSql = $"EXEC [actualizaplatos] '{id_plato}','{txtplato.Text.Trim()}','{txtDescripcion.Text.Trim()}','{txtprecio.Text.Trim()}','{txtitbis.Text.Trim()}','{comboReceta.SelectedValue}','{comboCategoria.SelectedValue}'";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Receta guardada "); }
                else MessageBox.Show("Error guardando receta ");
            }
            catch (Exception)
            {

                throw;
            }

            traerPlatos();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DataTable dt = new DataTable();
            dt.ejecuta($"select a.id_plato, a.plato, a.descripcion, a.precio, a.itbis, b.receta, c.DESCRIPCION from platos a inner join recetas b on a.id_receta = b.id_receta inner join categoria c on a.id_categoria = c.ID_CATEGORIA where a.descripcion like ('%" + txtBuscar.Text.Trim() + "%')");
            foreach (DataRow Fila in dt.Rows)
            {
                dataGridView1.Rows.Add
                (
                    Convert.ToString(Fila["id_plato"]),
                    Convert.ToString(Fila["plato"]),
                    Convert.ToString(Fila["descripcion"]),
                    Convert.ToString(Fila["precio"]),
                    Convert.ToString(Fila["itbis"]),
                    Convert.ToString(Fila["receta"]),
                    Convert.ToString(Fila["DESCRIPCION"])
                );
            }
        }
    }
}
