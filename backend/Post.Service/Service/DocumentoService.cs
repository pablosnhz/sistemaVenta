using Pos.Model.Model;
using Pos.Repository.Interface;
using Pos.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Service.Service
{
    public class DocumentoService : IDocumento_Service
    {
        private readonly IDocumento_Repository _documentoRepository;
        
        public DocumentoService(IDocumento_Repository documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }

        public async Task<NumeroDocumento?> Get()
        {
            var documento = await _documentoRepository.Get();
            if (documento == null)
            {
                return null;
            }
            return documento;
        }

        public async Task<NumeroDocumento> Save(NumeroDocumento documento)
        {
            var entidadExistente = await _documentoRepository.Get();
            if(entidadExistente == null)
            {
                var entidadCreada = await _documentoRepository.Create(documento);
                return entidadCreada;
            }
            else
            {
                documento.idNumeroDocumento = entidadExistente.idNumeroDocumento;
                var entidadActualizada = await _documentoRepository.Update(documento);
                return entidadActualizada;
            }
        }
    }
}
