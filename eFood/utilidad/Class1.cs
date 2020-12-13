using System;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Linq;

namespace utilidad
{
    public static class utilidades

    {


        public static object GetCellValueFromColumnHeader(this DataGridViewCellCollection CellCollection, string HeaderText)
        {
            return CellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == HeaderText).Value;
        }
        //public static string GetCellValueFromColumnHeader(this DataGridViewCellCollection CellCollection, string HeaderText)
        //{
        //    return CellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == HeaderText).Value.ToString();

        //}

        public static void SetCellValueFromColumnHeader(this DataGridViewCellCollection CellCollection, string HeaderText, object value)
        {
             CellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == HeaderText).Value = value;
        }

        public static string A_Encriptar(string cad)
        {
            string pal = " ";
            byte[] encrypted = Encoding.Unicode.GetBytes(cad);
            pal = Convert.ToBase64String(encrypted);
            return pal;
        }
        public static string A_Desencriptar(string cad)
        {
            string pal = " ";
            byte[] decryted = Convert.FromBase64String(cad);
            pal = System.Text.Encoding.Unicode.GetString(decryted);
            return pal;
        }
        public static bool ejecuta(this DataSet MiDataset, string sentencia)
        {
            //DataSet ds = new DataSet();
            bool vEstado = false;
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand(sentencia, con);
                //cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(sentencia, con);
                da.Fill(MiDataset);
                con.Close();
                con.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Conectar con la Base de Datos : " + ex.ToString());
                //return null;

            }
            return vEstado;
        }

        public static bool ejecutaTransaccion(this DataSet MiDataset, string sentencia)
        {
            //DataSet ds = new DataSet();
            bool vEstado = false;
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();

                    using (SqlTransaction tran = con.BeginTransaction("tran->" + DateTime.Now.Hour.ToString()))
                    {
                        try
                        {
                            cmd.Transaction = tran;
                            cmd.Connection = con;
                            cmd.CommandText = sentencia;
                            cmd.ExecuteNonQuery();

                            tran.Commit();

                            SqlDataAdapter da = new SqlDataAdapter(sentencia, con);

                            da.Fill(MiDataset);
                            con.Close();
                            con.Dispose();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Conectar con la Base de Datos : " + ex.ToString());
                //return null;

            }
            return vEstado;
        }

        static SqlTransaction tran_ex;
        static SqlConnection conn;

        public static SqlTransaction BeginTransation()
        {

            conn = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True");
            conn.Open();

            tran_ex = conn.BeginTransaction("tran->" + DateTime.Now.Hour.ToString());
            return tran_ex;
        }
        public static void ConectionClose(this SqlTransaction pCon)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static int GetIdentity(this DataTable data)
        {
            try
            {
                return Convert.ToInt32(data.Rows[0][0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ExecuteSQL(string sentencia)
        {
            bool vEstado = false;
            DataTable MiDataset = new DataTable();
            try
            {
                if (conn == null) conn = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True");
                if (conn.State == ConnectionState.Closed) { conn = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True"); conn.Open(); }

                SqlCommand cmd;

                if (tran_ex != null)
                    cmd = new SqlCommand(sentencia, conn, tran_ex);
                else
                    cmd = new SqlCommand(sentencia, conn);
                try
                {

                    using (SqlDataAdapter dr = new SqlDataAdapter(cmd))
                    {
                        dr.Fill(MiDataset);
                    }


                    return MiDataset;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error al Conectar con la Base de Datos : " + ex.ToString());
                throw ex;
            }
        }

        public static bool ExecuteSQL(this DataTable MiDataset, string sentencia)
        {
            bool vEstado = false;
            try
            {
                if (conn == null) conn = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True");
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd;

                if (tran_ex != null)
                    cmd = new SqlCommand(sentencia, conn, tran_ex);
                else
                    cmd = new SqlCommand(sentencia, conn);
                try
                {

                    using (SqlDataAdapter dr = new SqlDataAdapter(cmd))
                    {
                        dr.Fill(MiDataset);
                    }


                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error al Conectar con la Base de Datos : " + ex.ToString());
                throw ex;
            }
            return vEstado;
        }

        public static bool ejecutaTransaccion(this DataTable MiDataset, string sentencia)
        {
            //DataSet ds = new DataSet();
            bool vEstado = false;
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();

                    using (SqlTransaction tran = con.BeginTransaction("tran->" + DateTime.Now.Hour.ToString()))
                    {
                        try
                        {
                            cmd.Transaction = tran;
                            cmd.Connection = con;
                            cmd.CommandText = sentencia;
                            cmd.ExecuteNonQuery();

                            tran.Commit();

                            SqlDataAdapter da = new SqlDataAdapter(sentencia, con);

                            da.Fill(MiDataset);
                            con.Close();
                            con.Dispose();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Conectar con la Base de Datos : " + ex.ToString());
                //return null;

            }
            return vEstado;
        }


        public static DataTable ejecuta(string sentencia)
        {
            DataTable dt;

            try
            {
               dt = new DataTable();
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True");
                con.Open();

               // SqlCommand cmd = new SqlCommand(sentencia, con);
                //cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(sentencia, con);
                da.Fill(dt);
                con.Close();
                con.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Conectar con la Base de Datos : " + ex.ToString());
            }
    
        }
        /// <summary>
        /// Para los data table
        /// </summary>
        /// <param name="MiDatatable">Datatable</param>
        /// <param name="sentencia">Sentencia sql</param>
        /// <returns>Retorna un valor boleano</returns>
        public static bool ejecuta(this DataTable MiDatatable, string sentencia)
        {
            //DataSet ds = new DataSet();
            bool vEstado = false;
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=efood;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand(sentencia, con);
                //cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(sentencia, con);
                da.Fill(MiDatatable);
                con.Close();
                con.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Conectar con la Base de Datos : " + ex.ToString());
                //return null;

            }
            return vEstado;
        }
        /// <summary>
        /// /Para los datatable select count
        /// </summary>
        /// <param name="MiDatatable">Datatable</param>
        /// <param name="sql">Sentencia sql</param>
        /// <returns>retorna un valor booleando</returns>
        public static bool CountDataTable(this DataTable MiDatatable, string sql)
        {
            bool existe = false;
            try
            {
                if (MiDatatable.ejecuta(sql))
                {
                    if (MiDatatable.Rows.Count > 0) return existe = true;
                    else existe = false; ;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return existe;
        }

        public static bool CountDataset(this DataSet MiDataset, string sql)
        {
            bool existe = false;
            try
            {
                if (MiDataset.ejecuta(sql))
                {
                    if (MiDataset.Tables[0].Rows.Count > 0) return existe = true;
                    else existe = false; ;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return existe;
        }
        public static bool DsTieneDatos(DataSet Ds)
        {
            try
            {
                if (Ds.Tables[0].Rows.Count > 0) return true;
            }
            catch { }
            return false;
        }
        public enum Effect { roll, Slide, Center, Blend }

        public static void Animate(System.Windows.Forms.Control ctl, Effect effect, int msec, int angle)
        {
            try
            {
                int flags = effmap[(int)effect];
                if (ctl.Visible)
                {

                    flags |= 0x10000; angle += 180;
                }
                else
                {
                    if (ctl.TopLevelControl == ctl) { flags |= 0x20000; }
                    else if (effect == Effect.Blend) throw new ArgumentException();

                }

                flags |= dirmap[(angle % 360) / 45];


                bool ok = AnimateWindow(ctl.Handle, msec, flags);


                if (!ok) throw new Exception("Animacion Fallida");
                ctl.Visible = !ctl.Visible;
            }
            catch { }

        }
        ///<summary>
        ///Efecto Fade.Debe llamar este metodo desde un stick de un control Timer
        ///</summary>
        public static void Effect_F(System.Windows.Forms.Form form, System.Windows.Forms.Timer timer1, int intervalo)
        {

            timer1.Interval = intervalo;
            if (form.Opacity <= 25)
            {

                form.Opacity += .10;
            }

            if (form.Opacity > 25) { form.Opacity += .20; }
            if (form.Opacity > 50) { form.Opacity += .30; }

            if (form.Opacity > 75)
            {
                form.Opacity += .10; if (form.Opacity == 1)
                {
                    timer1.Stop();
                }
            }
        }


        private static int[] dirmap = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);




        //posicion en pantalla
        public static void Position(bool mover, System.Windows.Forms.Form form, int px, int py)
        {
            //px coordenada en X
            //py coordenada en Y
            //Mover deshabilitar y habilitar el movimiento del objeto
            //posiciones capturados en 'objeto'_Down
            if (mover == true) { form.Location = form.PointToScreen(new System.Drawing.Point(System.Windows.Forms.Control.MousePosition.X - px - form.Location.X, System.Windows.Forms.Control.MousePosition.Y - py - form.Location.Y)); }
        }
        public static void TextBox_Let_Opac_Temp(System.Windows.Forms.TextBox tex, string TextoDescrip)
        {

            tex.Text = TextoDescrip;

        }

        public static string Mask(this string data, string mask)
        {
            MaskedTextProvider maskedTextProvider = new MaskedTextProvider(mask);
            maskedTextProvider.Set(data);
            return maskedTextProvider.ToDisplayString();

        }

        public static string xtract(this string data)
        {
            string oString = "";
            foreach (Match item in Regex.Matches(data, @"[\d+|a-zA-Z]+"))
            {
                if (item.Success)
                    oString += item.Value;
            }
            return oString;
        }

        public static T Nvl<T>(this T obj, object value)
        {
            if (obj == null)
            {
                return (T)value;
            }
            else if ((obj is string))
            {
                return string.IsNullOrEmpty(obj.ToString()) ? (T)value : obj;
            }
            else
            {
                return obj;
            }

        }

    }



}
