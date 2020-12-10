using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eFood.Reportes
{
    public partial class ReporteFactura : Form
    {
        public ReporteFactura()
        {
            InitializeComponent();
        }
        public int idFactura { get; set; }

        private void ReporteFactura_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSetFactura.datos_factura' table. You can move, or remove it, as needed.
           // this.datos_facturaTableAdapter.Fill(this.DataSetFactura.datos_factura, idFactura);

            this.reportViewer1.RefreshReport();
        }
    }
}
