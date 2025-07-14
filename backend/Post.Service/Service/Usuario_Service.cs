using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pos.Model.Model;
using Pos.Repository.Interface;
using Pos.Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Service.Service
{
    public class Usuario_Service : IUsuario_Service
    {
        public readonly IUsuario_Repository _usuarioRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;
    
        public Usuario_Service(IUsuario_Repository usuarioRepository, UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Usuario> Create(Usuario entity, string password)
        {
            return await _usuarioRepository.Create(entity, password);
        }

        public async Task<bool> Delete(int id)
        {
            return await _usuarioRepository.Delete(id);
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _usuarioRepository.GetAll();
        }

        public async Task<Usuario?> GetById(int id)
        {
            return await _usuarioRepository.GetById(id);
        }

        public async Task<string> GetRolById(int id)
        {
            return await _usuarioRepository.GetRolById(id);
        }

        public async Task<string> Login(string email, string password)
        {
            var usuario = await _usuarioRepository.GetByEmail(email);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            var resultado = await _userManager.CheckPasswordAsync(usuario, password);
            if(!resultado)
            {
                throw new Exception("Credenciales incorrectas. Verifique por favor.");
            }
            return GenerarToken(usuario);
        }

        public async Task<Usuario> Update(Usuario entity, string password)
        {
            return await _usuarioRepository.Update(entity, password);
        }

        private string GenerarToken(Usuario usuario)
        {
            // generamos el jwt
            var secretKey = _configuration["Jwt:secretKey"] ?? throw new ArgumentNullException("Jwt: secretKey no esta bien configurada");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.idUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, usuario.Rol?.descripcion ?? string.Empty),
            };

            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
