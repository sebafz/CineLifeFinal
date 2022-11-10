﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objCapaDato = new CD_Producto();
        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        public List<Producto> ListarVacio()
        {
            return objCapaDato.ListarVacio();
        }

        public List<Producto> ListarActivos()
        {
            return objCapaDato.ListarActivos();
        }

        public List<Producto> ListarXProveedor(int idproveedor)
        {
            return objCapaDato.ListarXProveedor(idproveedor);
        }
        public List<Producto> ListarXMovimiento(int idproveedor)
        {
            return objCapaDato.ListarXMovimiento(idproveedor);
        }

        public List<DetalleComprobante> ListarXComprobanteCompra(int id)
        {
            return objCapaDato.ListarXComprobanteCompra(id);
        }
        public List<DetalleComprobante> ListarXComprobanteVenta(int id)
        {
            return objCapaDato.ListarXComprobanteVenta(id);
        }

        public List<Producto> ListarXDeposito(int id)
        {
            return objCapaDato.ListarXDeposito(id);
        }

        public List<Producto> ObtenerProductos(int idMarca, int idCategoria, int nroPagina, int obtenerRegistros, out int TotalRegistros, out int TotalPaginas)
        {
            return objCapaDato.ObtenerProductos(idMarca,idCategoria,nroPagina,obtenerRegistros, out TotalRegistros, out TotalPaginas);

        }

        public List<Producto> ObtenerProductosActivos(int idMarca, int idCategoria, int nroPagina, int obtenerRegistros, out int TotalRegistros, out int TotalPaginas)
        {
            return objCapaDato.ObtenerProductosActivos(idMarca, idCategoria, nroPagina, obtenerRegistros, out TotalRegistros, out TotalPaginas);

        }

        public int Registrar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción del producto no puede estar vacía";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría";
            }
            else if (obj.oProveedor.IdProveedor == 0)
            {
                Mensaje = "Debe seleccionar un proveedor";
            }
            else if (obj.Precio == 0) {

                Mensaje = "Debe ingresar el precio del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Registrar(obj, out Mensaje);

            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción del producto no puede estar vacía";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría";
            }
            else if (obj.oProveedor.IdProveedor == 0)
            {
                Mensaje = "Debe seleccionar un proveedor";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarDatosImagen(Producto obj, out string Mensaje) {

            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }



        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
        public bool EliminarProductoXDeposito(int idart, int iddep, out string Mensaje)
        {
            return objCapaDato.EliminarXDeposito(idart, iddep, out Mensaje);
        }



    }
}
