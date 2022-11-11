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
        public List<Comprobante> ListarCompra(int tipo)
        {

            List<Comprobante> lista = new List<Comprobante>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select Numero, s.IdSede, d.IdDeposito, s.Nombre NomSede, d.Nombre NomDep, m.IdComprobanteCompra, isnull(m.IdComprobanteVinculo,0) IdComprobanteVinculo, ");
                    sb.AppendLine("isnull((select Numero from ComprobanteCompra where IdComprobanteCompra=IdComprobanteVinculo),0) NumeroVinculo,Letra, m.IdProveedor, CONVERT(varchar(20), Fecha, 103) Fecha, ");
                    sb.AppendLine("isnull((select sum(precio*cantidad) from DetalleComprobanteCompra where IdComprobanteCompra=m.IdComprobanteCompra),0.0) Total, p.Nombres from ComprobanteCompra m ");
                    sb.AppendLine("inner join Proveedor p on p.IdProveedor= m.IdProveedor ");
                    sb.AppendLine("inner join Deposito d on d.IdDeposito=m.idDeposito ");
                    sb.AppendLine("inner join Sede s on s.IdSede=d.IdSede ");
                    sb.AppendLine("where m.Tipo=@tipo");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Comprobante()
                            {
                                IdComprobante = Convert.ToInt32(dr["IdComprobanteCompra"]),
                                Letra = dr["Letra"].ToString(),
                                Total = Convert.ToInt32(dr["Total"]),
                                Numero = Convert.ToInt32(dr["Numero"]),
                                Fecha = dr["Fecha"].ToString(),
                                oComprobanteVinculo= new Comprobante() { IdComprobante = Convert.ToInt32(dr["IdComprobanteVinculo"]), Numero = Convert.ToInt32(dr["NumeroVinculo"]) },
                                oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(dr["IdProveedor"]), Nombres = dr["Nombres"].ToString() },
                                oDeposito = new Deposito() {
                                    Descripcion = dr["NomDep"].ToString(),
                                    IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                    oSede = new Sede()
                                    {
                                        IdSede = Convert.ToInt32(dr["IdSede"]),
                                        Nombre = dr["NomSede"].ToString()
                                    }
                                }
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
        public List<Comprobante> ListarNotasCompra()
        {

            List<Comprobante> lista = new List<Comprobante>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select Numero, s.IdSede, d.IdDeposito, s.Nombre NomSede, d.Nombre NomDep, m.IdComprobanteCompra, isnull(m.IdComprobanteVinculo,0) IdComprobanteVinculo, ");
                    sb.AppendLine("isnull((select Numero from ComprobanteCompra where IdComprobanteCompra=IdComprobanteVinculo),0) NumeroVinculo,Letra, m.IdProveedor, CONVERT(varchar(20), Fecha, 103) Fecha, ");
                    sb.AppendLine("isnull((select sum(precio*cantidad) from DetalleComprobanteCompra where IdComprobanteCompra=m.IdComprobanteCompra),0.0) Total, p.Nombres from ComprobanteCompra m ");
                    sb.AppendLine("inner join Proveedor p on p.IdProveedor= m.IdProveedor ");
                    sb.AppendLine("inner join Deposito d on d.IdDeposito=m.idDeposito ");
                    sb.AppendLine("inner join Sede s on s.IdSede=d.IdSede ");
                    sb.AppendLine("where m.Tipo=2 or m.Tipo=3");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Comprobante()
                            {
                                IdComprobante = Convert.ToInt32(dr["IdComprobanteCompra"]),
                                Letra = dr["Letra"].ToString(),
                                Total = Convert.ToInt32(dr["Total"]),
                                Numero = Convert.ToInt32(dr["Numero"]),
                                Fecha = dr["Fecha"].ToString(),
                                oComprobanteVinculo = new Comprobante() { IdComprobante = Convert.ToInt32(dr["IdComprobanteVinculo"]), Numero = Convert.ToInt32(dr["NumeroVinculo"]) },
                                oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(dr["IdProveedor"]), Nombres = dr["Nombres"].ToString() },
                                oDeposito = new Deposito()
                                {
                                    Descripcion = dr["NomDep"].ToString(),
                                    IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                    oSede = new Sede()
                                    {
                                        IdSede = Convert.ToInt32(dr["IdSede"]),
                                        Nombre = dr["NomSede"].ToString()
                                    }
                                }
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
        public List<Comprobante> ListarNotasVenta()
        {

            List<Comprobante> lista = new List<Comprobante>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select Numero, s.IdSede, d.IdDeposito, s.Nombre NomSede, d.Nombre NomDep, m.IdComprobanteVenta, isnull(m.IdComprobanteVinculo,0) IdComprobanteVinculo, ");
                    sb.AppendLine("isnull((select Numero from ComprobanteVenta where IdComprobanteVenta=IdComprobanteVinculo),0) NumeroVinculo,Letra, m.IdCliente, CONVERT(varchar(20), Fecha, 103) Fecha, ");
                    sb.AppendLine("isnull((select sum(precio*cantidad) from DetalleComprobanteVenta where IdComprobanteVenta=m.IdComprobanteVenta),0.0) Total, p.Nombres from ComprobanteVenta m ");
                    sb.AppendLine("inner join Cliente p on p.IdCliente= m.IdCliente ");
                    sb.AppendLine("inner join Deposito d on d.IdDeposito=m.idDeposito ");
                    sb.AppendLine("inner join Sede s on s.IdSede=d.IdSede ");
                    sb.AppendLine("where m.Tipo=2 or m.Tipo=3");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Comprobante()
                            {
                                IdComprobante = Convert.ToInt32(dr["IdComprobanteVenta"]),
                                Letra = dr["Letra"].ToString(),
                                Total = Convert.ToInt32(dr["Total"]),
                                Numero = Convert.ToInt32(dr["Numero"]),
                                Fecha = dr["Fecha"].ToString(),
                                oComprobanteVinculo = new Comprobante() { IdComprobante = Convert.ToInt32(dr["IdComprobanteVinculo"]), Numero = Convert.ToInt32(dr["NumeroVinculo"]) },
                                oCliente = new Cliente() { IdCliente = Convert.ToInt32(dr["IdCliente"]), Nombres = dr["Nombres"].ToString() },
                                oDeposito = new Deposito()
                                {
                                    Descripcion = dr["NomDep"].ToString(),
                                    IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                    oSede = new Sede()
                                    {
                                        IdSede = Convert.ToInt32(dr["IdSede"]),
                                        Nombre = dr["NomSede"].ToString()
                                    }
                                }
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
        public List<Comprobante> ListarVenta(int tipo)
        {

            List<Comprobante> lista = new List<Comprobante>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select Numero, s.IdSede, d.IdDeposito, s.Nombre NomSede, d.Nombre NomDep, m.IdComprobanteVenta, isnull(m.IdComprobanteVinculo,0) IdComprobanteVinculo, ");
                    sb.AppendLine("isnull((select Numero from ComprobanteVenta where IdComprobanteVenta=IdComprobanteVinculo),0) NumeroVinculo,Letra, m.IdCliente, CONVERT(varchar(20), Fecha, 103) Fecha, ");
                    sb.AppendLine("isnull((select sum(precio*cantidad) from DetalleComprobanteVenta where IdComprobanteVenta=m.IdComprobanteVenta),0.0) Total, p.Nombres from ComprobanteVenta m ");
                    sb.AppendLine("inner join Cliente p on p.IdCliente= m.IdCliente ");
                    sb.AppendLine("inner join Deposito d on d.IdDeposito=m.idDeposito ");
                    sb.AppendLine("inner join Sede s on s.IdSede=d.IdSede ");
                    sb.AppendLine("where m.Tipo=@tipo");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Comprobante()
                            {
                                IdComprobante = Convert.ToInt32(dr["IdComprobanteVenta"]),
                                Letra = dr["Letra"].ToString(),
                                Total = Convert.ToInt32(dr["Total"]),
                                Numero = Convert.ToInt32(dr["Numero"]),
                                Fecha = dr["Fecha"].ToString(),
                                oComprobanteVinculo = new Comprobante() { IdComprobante = Convert.ToInt32(dr["IdComprobanteVinculo"]), Numero = Convert.ToInt32(dr["NumeroVinculo"]) },
                                oCliente = new Cliente() { IdCliente = Convert.ToInt32(dr["IdCliente"]), Nombres = dr["Nombres"].ToString() },
                                oDeposito = new Deposito()
                                {
                                    Descripcion = dr["NomDep"].ToString(),
                                    IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                    oSede = new Sede()
                                    {
                                        IdSede = Convert.ToInt32(dr["IdSede"]),
                                        Nombre = dr["NomSede"].ToString()
                                    }
                                }
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

        public int RegistrarCompra(Comprobante comp, List<Producto> list, out string Mensaje)
        {
            Random random= new Random();
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarComprobanteCompra", oconexion);
                    cmd.Parameters.AddWithValue("Letra", comp.Letra);
                    cmd.Parameters.AddWithValue("Tipo", comp.Tipo);
                    cmd.Parameters.AddWithValue("Numero", random.Next(100,1000));
                    cmd.Parameters.AddWithValue("IdProveedor", comp.oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("IdMedioPago", comp.oMedioPago.IdMedioPago);
                    cmd.Parameters.AddWithValue("IdComprobanteVinculo", comp.oComprobanteVinculo.IdComprobante);
                    cmd.Parameters.AddWithValue("IdDeposito", comp.oDeposito.IdDeposito);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    oconexion.Close();

                    SqlCommand cmd2 = null;

                    foreach (Producto prod in list)
                    {
                        cmd2 = new SqlCommand("sp_RegistrarDetalleComprobanteCompra", oconexion);
                        cmd2.Parameters.AddWithValue("IdComprobanteCompra", idautogenerado);
                        cmd2.Parameters.AddWithValue("Cantidad", prod.Cantidad);
                        cmd2.Parameters.AddWithValue("Precio", prod.Precio);
                        cmd2.Parameters.AddWithValue("IdProducto", prod.IdProducto);
                        cmd2.CommandType = CommandType.StoredProcedure;

                        oconexion.Open();

                        cmd2.ExecuteNonQuery();

                        oconexion.Close();
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
        public int RegistrarVenta(Comprobante comp, List<Producto> list, out string Mensaje)
        {
            Random random = new Random();
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarComprobanteVenta", oconexion);
                    cmd.Parameters.AddWithValue("Letra", comp.Letra);
                    cmd.Parameters.AddWithValue("Tipo", comp.Tipo);
                    cmd.Parameters.AddWithValue("Numero", random.Next(100, 1000));
                    cmd.Parameters.AddWithValue("IdCliente", comp.oCliente.IdCliente);
                    cmd.Parameters.AddWithValue("IdMedioPago", comp.oMedioPago.IdMedioPago);
                    cmd.Parameters.AddWithValue("IdComprobanteVinculo", comp.oComprobanteVinculo.IdComprobante);
                    cmd.Parameters.AddWithValue("IdDeposito", comp.oDeposito.IdDeposito);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    oconexion.Close();

                    SqlCommand cmd2 = null;


                    foreach (Producto prod in list)
                    {
                        cmd2 = new SqlCommand("sp_RegistrarDetalleComprobanteVenta", oconexion);
                        cmd2.Parameters.AddWithValue("IdComprobanteVenta", idautogenerado);
                        cmd2.Parameters.AddWithValue("Cantidad", prod.Cantidad);
                        cmd2.Parameters.AddWithValue("Precio", prod.Precio);
                        cmd2.Parameters.AddWithValue("IdProducto", prod.IdProducto);
                        cmd2.Parameters.AddWithValue("IdDeposito", comp.oDeposito.IdDeposito);
                        cmd2.CommandType = CommandType.StoredProcedure;

                        oconexion.Open();

                        cmd2.ExecuteNonQuery();

                        oconexion.Close();
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

        public int RegistrarVentaPelicula(Funcion funcion, int idcliente, List<Butaca> list, int numero, out string Mensaje)
        {
            
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarComprobanteVenta", oconexion);
                    cmd.Parameters.AddWithValue("Letra", 'A');
                    cmd.Parameters.AddWithValue("Tipo", 1);
                    cmd.Parameters.AddWithValue("Numero", numero);
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdMedioPago", 1);
                    cmd.Parameters.AddWithValue("IdComprobanteVinculo", 0);
                    cmd.Parameters.AddWithValue("IdDeposito", 0);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    oconexion.Close();

                    SqlCommand cmd2 = null;


                    foreach (Butaca but in list)
                    {
                        cmd2 = new SqlCommand("sp_RegistrarDetalleBoleto", oconexion);
                        cmd2.Parameters.AddWithValue("IdComprobanteVenta", idautogenerado);
                        cmd2.Parameters.AddWithValue("Fila", but.Fila);
                        cmd2.Parameters.AddWithValue("Numero", but.Numero);
                        cmd2.Parameters.AddWithValue("IdFuncion", funcion.IdFuncion);
                        cmd2.Parameters.AddWithValue("Precio", funcion.Precio);
                        cmd2.CommandType = CommandType.StoredProcedure;

                        oconexion.Open();

                        cmd2.ExecuteNonQuery();

                        oconexion.Close();
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
        public List<decimal> ObtenerVentas()
        {
            List<decimal> lista = new List<decimal>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select sum(Precio) Total, month(fecha) Fecha from DetalleComprobanteCompra dcc " +
                    "inner join ComprobanteCompra cc on cc.IdComprobanteCompra = dcc.IdComprobanteCompra " +
                    "where month(fecha)= month(getdate()) or month(fecha)= month(getdate()) - 1 or month(fecha)= month(getdate()) - 2 or month(fecha)= month(getdate()) - 3 " +
                    "group by month(fecha) order by 2";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(Convert.ToDecimal(dr["Total"]));
                        }
                    }
                }

            }
            catch
            {
                lista = new List<decimal>();

            }

            return lista;

        }
    }
}
