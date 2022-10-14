using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;

using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Proveedores
    {
        public List<Proveedor> Listar()
        {

            List<Proveedor> lista = new List<Proveedor>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdProveedor,Nombres,Apellidos,CUIL,Correo,Telefono,Direccion,Activo, p.IdProvincia, p.Descripcion DesProv, l.IdLocalidad, l.Descripcion DesLoc "+
                                    "from PROVEEDOR pp inner join Localidad l on pp.IdLocalidad=l.IdLocalidad "+
                                    "inner join provincia p on l.IdProvincia=p.IdProvincia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Proveedor()
                                {
                                    IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    CUIL = Convert.ToInt32(dr["CUIL"]),
                                    Correo = dr["Correo"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Direccion = dr["Direccion"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    oLocalidad= new Localidad()
                                    {
                                        IdLocalidad = Convert.ToInt32(dr["IdLocalidad"]),
                                        Descripcion = dr["DesLoc"].ToString(),
                                        oProvincia = new Provincia() { IdProvincia = Convert.ToInt32(dr["IdProvincia"]), Descripcion = dr["DesProv"].ToString() }
                                    }
                                }

                                );
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Proveedor>();

            }


            return lista;


        }

        public List<Proveedor> ListarActivos()
        {

            List<Proveedor> lista = new List<Proveedor>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdProveedor,Nombres,Apellidos,CUIL,Correo,Telefono,Direccion, p.IdProvincia, p.Descripcion DesProv, l.IdLocalidad, l.Descripcion DesLoc " +
                                    "from PROVEEDOR pp inner join Localidad l on pp.IdLocalidad=l.IdLocalidad " +
                                    "inner join provincia p on l.IdProvincia=p.IdProvincia where pp.Activo=1";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Proveedor()
                                {
                                    IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    CUIL = Convert.ToInt32(dr["CUIL"]),
                                    Correo = dr["Correo"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Direccion = dr["Direccion"].ToString(),
                                    oLocalidad = new Localidad()
                                    {
                                        IdLocalidad = Convert.ToInt32(dr["IdLocalidad"]),
                                        Descripcion = dr["DesLoc"].ToString(),
                                        oProvincia = new Provincia() { IdProvincia = Convert.ToInt32(dr["IdProvincia"]), Descripcion = dr["DesProv"].ToString() }
                                    }
                                }

                                );
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Proveedor>();

            }


            return lista;


        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProveedor", oconexion);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("CUIL", obj.CUIL);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("IdLocalidad", obj.oLocalidad.IdLocalidad);
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


        public bool Editar(Proveedor obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProveedor", oconexion);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.IdProveedor);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("CUIL", obj.CUIL);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("IdLocalidad", obj.oLocalidad.IdLocalidad);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
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
                    SqlCommand cmd = new SqlCommand("update proveedor set activo=0 where IdProveedor = @id", oconexion);
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
