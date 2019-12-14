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
        public Recetas()
        {
            InitializeComponent();
        }
        int id_receta;
        private void Recetas_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.ejecuta("SELECT MAX(id_receta) id_receta FROM recetas");
            id_receta = Convert.ToInt32(dt.Rows[0]["id_receta"]);
            id_receta++;
    
        }

        private void txtCodProducto_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                vSql = $"SELECT id_productos, productos FROM productos WHERE id_productos = {txtCodProducto.Text} ";
                DataTable dt = new DataTable();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto)
                {
                    txtCodProducto.Text = dt.Rows[0]["id_productos"].ToString();
                    txtDetProducto.Text = dt.Rows[0]["productos"].ToString();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
    }
}
