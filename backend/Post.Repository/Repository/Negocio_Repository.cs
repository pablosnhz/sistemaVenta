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
    public class Negocio_Repository : INegocio_Repository
    {
        private readonly PosContext _posContext;

        public Negocio_Repository(PosContext posContext)
        {
            this._posContext = posContext;
        }

        public async Task<Negocio?> Create(Negocio negocio)
        {
            if(negocio == null)
            {
                throw new ArgumentNullException(nameof(negocio));
            }
            _posContext.Negocios.Add(negocio);
            await _posContext.SaveChangesAsync();
            return negocio;
        }

        public async Task<Negocio?> Get()
        {
            return await _posContext.Negocios.FirstOrDefaultAsync();
        }

        public async Task<Negocio?> Update(Negocio negocio)
        {
            var negocioExistente = await _posContext.Negocios.FirstOrDefaultAsync(n => n.idNegocio == negocio.idNegocio);

            if(negocioExistente == null)
            {
                throw new KeyNotFoundException($"No se encontro el registro con el ID: {negocio.idNegocio}.");
            }

            negocioExistente.ruc = negocio.ruc;
            negocioExistente.razonSocial = negocio.razonSocial;
            negocioExistente.email = negocio.email;
            negocioExistente.telefono = negocio.telefono;
            negocioExistente.direccion = negocio.direccion;
            negocioExistente.propietario = negocio.propietario;
            negocioExistente.descuento = negocio.descuento;

            _posContext.Negocios.Update(negocioExistente);
            await _posContext.SaveChangesAsync();
            return negocioExistente;
        }
    }
}
