﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Pelicula
    {

        public int IdPelicula { get; set; }
        public int Calificacion { get; set; }
        public Clasificacion oClasificacion { get; set; }
        public Genero oGenero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaEgreso { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public string Base64 { get; set; }
        public string Extension { get; set; }

    }
}
