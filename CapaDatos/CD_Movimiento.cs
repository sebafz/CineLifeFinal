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

                    sb.AppendLine("select IdMovimiento, Numero, d.IdDeposito, d.Nombre NomDep, isnull(m.IdComprobante,0) IdComprobante, d.IdSede, s.Nombre NomSede, ");
                    sb.AppendLine("isnull((select Numero from Comprobante where IdComprobante=m.IdComprobante),0) NumeroComp, CONVERT (varchar(20), m.Fecha, 103) Fecha, ");
                    sb.AppendLine("Ingreso, mm.IdMotivoMovimiento, mm.Descripcion DescMotivo from Movimiento m ");
                    sb.AppendLine("inner join Deposito d on d.IdDeposito=m.idDeposito ");
                    sb.AppendLine("inner join Sede s on s.IdSede=d.IdSede ");
                    sb.AppendLine("inner join MotivoMovimiento mm on mm.IdMotivoMovimiento=m.IdMotivoMovimiento ");


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
    }
}
