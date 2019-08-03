﻿using System;
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = string.Format("Select *  FROM usuarios where cuenta='{0}' AND password='{1}'", txtnom.Text.Trim(), txtpass.Text.Trim());

                DataSet ds = new DataSet();
               
                bool esta= ds.CountDataset(cmd);
                //string cuenta = ds.Tables[0].Rows[0]["cuenta"].ToString().Trim();
                //string pass = ds.Tables[0].Rows[0]["password"].ToString().Trim();

                if (esta)
                {
                    MessageBox.Show("Ah iniciado Sesion Exitosamente");
                    contenedor obj = new contenedor();
                    Hide();
                    obj.Show();


                }
                else { MessageBox.Show("usuario incorrecto"); }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error BD" + error.Message);
            }
        }
    }
}