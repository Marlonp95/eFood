using Ex.OM.Extentions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace eFood.Utils
{
    public static class Metodos
    {

        public enum TipoControl
        {
            TextBox,
            ComboBox
        }
        /// <summary>
        ///  Validacion de formulario
        /// </summary>
        /// <param name="form">Formulario</param>
        /// <returns>si esta vacio devuelve true, de lo contrario false</returns>
        public static bool IsValid(this Form form, TipoControl tipoControl = TipoControl.TextBox)
        {
            ErrorMessage = string.Empty;
            bool vResult = false;
            switch (tipoControl)
            {
                case TipoControl.TextBox:
                     var data =  form.Controls.OfType<TextBox>().Where(x => x.Tag.ToLowerM() == "required" && string.IsNullOrEmpty(x.Text));

                    vResult = form.Controls.OfType<TextBox>().Any(x => x.Tag.ToLowerM() == "required" && string.IsNullOrEmpty(x.Text));

                    foreach (var item in data)
                    {
                        if (item.Tag != null)
                        {
                            if (item.Tag.ToString().ToLowerM() == "required")
                            {
                                string fields = item.Name.Replace("txt", "");
                                string formatField = string.Empty;

                                foreach (Match match in Regex.Matches(fields, "[A-Z][a-z]+"))
                                {
                                    if (match.Value != null)
                                        formatField += match.Value + "  ";
                                }
                                ErrorMessage += $"El Campo {formatField.Trim()} es obligatorio \n";
                            }
                        }
                    }

              
                    break;


                case TipoControl.ComboBox:
                    vResult = form.Controls.OfType<ComboBox>().Any(x => x.Tag.ToLowerM() == "required" && x.SelectedValue == null);

                    foreach (var item in form.Controls.OfType<ComboBox>())
                    {
                        if (form.Controls.OfType<ComboBox>().Any(x => x.Tag != null && x.Name == item.Name))
                        {
                            if (item.Tag.ToString().ToLowerM() == "required")
                            {
                                string fields = item.Name.Replace("combo", "");
                                string formatField = string.Empty;

                                foreach (Match match in Regex.Matches(fields, "[A-Z][a-z]+"))
                                {
                                    if (match.Value != null)
                                        formatField += match.Value + "  ";
                                }
                                ErrorMessage += $"El Campo {formatField.Trim()} es obligatorio \n";
                            }
                        }
                    }

                    break;
                    default:
                    break;
            }
            return vResult;

        }
        public static String ErrorMessage { get; private set; } = string.Empty;
        public static string ToLowerM(this object  data)
        {
            if (data != null)
            {
                return  ((string)data).ToLower();
            }
            else return null;
        }       
    }

    public class TextBoxOM
    {
        public void TextBoxNumberKeyPress(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var start = textBox.SelectionStart;
            textBox.Text = textBox.Text.Digits();
            textBox.SelectionStart = start;
        }

        public void TextBoxLetterKeyPress(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var start = textBox.SelectionStart;
            textBox.Text = textBox.Text.OnlyLetterCapitalizerSpace();
            textBox.SelectionStart = start;
        }
    } 
}
