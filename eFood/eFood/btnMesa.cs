﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using utilidad;

namespace eFood
{
    public partial class btnMesa : Button
    {
        public int    IdMesa { get; set; }
        public string Estado { get; set; }
        public bool RunForm { get; set; }
        public string id_ubicacion { get; set; }
        public Control pFormulario;
        public Form runFormulario ;
        public bool run_mesa = true;

        public btnMesa()
        {
            InitializeComponent();
            this.Click += button1_Click;
        }
     

       
        public btnMesa(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (run_mesa)
            {
                if (mesa(this))
                {  
                    CallForm callForm = new CallForm();
                    callForm.LlamarForm(ref runFormulario, ref pFormulario);
                    
                  
                }
            }
        }

        public bool mesa(btnMesa btn)
        {
           
            string estado;
            string vSql = $"SELECT estado  FROM mesa where  id_mesa = {btn.Tag.ToString()} ";
            DataSet dt = new DataSet();
            dt.ejecuta(vSql);
            bool correcto = dt.ejecuta(vSql);
            estado = dt.Tables[0].Rows[0]["estado"].ToString();

            if (utilidades.DsTieneDatos(dt))
            {

                if (MessageBox.Show("Desea Abrir Mesa " + btn.Tag.ToString(), "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    vSql = $"UPDATE mesa SET estado = 'O'  WHERE  id_mesa = " + btn.Tag.ToString(); ;
                    dt = new DataSet();
                    dt.ejecuta(vSql);
                    btn.BackColor = Color.Red;
                    btn.ForeColor = Color.White;
                    return true;
                }
                else {
                    return false;
                }    
            }

            return false;
        }
    }
}
