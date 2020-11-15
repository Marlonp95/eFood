namespace eFood
{
    partial class reporte
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DataFactura = new eFood.DataFactura();
            this.datos_facturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datos_facturaTableAdapter = new eFood.DataFacturaTableAdapters.datos_facturaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DataFactura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datos_facturaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataFactura";
            reportDataSource1.Value = this.datos_facturaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "eFood.Factura.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // DataFactura
            // 
            this.DataFactura.DataSetName = "DataFactura";
            this.DataFactura.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // datos_facturaBindingSource
            // 
            this.datos_facturaBindingSource.DataMember = "datos_factura";
            this.datos_facturaBindingSource.DataSource = this.DataFactura;
            // 
            // datos_facturaTableAdapter
            // 
            this.datos_facturaTableAdapter.ClearBeforeFill = true;
            // 
            // reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "reporte";
            this.Text = "reporte";
            this.Load += new System.EventHandler(this.reporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataFactura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datos_facturaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource datos_facturaBindingSource;
        private DataFactura DataFactura;
        private DataFacturaTableAdapters.datos_facturaTableAdapter datos_facturaTableAdapter;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}