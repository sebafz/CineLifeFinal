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
    public class CD_Ubicacion
    {

        public List<Departamento> ObtenerDepartamento()
        {
            List<Departamento> lista = new List<Departamento>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from departamento";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Departamento()
                                {
                                    //IdDepartamento = dr["IdDepartamento"].ToString(),
                                    //Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Departamento>();

            }

            return lista;

        }


        public List<Provincia> ObtenerProvincia(string iddepartamento)
        {
            List<Provincia> lista = new List<Provincia>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from provincia where IdDepartamento = @iddepartamento";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Provincia()
                                {
                                    //Revisar esto:
                                    //IdProvincia = dr["IdProvincia"],
                                    //Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Provincia>();

            }

            return lista;

        }

        public List<Provincia> ObtenerProvinciaArg()
        {
            List<Provincia> lista = new List<Provincia>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from provincia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Provincia()
                                {
                                    IdProvincia = Convert.ToInt32(dr["IdProvincia"]),
                                    Identificador = dr["Identificador"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Provincia>();

            }

            return lista;

        }

        public List<DetalleComprobante> ObtenerVentas()
        {
            List<DetalleComprobante> lista = new List<DetalleComprobante>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "sp_ObtenerVentas";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new DetalleComprobante()
                                {
                                    Precio= Convert.ToInt32(dr["Total"])
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<DetalleComprobante>();

            }

            return lista;

        }

        public List<DetalleComprobante> ObtenerCompras()
        {
            List<DetalleComprobante> lista = new List<DetalleComprobante>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "sp_ObtenerCompras";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new DetalleComprobante()
                                {
                                    Precio = Convert.ToInt32(dr["Total"])
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<DetalleComprobante>();

            }

            return lista;

        }


        public List<Localidad> ObtenerLocalidadArg(string idprovincia)
        {
            List<Localidad> lista = new List<Localidad>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdLocalidad, Descripcion from Localidad where IdProvincia = @idprovincia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Localidad()
                                {
                                    IdLocalidad = Convert.ToInt32(dr["IdLocalidad"]),
                                    Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Localidad>();

            }

            return lista;

        }


        public List<Distrito> ObtenerDistrito(string iddepartamento,string idprovincia)
        {
            List<Distrito> lista = new List<Distrito>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from DISTRITO where IdProvincia = @idprovincia and IdDepartamento = @iddepartamento";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Distrito()
                                {
                                    IdDistrito = dr["IdDistrito"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Distrito>();

            }

            return lista;

        }

    }
}
