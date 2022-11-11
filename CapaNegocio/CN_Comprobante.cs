using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Comprobante
    {
        private CD_Comprobante objCapaDato = new CD_Comprobante();
        public List<Comprobante> ListarCompra(int tipo)
        {
            return objCapaDato.ListarCompra(tipo);
        }
        public List<Comprobante> ListarNotasCompra()
        {
            return objCapaDato.ListarNotasCompra();
        }
        public List<decimal> ObtenerVentas()
        {
            return objCapaDato.ObtenerVentas();
        }
        public List<Comprobante> ListarNotasVenta()
        {
            return objCapaDato.ListarNotasVenta();
        }
        public List<Comprobante> ListarVenta(int tipo)
        {
            return objCapaDato.ListarVenta(tipo);
        }
        public int RegistrarCompra(Comprobante comp, List<Producto> list, out string Mensaje)
        {
        return objCapaDato.RegistrarCompra(comp, list, out Mensaje);
        }
        public int RegistrarVenta(Comprobante comp, List<Producto> list, out string Mensaje)
        {
            return objCapaDato.RegistrarVenta(comp, list, out Mensaje);
        }
        public int RegistrarVentaPelicula(Funcion funcion, int idcliente, List<Butaca> list, int numero, out string Mensaje)
        {
            return objCapaDato.RegistrarVentaPelicula(funcion, idcliente, list, numero, out Mensaje);
        }

    }
}
