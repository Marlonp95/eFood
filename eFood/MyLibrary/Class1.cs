using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace MyLibrary
{
    public class utilidades
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
        public static DataSet ejecuta(string sentencia)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog = inventario; Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand(sentencia, con);
                //cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(sentencia, con);
                da.Fill(ds);
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Conectar con la Base de Datos : " + ex.ToString());
            }
            return ds;
        }
    }
}
