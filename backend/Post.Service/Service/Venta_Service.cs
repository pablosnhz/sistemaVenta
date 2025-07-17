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
    public class Venta_Service : IVenta_Service
    {
        private readonly IVenta_Repository _ventaRepository;

        public Venta_Service(IVenta_Repository ventaRepository)
        { 
            _ventaRepository = ventaRepository;
        }

        public async Task<Venta?> AnularVenta(int idVenta, string motivo, int idUsuario)
        {
            return await _ventaRepository.AnularVenta(idVenta, motivo, idUsuario);
        }

        public async Task<List<Venta>> BuscarFecha(DateOnly FechaInicio, DateOnly FechaFIn)
        {
            return await _ventaRepository.BuscarFecha(FechaInicio, FechaFIn);
        }

        public async Task<Venta> Create(Venta venta)
        {
            return await _ventaRepository.Create(venta);
        }

        public async Task<List<Venta>> GetAll()
        {
            return await _ventaRepository.GetAll();
        }

        public async Task<List<DetalleVenta>> GetDetallesById(int idVenta)
        {
            return await _ventaRepository.GetDetallesById(idVenta);
        }
    }
}
