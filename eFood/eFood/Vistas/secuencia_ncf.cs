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
            dateTimePicker.CustomFormat = " ";
            dateTimePicker.ValueChanged += DateTimePicker_ValueChanged;
            dateTimePicker.KeyUp += DateTimePicker_KeyUp;
            

            comboTipoNcf.DataSource = utilidades.ejecuta("select tipo, convert(varchar(max),tipo)+' - '+descripcion descripcion from tipo_ncf");
            comboTipoNcf.DisplayMember = "descripcion";
            comboTipoNcf.ValueMember = "tipo";
            trarSecuencias();
        }

        public void trarSecuencias()
        {
            DataTable dt = new DataTable();
            dt.ejecuta($@"select  convert(varchar(max),d.tipo)+'-'+d.descripcion tipo_ncf, a.letra, a.secuencia_inicial, a.secuencia_final, a.ultimo_generado,
	                           a.fecha_creacion, fecha_vencimiento, c.nombre1+' '+c.apellido1+' '+c.apellido2 usuario
	                        from secuencia_ncf a inner join usuarios b on a.id_usuario = b.id_usuario
						                         inner join persona c on b.id_persona = c.id_persona
						                         inner join tipo_ncf d on a.id_tipo_ncf = d.tipo");
            dataSecuecia.DataSource = dt;
        }

        private void DateTimePicker_KeyUp(object sender, KeyEventArgs e)
        {
          if(e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space)
            {
                dateTimePicker.CustomFormat = " ";
            }
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
           dateTimePicker.CustomFormat = (dateTimePicker.Checked && dateTimePicker.Value != dateTimePicker.MinDate) ? "dd/MM/yyyy" : " ";
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
                if (Convert.ToInt32(txtSecuenciaInicial.Text) > Convert.ToInt32(txtSecuenciaFinal.Text))
                {
                    MessageBox.Show("La secuencia incial no puede ser mayor a la final.");
                }
                else
                {

                   var data = utilidad.utilidades.ejecuta($@"Select *  
                                                             from secuencia_ncf
                                                             where
                                                             (secuencia_inicial between {txtSecuenciaInicial.Text} and {txtSecuenciaFinal.Text}
                                                                or secuencia_final between {txtSecuenciaInicial.Text} and {txtSecuenciaFinal.Text})
                                                              and letra = 'B'
                                                              and id_tipo_ncf = {comboTipoNcf.SelectedValue}");
                    if (data.Rows.Count > 0)
                    {
                        MessageBox.Show("La secuencia que intenta insertar esta comprometida");
                        return;
                    }
                    else
                    {
                        var time = dateTimePicker.CustomFormat == " "? "NULL": $"'{dateTimePicker.Value}'";
                        
                        string vSql = $"EXEC [actualiza_secuencia_ncf] {comboTipoNcf.SelectedValue},'{txtletra.Text.Trim()}',{txtSecuenciaInicial.Text.Trim()},{txtSecuenciaFinal.Text.Trim()},{time},{Globals.Usuarios}";
                        DataSet dt = new DataSet();
                        bool correcto = dt.ejecuta(vSql);
                        if (correcto)
                        {
                            MessageBox.Show("Secuencia NCF guardada ");
                            trarSecuencias();
                        }
                        else MessageBox.Show("Error guardando Secuencia NCF ");
                    }
                }
                
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                MessageBox.Show("Error" + ex.ToString());
               
            }
          
        }
    }
}
