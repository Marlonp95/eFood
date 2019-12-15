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
using System.Data;
using System.Drawing;

namespace eFood
{
    public partial class Recetas : Form
    {
        string vSql;
        string unidadmedida;
        bool existe = false;
        int num_fila = 0;
        int contador = 0;
        public Recetas()
        {
            InitializeComponent();
        }
        int id_receta;
        private void Recetas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'efoodDataSet8.unidad_medida' table. You can move, or remove it, as needed.
            this.unidad_medidaTableAdapter.Fill(this.efoodDataSet8.unidad_medida);
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT MAX(id_receta) id_receta FROM recetas");
            id_receta = Convert.ToInt32(dt.Rows[0]["id_receta"]);
            id_receta++;
        }

        private void txtCodProducto_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                vSql = $"SELECT id_plato, descripcion FROM platos WHERE id_plato = {txtCodProducto.Text} ";
                DataTable dt = new DataTable();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto)
                {
                    txtCodProducto.Text = dt.Rows[0]["id_plato"].ToString();
                    txtDetProducto.Text = dt.Rows[0]["descripcion"].ToString();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                vSql = $"SELECT id_productos, productos FROM productos WHERE id_productos = {txtCodigo.Text} ";
                DataTable dt = new DataTable();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto)
                {
                    txtCodigo.Text = dt.Rows[0]["id_productos"].ToString();
                    txtDescripcion.Text = dt.Rows[0]["productos"].ToString();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        
            try
            {
                vSql = $"SELECT descripcion FROM unidad_medida WHERE id_unidad = {comboBox1.SelectedValue} ";
                DataTable dt = new DataTable();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto)
                {
                    unidadmedida = dt.Rows[0]["descripcion"].ToString();
                    MessageBox.Show(unidadmedida);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }

            DataSet DS = new DataSet();
            DS.ejecuta(vSql);
            //AGREGO EL ARTICULO
            if (contador == 0)
            {
                dataGridView1.Rows.Add(txtCodigo.Text, txtDescripcion.Text, txtPorcion.Text, comboBox1.SelectedValue, unidadmedida );
                contador++;
            }
            //PARA DETERMINAR EXISTENCIA Y LA POSICION DE DICHO ARTICULO
            else
            {
                foreach (DataGridViewRow Fila in dataGridView1.Rows)
                {
                    if (Fila.Cells[0].Value.ToString() == txtCodigo.Text)
                    {
                        existe = true;
                        num_fila = Fila.Index;
                    }
                }
                //SI ESTA AGREGADO ACTUALIZO CANTIDAD

                if (existe == true)
                {
                    string vSql1 = $"Select cantidad, productos, reorden From productos Where id_productos = " + txtCodigo.Text.Trim();
                    DataSet Dt = new DataSet();
                    Dt.ejecuta(vSql1);
                    dataGridView1.Rows[num_fila].Cells[2].Value = (Convert.ToDouble(txtPorcion.Text) + Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[2].Value)).ToString();
                }          
                else
                {
                dataGridView1.Rows.Add(txtCodigo.Text, txtDescripcion.Text, txtPorcion.Text, comboBox1.SelectedValue, unidadmedida);
                

                contador++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (contador > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                contador--;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Hide();
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
                MessageBox.Show("Error Guardando Imagen" + error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string categoria_plato ="";

            if (radioEntrada.Checked) { categoria_plato = "Entrada";}
            if (radioPrimer.Checked)  { categoria_plato  = "Primer Plato"; }
            if (radioSegundo.Checked) { categoria_plato = "Segundo Plato"; }
            if (radioPostre.Checked)  { categoria_plato  = "Postre"; }

            MessageBox.Show(categoria_plato);
            try
            {
                string vSql = $"EXEC [actualizareceta] '{id_receta}','{txtNombreReceta.Text.Trim()}','{txtReceta.Text.Trim()}','{categoria_plato}','{txtComensales.Text.Trim()}','{txtPreparacion.Text.Trim()}'";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Receta guardada "); }
                else MessageBox.Show("Error guardando receta ");

                foreach (DataGridViewRow Fila in dataGridView1.Rows)
                {
                    vSql = $"EXEC [actualiza_detalle_receta] '{id_receta}','{ Fila.Cells[0].Value.ToString()}','{ Fila.Cells[2].Value.ToString()}','{ Fila.Cells[3].Value.ToString()}'";
                    dt = new DataSet();
                    dt.ejecuta(vSql);
                    correcto = dt.ejecuta(vSql);
                    if (correcto) { }
                    else MessageBox.Show("Error guardando receta ");
                }

            }
            catch(Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
    }
}
