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
    public class CD_MedioPago
    {
        public List<MedioPago> Listar()
        {

            List<MedioPago> lista = new List<MedioPago>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdMedioPago, Descripcion from MedioPago";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new MedioPago()
                                {
                                    IdMedioPago = Convert.ToInt32(dr["IdMedioPago"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<MedioPago>();

            }


            return lista;


        }
    }
}
