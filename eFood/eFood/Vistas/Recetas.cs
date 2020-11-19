using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using utilidad;
using eFood.Utils;

namespace eFood
{
    public partial class Recetas : Form
    {
        string vSql;
        string unidadmedida;
        bool existe = false;
        int num_fila = 0;
        int contador = 0;
        int id_receta;

        public Recetas()
        {
            InitializeComponent();
            TextBoxOM om = new TextBoxOM();
            txtNombreReceta.TextChanged+=  om.TextBoxLetterKeyPress;
        }
       
        private void Recetas_Load(object sender, EventArgs e)
        {

            ComboBuscaRecet.DataSource = utilidades.ejecuta("Select id_receta, receta from recetas");
            ComboBuscaRecet.DisplayMember = "receta";
            ComboBuscaRecet.ValueMember = "id_receta";

            comboUnidad.DataSource = utilidades.ejecuta("Select id_unidad, descripcion from unidad_medida");
            comboUnidad.DisplayMember = "descripcion";
            comboUnidad.ValueMember = "id_unidad";

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
                vSql = $"SELECT descripcion FROM unidad_medida WHERE id_unidad = {comboUnidad.SelectedValue} ";
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
                dataGridView1.Rows.Add(txtCodigo.Text, txtDescripcion.Text, txtPorcion.Text, comboUnidad.SelectedValue, unidadmedida );
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
                dataGridView1.Rows.Add(txtCodigo.Text, txtDescripcion.Text, txtPorcion.Text, comboUnidad.SelectedValue, unidadmedida);
                

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
                MessageBox.Show("Error Guardando Imagen");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (this.IsValid()) { MessageBox.Show(Metodos.ErrorMessage); return; }
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
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Receta guardada "); }
                else MessageBox.Show("Error guardando receta ");

                foreach (DataGridViewRow Fila in dataGridView1.Rows)
                {
                    vSql = $"EXEC [actualiza_detalle_receta] '{id_receta}','{ Fila.Cells[0].Value.ToString()}','{ Fila.Cells[2].Value.ToString()}','{ Fila.Cells[3].Value.ToString()}'";
                    dt = new DataSet();
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

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"Select a.id_productos, a.productos, b.porcion, c.id_unidad, c.descripcion from productos a  INNER JOIN detalle_receta b ON A.id_productos = b.id_producto INNER JOIN unidad_medida C ON b.id_unidad = c.id_unidad where b.id_receta = {ComboBuscaRecet.SelectedValue}");
            foreach (DataRow Fila in dt.Rows)
            {
                dataGridView1.Rows.Add
                (
                    Convert.ToString(Fila["id_productos"]),
                    Convert.ToString(Fila["productos"]),
                    Convert.ToString(Fila["porcion"]),
                    Convert.ToString(Fila["id_unidad"]),
                    Convert.ToString(Fila["descripcion"])
                );

                dt = new DataTable();

                dt.ejecuta($"SELECT * from Recetas WHERE id_receta = {ComboBuscaRecet.SelectedValue}");

                txtNombreReceta.Text = dt.Rows[0]["receta"].ToString();
                txtComensales.Text   = dt.Rows[0]["comensales"].ToString();
                txtPreparacion.Text  = dt.Rows[0]["tiempo"].ToString();
                txtReceta.Text       = dt.Rows[0]["descripcion"].ToString();
                string cat           = dt.Rows[0]["categoria"].ToString();

                if (cat == "Entrada") {radioEntrada.Checked= true;}
                if (cat == "Primer Plato")  {radioPrimer.Checked = true; }
                if (cat == "Segundo Plato") { radioSegundo.Checked = true; }
                if (cat == "Postre") { radioPostre.Checked = true; }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtReceta.Clear();
            txtPorcion.Clear();
            txtDescripcion.Clear();
            txtComensales.Clear();
            txtDetProducto.Clear();
            txtNombreReceta.Clear();
            txtCodigo.Clear();
            txtPreparacion.Clear();
            txtCodProducto.Clear();
            dataGridView1.Rows.Clear();
        }


    }
}
