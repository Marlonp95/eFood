using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eFood
{
    public partial class reporte : Form
    {
        public reporte()
        {
            InitializeComponent();
        }

        private void reporte_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataFactura.datos_factura' table. You can move, or remove it, as needed.
          //  this.datos_facturaTableAdapter.Fill(this.DataFactura.datos_factura);

            this.reportViewer1.RefreshReport();
        }
    }
}
