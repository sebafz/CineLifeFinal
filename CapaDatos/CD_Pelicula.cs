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
    public class CD_Pelicula
    {
        public List<Pelicula> Listar()
        {

            List<Pelicula> lista = new List<Pelicula>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdPelicula, Nombre, p.Descripcion, Calificacion, p.IdClasificacion, c.Descripcion DescClasificacion, p.IdGenero,g.Descripcion DescGenero, RutaImagen, NombreImagen, Activo, CONVERT(varchar(30),FechaEgreso,103) FechaEgreso, CONVERT(varchar(30),FechaIngreso,103) FechaIngreso from Pelicula p " +
                        "inner join Clasificacion c on c.IdClasificacion=p.IdClasificacion "+
                        "inner join Genero g on g.IdGenero=p.IdGenero";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pelicula()
                            {
                                IdPelicula = Convert.ToInt32(dr["IdPelicula"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Calificacion = Convert.ToInt32(dr["Calificacion"]),
                                oClasificacion = new Clasificacion { IdClasificacion = Convert.ToInt32(dr["IdClasificacion"]), Descripcion = dr["DescClasificacion"].ToString() },
                                oGenero = new Genero() { IdGenero= Convert.ToInt32(dr["IdGenero"]), Descripcion= dr["DescGenero"].ToString()},
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                FechaIngreso = dr["FechaIngreso"].ToString(),
                                FechaEgreso = dr["FechaEgreso"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Pelicula>();

            }
            return lista;
        }

        public List<Pelicula> ListarActivas()
        {

            List<Pelicula> lista = new List<Pelicula>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdPelicula, Nombre, p.Descripcion, Calificacion, p.IdClasificacion, c.Descripcion DescClasificacion, p.IdGenero,g.Descripcion DescGenero, RutaImagen, NombreImagen, CONVERT(varchar(30),FechaEgreso,103) FechaEgreso, CONVERT(varchar(30),FechaIngreso,103) FechaIngreso from Pelicula p " +
                        "inner join Clasificacion c on c.IdClasificacion=p.IdClasificacion " +
                        "inner join Genero g on g.IdGenero=p.IdGenero where p.Activo=1";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pelicula()
                            {
                                IdPelicula = Convert.ToInt32(dr["IdPelicula"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Calificacion = Convert.ToInt32(dr["Calificacion"]),
                                oClasificacion = new Clasificacion { IdClasificacion = Convert.ToInt32(dr["IdClasificacion"]), Descripcion = dr["DescClasificacion"].ToString() },
                                oGenero = new Genero() { IdGenero = Convert.ToInt32(dr["IdGenero"]), Descripcion = dr["DescGenero"].ToString() },
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                FechaIngreso = dr["FechaIngreso"].ToString(),
                                FechaEgreso = dr["FechaEgreso"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Pelicula>();

            }
            return lista;
        }

        public List<Pelicula> ObtenerPeliculas(int idGenero, int idClasificacion, int nroPagina, int obtenerRegistros, out int TotalRegistros, out int TotalPaginas)
        {

            List<Pelicula> lista = new List<Pelicula>();
            TotalRegistros = 0;
            TotalPaginas = 0;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_ObtenerPeliculas", oconexion);
                    cmd.Parameters.AddWithValue("idGenero", idGenero);
                    cmd.Parameters.AddWithValue("idClasificacion", idClasificacion);
                    cmd.Parameters.AddWithValue("nroPagina", nroPagina);
                    cmd.Parameters.AddWithValue("obtenerRegistros", obtenerRegistros);
                    cmd.Parameters.Add("TotalRegistros", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("TotalPaginas", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pelicula()
                            {
                                IdPelicula = Convert.ToInt32(dr["IdPelicula"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Calificacion = Convert.ToInt32(dr["Calificacion"]),
                                oClasificacion = new Clasificacion() { IdClasificacion = Convert.ToInt32(dr["IdClasificacion"]), Descripcion = dr["DesClasif"].ToString() },
                                oGenero = new Genero() { IdGenero = Convert.ToInt32(dr["IdGenero"]), Descripcion = dr["DesGenero"].ToString() },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }

                    TotalRegistros = Convert.ToInt32(cmd.Parameters["TotalRegistros"].Value);
                    TotalPaginas = Convert.ToInt32(cmd.Parameters["TotalPaginas"].Value);
                }
            }
            catch
            {
                lista = new List<Pelicula>();

            }
            return lista;
        }
        public int Registrar(Pelicula obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPelicula", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Genero", obj.oGenero.IdGenero);
                    cmd.Parameters.AddWithValue("Calificacion", obj.Calificacion);
                    cmd.Parameters.AddWithValue("Clasificacion", obj.oClasificacion.IdClasificacion);
                    cmd.Parameters.AddWithValue("FechaIngreso", obj.FechaIngreso);
                    cmd.Parameters.AddWithValue("FechaEgreso", obj.FechaEgreso);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(Pelicula obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarPelicula", oconexion);
                    cmd.Parameters.AddWithValue("IdPelicula", obj.IdPelicula);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Genero", obj.oGenero.IdGenero);
                    cmd.Parameters.AddWithValue("Calificacion", obj.Calificacion);
                    cmd.Parameters.AddWithValue("Clasificacion", obj.oClasificacion.IdClasificacion);
                    cmd.Parameters.AddWithValue("FechaIngreso", obj.FechaIngreso);
                    cmd.Parameters.AddWithValue("FechaEgreso", obj.FechaEgreso);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update pelicula set activo=0 where IdPelicula = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
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

        public bool GuardarDatosImagen(Pelicula obj, out string Mensaje)
        {

            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "update Pelicula set RutaImagen = @rutaimagen, NombreImagen = @nombreimagen where IdPelicula = @idpelicula";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@nombreimagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@idpelicula", obj.IdPelicula);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar la portada";
                    }
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
