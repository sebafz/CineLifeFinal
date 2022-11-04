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

                    sb.AppendLine("select Numero, s.IdSede, s.Nombre, m.IdComprobante, isnull(m.IdComprobanteVinculo,0) IdComprobanteVinculo, ");
                    sb.AppendLine("isnull((select Numero from Comprobante where IdComprobante=IdComprobanteVinculo),0) NumeroVinculo,Letra, m.IdProveedor, CONVERT (varchar(20), Fecha, 103) Fecha, ");
                    sb.AppendLine("isnull((select sum(precio*cantidad) from DetalleComprobante where IdComprobante=m.IdComprobante),0.0) Total, p.Nombres, ingreso from Comprobante m ");
                    sb.AppendLine("inner join Proveedor p on p.IdProveedor= m.IdProveedor ");
                    sb.AppendLine("inner join Deposito d on d.IdDeposito=m.idDeposito ");
                    sb.AppendLine("inner join Sede s on s.IdSede=d.IdSede ");
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
                                oComprobanteVinculo= new Comprobante() { IdComprobante = Convert.ToInt32(dr["IdComprobanteVinculo"]), Numero = Convert.ToInt32(dr["NumeroVinculo"]) },
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

        public int Registrar(Comprobante comp, List<Producto> list, out string Mensaje)
        {
            Random random= new Random();
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarComprobante", oconexion);
                    cmd.Parameters.AddWithValue("Letra", comp.Letra);
                    cmd.Parameters.AddWithValue("Tipo", comp.Tipo);
                    cmd.Parameters.AddWithValue("Ingreso", comp.Ingreso);
                    cmd.Parameters.AddWithValue("Numero", random.Next(100,1000));
                    cmd.Parameters.AddWithValue("IdProveedor", comp.oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("IdDeposito", comp.oDeposito.IdDeposito);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    SqlCommand cmd2 = null;

                    if (comp.Ingreso == 1)
                    {
                        foreach (Producto prod in list)
                        {
                            cmd2 = new SqlCommand("sp_RegistrarDetalleComprobanteCompra", oconexion);
                            cmd2.Parameters.AddWithValue("IdComprobante", idautogenerado);
                            cmd2.Parameters.AddWithValue("Cantidad", prod.Cantidad);
                            cmd2.Parameters.AddWithValue("Precio", prod.Precio);
                            cmd2.Parameters.AddWithValue("IdProducto", prod.IdProducto);
                            cmd2.CommandType = CommandType.StoredProcedure;

                            oconexion.Open();

                            cmd2.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        foreach (Producto prod in list)
                        {
                            cmd2 = new SqlCommand("sp_RegistrarDetalleComprobanteVenta", oconexion);
                            cmd2.Parameters.AddWithValue("IdComprobante", idautogenerado);
                            cmd2.Parameters.AddWithValue("Cantidad", prod.Cantidad);
                            cmd2.Parameters.AddWithValue("Precio", prod.Precio);
                            cmd2.Parameters.AddWithValue("IdProducto", prod.IdProducto);
                            cmd2.CommandType = CommandType.StoredProcedure;

                            oconexion.Open();

                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }
    }
}
