using eFood.Utils;
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
    public partial class secuencia_ncf : Form
    {
        public secuencia_ncf()
        {
            InitializeComponent();
        }

        private void secuencia_ncf_Load(object sender, EventArgs e)
        {

            comboTipoNcf.DataSource = utilidades.ejecuta("select tipo, descripcion from tipo_ncf");
            comboTipoNcf.DisplayMember = "descripcion";
            comboTipoNcf.ValueMember = "tipo";

            DataTable dt = new DataTable();
            dt.ejecuta("select * from secuencia_ncf");
            dataSecuecia.DataSource = dt;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string vSql = $"EXEC [actualiza_secuencia_ncf] '{comboTipoNcf.SelectedValue}','{txtletra.Text.Trim()}','{txtSecuenciaInicial.Text.Trim()}','{txtSecuenciaFinal.Text.Trim()}','{dateTimePicker.Value}','{Globals.Usuarios}'";
                DataSet dt = new DataSet();
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Secuencia NCF guardada "); }
                else MessageBox.Show("Error guardando Secuencia NCF ");
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                MessageBox.Show("Error" + ex.ToString());
               
            }
          
        }
    }
}
