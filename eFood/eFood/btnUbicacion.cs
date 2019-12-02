using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using utilidad;

namespace eFood
{
    public partial class btnUbicacion : btnMesa
    {
        public int IdUbicacion { get; set; }
        public FlowLayoutPanel contenedor;
        public Panel PanelPrincipal;

        public btnUbicacion()
        {
            InitializeComponent();
            run_mesa = false;
            this.Click += button1_Click;
        }

        public btnUbicacion(IContainer container)
        {
            container.Add(this);
            InitializeComponent();

        }
        int id_ubicacion;

        //public bool ubicacion(btnUbicacion btnUbicacion)
        //{

        //    string vSql = $"SELECT id_ubicacion FROM ubicacion where  id_ubicacion = " + btnUbicacion.Tag.ToString();
        //    DataSet dt = new DataSet();
        //    dt.ejecuta(vSql);
        //    bool correcto = dt.ejecuta(vSql);
        //    id_ubicacion = Convert.ToInt32(dt.Tables[0].Rows[0]["id_ubicacion"].ToString());


        //    if (utilidades.DsTieneDatos(dt))
        //    {
        //        MessageBox.Show("Testa" + id_ubicacion);
        //        return true;
        //    }
        //    return false;
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            string pcondicion = string.Empty;
            if (!string.IsNullOrEmpty(this.Tag.ToString())) pcondicion = $"where id_ubicacion={this.Tag}";
            List<btnMesa> lbtn= CallForm.Cargar_MesaxUbicacion(ref PanelPrincipal, pcondicion);
            
            if (contenedor!=null)contenedor.Controls.Clear();
            foreach (btnMesa item in lbtn)
            {
               
                contenedor.Controls.Add(item);
                CallForm.EstadosColor(item);
            }

        }
    }
}
