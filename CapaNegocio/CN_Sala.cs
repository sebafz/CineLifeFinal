﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Sala
    {



        private CD_Sala objCapaDato = new CD_Sala();
        public List<Sala> Listar()
        {
            return objCapaDato.Listar();
        }


        //public int Registrar(Marca obj, out string Mensaje)
        //{

        //    Mensaje = string.Empty;


        //    if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
        //    {
        //        Mensaje = "La descripción de la categoría no puede estar vacía";
        //    }



        //    if (string.IsNullOrEmpty(Mensaje))
        //    {

        //        return objCapaDato.Registrar(obj, out Mensaje);

        //    }
        //    else
        //    {

        //        return 0;
        //    }



        //}

        //public bool Editar(Marca obj, out string Mensaje)
        //{

        //    Mensaje = string.Empty;


        //    if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
        //    {
        //        Mensaje = "La descripción de la categoría no puede estar vacía ";
        //    }


        //    if (string.IsNullOrEmpty(Mensaje))
        //    {

        //        return objCapaDato.Editar(obj, out Mensaje);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}



        //public bool Eliminar(int id, out string Mensaje)
        //{
        //    return objCapaDato.Eliminar(id, out Mensaje);
        //}
        //public List<Marca> ListarMarcaporCategoria(int idcategoria) {

        //    return objCapaDato.ListarMarcaporCategoria(idcategoria);
        //}


    }
}