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
    public partial class btnUbicacion : Button
    {
        public int IdUbicacion { get; set; }

        public btnUbicacion()
        {
            InitializeComponent();
            this.Click += button1_Click;
        }

        public btnUbicacion(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        int id_ubicacion;

        public bool ubicacion(btnUbicacion btnUbicacion)
        {
           
            string vSql = $"SELECT id_ubicacion FROM ubicacion where  id_ubicacion = " + btnUbicacion.Tag.ToString();
            DataSet dt = new DataSet();
            dt.ejecuta(vSql);
            bool correcto = dt.ejecuta(vSql);
            id_ubicacion = Convert.ToInt32(dt.Tables[0].Rows[0]["id_ubicacion"].ToString());
           
                
            if (utilidades.DsTieneDatos(dt))
            {
                MessageBox.Show("Testa"+ id_ubicacion);
                return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Testa" + id_ubicacion);

        }
    }
}
