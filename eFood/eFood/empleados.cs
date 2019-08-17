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
    public partial class empleados : Form
    {
        public empleados()
        {
            InitializeComponent();
        }
        public void treaerEmpleado()
        {
            DataTable dt = new DataTable();
            dt.ejecuta("Select * from empleado");
            dataempleado.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            bool ValidaFormulario = false;
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox || item is ComboBox || item is DateTimePicker)
                {
                    item.BackColor = Color.White;
                }
            }
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox || item is ComboBox || item is DateTimePicker)
                {
                    if (item.Tag.ToString().ToUpper() == "NO VACIO".ToUpper() && string.IsNullOrEmpty(item.Text.Trim()))
                    {
                        item.BackColor = Color.Red;
                        ValidaFormulario = true;
                    }
                }
            }
            if (ValidaFormulario)
            {
                MessageBox.Show("Por favor Complete los campos");
                return;
            }
            try
            {
                string vSql = $"EXEC actualizapersona '{txtcodigo.Text.Trim()}','{txtnombre.Text.Trim()}','{txtapellido.Text.Trim()}','{txtapellido2.Text.Trim()}','{txtdireccion.Text.Trim()}','{txtdocumento.Text.Trim()}','{txturl.Text.Trim()}'";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
            try
            {
                string vSql = $"EXEC actualizaempleado '{txtficha.Text.Trim()}','{txtcodigo.Text.Trim()}','{fechaentrada.Value.Date}','{fechasalida.Value.Date}','{combocargo.SelectedValue.ToString()}','{combodepartamento.SelectedValue.ToString()}','{combopago.SelectedValue.ToString()}','{txtsalario.Text.Trim()}'";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Se Guardo "); treaerEmpleado(); }
                else MessageBox.Show("Error Salvando datos ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                string vSql = $"EXEC eliminaempleado '{txtcodigo.Text.Trim()}'";

                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Se Elimino El Empleado "); treaerEmpleado(); }
                else MessageBox.Show("Error Eliminando datos ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
        }

        private void empleados_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'efoodDataSet4.tipo_pago' table. You can move, or remove it, as needed.
            this.tipo_pagoTableAdapter.Fill(this.efoodDataSet4.tipo_pago);
            // TODO: This line of code loads data into the 'efoodDataSet3.departamento' table. You can move, or remove it, as needed.
            this.departamentoTableAdapter.Fill(this.efoodDataSet3.departamento);
            // TODO: This line of code loads data into the 'efoodDataSet2.cargo' table. You can move, or remove it, as needed.
            this.cargoTableAdapter.Fill(this.efoodDataSet2.cargo);
            treaerEmpleado();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtcodigo_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtficha_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtficha_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtficha.Text)) return;

                string vSql = $"SELECT * From empleado Where ficha Like ('%" + txtficha.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtcodigo.Text = dt.Tables[0].Rows[0]["id_persona"].ToString();
                    combocargo.SelectedValue = dt.Tables[0].Rows[0]["id_cargo"].ToString();
                    combodepartamento.SelectedValue = dt.Tables[0].Rows[0]["id_departamento"].ToString();
                    combopago.SelectedValue = dt.Tables[0].Rows[0]["id_pago"].ToString();
                    txtsalario.Text = dt.Tables[0].Rows[0]["salario"].ToString();

                }
                else
                {
                    MessageBox.Show("EMPLEADO NO ENCONTRADO");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.Message);
            }

            try
            {
                string url;
                if (string.IsNullOrEmpty(txtficha.Text)) return;

                string vSql = $"SELECT * From persona Where id_persona Like ('%" + txtcodigo.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtnombre.Text = dt.Tables[0].Rows[0]["nombre1"].ToString();
                    txtapellido.Text = dt.Tables[0].Rows[0]["apellido1"].ToString();
                    txtapellido2.Text = dt.Tables[0].Rows[0]["apellido2"].ToString();
                    txtdireccion.Text = dt.Tables[0].Rows[0]["direccion"].ToString();             
                    txtdocumento.Text = dt.Tables[0].Rows[0]["documento"].ToString();
                    url = dt.Tables[0].Rows[0]["foto"].ToString();
                    pictureBox1.Image = Image.FromFile(url);
                }
                else
                { 
                        MessageBox.Show("CREAR EMPLEADO");          
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.Message);
            }

        }

        private void dataempleado_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            try
            {
                openFileDialog1.ShowDialog();
                if(openFileDialog1.FileName.Equals("")==false)
                {
                    pictureBox1.Load(openFileDialog1.FileName);
                    txturl.Text = openFileDialog1.FileName;
                }
            }
            catch(Exception error)
            {
                MessageBox.Show("Error Guardando Imagen"+error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string vSql = $"SELECT TOP 1 * FROM persona ORDER by id_persona DESC";
            DataSet dt = new DataSet();
            dt.ejecuta(vSql);
            bool correcto = dt.ejecuta(vSql);
            if (utilidades.DsTieneDatos(dt))
            {
                int codigo = Convert.ToInt32(dt.Tables[0].Rows[0]["id_persona"]);
                codigo++;
                txtcodigo.Text = Convert.ToString(codigo);
              
            }
            else
            {
                MessageBox.Show("CREAR EMPLEADO");
            }
        }
    }
}

