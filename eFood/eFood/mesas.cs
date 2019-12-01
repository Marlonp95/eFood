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
        private Control PanelPrincipal ;
        public mesas( Control pPanel)
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
            
            Cargar_MesaxUbicacion();
            Cargar_Ubicacion();

        }
        public void Cargar_MesaxUbicacion(string pCondicion=null)
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"select * from mesa {pCondicion}");

            foreach (DataRow dr in dt.Rows)
            {
                btn = new btnMesa()
                {
                    Height = 120,
                    Width = 120,
                    Image = Properties.Resources.mesa1,
                    TextAlign = ContentAlignment.BottomCenter,
                    Name = $"Mesa_{dr["id_mesa"].ToString()}",
                    Tag = dr["id_mesa"].ToString(),
                    Text = dr["descripcion"].ToString(),
                    Estado = dr["estado"].ToString(),
                    pFormulario = PanelPrincipal,
                    runFormulario = new pedido(),
                };
                contenedor.Controls.Add(btn);
                btn.EstadosColor(btn.Estado);
                btn.Click += btnMesa_Click;
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
        
                };
                ContenedorUbicaciones.Controls.Add(btnUbicacion);
               // btn.Click += btnMesa_Click;
            }
        }

    }
}
