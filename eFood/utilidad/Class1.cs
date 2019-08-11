using System;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace utilidad
{
    public static class utilidades

    {
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
        public static bool  ejecuta(this DataSet MiDataset,string sentencia)
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

        public static bool CountDataset(this DataSet MiDataset, string sql) {
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


    }
}
