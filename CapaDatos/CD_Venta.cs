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
    public class CD_Venta
    {

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarVenta", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    cmd.Parameters.AddWithValue("TotalProducto", obj.TotalProducto);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("Contacto", obj.Contacto);
                    cmd.Parameters.AddWithValue("IdDistrito", obj.IdDistrito);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("IdTransaccion", obj.IdTransaccion);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }


        public List<DetalleBoleto> ListarCompras(int idcliente)
        {

            List<DetalleBoleto> lista = new List<DetalleBoleto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select * from fn_ListarCompra(@idcliente)";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetalleBoleto()
                            {
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                oFuncion = new Funcion()
                                {
                                    Fecha = dr["FechaFunc"].ToString(),
                                    Hora = dr["HoraFunc"].ToString(),
                                    oIdioma = new Idioma()
                                    {
                                        Descripcion = dr["DescIdioma"].ToString()
                                    },
                                    oSala = new Sala()
                                    {
                                        Descripcion = dr["DescSala"].ToString()
                                    },
                                    oPelicula = new Pelicula()
                                    {
                                        Nombre = dr["Nombre"].ToString(),
                                        RutaImagen = dr["RutaImagen"].ToString(),
                                        NombreImagen= dr["NombreImagen"].ToString()
                                    }
                                },
                                oComprobante = new Comprobante()
                                {
                                    Fecha = dr["FechaComp"].ToString(),
                                    Numero = Convert.ToInt32(dr["NumComp"])
                                },
                                oButaca = new Butaca()
                                {
                                    Fila = dr["Fila"].ToString(),
                                    Numero = Convert.ToInt32(dr["Numero"])
                                }
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<DetalleBoleto>();

            }
            return lista;
        }
    }
}
