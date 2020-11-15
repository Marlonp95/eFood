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
using System.Data.SqlClient;
using System.Collections;

namespace eFood
{
    public partial class dashboard : Form
    {

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=PRACTICA_DASHBOARD;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;

        public dashboard()
        {
            InitializeComponent();
        }
        ArrayList Categoria = new ArrayList();
        ArrayList CantidadProducto = new ArrayList();
        ArrayList Cantidad = new ArrayList();
        ArrayList Producto = new ArrayList();

        private void GraficoCategorias()
        {

            cmd = new SqlCommand("ProdPorCategoria", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Categoria.Add(dr.GetString(0));
                CantidadProducto.Add(dr.GetInt32(1));
            }

            chart1.Series[0].Points.DataBindXY(Categoria, CantidadProducto);
            dr.Close();
            con.Close();
        }

        private void GraficoProductosPreferidos()
        {

            cmd = new SqlCommand("ProdPreferidos", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Producto.Add(dr.GetString(0));
                Cantidad.Add(dr.GetInt32(1));
            }

            test.Series[0].Points.DataBindXY(Producto, Cantidad);
            dr.Close();
            con.Close();
        }
        private void DashboardDatos()
        {
            cmd = new SqlCommand("DashboardDatos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter total = new SqlParameter("@totVentas", 0); total.Direction = ParameterDirection.Output;
            SqlParameter nprod = new SqlParameter("@nprod", 0); nprod.Direction = ParameterDirection.Output;
            SqlParameter nmarca = new SqlParameter("@nmarc", 0); nmarca.Direction = ParameterDirection.Output;
            SqlParameter ncliente = new SqlParameter("@nclient", 0); ncliente.Direction = ParameterDirection.Output;
            SqlParameter nproveedores = new SqlParameter("@nprove", 0); nproveedores.Direction = ParameterDirection.Output;
            SqlParameter nempleados = new SqlParameter("@nemple", 0); nempleados.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(total);
            cmd.Parameters.Add(nprod);
            cmd.Parameters.Add(nmarca);
            cmd.Parameters.Add(ncliente);
            cmd.Parameters.Add(nproveedores);
            cmd.Parameters.Add(nempleados);
            con.Open();
            cmd.ExecuteNonQuery();
            lblTotalVentas.Text = cmd.Parameters["@totVentas"].Value.ToString();
            lblCAntMarcas.Text = cmd.Parameters["@nmarc"].Value.ToString();
            lblCantProd.Text = cmd.Parameters["@nprod"].Value.ToString();
            lblCantClient.Text = cmd.Parameters["@nclient"].Value.ToString();
            lblCantEmple.Text = cmd.Parameters["@nemple"].Value.ToString();
            lblCantProve.Text = cmd.Parameters["@nprove"].Value.ToString();
            con.Close();
        }

      


        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            GraficoCategorias();
            GraficoProductosPreferidos();
            DashboardDatos();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
