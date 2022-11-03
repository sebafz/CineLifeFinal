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
    public class CD_Idioma
    {
        public List<Idioma> Listar()
        {

            List<Idioma> lista = new List<Idioma>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select * from Idioma";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Idioma()
                                {
                                    IdIdioma = Convert.ToInt32(dr["IdIdioma"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Idioma>();

            }


            return lista;


        }
    }
}
