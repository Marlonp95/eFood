namespace eFood
{
    partial class mesas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
         private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contenedor = new System.Windows.Forms.FlowLayoutPanel();
            this.ContenedorUbicaciones = new System.Windows.Forms.FlowLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1153, 40);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(816, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ubicaciones";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Distribucion De Mesas";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 496);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1153, 58);
            this.panel2.TabIndex = 6;
            // 
            // contenedor
            // 
            this.contenedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Left;
            this.contenedor.Location = new System.Drawing.Point(0, 40);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(838, 456);
            this.contenedor.TabIndex = 10;
            // 
            // ContenedorUbicaciones
            // 
            this.ContenedorUbicaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContenedorUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContenedorUbicaciones.Location = new System.Drawing.Point(838, 40);
            this.ContenedorUbicaciones.Name = "ContenedorUbicaciones";
            this.ContenedorUbicaciones.Size = new System.Drawing.Size(315, 456);
            this.ContenedorUbicaciones.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(103, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 47);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            //this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::eFood.Properties.Resources.iconfinder_loop_326543;
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 50);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::eFood.Properties.Resources.iconfinder_2_2739118;
            this.button1.Location = new System.Drawing.Point(1097, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 50);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 554);
            this.Controls.Add(this.ContenedorUbicaciones);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "mesas";
            this.Text = "mesas";
            this.Load += new System.EventHandler(this.mesas_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel contenedor;
        private System.Windows.Forms.FlowLayoutPanel ContenedorUbicaciones;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}