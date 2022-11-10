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
    public class CD_Deposito
    {

        public List<Deposito> Listar()
        {

            List<Deposito> lista = new List<Deposito>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdDeposito, s.IdSede, d.Nombre NombreArea, s.Nombre, s.Direccion,s.Telefono,l.Descripcion Localidad, p.Descripcion Provincia, d.Activo "+
                        "from Sede s join Deposito d on s.IdSede = d.IdSede join Localidad l on s.IdLocalidad = l.IdLocalidad join Provincia p on l.IdProvincia = p.IdProvincia";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Deposito()
                                {

                                IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                Descripcion = dr["NombreArea"].ToString(),
                                oSede=new Sede()
                                {
                                    IdSede= Convert.ToInt32(dr["IdSede"]),
                                    Nombre= dr["Nombre"].ToString(),
                                    Direccion = dr["Direccion"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    oLocalidad=new Localidad()
                                    {
                                        Descripcion = dr["Localidad"].ToString(),
                                        oProvincia =new Provincia()
                                        {
                                            Descripcion= dr["Provincia"].ToString(),
                                        }
                                    }
                                },
                                Activo = Convert.ToBoolean(dr["Activo"])

                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Deposito>();

            }


            return lista;


        }
        public List<Deposito> ObtenerActivos(string idprovincia)
        {
            List<Deposito> lista = new List<Deposito>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdDeposito, Nombre from deposito where IdSede = @idprovincia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Deposito()
                                {
                                    IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                    Descripcion = dr["Nombre"].ToString(),
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Deposito>();

            }

            return lista;

        }

        public List<Deposito> ObtenerDeposito(string idsede)
        {
            List<Deposito> lista = new List<Deposito>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdDeposito, Nombre from Deposito where IdSede = @idsede";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idsede", idsede);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Deposito()
                                {
                                    IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                    Descripcion = dr["Nombre"].ToString(),
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Deposito>();

            }

            return lista;

        }

        public List<Deposito> ObtenerDepositoTransferir(string iddeposito)
        {
            List<Deposito> lista = new List<Deposito>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdDeposito, Nombre from Deposito where IdDeposito != @iddeposito";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@iddeposito", iddeposito);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Deposito()
                                {
                                    IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                    Descripcion = dr["Nombre"].ToString(),
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Deposito>();

            }

            return lista;

        }


        public int Registrar(Deposito obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarDeposito", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Sede", obj.Sede);
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


        public bool Editar(Deposito obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarDeposito", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Sede", obj.Sede);
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
                    SqlCommand cmd = new SqlCommand("update Deposito set activo=0 where IdDeposito = @id", oconexion);
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

        public bool Transferir(int idartxdep, int iddepositoorigen, int iddepositodestino, int cantidad, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_TransferirArticulo", oconexion);
                    cmd.Parameters.AddWithValue("idartxdep", idartxdep);
                    cmd.Parameters.AddWithValue("iddepositoorigen", iddepositoorigen);
                    cmd.Parameters.AddWithValue("iddepositodestino", iddepositodestino);
                    cmd.Parameters.AddWithValue("cantidad", cantidad);
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

    }
}