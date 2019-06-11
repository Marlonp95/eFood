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

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "USUARIO")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.White;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text =="")
            {
                textBox1.Text = "USUARIO";
                textBox1.ForeColor = Color.DimGray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "CONTRASEÑA")
            {
               
                textBox2.Text = "";
                pictureBox2.Visible = true;
                textBox2.ForeColor = Color.White;
                textBox2.UseSystemPasswordChar = true;                
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "CONTRASEÑA";
                textBox2.ForeColor = Color.DimGray;
                textBox2.UseSystemPasswordChar = false;
            } 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox2.Visible =false;
            pictureBox3.Visible = true;
            }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            textBox2.UseSystemPasswordChar = true;
            pictureBox2.Visible = true;
          
        }
    }
}
