using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca oMarca { get; set; }
        public Categoria oCategoria { get; set; }
        public Proveedor oProveedor { get; set; }
        public decimal Precio { get; set; }

        public string PrecioTexto { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }

        public string Base64 { get; set; }

        public string Extension { get; set; }
        public string FechaVencimiento { get; set; }
        public string FechaRegistro { get; set; }


        //Solo usar estos de abajo para artxdeposito
        public int Stock { get; set; }
        public int Cantidad { get; set; }
        public int StockMaximo { get; set; }
        public int StockMinimo { get; set; }
        public int PuntoDePedido { get; set; }
    }
}
