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
    public class CD_MotivoMovimiento
    {
        public List<MotivoMovimiento> Listar()
        {

            List<MotivoMovimiento> lista = new List<MotivoMovimiento>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdMotivoMovimiento, Descripcion from MotivoMovimiento";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new MotivoMovimiento()
                                {
                                    IdMotivoMovimiento = Convert.ToInt32(dr["IdMotivoMovimiento"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<MotivoMovimiento>();

            }


            return lista;


        }
    }
}
