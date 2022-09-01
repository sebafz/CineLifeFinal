using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {


        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int DNI { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool Reestablecer { get; set; }
        public bool Activo { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaRegistro { get; set; }
        public Localidad oLocalidad { get; set; }

    }
}
