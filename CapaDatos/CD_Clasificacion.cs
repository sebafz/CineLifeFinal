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
    public class CD_Clasificacion
    {
        public List<Clasificacion> Listar()
        {

            List<Clasificacion> lista = new List<Clasificacion>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select * from Clasificacion";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Clasificacion()
                                {
                                    IdClasificacion = Convert.ToInt32(dr["IdClasificacion"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Clasificacion>();

            }


            return lista;


        }
    }
}
