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
    public class Documento_Repository : IDocumento_Repository
    {
        private readonly PosContext _posContext;

        public Documento_Repository(PosContext posContext)
        {
            _posContext = posContext;
        }

        public async Task<NumeroDocumento> Create(NumeroDocumento documento)
        {
            if (documento == null)
            {
                throw new ArgumentNullException(nameof(documento));
            }
            _posContext.NumeroDocumentos.Add(documento);
            await _posContext.SaveChangesAsync();
            return documento;
        }

        public async Task<NumeroDocumento?> Get()
        {
            return await _posContext.NumeroDocumentos.FirstOrDefaultAsync();
        }

        public async Task<NumeroDocumento> Update(NumeroDocumento documento)
        {
            var documentoExistente = await _posContext.NumeroDocumentos.FirstOrDefaultAsync(d => d.idNumeroDocumento == documento.idNumeroDocumento);
            if (documentoExistente == null)
            {
                throw new KeyNotFoundException($"No se encontro el registro con el ID: {documento.idNumeroDocumento}.");
            }

            documentoExistente.documento = documento.documento;

            _posContext.NumeroDocumentos.Update(documentoExistente);
            await _posContext.SaveChangesAsync();
            return documentoExistente;
        }
    }
}
