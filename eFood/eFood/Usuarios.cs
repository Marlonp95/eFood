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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
      
        
        public void traerUsuario()
        {
            DataTable dt = new DataTable();
            dt.ejecuta("Select * from usuarios where estado = 1");
            datacliente.DataSource = dt;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            traerUsuario();
        }
        string comp;

      /*  private void txtficha_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtficha.Text)) return;
                
                string vSql = $"SELECT * From usuarios Where ficha Like ('%" + txtficha.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtcodigo.Text = dt.Tables[0].Rows[0]["id_usuario"].ToString();
                    txtusuario.Text = dt.Tables[0].Rows[0]["usuario"].ToString();
                    txtpass.Text = dt.Tables[0].Rows[0]["pass"].ToString();
                }
                else
                {
                    MessageBox.Show("USUARIO NO ENCONTRADO");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.Message);
            }

            try
            {
                if (string.IsNullOrEmpty(txtficha.Text)) return;

                string vSql = $"SELECT * From empleado Where ficha Like ('%" + txtficha.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    comp= dt.Tables[0].Rows[0]["id_persona"].ToString();
                    txtcargo.Text = dt.Tables[0].Rows[0]["id_cargo"].ToString();
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

            try
            {
                if (string.IsNullOrEmpty(txtficha.Text)) return;

                string vSql = $"SELECT * From persona Where id_persona Like ('%" + comp + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                  txtnombre.Text = dt.Tables[0].Rows[0]["nombre1"].ToString();
                  txtapellido.Text = dt.Tables[0].Rows[0]["apellido1"].ToString();

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
        }*/


        private void txtficha_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            bool ValidaFormulario = false;
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox || item is DateTimePicker)
                {
                    item.BackColor = Color.White;
                }
            }
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox || item is DateTimePicker)
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
                string vSql = $"EXEC actualizausuarios '{txtcodigo.Text.Trim()}','{txtusuario.Text.Trim()}','{txtpass.Text.Trim()}','{dateTimePicker1.Value.Date}','{txtcodigo.Text.Trim()}','{txtficha.Text.Trim()}'";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Se Guardo "); traerUsuario(); }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
        }

        private void txtficha_Validating_1(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtficha.Text)) return;

                string vSql = $"SELECT * From usuarios Where ficha Like ('%" + txtficha.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtcodigo.Text = dt.Tables[0].Rows[0]["id_usuario"].ToString();
                    txtusuario.Text = dt.Tables[0].Rows[0]["usuario"].ToString();
                }
                else
                {
                    MessageBox.Show("USUARIO NO ENCONTRADO");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.Message);
            }
            try
            {
                if (string.IsNullOrEmpty(txtficha.Text)) return;

                string vSql = $"SELECT * From empleado Where ficha Like ('%" + txtficha.Text.Trim() + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    comp = dt.Tables[0].Rows[0]["id_persona"].ToString();
                    txtcodigo.Text = comp; 
                    txtcargo.Text = dt.Tables[0].Rows[0]["id_cargo"].ToString();
                    txtcodigo.Focus();
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

            try
            {
                if (string.IsNullOrEmpty(txtficha.Text)) return;

                string vSql = $"SELECT * From persona Where id_persona Like ('%" + comp + "%') ";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (utilidades.DsTieneDatos(dt))
                {
                    txtnombre.Text = dt.Tables[0].Rows[0]["nombre1"].ToString();
                    txtapellido.Text = dt.Tables[0].Rows[0]["apellido1"].ToString();

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

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                string vSql = $"EXEC eliminausuarios '{txtcodigo.Text.Trim()}'";

                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Se Elimino El Empleado "); traerUsuario(); }
                else MessageBox.Show("Error Eliminando datos ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
        }
    }
}

