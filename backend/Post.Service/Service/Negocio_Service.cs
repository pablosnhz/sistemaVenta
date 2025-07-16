using Pos.Model.Model;
using Pos.Repository.Interface;
using Pos.Repository.Repository;
using Pos.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Service.Service
{
    public class Negocio_Service : INegocio_Service
    {
        private readonly INegocio_Repository _negocioRepository;

        public Negocio_Service(INegocio_Repository negocioRepository)
        {
            _negocioRepository = negocioRepository;
        }

        public async Task<Negocio?> Get()
        {
            var documento = await _negocioRepository.Get();
            if (documento == null)
            {
                return null;
            }
            return documento;
        }

        public async Task<Negocio> Save(Negocio negocio)
        {
            var entidadExistente = await _negocioRepository.Get();
            if (entidadExistente == null)
            {
                var entidadCreada = await _negocioRepository.Create(negocio);
                return entidadCreada;
            }
            else
            {
                negocio.idNegocio = entidadExistente.idNegocio;
                var entidadActualizada = await _negocioRepository.Update(negocio);
                return entidadActualizada;
            }
        }
    }
}
