using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Service.Interface
{
    public interface IUsuario_Service
    {
        Task<Usuario> Create(Usuario entity, string password);
        Task<bool> Delete (int id);
        Task<List<Usuario>> GetAll ();
        Task<Usuario?> GetById(int id);
        Task<Usuario> Update(Usuario entity, string password);
        Task<string> GetRolById(int id);
        Task<string> Login(string email, string password);
    }
}
