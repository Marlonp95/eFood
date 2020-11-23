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
                    var data = form.Controls.OfType<TextBox>().Where(x => x.Tag.ToLowerM() == "required" && string.IsNullOrEmpty(x.Text));

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
        public static string ToLowerM(this object data)
        {
            if (data != null)
            {
                return ((string)data).ToLower();
            }
            else return null;
        }


        /// <summary>
        /// Valida el número de Cédula o Rnc de la República Dominicana.
        /// </summary>
        /// <param name="pDocumento">Número de cédula o rnc que desea verificar.</param>
        /// <returns>Verdadero si el documento es una cédula o rnc válido, de lo contrario falso.</returns>
        public static bool ValidarDocumento(string pDocumento)
        {
            if (string.IsNullOrEmpty(pDocumento))
                return false;

            //Remover guiones en caso de que se pase el documento con guiones incluidos.
            string vDocumento = pDocumento.Replace("-", "");

            //Validar que el documento solo contenga números.
            foreach (char s in vDocumento)
            {
                if (char.IsLetter(s))
                    return false;
            }

            if (vDocumento.Length == 11)
                return ValidarCedula(vDocumento);
            if (vDocumento.Length == 9)
                return ValidarRnc(vDocumento);

            //Retorno de la Función
            return false;
        }

        private static bool ValidarCedula(string pCedula)
        {
            //Validar que no este vacio y que contenga el número de digitos correcto.
            if (string.IsNullOrEmpty(pCedula) || pCedula.Length != 11)
                return false;

            char[] cCedula = pCedula.ToArray();
            int vSuma = 0, vDivision = 0, vMult = 0, vDigito = 0;
            int[] vPeso = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };

            for (int i = 0; i < 10; i++)
            {
                vMult = int.Parse(cCedula[i].ToString()) * vPeso[i];
                while (vMult > 0)
                {
                    vSuma += vMult % 10;
                    vMult /= 10;
                }
            }

            vDivision = (vSuma / 10) * 10;

            if (vDivision < vSuma)
                vDivision += 10;

            vDigito = vDivision - vSuma;

            if (vDigito != int.Parse(cCedula[10].ToString()))
                return false;

            //Retorno de la Función
            return true;
        }

        private static bool ValidarRnc(string pRnc)
        {
            //Validar que no este vacio y que contenga el número de digitos correcto.
            if (string.IsNullOrEmpty(pRnc) || pRnc.Length != 9)
                return false;

            char[] cRnc = pRnc.ToArray();
            int[] vPeso = { 7, 9, 8, 6, 5, 4, 3, 2 };
            int vSuma = 0, vDivision = 0, vResto = 0, vDigito = 0;

            for (int i = 0; i < 8; i++)
            {
                vSuma += int.Parse(cRnc[i].ToString()) * vPeso[i];
            }

            vDivision = vSuma / 11;
            vResto = vSuma - (vDivision * 11);

            if (vResto == 0)
                vDigito = 2;
            else if (vResto == 1)
                vDigito = 1;
            else
                vDigito = 11 - vResto;

            if (vDigito != int.Parse(cRnc[8].ToString()))
                return false;

            //Retorno de la Función
            return true;
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
