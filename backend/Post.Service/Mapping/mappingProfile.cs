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
        }
    }
}
