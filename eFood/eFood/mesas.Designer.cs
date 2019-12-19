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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboDestino = new System.Windows.Forms.ComboBox();
            this.mesaBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.efoodDataSet15 = new eFood.efoodDataSet15();
            this.comboOrigen = new System.Windows.Forms.ComboBox();
            this.mesaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.efoodDataSet13 = new eFood.efoodDataSet13();
            this.button18 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.contenedor = new System.Windows.Forms.FlowLayoutPanel();
            this.ContenedorUbicaciones = new System.Windows.Forms.FlowLayoutPanel();
            this.efoodDataSet = new eFood.efoodDataSet();
            this.efoodDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mesaTableAdapter = new eFood.efoodDataSet13TableAdapters.mesaTableAdapter();
            this.mesaTableAdapter1 = new eFood.efoodDataSet15TableAdapters.mesaTableAdapter();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mesaBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mesaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSetBindingSource)).BeginInit();
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
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.comboDestino);
            this.panel2.Controls.Add(this.comboOrigen);
            this.panel2.Controls.Add(this.button18);
            this.panel2.Controls.Add(this.button16);
            this.panel2.Controls.Add(this.button17);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 489);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1153, 65);
            this.panel2.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(550, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Mesa Destino";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(391, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Mesa Origen";
            // 
            // comboDestino
            // 
            this.comboDestino.DataSource = this.mesaBindingSource1;
            this.comboDestino.DisplayMember = "descripcion";
            this.comboDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboDestino.FormattingEnabled = true;
            this.comboDestino.Location = new System.Drawing.Point(553, 31);
            this.comboDestino.Name = "comboDestino";
            this.comboDestino.Size = new System.Drawing.Size(121, 21);
            this.comboDestino.TabIndex = 21;
            this.comboDestino.ValueMember = "id_mesa";
            // 
            // mesaBindingSource1
            // 
            this.mesaBindingSource1.DataMember = "mesa";
            this.mesaBindingSource1.DataSource = this.efoodDataSet15;
            // 
            // efoodDataSet15
            // 
            this.efoodDataSet15.DataSetName = "efoodDataSet15";
            this.efoodDataSet15.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboOrigen
            // 
            this.comboOrigen.DataSource = this.mesaBindingSource;
            this.comboOrigen.DisplayMember = "descripcion";
            this.comboOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboOrigen.FormattingEnabled = true;
            this.comboOrigen.Location = new System.Drawing.Point(391, 31);
            this.comboOrigen.Name = "comboOrigen";
            this.comboOrigen.Size = new System.Drawing.Size(121, 21);
            this.comboOrigen.TabIndex = 20;
            this.comboOrigen.ValueMember = "id_mesa";
            // 
            // mesaBindingSource
            // 
            this.mesaBindingSource.DataMember = "mesa";
            this.mesaBindingSource.DataSource = this.efoodDataSet13;
            // 
            // efoodDataSet13
            // 
            this.efoodDataSet13.DataSetName = "efoodDataSet13";
            this.efoodDataSet13.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button18
            // 
            this.button18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button18.BackColor = System.Drawing.Color.White;
            this.button18.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button18.FlatAppearance.BorderSize = 2;
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button18.Location = new System.Drawing.Point(160, 10);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(103, 50);
            this.button18.TabIndex = 19;
            this.button18.Text = "Resevaciones";
            this.button18.UseVisualStyleBackColor = false;
            // 
            // button16
            // 
            this.button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button16.BackColor = System.Drawing.Color.White;
            this.button16.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button16.FlatAppearance.BorderSize = 2;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.Location = new System.Drawing.Point(269, 10);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(97, 50);
            this.button16.TabIndex = 19;
            this.button16.Text = "Unir Mesas";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button17.BackColor = System.Drawing.Color.White;
            this.button17.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button17.FlatAppearance.BorderSize = 2;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button17.Location = new System.Drawing.Point(57, 10);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(97, 50);
            this.button17.TabIndex = 18;
            this.button17.Text = "Cambiar Mesas";
            this.button17.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::eFood.Properties.Resources.iconfinder_loop_326543;
            this.button2.Location = new System.Drawing.Point(3, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 50);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::eFood.Properties.Resources.iconfinder_2_2739118;
            this.button1.Location = new System.Drawing.Point(1100, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 50);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contenedor
            // 
            this.contenedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Left;
            this.contenedor.Location = new System.Drawing.Point(0, 40);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(838, 449);
            this.contenedor.TabIndex = 10;
            // 
            // ContenedorUbicaciones
            // 
            this.ContenedorUbicaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContenedorUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContenedorUbicaciones.Location = new System.Drawing.Point(838, 40);
            this.ContenedorUbicaciones.Name = "ContenedorUbicaciones";
            this.ContenedorUbicaciones.Size = new System.Drawing.Size(315, 449);
            this.ContenedorUbicaciones.TabIndex = 11;
            // 
            // efoodDataSet
            // 
            this.efoodDataSet.DataSetName = "efoodDataSet";
            this.efoodDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // efoodDataSetBindingSource
            // 
            this.efoodDataSetBindingSource.DataSource = this.efoodDataSet;
            this.efoodDataSetBindingSource.Position = 0;
            // 
            // mesaTableAdapter
            // 
            this.mesaTableAdapter.ClearBeforeFill = true;
            // 
            // mesaTableAdapter1
            // 
            this.mesaTableAdapter1.ClearBeforeFill = true;
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
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mesaBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mesaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSetBindingSource)).EndInit();
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
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.ComboBox comboOrigen;
        private System.Windows.Forms.BindingSource efoodDataSetBindingSource;
        private efoodDataSet efoodDataSet;
        private efoodDataSet13 efoodDataSet13;
        private System.Windows.Forms.BindingSource mesaBindingSource;
        private efoodDataSet13TableAdapters.mesaTableAdapter mesaTableAdapter;
        private System.Windows.Forms.ComboBox comboDestino;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private efoodDataSet15 efoodDataSet15;
        private System.Windows.Forms.BindingSource mesaBindingSource1;
        private efoodDataSet15TableAdapters.mesaTableAdapter mesaTableAdapter1;
    }
}