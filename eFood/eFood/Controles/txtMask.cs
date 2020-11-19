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
         
            OriginalValue = this.Text.xtract().Mask(UnMask);
        }


        public txtMask(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void txtMask_TextChanged(object sender, System.EventArgs e)
        {
            OriginalValue = this.Text.xtract().Mask(UnMask);
        }
    }
}
