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

namespace eFood
{
    public partial class Usuarios : Form
    {


        int codigopersona;
        int codigo;

        public Usuarios()
        {
            InitializeComponent();
        }
        
        public void traerUsuario()
        {
            DataTable dt = new DataTable();
            dt.ejecuta(@"select b.nombre1,CONCAT( b.apellido1,' ',b.apellido2) Apellidos, a.ficha,a.usuario,a.fecha_creacion, a.estado, d.departamento
	                        from usuarios a inner join persona b on a.id_persona = b.id_persona 
					                        inner join empleado c on a.id_persona = c.id_persona
					                        inner join departamento d on c.id_departamento = d.id_departamento");
            datacliente.DataSource = dt;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            traerUsuario();
        }

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


        private void button2_Click(object sender, EventArgs e)
        {

            if (this.IsValid())
            {
                MessageBox.Show(Metodos.ErrorMessage);
                return;
            }

            string vSecuencia = $"SELECT TOP 1 * FROM usuarios ORDER by id_usuario DESC";
            DataSet dts = new DataSet();
            bool correctos = dts.ejecuta(vSecuencia);
            if (utilidades.DsTieneDatos(dts))
            {
                codigo = Convert.ToInt32(dts.Tables[0].Rows[0]["id_persona"]);
                codigo = codigo + 1;
                MessageBox.Show("secuencia"+codigo.ToString());
            }
            else
            {
                MessageBox.Show("CREAR EMPLEADO");
            }
            try
            {
                string psencrip = utilidades.A_Encriptar(txtpass.Text);

                string vSql = $"EXEC actualizausuarios '{codigo}','{txtusuario.Text.Trim()}','{psencrip}','{dateTimePicker1.Value.Date}','{codigopersona}','{txtficha.Text.Trim()}'";
                DataSet dt = new DataSet();
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Se Guardo "); traerUsuario(); }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
        }



       
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                string vSql = $"EXEC eliminausuarios '{txtficha.Text.Trim()}'";

                DataSet dt = new DataSet();
                bool correcto = dt.ejecuta(vSql);
                if (correcto) { MessageBox.Show("Usuario eliminado "); traerUsuario(); }
                else MessageBox.Show("Error Eliminando datos ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dt = new DataSet();
            string vSql = "Select * From usuarios ";
            if (string.IsNullOrEmpty(textBox3.Text.Trim()) == false)
            {
                vSql += "Where usuario like ('%" + textBox3.Text.Trim() + "%')";
                dt.ejecuta(vSql);
                datacliente.DataSource = dt.Tables[0];
                textBox3.Clear();
            }
            else
            {
                traerUsuario();
            }
        }
  
        private void button7_Click(object sender, EventArgs e)
        {
            
            traerUsuario();
            txtapellido.Clear();
            txtficha.Clear();
            txtcargo.Clear();
            txtnombre.Clear();
            txtpass.Clear();
            txtusuario.Clear();
        }     

        private void txtficha_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtficha.Text)) return;
                string vSql = $@"select p.id_persona, p.nombre1, CONCAT( p.apellido1,' ',p.apellido2 ) apellidos, e.id_cargo,c.cargo, u.usuario, u.fecha_creacion from persona as p inner join empleado as e on p.id_persona = e.id_persona 
								            left join usuarios as u on p.id_persona =u.id_persona
								            inner join cargo as c on  e.id_cargo = c.id_cargo";

                vSql += " where e.ficha like ('%" + txtficha.Text.Trim() + "%')";
                DataSet dt = new DataSet();
                dt.ejecuta(vSql);
                if (utilidad.utilidades.DsTieneDatos(dt))
                {
                    codigopersona = Convert.ToInt32(dt.Tables[0].Rows[0]["id_persona"]);
                    MessageBox.Show("codigo_persona" + codigopersona.ToString());
                    txtapellido.Text = dt.Tables[0].Rows[0]["apellidos"].ToString();
                    txtnombre.Text = dt.Tables[0].Rows[0]["nombre1"].ToString();
                    txtcargo.Text = dt.Tables[0].Rows[0]["id_cargo"].ToString();
                    txtusuario.Text = dt.Tables[0].Rows[0]["usuario"].ToString();
                    if (string.IsNullOrEmpty(txtusuario.Text)) MessageBox.Show("USUARIO NO ENCONTRADO POR FAVOR CREARLO");

                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error.Message);
            }
        }

        // private void txtcodigo_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(txtcodigo.Text)) return;
        //        string vSql = $"select p.nombre1, p.apellido1, e.id_cargo, e.ficha ,u.usuario,u.id_usuario , u.fecha_creacion from persona as p inner join empleado as e  on p.id_persona = e.id_persona inner join usuarios as u on e.id_persona=u.id_persona";
        //        vSql += " where u.id_usuario like ('%" + txtcodigo.Text.Trim() + "%')";
        //        DataSet dt = new DataSet();
        //        dt.ejecuta(vSql);
        //        if (utilidad.utilidades.DsTieneDatos(dt))
        //        {
        //            txtficha.Text = dt.Tables[0].Rows[0]["ficha"].ToString();
        //            txtcargo.Text = dt.Tables[0].Rows[0]["id_cargo"].ToString();
        //            txtcodigo.Text = dt.Tables[0].Rows[0]["id_usuario"].ToString();
        //            txtusuario.Text = dt.Tables[0].Rows[0]["usuario"].ToString();
        //            txtnombre.Text = dt.Tables[0].Rows[0]["nombre1"].ToString();
        //            txtapellido.Text = dt.Tables[0].Rows[0]["apellido1"].ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("USUARIO NO ENCONTRADO");
        //        }
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show("Error" + error.Message);
        //    }

    }
}

