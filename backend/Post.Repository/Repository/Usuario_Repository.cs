using Microsoft.AspNetCore.Identity;
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
    public class Usuario_Repository : IUsuario_Repository
    {
        private readonly PosContext _posContext;
        private readonly UserManager<Usuario> _userManager;

        public Usuario_Repository(PosContext posContext, UserManager<Usuario> userManager)
        {
            _posContext = posContext;
            _userManager = userManager;
        }

        public async Task<Usuario> Create(Usuario entity, string password)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.UserName = entity.Email;
            var resultado = await _userManager.CreateAsync(entity, password);
            if (!resultado.Succeeded)
            {
                throw new Exception(string.Join(", ", resultado.Errors.Select(e => e.Description)));
            }

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await _posContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            _posContext.Usuarios.Remove(usuario);
            await _posContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _posContext.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await _posContext.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetById(int id)
        {
            return await _posContext.Usuarios
                .Include (u => u.Rol)
                .FirstOrDefaultAsync(u => u.idUsuario == id);
        }

        public async Task<string> GetRolById(int id)
        {
            var usuario = await _posContext.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.idUsuario == id);

            return usuario?.Rol?.descripcion ?? string.Empty;
        }

        public async Task<Usuario> Update(Usuario entity, string password)
        {
            
            var usuarioExistente = await _posContext.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.idUsuario == entity.idUsuario);

            if (usuarioExistente == null) 
            {
                throw new KeyNotFoundException($"No se encontro el usuario con el ID: {entity.idUsuario}.");
            }

            usuarioExistente.nombres = entity.nombres;
            usuarioExistente.apellidos = entity.apellidos;
            usuarioExistente.idRol = entity.idRol;
            usuarioExistente.Telefono = entity.Telefono;
            usuarioExistente.UserName = entity.UserName;
            if(!string.IsNullOrEmpty(password))
            {
                var passwordStr = new PasswordHasher<Usuario>();
                usuarioExistente.PasswordHash = passwordStr.HashPassword(usuarioExistente, password);
            }
            usuarioExistente.estado = entity.estado;

            _posContext.Usuarios.Update(usuarioExistente);
            await _posContext.SaveChangesAsync();
            return usuarioExistente;
        }
    }
}
