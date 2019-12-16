namespace eFood
{
    partial class platos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(platos));
            this.txtplato = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboCategoria = new System.Windows.Forms.ComboBox();
            this.categoriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.efoodDataSet10 = new eFood.efoodDataSet10();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txturl = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtprecio = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtitbis = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboReceta = new System.Windows.Forms.ComboBox();
            this.recetasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.efoodDataSet11 = new eFood.efoodDataSet11();
            this.label5 = new System.Windows.Forms.Label();
            this.categoriaTableAdapter = new eFood.efoodDataSet10TableAdapters.categoriaTableAdapter();
            this.recetasTableAdapter = new eFood.efoodDataSet11TableAdapters.recetasTableAdapter();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itbis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button7 = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recetasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet11)).BeginInit();
            this.SuspendLayout();
            // 
            // txtplato
            // 
            this.txtplato.Location = new System.Drawing.Point(68, 22);
            this.txtplato.Name = "txtplato";
            this.txtplato.Size = new System.Drawing.Size(240, 20);
            this.txtplato.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(830, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 132);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
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
            this.plato,
            this.descripcion,
            this.itbis,
            this.receta,
            this.categoria,
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(11, 305);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1039, 155);
            this.dataGridView1.TabIndex = 29;
            // 
            // comboCategoria
            // 
            this.comboCategoria.DataSource = this.categoriaBindingSource;
            this.comboCategoria.DisplayMember = "descripcion";
            this.comboCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCategoria.FormattingEnabled = true;
            this.comboCategoria.Location = new System.Drawing.Point(12, 245);
            this.comboCategoria.Name = "comboCategoria";
            this.comboCategoria.Size = new System.Drawing.Size(194, 21);
            this.comboCategoria.TabIndex = 30;
            this.comboCategoria.ValueMember = "ID_CATEGORIA";
            // 
            // categoriaBindingSource
            // 
            this.categoriaBindingSource.DataMember = "categoria";
            this.categoriaBindingSource.DataSource = this.efoodDataSet10;
            // 
            // efoodDataSet10
            // 
            this.efoodDataSet10.DataSetName = "efoodDataSet10";
            this.efoodDataSet10.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.BackColor = System.Drawing.Color.White;
            this.button8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button8.FlatAppearance.BorderSize = 2;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Image = global::eFood.Properties.Resources._1167984_32;
            this.button8.Location = new System.Drawing.Point(975, 466);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(35, 37);
            this.button8.TabIndex = 173;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button6.FlatAppearance.BorderSize = 2;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Image = global::eFood.Properties.Resources._905644_321;
            this.button6.Location = new System.Drawing.Point(11, 466);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(35, 37);
            this.button6.TabIndex = 172;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.BackColor = System.Drawing.Color.White;
            this.button13.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button13.FlatAppearance.BorderSize = 2;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Image = ((System.Drawing.Image)(resources.GetObject("button13.Image")));
            this.button13.Location = new System.Drawing.Point(1016, 466);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(35, 37);
            this.button13.TabIndex = 171;
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txturl
            // 
            this.txturl.Location = new System.Drawing.Point(942, 173);
            this.txturl.Name = "txturl";
            this.txturl.Size = new System.Drawing.Size(109, 20);
            this.txturl.TabIndex = 175;
            this.txturl.Tag = "no vacio";
            this.txturl.Visible = false;
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
            this.button5.Location = new System.Drawing.Point(830, 173);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(106, 34);
            this.button5.TabIndex = 174;
            this.button5.Text = "Agregar";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(16, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 16);
            this.label14.TabIndex = 176;
            this.label14.Text = "Plato";
            // 
            // txtprecio
            // 
            this.txtprecio.Location = new System.Drawing.Point(405, 22);
            this.txtprecio.Name = "txtprecio";
            this.txtprecio.Size = new System.Drawing.Size(99, 20);
            this.txtprecio.TabIndex = 177;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(15, 71);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(774, 96);
            this.txtDescripcion.TabIndex = 178;
            this.txtDescripcion.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(346, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 179;
            this.label1.Text = "Precio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(544, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 180;
            this.label2.Text = "ITBIS";
            // 
            // txtitbis
            // 
            this.txtitbis.Location = new System.Drawing.Point(596, 22);
            this.txtitbis.Name = "txtitbis";
            this.txtitbis.Size = new System.Drawing.Size(99, 20);
            this.txtitbis.TabIndex = 181;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(826, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 182;
            this.label3.Text = "Imagen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 183;
            this.label4.Text = "Categoria";
            // 
            // comboReceta
            // 
            this.comboReceta.DataSource = this.recetasBindingSource;
            this.comboReceta.DisplayMember = "receta";
            this.comboReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboReceta.FormattingEnabled = true;
            this.comboReceta.Location = new System.Drawing.Point(253, 245);
            this.comboReceta.Name = "comboReceta";
            this.comboReceta.Size = new System.Drawing.Size(194, 21);
            this.comboReceta.TabIndex = 184;
            this.comboReceta.ValueMember = "id_receta";
            // 
            // recetasBindingSource
            // 
            this.recetasBindingSource.DataMember = "recetas";
            this.recetasBindingSource.DataSource = this.efoodDataSet11;
            // 
            // efoodDataSet11
            // 
            this.efoodDataSet11.DataSetName = "efoodDataSet11";
            this.efoodDataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(250, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 16);
            this.label5.TabIndex = 185;
            this.label5.Text = "Receta";
            // 
            // categoriaTableAdapter
            // 
            this.categoriaTableAdapter.ClearBeforeFill = true;
            // 
            // recetasTableAdapter
            // 
            this.recetasTableAdapter.ClearBeforeFill = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 20);
            this.label6.TabIndex = 186;
            this.label6.Text = "Descripcion del plato";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label11.Location = new System.Drawing.Point(11, 277);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(156, 25);
            this.label11.TabIndex = 187;
            this.label11.Text = "Codigo";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(149, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 25);
            this.label7.TabIndex = 188;
            this.label7.Text = "Plato";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(304, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(180, 25);
            this.label8.TabIndex = 189;
            this.label8.Text = "Descripcion";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Location = new System.Drawing.Point(467, 277);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 25);
            this.label9.TabIndex = 190;
            this.label9.Text = "Precio";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label10.Location = new System.Drawing.Point(593, 277);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 25);
            this.label10.TabIndex = 191;
            this.label10.Text = "ITBIS";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label12.Location = new System.Drawing.Point(883, 277);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(167, 25);
            this.label12.TabIndex = 192;
            this.label12.Text = "Categoria";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label13.Location = new System.Drawing.Point(717, 277);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(177, 25);
            this.label13.TabIndex = 193;
            this.label13.Text = "Receta";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "id_plato";
            this.codigo.Name = "codigo";
            // 
            // plato
            // 
            this.plato.HeaderText = "plato";
            this.plato.Name = "plato";
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "descripcion";
            this.descripcion.Name = "descripcion";
            // 
            // itbis
            // 
            this.itbis.HeaderText = "itbis";
            this.itbis.Name = "itbis";
            // 
            // receta
            // 
            this.receta.HeaderText = "receta";
            this.receta.Name = "receta";
            // 
            // categoria
            // 
            this.categoria.HeaderText = "categoria";
            this.categoria.Name = "categoria";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::eFood.Properties.Resources.iconfinder_67_111124;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(91)))), ((int)(((byte)(140)))));
            this.button7.FlatAppearance.BorderSize = 2;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(1016, 239);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(34, 35);
            this.button7.TabIndex = 194;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(852, 247);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(158, 20);
            this.txtBuscar.TabIndex = 195;
            this.txtBuscar.Tag = "no vacio";
            this.txtBuscar.Visible = false;
            // 
            // platos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 507);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboReceta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtitbis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtprecio);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txturl);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.comboCategoria);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtplato);
            this.Name = "platos";
            this.Text = "platos";
            this.Load += new System.EventHandler(this.platos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recetasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efoodDataSet11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtplato;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboCategoria;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txturl;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtprecio;
        private System.Windows.Forms.RichTextBox txtDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtitbis;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboReceta;
        private System.Windows.Forms.Label label5;
        private efoodDataSet10 efoodDataSet10;
        private System.Windows.Forms.BindingSource categoriaBindingSource;
        private efoodDataSet10TableAdapters.categoriaTableAdapter categoriaTableAdapter;
        private efoodDataSet11 efoodDataSet11;
        private System.Windows.Forms.BindingSource recetasBindingSource;
        private efoodDataSet11TableAdapters.recetasTableAdapter recetasTableAdapter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn plato;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn itbis;
        private System.Windows.Forms.DataGridViewTextBoxColumn receta;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox txtBuscar;
    }
}