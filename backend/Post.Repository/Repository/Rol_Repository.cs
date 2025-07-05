using Microsoft.EntityFrameworkCore;
using Pos.Model.Context;
using Pos.Model.Model;
using Pos.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Repository.Repository
{
    public class Rol_Repository : IRepository<Rol>
    {
        private readonly PosContext _poscontext;

        public Rol_Repository( PosContext poscontext )
        {
            _poscontext = poscontext;
        }

        public async Task<Rol> Create(Rol entity)
        {
            if( entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _poscontext.Roles.Add( entity );
            await _poscontext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var rol = await _poscontext.Roles.FindAsync(id);
            if( rol == null )
            {
                return false;
            }
            _poscontext.Roles.Remove( rol );
            await _poscontext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Rol>> GetAll()
        {
            return await _poscontext.Roles.ToListAsync();
        }

        public async Task<Rol?> GetById(int id)
        {
            return await _poscontext.Roles.FindAsync(id);
        }

        public async Task<Rol> Update(Rol entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var rolExistente = await _poscontext.Roles.FirstOrDefaultAsync(r => r.idRol == entity.idRol);
            if( rolExistente == null )
            {
                throw new KeyNotFoundException($"No se encontro el rol con el ID: {entity.idRol}");
            }
            rolExistente.descripcion = entity.descripcion;
            rolExistente.estado = entity.estado;

            _poscontext.Roles.Update(rolExistente);
            await _poscontext.SaveChangesAsync();
            return rolExistente;
        }

    }
}
