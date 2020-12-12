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
    public partial class mesas : Form
    {
        private Panel PanelPrincipal ;
        public mesas(ref Panel pPanel)
        {
            InitializeComponent();
            PanelPrincipal = pPanel;
        }
       
        public void mesa(btnMesa btn)
        {
                string estado;
                string vSql = $"SELECT estado  FROM mesa where  id_mesa = " + btn.IdMesa.ToString();
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                estado = dt.Tables[0].Rows[0]["estado"].ToString();

            if (utilidades.DsTieneDatos(dt))
                {              
                    
                    if (MessageBox.Show("Desea Abrir Mesa " + btn.IdMesa.ToString(), "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        vSql = $"UPDATE mesa SET estado = 'O'  WHERE  id_mesa = " +btn.IdMesa.ToString(); ; 
                        dt = new DataSet();
                        dt.ejecuta(vSql);
                    }
            }
        }
  
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnMesa_Click(object sender, EventArgs e)
        {
           
        }

        btnMesa btn;
        btnUbicacion btnUbicacion;

        private void mesas_Load(object sender, EventArgs e)
        {
            comboOrigen.DataSource = utilidades.ejecuta(" select id_mesa, descripcion from mesa");
            comboOrigen.DisplayMember = "descripcion";
            comboOrigen.ValueMember = "id_mesa";

            comboDestino.DataSource = utilidades.ejecuta(" select id_mesa, descripcion from mesa");
            comboDestino.DisplayMember = "descripcion";
            comboDestino.ValueMember = "id_mesa";

            Cargar_MxUb();
            Cargar_Ubicacion();
        }
        public void Cargar_MxUb(string pCondicion = null)
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"select * from mesa {pCondicion}");

            List<btnMesa> lbtn = CallForm.Cargar_MesaxUbicacion(ref PanelPrincipal);
            foreach (btnMesa item in lbtn)
            {
                contenedor.Controls.Add(item);
                CallForm.EstadosColor(item);
            }
        }
        public void Cargar_Ubicacion(string pCondicion = null)
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"select * from ubicacion {pCondicion}");
           
            foreach (DataRow dr in dt.Rows)
            {
                btnUbicacion = new btnUbicacion()
                {
                    Height = 100,
                    Width = 100,
                    BackColor = Color.White,
                    Image = Properties.Resources.iconfinder_Artboard_10_3915333__1_,
                    TextAlign = ContentAlignment.BottomCenter,
                    Name = $"Area_{dr["descripcion"].ToString()}",
                    Tag = dr["id_ubicacion"].ToString(),
                    Text = dr["descripcion"].ToString(),
                    contenedor=contenedor,
                    PanelPrincipal= PanelPrincipal,
                };
                ContenedorUbicaciones.Controls.Add(btnUbicacion);
               
            }
            btnUbicacion = new btnUbicacion()
            {
                Height = 100,
                Width = 100,
                BackColor = Color.White,
                Image = Properties.Resources.iconfinder_Artboard_10_3915333__1_,
                TextAlign = ContentAlignment.BottomCenter,
                contenedor = contenedor,
                PanelPrincipal = PanelPrincipal,
                Tag = string.Empty,
                Text ="Todas",

            };
            ContenedorUbicaciones.Controls.Add(btnUbicacion);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contenedor.Controls.Clear();
            Cargar_MxUb();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea unir Mesa " + comboOrigen.SelectedValue.ToString() + " con la mesa "+ comboDestino.SelectedValue.ToString() , "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                DialogResult vrest =  MessageBox.Show("Desea unir mesas con cuentas separadas ?", "Aviso", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (vrest == DialogResult.Yes)
                {
                    try
                    {
                        string vSql;

                        DataTable dt = new DataTable();
                        vSql = $"UPDATE temp_enc_factura SET id_mesa = '{comboDestino.SelectedValue}'  WHERE  id_mesa = {comboOrigen.SelectedValue}";
                        dt = new DataTable();
                        dt.ejecuta(vSql);


                        dt = new DataTable();
                        vSql = $"update mesa set estado ='D' where id_mesa = {comboOrigen.SelectedValue}";
                        dt.ejecuta(vSql);  
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    contenedor.Controls.Clear();
                    Cargar_MxUb();

                }
                else if(vrest== DialogResult.No)
                {
                    string vSql;
                    DataTable dt = new DataTable();
                    vSql = $"UPDATE mesa SET estado = 'O'  WHERE  id_mesa = " + btn.IdMesa.ToString(); ;
                    dt = new DataTable();
                    dt.ejecuta(vSql);
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        //private void button3_Click(object sender, EventArgs e)
        //{

        //    string vSql = $"UPDATE mesa SET estado = 'S' where  id_mesa = " + btn.Tag.ToString();
        //    DataSet dt = new DataSet();
        //    dt.ejecuta(vSql);
        //    MessageBox.Show("LISTO");
        //}
    }
}
