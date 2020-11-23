using Ex.OM.Extentions;
using System.ComponentModel;
using System.Windows.Forms;
using utilidad;

namespace eFood.Controles
{
    public partial class txtMask : MaskedTextBox
    {
        //public string Mask { get; set; } = "###-###-####";
        public string UnMask { get; set; } = "##########";
        public string OriginalValue { get; set; }

        public txtMask()
        {
            InitializeComponent();
         
            OriginalValue = this.Text.UnMask().Digits();
        }


        public txtMask(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            OriginalValue = this.Text.UnMask().Digits();
        }

        private void txtMask_TextChanged(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Text))
                OriginalValue = this.Text.UnMask().Digits();
        }

        private void txtMask_Click(object sender, System.EventArgs e)
        {
            this.Focus();
            this.SelectAll();
        }
    }
}
