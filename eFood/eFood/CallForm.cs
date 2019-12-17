using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using utilidad;
namespace eFood
{
    public class CallForm
    {

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
                Form Formulario_Ejecuta = ((Form)MyForm);

                Formulario_Ejecuta.TopLevel = false;
                PanelFormulario.Controls.Add(Formulario_Ejecuta);
                PanelFormulario.Tag = Formulario_Ejecuta;
                Formulario_Ejecuta.FormBorderStyle = FormBorderStyle.None;
                Formulario_Ejecuta.Dock = DockStyle.Fill;
                Formulario_Ejecuta.Show();
                Formulario_Ejecuta.BringToFront();

            }
        }

        public void LlamarForm(ref Form Formulario, ref Control PanelFormulario)
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

        public static List<btnMesa> Cargar_MesaxUbicacion(ref Panel PanelPrincipal, string pCondicion = null)
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"select * from mesa {pCondicion}");
            btnMesa btn;
            List<btnMesa> ListaBotones = new List<btnMesa>();
            foreach (DataRow dr in dt.Rows)
            {
                btn = new btnMesa()
                {
                    Height = 120,
                    Width = 120,
                    Image = Properties.Resources.mesa1,
                    TextAlign = ContentAlignment.BottomCenter,
                    Name = $"Mesa_{dr["id_mesa"].ToString()}",
                    Tag = dr["id_mesa"].ToString(),
                    Text = dr["descripcion"].ToString(),
                    Estado = dr["estado"].ToString(),
                    pFormulario = PanelPrincipal,
                    runFormulario = new pedido(dr["id_mesa"].ToString())
                };
                ListaBotones.Add(btn);
                EstadosColor(btn);




            }
            return ListaBotones;
        }

        public static List<btnMesa> Cargar_MesaxUbicacion(ref Control PanelPrincipal, string pCondicion = null)
        {
            DataTable dt = new DataTable();
            dt.ejecuta($"select * from mesa {pCondicion}");
            btnMesa btn;
            List<btnMesa> ListaBotones = new List<btnMesa>();
            Control c = PanelPrincipal;
            foreach (DataRow dr in dt.Rows)
            {
                btn = new btnMesa()
                {
                    Height = 120,
                    Width = 120,
                    Image = Properties.Resources.mesa1,
                    TextAlign = ContentAlignment.BottomCenter,
                    Name = $"Mesa_{dr["id_mesa"].ToString()}",
                    Tag = dr["id_mesa"].ToString(),
                    Text = dr["descripcion"].ToString(),
                    Estado = dr["estado"].ToString(),
                    pFormulario = PanelPrincipal,
                    runFormulario = new pedido(dr["id_mesa"].ToString()),
                };
                ListaBotones.Add(btn);
                EstadosColor(btn);




            }
            return ListaBotones;
        }


        public static void EstadosColor( btnMesa btn)
        {
           string  pstatus = btn.Estado.ToUpper();
            if (pstatus == "D")
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }
            else if (pstatus == "O")
            {
                btn.BackColor = Color.Red;
                btn.ForeColor = Color.White;
            }
            else if (pstatus == "S" || pstatus == "s")
            {
                btn.BackColor = Color.Yellow;
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.Yellow;
                btn.ForeColor = Color.Black;
            }
        }
    }
}
