using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaEntidad;

using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Comprobante
    {
        public List<Comprobante> Listar(int tipo, int ingreso)
        {

            List<Comprobante> lista = new List<Comprobante>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select s.IdSede, s.Nombre, IdComprobante, Letra, m.IdProveedor, Numero, CONVERT (varchar(20), Fecha, 103) Fecha, (select SUM(precio) from DetalleComprobante) Total,p.Nombres from Comprobante m ");
                    sb.AppendLine("inner join Proveedor p on p.IdProveedor= m.IdProveedor ");
                    sb.AppendLine("inner join Sede s on s.IdSede=m.IdSede ");
                    sb.AppendLine("where m.Tipo=@tipo and m.Ingreso=@ingreso");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@ingreso", ingreso);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Comprobante()
                            {
                                IdComprobante = Convert.ToInt32(dr["IdComprobante"]),
                                Letra = dr["Letra"].ToString(),
                                Total = Convert.ToInt32(dr["Total"]),
                                Numero = Convert.ToInt32(dr["Numero"]),
                                Fecha = dr["Fecha"].ToString(),
                                oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(dr["IdProveedor"]), Nombres = dr["Nombres"].ToString() },
                                oSede = new Sede() { IdSede = Convert.ToInt32(dr["IdSede"]), Nombre = dr["Nombre"].ToString() }
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Comprobante>();

            }
            return lista;
        }

        public bool Vincular(int id, int tipo, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_VincularMovimiento", oconexion);
                    cmd.Parameters.AddWithValue("idmovimiento", id);
                    cmd.Parameters.AddWithValue("tipomovimiento", tipo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

    }
}
