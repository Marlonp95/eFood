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
    public partial class ConsultaPlatos : Form
    {
        public ConsultaPlatos()
        {
            InitializeComponent();
        }

        private void ConsultaPlatos_Load(object sender, EventArgs e)
        {
            comboCategoria.DataSource = utilidades.ejecuta("select id_categoria, descripcion from categoria");
            comboCategoria.DisplayMember = "descripcion";
            comboCategoria.ValueMember = "id_categoria";


            traerPlatos();
        }

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

        private void button14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar Facturacion", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string vSql ;

            if (txtBuscar.Text != string.Empty)
            {
                vSql = $@"select a.id_plato, a.plato, a.descripcion, a.precio, a.itbis, b.receta, c.DESCRIPCION 
                            from platos a inner join recetas b on a.id_receta = b.id_receta 
                                inner join categoria c on a.id_categoria = c.ID_CATEGORIA where a.descripcion like ('%" + txtBuscar.Text.Trim() + "%')";
            }
            else
            {
                vSql = $@"select a.id_plato, a.plato, a.descripcion, a.precio, a.itbis, b.receta, c.DESCRIPCION 
                            from platos a inner join recetas b on a.id_receta = b.id_receta 
                                inner join categoria c on a.id_categoria = c.ID_CATEGORIA where c.ID_CATEGORIA = {comboCategoria.SelectedValue}";
            }

            dataGridView1.Rows.Clear();
            DataTable dt = new DataTable();
            dt.ejecuta(vSql);

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            return;
            DialogResult = DialogResult.OK;
        }
    }
}
