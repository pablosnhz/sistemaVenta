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
    public class Producto_Service : IService<Producto>
    {
        private readonly Producto_Repository _productoRepository;


        public Producto_Service(Producto_Repository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Producto> Create(Producto entity)
        {
            return await _productoRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _productoRepository.Delete(id);
        }

        public async Task<List<Producto>> GetAll()
        {
            return await _productoRepository.GetAll();
        }

        public async Task<Producto?> GetById(int id)
        {
            return await _productoRepository.GetById(id);
        }

        public async Task<Producto> Update(Producto entity)
        {
            return await _productoRepository.Update(entity);
        }
    }
}