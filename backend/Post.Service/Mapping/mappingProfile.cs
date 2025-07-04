using AutoMapper;
using Pos.Dto.Dto;
using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Service.Mapping
{
    public class mappingProfile : Profile
    {
        public mappingProfile() 
        {
            #region Rol
            //Mapeo desde Rol a RolDto
            CreateMap<Rol, RolDto>()
            .ForMember(destino => destino.idRol,
            opt => opt.MapFrom(origen => origen.idRol)
            )
            .ForMember(destino => destino.descripcion,
            opt => opt.MapFrom(origen => origen.descripcion)
            )
            .ForMember(destino => destino.estado,
            opt => opt.MapFrom(origen => origen.estado));

            CreateMap<RolDto, Rol>()
            .ForMember(destino => destino.idRol,
            opt => opt.MapFrom(origen => origen.idRol)
            )
            .ForMember(destino => destino.descripcion,
            opt => opt.MapFrom(origen => origen.descripcion)
            )
            .ForMember(destino => destino.estado,
            opt => opt.MapFrom(origen => origen.estado));
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(destino => destino.idUsuario,
                opt => opt.MapFrom(origen => origen.idUsuario)
            )
                .ForMember(destino => destino.nombres,
                opt => opt.MapFrom(origen => origen.nombres)
            )
                    .ForMember(destino => destino.apellidos,
                opt => opt.MapFrom(origen => origen.apellidos)
            )
                .ForMember(destino => destino.apellidos,
                opt => opt.MapFrom(origen => origen.apellidos)
            )
                .ForMember(destino => destino.rolDescripcion,
                opt => opt.MapFrom(origen => origen.Rol != null ? origen.Rol.descripcion : string.Empty)
            )
                    .ForMember(destino => destino.Telefono,
                opt => opt.MapFrom(origen => origen.Telefono)
            )
                .ForMember(destino => destino.email,
                opt => opt.MapFrom(origen => origen.Email)
            )
                .ForMember(destino => destino.clave,
                opt => opt.Ignore()
            )
                .ForMember(destino => destino.estado,
                opt => opt.MapFrom(origen => origen.estado)
            );

            CreateMap<UsuarioDto, Usuario>()
                .ForMember(destino => destino.idUsuario,
                opt => opt.MapFrom(origen => origen.idUsuario)
            )
                .ForMember(destino => destino.nombres,
                opt => opt.MapFrom(origen => origen.nombres)
            )
                    .ForMember(destino => destino.apellidos,
                opt => opt.MapFrom(origen => origen.apellidos)
            )
                .ForMember(destino => destino.apellidos,
                opt => opt.MapFrom(origen => origen.apellidos)
            )
                .ForMember(destino => destino.idRol,
                opt => opt.MapFrom(origen => origen.idRol)
            )
                    .ForMember(destino => destino.Telefono,
                opt => opt.MapFrom(origen => origen.Telefono)
            )
                .ForMember(destino => destino.Email,
                opt => opt.MapFrom(origen => origen.email)
            )
                .ForMember(destino => destino.estado,
                opt => opt.MapFrom(origen => origen.estado)
            );
            #endregion

            #region Negocio 
            // Mapeo de Negocio a NegocioDto
            CreateMap<Negocio, NegocioDto>()
                .ForMember(destino => destino.idNegocio,
                    opt => opt.MapFrom(origen => origen.idNegocio)
                 )
                .ForMember(destino => destino.ruc,
                    opt => opt.MapFrom(origen => origen.ruc)
                 )
                .ForMember(destino => destino.razonSocial,
                    opt => opt.MapFrom(origen => origen.razonSocial)
                 )
                .ForMember(destino => destino.email,
                    opt => opt.MapFrom(origen => origen.email)
                 )
                .ForMember(destino => destino.telefono,
                    opt => opt.MapFrom(origen => origen.telefono)
                 )
                .ForMember(destino => destino.direccion,
                    opt => opt.MapFrom(origen => origen.direccion)
                 )
                .ForMember(destino => destino.propietario,
                    opt => opt.MapFrom(origen => origen.propietario)
                 )
                .ForMember(destino => destino.descuento,
                    opt => opt.MapFrom(origen => origen.descuento)
                 );

            CreateMap<NegocioDto, Negocio>()
               .ForMember(destino => destino.idNegocio,
                   opt => opt.MapFrom(origen => origen.idNegocio)
                )
               .ForMember(destino => destino.ruc,
                   opt => opt.MapFrom(origen => origen.ruc)
                )
               .ForMember(destino => destino.razonSocial,
                   opt => opt.MapFrom(origen => origen.razonSocial)
                )
               .ForMember(destino => destino.email,
                   opt => opt.MapFrom(origen => origen.email)
                )
               .ForMember(destino => destino.telefono,
                   opt => opt.MapFrom(origen => origen.telefono)
                )
               .ForMember(destino => destino.direccion,
                   opt => opt.MapFrom(origen => origen.direccion)
                )
               .ForMember(destino => destino.propietario,
                   opt => opt.MapFrom(origen => origen.propietario)
                )
               .ForMember(destino => destino.descuento,
                   opt => opt.MapFrom(origen => origen.descuento)
                );
            #endregion

            #region NumeroDocumento
            // Mapeo de numeroDocumento a NumeroDocumentoDto
            CreateMap<NumeroDocumento, NumeroDocumentoDto>()
               .ForMember(destino => destino.idNumeroDocumento,
                   opt => opt.MapFrom(origen => origen.idNumeroDocumento)
                )
               .ForMember(destino => destino.documento,
                   opt => opt.MapFrom(origen => origen.documento)
                );

            CreateMap<NumeroDocumentoDto, NumeroDocumento>()
               .ForMember(destino => destino.idNumeroDocumento,
                   opt => opt.MapFrom(origen => origen.idNumeroDocumento)
                )
               .ForMember(destino => destino.documento,
                   opt => opt.MapFrom(origen => origen.documento)
                );
            #endregion

            #region Categoria
            // Mapeo de Categoria a CategoriaDto
            CreateMap<Categoria, CategoriaDto>()
               .ForMember(destino => destino.idCategoria,
                   opt => opt.MapFrom(origen => origen.idCategoria)
                )
               .ForMember(destino => destino.descripcion,
                   opt => opt.MapFrom(origen => origen.descripcion)
                )
               .ForMember(destino => destino.estado,
                   opt => opt.MapFrom(origen => origen.estado)
                );

            CreateMap<CategoriaDto, Categoria>()
               .ForMember(destino => destino.idCategoria,
                   opt => opt.MapFrom(origen => origen.idCategoria)
                )
               .ForMember(destino => destino.descripcion,
                   opt => opt.MapFrom(origen => origen.descripcion)
                )
               .ForMember(destino => destino.estado,
                   opt => opt.MapFrom(origen => origen.estado)
                );
            #endregion

            #region Producto
            // Mapeo de Producto a ProductoDto
            CreateMap<Producto, ProductoDto>()
               .ForMember(destino => destino.idProducto,
                   opt => opt.MapFrom(origen => origen.idProducto)
                )
               .ForMember(destino => destino.codigoBarra,
                   opt => opt.MapFrom(origen => origen.codigoBarra)
                )
               .ForMember(destino => destino.descripcion,
                   opt => opt.MapFrom(origen => origen.descripcion)
                )
               .ForMember(destino => destino.categoriaDescripcion,
                   opt => opt.MapFrom(origen => origen.categoria != null ?
                        origen.categoria.descripcion : string.Empty)
                )
               .ForMember(destino => destino.precioVenta,
                   opt => opt.MapFrom(origen => origen.precioVenta)
                )
               .ForMember(destino => destino.stock,
                   opt => opt.MapFrom(origen => origen.stock)
                )
               .ForMember(destino => destino.stockMinimo,
                   opt => opt.MapFrom(origen => origen.stockMinimo)
                )
               .ForMember(destino => destino.estado,
                   opt => opt.MapFrom(origen => origen.estado)
                );

            CreateMap<ProductoDto, Producto>()
               .ForMember(destino => destino.idProducto,
                   opt => opt.MapFrom(origen => origen.idProducto)
                )
               .ForMember(destino => destino.codigoBarra,
                   opt => opt.MapFrom(origen => origen.codigoBarra)
                )
               .ForMember(destino => destino.descripcion,
                   opt => opt.MapFrom(origen => origen.descripcion)
                )
               .ForMember(destino => destino.idCategoria,
                   opt => opt.MapFrom(origen => origen.idCategoria)
                )
               .ForMember(destino => destino.precioVenta,
                   opt => opt.MapFrom(origen => origen.precioVenta)
                )
               .ForMember(destino => destino.stock,
                   opt => opt.MapFrom(origen => origen.stock)
                )
               .ForMember(destino => destino.stockMinimo,
                   opt => opt.MapFrom(origen => origen.stockMinimo)
                )
               .ForMember(destino => destino.estado,
                   opt => opt.MapFrom(origen => origen.estado)
                );
            #endregion

            #region Venta
            // Mapeo de Venta a VentaDto
            CreateMap<Venta, VentaDto>()
               .ForMember(destino => destino.dni,
                   opt => opt.MapFrom(origen => origen.dni)
                )
               .ForMember(destino => destino.cliente,
                   opt => opt.MapFrom(origen => origen.cliente)
                )
               .ForMember(destino => destino.descuento,
                   opt => opt.MapFrom(origen => origen.descuento)
                )
               .ForMember(destino => destino.total,
                   opt => opt.MapFrom(origen => origen.total)
                )
               .ForMember(destino => destino.idUsuario,
                   opt => opt.MapFrom(origen => origen.idUsuario)
                )
               .ForMember(destino => destino.estado,
                   opt => opt.MapFrom(origen => origen.estado)
                )
               .ForMember(destino => destino.motivo,
                   opt => opt.MapFrom(origen => origen.motivo)
                )
               .ForMember(destino => destino.usuarioAnula,
                   opt => opt.MapFrom(origen => origen.usuarioAnula)
                );

            CreateMap<VentaDto, Venta>()
               .ForMember(destino => destino.idVenta,
                   opt => opt.Ignore()
                )
               .ForMember(destino => destino.cliente,
                   opt => opt.MapFrom(origen => origen.cliente)
                )
               .ForMember(destino => destino.descuento,
                   opt => opt.MapFrom(origen => origen.descuento)
                )
               .ForMember(destino => destino.total,
                   opt => opt.MapFrom(origen => origen.total)
                )
               .ForMember(destino => destino.idUsuario,
                   opt => opt.MapFrom(origen => origen.idUsuario)
                )
               .ForMember(destino => destino.estado,
                   opt => opt.MapFrom(origen => origen.estado)
                )
               .ForMember(destino => destino.motivo,
                   opt => opt.MapFrom(origen => origen.motivo)
                )
               .ForMember(destino => destino.usuarioAnula,
                   opt => opt.MapFrom(origen => origen.usuarioAnula)
                );
            #endregion

            #region DetalleVenta
            // Mapeo de DetalleVenta a DetalleVentaDto
            CreateMap<DetalleVenta, DetalleVentaDto>()
               .ForMember(destino => destino.idVenta,
                   opt => opt.MapFrom(origen => origen.idVenta)
                )
               .ForMember(destino => destino.idProducto,
                   opt => opt.MapFrom(origen => origen.idProducto)
                )
               .ForMember(destino => destino.nombreProducto,
                   opt => opt.MapFrom(origen => origen.nombreProducto)
                )
               .ForMember(destino => destino.precio,
                   opt => opt.MapFrom(origen => origen.precio)
                )
               .ForMember(destino => destino.cantidad,
                   opt => opt.MapFrom(origen => origen.cantidad)
                )
               .ForMember(destino => destino.descuento,
                   opt => opt.MapFrom(origen => origen.descuento)
                )
               .ForMember(destino => destino.total,
                   opt => opt.MapFrom(origen => origen.total)
                );

            CreateMap<DetalleVentaDto, DetalleVenta>()
               .ForMember(destino => destino.idDetalleVenta,
                   opt => opt.Ignore()
                )
               .ForMember(destino => destino.idProducto,
                   opt => opt.MapFrom(origen => origen.idProducto)
                )
               .ForMember(destino => destino.nombreProducto,
                   opt => opt.MapFrom(origen => origen.nombreProducto)
                )
               .ForMember(destino => destino.precio,
                   opt => opt.MapFrom(origen => origen.precio)
                )
               .ForMember(destino => destino.cantidad,
                   opt => opt.MapFrom(origen => origen.cantidad)
                )
               .ForMember(destino => destino.descuento,
                   opt => opt.MapFrom(origen => origen.descuento)
                )
               .ForMember(destino => destino.total,
                   opt => opt.MapFrom(origen => origen.total)
                );
            #endregion
        }
    }
}
