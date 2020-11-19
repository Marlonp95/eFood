namespace eFood
{
    partial class Recetas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recetas));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad_medida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCodProducto = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtReceta = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPorcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.radioEntrada = new System.Windows.Forms.RadioButton();
            this.radioPrimer = new System.Windows.Forms.RadioButton();
            this.radioSegundo = new System.Windows.Forms.RadioButton();
            this.radioPostre = new System.Windows.Forms.RadioButton();
            this.txtDetProducto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtComensales = new System.Windows.Forms.TextBox();
            this.txtPreparacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.comboUnidad = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txturl = new System.Windows.Forms.TextBox();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNombreReceta = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.ComboBuscaRecet = new System.Windows.Forms.ComboBox();
            this.button8 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.descripcion,
            this.porcion,
            this.unidad,
            this.unidad_medida});
            this.dataGridView1.Location = new System.Drawing.Point(12, 245);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1022, 155);
            this.dataGridView1.TabIndex = 0;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "codigo";
            this.codigo.Name = "codigo";
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "descripcion";
            this.descripcion.Name = "descripcion";
            // 
            // porcion
            // 
            this.porcion.HeaderText = "porcion";
            this.porcion.Name = "porcion";
            // 
            // unidad
            // 
            this.unidad.HeaderText = "unidad";
            this.unidad.Name = "unidad";
            this.unidad.Visible = false;
            // 
            // unidad_medida
            // 
            this.unidad_medida.HeaderText = "unidad_medida";
            this.unidad_medida.Name = "unidad_medida";
            // 
            // txtCodProducto
            // 
            this.txtCodProducto.Location = new System.Drawing.Point(83, 50);
            this.txtCodProducto.Name = "txtCodProducto";
            this.txtCodProducto.Size = new System.Drawing.Size(42, 20);
            this.txtCodProducto.TabIndex = 1;
            this.txtCodProducto.Tag = "required";
            this.txtCodProducto.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodProducto_Validating);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(169, 188);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(148, 20);
            this.txtDescripcion.TabIndex = 3;
            // 
            // txtReceta
            // 
            this.txtReceta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReceta.Location = new System.Drawing.Point(12, 431);
            this.txtReceta.Name = "txtReceta";
            this.txtReceta.Size = new System.Drawing.Size(1022, 156);
            this.txtReceta.TabIndex = 4;
            this.txtReceta.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Plato";
            // 
            // txtPorcion
            // 
            this.txtPorcion.Location = new System.Drawing.Point(334, 186);
            this.txtPorcion.Name = "txtPorcion";
            this.txtPorcion.Size = new System.Drawing.Size(71, 20);
            this.txtPorcion.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Categoria";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Comensales";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ingredientes";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 189);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(148, 20);
            this.txtCodigo.TabIndex = 10;
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // radioEntrada
            // 
            this.radioEntrada.AutoSize = true;
            this.radioEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioEntrada.Location = new System.Drawing.Point(96, 85);
            this.radioEntrada.Name = "radioEntrada";
            this.radioEntrada.Size = new System.Drawing.Size(69, 17);
            this.radioEntrada.TabIndex = 12;
            this.radioEntrada.Text = "Entrada";
            this.radioEntrada.UseVisualStyleBackColor = true;
            // 
            // radioPrimer
            // 
            this.radioPrimer.AutoSize = true;
            this.radioPrimer.Checked = true;
            this.radioPrimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioPrimer.Location = new System.Drawing.Point(202, 85);
            this.radioPrimer.Name = "radioPrimer";
            this.radioPrimer.Size = new System.Drawing.Size(93, 17);
            this.radioPrimer.TabIndex = 13;
            this.radioPrimer.TabStop = true;
            this.radioPrimer.Text = "Primer Plato";
            this.radioPrimer.UseVisualStyleBackColor = true;
            // 
            // radioSegundo
            // 
            this.radioSegundo.AutoSize = true;
            this.radioSegundo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioSegundo.Location = new System.Drawing.Point(348, 85);
            this.radioSegundo.Name = "radioSegundo";
            this.radioSegundo.Size = new System.Drawing.Size(108, 17);
            this.radioSegundo.TabIndex = 14;
            this.radioSegundo.Text = "Segundo Plato";
            this.radioSegundo.UseVisualStyleBackColor = true;
            // 
            // radioPostre
            // 
            this.radioPostre.AutoSize = true;
            this.radioPostre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioPostre.Location = new System.Drawing.Point(508, 85);
            this.radioPostre.Name = "radioPostre";
            this.radioPostre.Size = new System.Drawing.Size(61, 17);
            this.radioPostre.TabIndex = 15;
            this.radioPostre.Text = "Postre";
            this.radioPostre.UseVisualStyleBackColor = true;
            // 
            // txtDetProducto
            // 
            this.txtDetProducto.Location = new System.Drawing.Point(131, 50);
            this.txtDetProducto.Name = "txtDetProducto";
            this.txtDetProducto.Size = new System.Drawing.Size(124, 20);
            this.txtDetProducto.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(155, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "Tiempo de preparacion";
            // 
            // txtComensales
            // 
            this.txtComensales.Location = new System.Drawing.Point(105, 113);
            this.txtComensales.Name = "txtComensales";
            this.txtComensales.Size = new System.Drawing.Size(42, 20);
            this.txtComensales.TabIndex = 21;
            this.txtComensales.Tag = "required";
            // 
            // txtPreparacion
            // 
            this.txtPreparacion.Location = new System.Drawing.Point(316, 113);
            this.txtPreparacion.Name = "txtPreparacion";
            this.txtPreparacion.Size = new System.Drawing.Size(48, 20);
            this.txtPreparacion.TabIndex = 22;
            this.txtPreparacion.Tag = "required";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Codigo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(166, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Descripcion";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(331, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Porcion";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(12, 407);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 20);
            this.label9.TabIndex = 26;
            this.label9.Text = "Preparacion";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(813, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 132);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::eFood.Properties.Resources.iconfinder_67_111124;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button3.FlatAppearance.BorderSize = 2;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(261, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(34, 35);
            this.button3.TabIndex = 19;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::eFood.Properties.Resources.iconfinder_plus_511939;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(999, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImage = global::eFood.Properties.Resources.iconfinder_67_111124;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(673, 177);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(34, 32);
            this.button4.TabIndex = 28;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // comboUnidad
            // 
            this.comboUnidad.DisplayMember = "id_unidad";
            this.comboUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboUnidad.FormattingEnabled = true;
            this.comboUnidad.Location = new System.Drawing.Point(429, 186);
            this.comboUnidad.Name = "comboUnidad";
            this.comboUnidad.Size = new System.Drawing.Size(194, 21);
            this.comboUnidad.TabIndex = 29;
            this.comboUnidad.ValueMember = "id_unidad";
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::eFood.Properties.Resources.iconfinder_minus_309051;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(961, 176);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 32);
            this.button2.TabIndex = 30;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label11.Location = new System.Drawing.Point(13, 217);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(242, 25);
            this.label11.TabIndex = 39;
            this.label11.Text = "Codigo";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label10.Location = new System.Drawing.Point(247, 217);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(322, 25);
            this.label10.TabIndex = 40;
            this.label10.Text = "Descripcion";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label12.Location = new System.Drawing.Point(562, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(242, 25);
            this.label12.TabIndex = 41;
            this.label12.Text = "Porcion";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label13.Location = new System.Drawing.Point(801, 217);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(233, 25);
            this.label13.TabIndex = 42;
            this.label13.Text = "Unidad Medida";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.BackColor = System.Drawing.Color.White;
            this.button13.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button13.FlatAppearance.BorderSize = 2;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Image = ((System.Drawing.Image)(resources.GetObject("button13.Image")));
            this.button13.Location = new System.Drawing.Point(987, 6);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(44, 43);
            this.button13.TabIndex = 43;
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button5.Image = global::eFood.Properties.Resources.iconfinder_plus_511939;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.Location = new System.Drawing.Point(813, 149);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(106, 34);
            this.button5.TabIndex = 161;
            this.button5.Text = "Agregar";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txturl
            // 
            this.txturl.Location = new System.Drawing.Point(925, 149);
            this.txturl.Name = "txturl";
            this.txturl.Size = new System.Drawing.Size(109, 20);
            this.txturl.TabIndex = 162;
            this.txturl.Tag = "no vacio";
            this.txturl.Visible = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lineShape1.Enabled = false;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 12;
            this.lineShape1.X2 = 709;
            this.lineShape1.Y1 = 165;
            this.lineShape1.Y2 = 165;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1046, 648);
            this.shapeContainer1.TabIndex = 163;
            this.shapeContainer1.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(8, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 20);
            this.label14.TabIndex = 164;
            this.label14.Text = "Receta";
            // 
            // txtNombreReceta
            // 
            this.txtNombreReceta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombreReceta.Location = new System.Drawing.Point(81, 16);
            this.txtNombreReceta.Name = "txtNombreReceta";
            this.txtNombreReceta.Size = new System.Drawing.Size(332, 20);
            this.txtNombreReceta.TabIndex = 165;
            this.txtNombreReceta.Tag = "required";
         
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button6.FlatAppearance.BorderSize = 2;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Image = global::eFood.Properties.Resources._905644_321;
            this.button6.Location = new System.Drawing.Point(12, 6);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(44, 43);
            this.button6.TabIndex = 166;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(369, 115);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 15);
            this.label15.TabIndex = 167;
            this.label15.Text = "Minutos";
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::eFood.Properties.Resources.iconfinder_67_111124;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button7.FlatAppearance.BorderSize = 2;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(696, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(34, 35);
            this.button7.TabIndex = 168;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // ComboBuscaRecet
            // 
            this.ComboBuscaRecet.DisplayMember = "id_receta";
            this.ComboBuscaRecet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBuscaRecet.FormattingEnabled = true;
            this.ComboBuscaRecet.Location = new System.Drawing.Point(429, 16);
            this.ComboBuscaRecet.Name = "ComboBuscaRecet";
            this.ComboBuscaRecet.Size = new System.Drawing.Size(261, 21);
            this.ComboBuscaRecet.TabIndex = 169;
            this.ComboBuscaRecet.ValueMember = "id_receta";
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button8.BackColor = System.Drawing.Color.White;
            this.button8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button8.FlatAppearance.BorderSize = 2;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Image = global::eFood.Properties.Resources._1167984_32;
            this.button8.Location = new System.Drawing.Point(112, 6);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(44, 43);
            this.button8.TabIndex = 170;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(738, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(69, 20);
            this.label16.TabIndex = 183;
            this.label16.Text = "Imagen";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 593);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1046, 55);
            this.panel1.TabIndex = 184;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.White;
            this.button9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button9.FlatAppearance.BorderSize = 2;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button9.Image = global::eFood.Properties.Resources.iconfinder_loop_326543;
            this.button9.Location = new System.Drawing.Point(62, 6);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(44, 43);
            this.button9.TabIndex = 171;
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = false;
            // 
            // Recetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 648);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.ComboBuscaRecet);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtNombreReceta);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txturl);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboUnidad);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPreparacion);
            this.Controls.Add(this.txtComensales);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtDetProducto);
            this.Controls.Add(this.radioPostre);
            this.Controls.Add(this.radioSegundo);
            this.Controls.Add(this.radioPrimer);
            this.Controls.Add(this.radioEntrada);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPorcion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReceta);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCodProducto);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "Recetas";
            this.Text = "producto";
            this.Load += new System.EventHandler(this.Recetas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtCodProducto;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.RichTextBox txtReceta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPorcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.RadioButton radioEntrada;
        private System.Windows.Forms.RadioButton radioPrimer;
        private System.Windows.Forms.RadioButton radioSegundo;
        private System.Windows.Forms.RadioButton radioPostre;
        private System.Windows.Forms.TextBox txtDetProducto;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtComensales;
        private System.Windows.Forms.TextBox txtPreparacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn porcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad_medida;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txturl;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNombreReceta;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ComboBox ComboBuscaRecet;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button9;
    }
}