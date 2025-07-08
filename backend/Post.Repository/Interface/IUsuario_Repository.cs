using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Repository.Interface
{
    public interface IUsuario_Repository
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario?> GetById(int id);
        Task<Usuario> Create(Usuario entity, string password);
        Task<Usuario> Update(Usuario entity, string password);
        Task<bool> Delete(int id);
        Task<string> GetRolById(int id);
        Task<Usuario?> GetByEmail (string email);
    }
}
