using Pos.Model.Model;
using Pos.Repository.Repository;
using Pos.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Service.Service
{
    public class Rol_Service : IService<Rol>
    {
        private readonly Rol_Repository _rolRepository;
        
        public Rol_Service(Rol_Repository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<Rol> Create(Rol entity)
        {
            return await _rolRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _rolRepository.Delete(id);
        }

        public async Task<List<Rol>> GetAll()
        {
            return await _rolRepository.GetAll();
        }

        public async Task<Rol?> GetById(int id)
        {
            return await _rolRepository.GetById(id);
        }

        public async Task<Rol> Update(Rol entity)
        {
            return await _rolRepository.Update(entity); 
        }
    }
}
