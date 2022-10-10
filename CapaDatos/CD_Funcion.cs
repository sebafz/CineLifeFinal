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
    public class CD_Funcion
    {
        public List<Funcion> Listar()
        {

            List<Funcion> lista = new List<Funcion>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select f.IdFuncion,p.Nombre,s.Descripcion,f.Activo, h.Hora from Funciones f join Pelicula p on f.IdPelicula=p.IdPelicula join Sala s on s.IdSala=f.IdSala join Horario h on h.IdHorario=f.Horario";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Funcion()
                            {
                                IdFuncion = Convert.ToInt32(dr["IdFuncion"]),
                                NombrePelicula = dr["Nombre"].ToString(),
                                DescripcionSala = dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                Horario= dr["Hora"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Funcion>();

            }
            return lista;
        //}
        //public int Registrar(Pelicula obj, out string Mensaje)
        //{
        //    int idautogenerado = 0;

        //    Mensaje = string.Empty;
        //    try
        //    {


        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_RegistrarPelicula", oconexion);
        //            cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
        //            cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
        //            cmd.Parameters.AddWithValue("Genero", obj.Genero);
        //            cmd.Parameters.AddWithValue("Duracion", obj.Duracion);
        //            cmd.Parameters.AddWithValue("Activo", obj.Activo);
        //            cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            oconexion.Open();

        //            cmd.ExecuteNonQuery();

        //            idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
        //            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        idautogenerado = 0;
        //        Mensaje = ex.Message;
        //    }
        //    return idautogenerado;
        //}

        //public bool Editar(Producto obj, out string Mensaje)
        //{
        //    bool resultado = false;
        //    Mensaje = string.Empty;
        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);
        //            cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
        //            cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
        //            cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
        //            cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
        //            cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
        //            cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedor.IdProveedor);
        //            cmd.Parameters.AddWithValue("Precio", obj.Precio);
        //            cmd.Parameters.AddWithValue("Activo", obj.Activo);
        //            cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            oconexion.Open();

        //            cmd.ExecuteNonQuery();

        //            resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
        //            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = false;
        //        Mensaje = ex.Message;
        //    }
        //    return resultado;
        //}



    }
    }
}

