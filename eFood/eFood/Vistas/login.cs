using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using utilidad;
using System.Data;
using eFood.Utils;

namespace eFood
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            pictureBox3.Visible = false;
            pictureBox2.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtnom.Focus();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(txtnom.Text == "USUARIO")
            {
                txtnom.Text = "";
                txtnom.ForeColor = Color.White;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(txtnom.Text =="")
            {
                txtnom.Text = "USUARIO";
                txtnom.ForeColor = Color.DimGray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "CONTRASEÑA")
            {
               
                txtpass.Text = "";
                pictureBox2.Visible = true;
                txtpass.ForeColor = Color.White;
                txtpass.UseSystemPasswordChar = true;                
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "CONTRASEÑA";
                txtpass.ForeColor = Color.DimGray;
                txtpass.UseSystemPasswordChar = false;
            } 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txtpass.UseSystemPasswordChar = false;
            pictureBox2.Visible =false;
            pictureBox3.Visible = true;
            }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            txtpass.UseSystemPasswordChar = true;
            pictureBox2.Visible = true;
          
        }
        public static string codigo;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ps = utilidades.A_Encriptar(txtpass.Text);
                string cmd = string.Format("Select *  FROM usuarios where usuario='{0}' AND pass='{1}'", txtnom.Text.Trim(), ps);
                DataSet ds = new DataSet();
                bool correcto = ds.CountDataset(cmd);
                 
                if (correcto)
                {
                    int codPersona = Convert.ToInt32(ds.Tables[0].Rows[0]["id_persona"].ToString().Trim());
                    Globals.Usuarios = Convert.ToInt32(ds.Tables[0].Rows[0]["id_usuario"].ToString().Trim());
                    var data = utilidades.ejecuta($@"select nombre1+' '+ apellido1 nombre from persona where id_persona = {codPersona}");
                    Globals.NombreUsuario = data.Rows[0]["nombre"].ToString();
                    Globals.IdUsuario =Convert.ToInt32( ds.Tables[0].Rows[0]["id_usuario"].ToString());
                    codigo = ds.Tables[0].Rows[0]["id_persona"].ToString().Trim();         
                    contenedor obj = new contenedor();
                    Hide();
                    obj.Show();

                    var row = utilidades.ejecuta($@"select id from enc_apertura_caja where id_usuario ={Globals.IdUsuario} and estado ='A'").Rows;
                    if (row.Count > 0)
                    {
                        Globals.idAperturaCaja = row[0].Field<int>("id");
                    }
                   

                }
                else
                {
                    MessageBox.Show(" USUARIO O CONTRASEÑA INCORRECTOS");
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error BD" + error.Message);
            }
        }
    }
}
