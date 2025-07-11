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
    public class Categoria_Service : IService<Categoria>
    {
        private readonly Categoria_Repository _categoriaRepository;

        public Categoria_Service(Categoria_Repository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> Create(Categoria entity)
        {
            return await _categoriaRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _categoriaRepository.Delete(id);
        }

        public async Task<List<Categoria>> GetAll()
        {
            return await _categoriaRepository.GetAll();
        }

        public async Task<Categoria?> GetById(int id)
        {
            return await _categoriaRepository.GetById(id);
        }

        public async Task<Categoria> Update(Categoria entity)
        {
            return await _categoriaRepository.Update(entity);
        }
    }
}
