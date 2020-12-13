using System;
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
            dataGridView1.Rows.Clear();
            
            DataTable dt = new DataTable();
            dt.ejecuta($@"select a.id_plato, a.plato, a.descripcion, a.precio, a.itbis, b.receta, c.DESCRIPCION categoria
                            from platos a inner join recetas b on a.id_receta = b.id_receta 
                                          inner join categoria c on a.id_categoria = c.ID_CATEGORIA");
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
                    Convert.ToString(Fila["categoria"])
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
            catch (Exception ex)
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
            try
            {
                comboCategoria.DataSource = utilidades.ejecuta("select id_categoria, descripcion from categoria");
                comboCategoria.DisplayMember = "descripcion";
                comboCategoria.ValueMember = "id_categoria";

                comboitbis.DataSource = utilidades.ejecuta($@"select id_itbis ,convert(varchar(max),id_itbis)+' - '+ convert(varchar(max),itbis) descripcion from itbis");
                comboitbis.DisplayMember = "descripcion";
                comboitbis.ValueMember = "id_itbis";

                comboReceta.DataSource = utilidades.ejecuta("Select id_receta, receta from recetas");
                comboReceta.DisplayMember = "receta";
                comboReceta.ValueMember = "id_receta";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           

            traerPlatos();                   

            DataTable ds = new DataTable();
            ds.ejecuta("select * from categoria");

            comboCategoria.DisplayMember = "DESCRIPCION";
            comboCategoria.ValueMember = "ID_CATEGORIA";
            comboCategoria.DataSource = ds;
        
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
                if (correcto)
                {
                    MessageBox.Show("Receta guardada ");
                    traerPlatos();

                    txtplato.Clear();
                    txtprecio.Clear();
                    txtitbis.Clear();
                    txtDescripcion.Clear();

                }
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

        private void comboItbis_SelectedValueChanged(object sender, EventArgs e)
        {
            var data = utilidades.ejecuta($"select itbis from itbis where id_itbis = {comboitbis.SelectedValue}").Rows;
            txtitbis.Text = string.Empty;
            txtitbis.Text = data[0].Field<decimal>("itbis").ToString();
        }

        private void comboitbis_ValueMemberChanged(object sender, EventArgs e)
        {
            var data = utilidades.ejecuta($"select itbis from itbis where id_itbis = {comboitbis.SelectedValue}").Rows;
            txtitbis.Text = string.Empty;
            txtitbis.Text = data[0].Field<decimal>("itbis").ToString();
        }
    }
}
