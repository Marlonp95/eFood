using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eFood
{
   public class CallForm
    {
        public void OpenForm()
        {
        //    Form formulario = new Form();
        //    formulario = panelFormulario.Controls.OfType<MiForm>().FirstOrDefault();

        //    if (formulario == null)
        //    {
        //        formulario = new MiForm();
        //        formulario.TopLevel = false;
        //        panelFormulario.Controls.Add(formulario);
        //        panelFormulario.Tag = formulario;
        //        formulario.FormBorderStyle = FormBorderStyle.None;
        //        formulario.Dock = DockStyle.Fill;
        //        formulario.Show();
        //        formulario.BringToFront(); ;
        //    }
        //    else
        //    {
        //        formulario.BringToFront();
        //    }
        }
        public void LlamarForm(string lcNameForm)
        {
            string lcProject = Application.ProductName.ToString();
            object MyForm = new object();
            Type tForm = Assembly.GetExecutingAssembly().GetType(lcProject + "." + lcNameForm);
            if (tForm != null)
            {
                MyForm = Activator.CreateInstance(tForm);
                ((Form)MyForm).Show();

            }
        }
        public void LlamarForm(string lcNameForm, Control PanelFormulario)
        {
            string lcProject = Application.ProductName.ToString();
            object MyForm = new object();
            Type tForm = Assembly.GetExecutingAssembly().GetType(lcProject + "." + lcNameForm);
            if (tForm != null)
            {
                MyForm = Activator.CreateInstance(tForm);
                Form Formulario_Ejecuta= ((Form)MyForm);
               
                Formulario_Ejecuta.TopLevel = false;
                PanelFormulario.Controls.Add(Formulario_Ejecuta);
                PanelFormulario.Tag = Formulario_Ejecuta;
                Formulario_Ejecuta.FormBorderStyle = FormBorderStyle.None;
                Formulario_Ejecuta.Dock = DockStyle.Fill;
                Formulario_Ejecuta.Show();
                Formulario_Ejecuta.BringToFront();

            }
        }

        public void LlamarForm( ref Form Formulario, ref Control PanelFormulario)
        {
      
            if (Formulario != null)
            {              

                Formulario.TopLevel = false;
                PanelFormulario.Controls.Add(Formulario);
                PanelFormulario.Tag = Formulario;
                Formulario.FormBorderStyle = FormBorderStyle.None;
                Formulario.Dock = DockStyle.Fill;
                Formulario.Show();
                Formulario.BringToFront();

            }
        }
        //formulario = new MiForm();
        //formulario.TopLevel = false;
        //        panelFormulario.Controls.Add(formulario);
        //        panelFormulario.Tag = formulario;
        //        formulario.FormBorderStyle = FormBorderStyle.None;
        //        formulario.Dock = DockStyle.Fill;
        //        formulario.Show();
        //        formulario.BringToFront(); 
    }
}
