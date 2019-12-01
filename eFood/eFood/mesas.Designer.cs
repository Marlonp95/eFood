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
            this.contenedor = new System.Windows.Forms.FlowLayoutPanel();
            this.ContenedorUbicaciones = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1244, 40);
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
            // contenedor
            // 
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Left;
            this.contenedor.Location = new System.Drawing.Point(0, 40);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(822, 514);
            this.contenedor.TabIndex = 4;
            // 
            // ContenedorUbicaciones
            // 
            this.ContenedorUbicaciones.Dock = System.Windows.Forms.DockStyle.Left;
            this.ContenedorUbicaciones.Location = new System.Drawing.Point(822, 40);
            this.ContenedorUbicaciones.Name = "ContenedorUbicaciones";
            this.ContenedorUbicaciones.Size = new System.Drawing.Size(410, 514);
            this.ContenedorUbicaciones.TabIndex = 5;
            // 
            // mesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 554);
            this.Controls.Add(this.ContenedorUbicaciones);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.panel1);
            this.Name = "mesas";
            this.Text = "mesas";
            this.Load += new System.EventHandler(this.mesas_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel contenedor;
        private System.Windows.Forms.FlowLayoutPanel ContenedorUbicaciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}