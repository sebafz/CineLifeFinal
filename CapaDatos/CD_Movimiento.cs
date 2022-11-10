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
    public class CD_Movimiento
    {
        public List<Movimiento> Listar()
        {

            List<Movimiento> lista = new List<Movimiento>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("sp_ListarMovimientos");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Movimiento()
                            {
                                IdMovimiento = Convert.ToInt32(dr["IdMovimiento"]),
                                Numero = Convert.ToInt32(dr["Numero"]),
                                Fecha = dr["Fecha"].ToString(),
                                Ingreso = Convert.ToInt32(dr["Ingreso"]),
                                oComprobante = new Comprobante() { IdComprobante = Convert.ToInt32(dr["IdComprobante"]), Numero = Convert.ToInt32(dr["NumeroComp"]) },
                                oMotivoMovimiento = new MotivoMovimiento() { IdMotivoMovimiento = Convert.ToInt32(dr["IdMotivoMovimiento"]), Descripcion = dr["DescMotivo"].ToString() },
                                oDeposito = new Deposito() {
                                    oSede = new Sede() { 
                                        IdSede = Convert.ToInt32(dr["Idsede"]),
                                        Nombre = dr["NomSede"].ToString() },
                                    IdDeposito = Convert.ToInt32(dr["IdDeposito"]),
                                    Descripcion = dr["NomDep"].ToString()},
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Movimiento>();

            }
            return lista;
        }

        public int RegistrarComprobante(Comprobante comp, int mot, int ingreso, out string Mensaje)
        {
            Random random = new Random();
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {


                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMovimientoComprobante", oconexion);
                    cmd.Parameters.AddWithValue("IdComprobante", comp.IdComprobante);
                    cmd.Parameters.AddWithValue("IdDeposito", comp.oDeposito.IdDeposito);
                    cmd.Parameters.AddWithValue("IdMotivo", mot);
                    cmd.Parameters.AddWithValue("Ingreso", ingreso);
                    cmd.Parameters.AddWithValue("Numero", random.Next(100, 1000));
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
    }
}
