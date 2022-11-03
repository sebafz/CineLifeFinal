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
    public class CD_Cartelera
    {
        public List<Cartelera> Listar()
        {

            List<Cartelera> lista = new List<Cartelera>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select c.IdCartelera, Hora, FechaDesde, FechaHasta, c.IdFuncion, p.IdPelicula, p.Nombre, i.Descripcion, c.Activo from Cartelera c " +
                    "inner join Funcion f on c.IdFuncion=f.IdFuncion " +
                    "inner join Idioma i on i.IdIdioma=f.IdIdioma " +
                    "inner join Pelicula p on p.IdPelicula=f.IdPelicula";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cartelera()
                            {
                                IdCartelera = Convert.ToInt32(dr["IdCartelera"]),
                                Hora= dr["Hora"].ToString(),
                                FechaDesde=dr["FechaDesde"].ToString(),
                                FechaHasta=dr["FechaHasta"].ToString(),
                                oFuncion= new Funcion() { 
                                    IdFuncion= Convert.ToInt32(dr["IdFuncion"]),
                                    oPelicula = new Pelicula() { IdPelicula = Convert.ToInt32(dr["IdPelicula"]), Nombre = dr["Nombre"].ToString() },
                                    oIdioma = new Idioma() { IdIdioma = Convert.ToInt32(dr["IdIdioma"]), Descripcion = dr["Descripcion"].ToString() },
                                },
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Cartelera>();

            }
            return lista;
        }
        public int Registrar(Cartelera obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {


                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarFuncion", oconexion);
                    //cmd.Parameters.AddWithValue("IdPelicula", obj.oPelicula.IdPelicula);
                    //cmd.Parameters.AddWithValue("IdIdioma", obj.oIdioma.IdIdioma);
                    //cmd.Parameters.AddWithValue("IdSala", obj.oSala.IdSala);
                    //cmd.Parameters.AddWithValue("Precio", obj.Precio);
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

        public bool Editar(Cartelera obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarFuncion", oconexion);
                    //cmd.Parameters.AddWithValue("IdFuncion", obj.IdFuncion);
                    //cmd.Parameters.AddWithValue("IdPelicula", obj.oPelicula.IdPelicula);
                    //cmd.Parameters.AddWithValue("IdIdioma", obj.oIdioma.IdIdioma);
                    //cmd.Parameters.AddWithValue("IdSala", obj.oSala.IdSala);
                    //cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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
                    SqlCommand cmd = new SqlCommand("update funcion set activo=0 where IdFuncion = @id", oconexion);
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
    }
}
