using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

using CapaDatos;
using CapaEntidad;
namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objCapaDato = new CD_Venta();
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje) {

            return objCapaDato.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public List<DetalleVenta> ListarCompras(int idcliente) {
            return objCapaDato.ListarCompras(idcliente);
        }
        public bool EnviarCorreoConfirmacion(int idcliente, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            List<DetalleVenta> lista = objCapaDato.ListarCompras(idcliente);

            string asunto = "Fun House: Comprobante de compra";

            StringBuilder mensaje_correo = new StringBuilder();

            mensaje_correo.Append("<h3>¡Gracias por tu compra!</h3></br>" +
                "+<h4>Resumen de la compra</h4>" +
                "+<h4>Número de operación: {NumOp}</h4>" +
                "Fecha de compra: " + DateTime.Now.ToString() +
                "<table> <tbody> <tr> <td></td> <th>Nro.</th> <th>Producto</th> <th>Precio</th> <th>Cantidad</th> </tr> ");

            int cont = 0;
            foreach (DetalleVenta detalle in lista)
            {
                cont++;
                mensaje_correo.Append("<tr>" +
                "<th>" + cont + "</th>" +
                "<td>{NomProd" + cont + "}</td>" +
                "<td>{Precio" + cont + "}</td>" +
                "<td>{Cantidad" + cont + "}</td>" +
                "</tr >");

                mensaje_correo.Replace("{NomProd" + cont + "}", detalle.oProducto.Nombre);
                mensaje_correo.Replace("{Precio" + cont + "}", detalle.oProducto.Precio.ToString());
                mensaje_correo.Replace("{Cantidad" + cont + "}", detalle.Cantidad.ToString());
                mensaje_correo.Replace("{NumOp}", detalle.IdTransaccion.ToString());
            }
            bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo.ToString());

            if (respuesta)
            {
                return true;
            }
            else
            {
                Mensaje = "No se pudo enviar el correo";
                return false;
            }

        }
    }
}
