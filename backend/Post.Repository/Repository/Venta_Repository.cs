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
    public class Venta_Repository : IVenta_Repository
    {
        private readonly PosContext _posContext;
        private readonly IDocumento_Repository _documentoRepository;

        public Venta_Repository(PosContext posContext, IDocumento_Repository documentoRepository)
        {
            _posContext = posContext;
            _documentoRepository = documentoRepository;
        }

        public async Task ActualizarStock(List<DetalleVenta> detalleVentas)
        {
            if (_documentoRepository == null)
            {
                throw new("La lista de detalles de venta esta vacia.");
            }
            foreach (var detalle in detalleVentas)
            {
                var producto = await _posContext.Productos
                    .AsTracking()
                    .FirstOrDefaultAsync(p => p.idProducto == detalle.idProducto);

                if (producto == null)
                {
                    throw new KeyNotFoundException($"El producto con el ID: {detalle.idProducto} no existe en el inventario.");
                }

                if(producto.stock < detalle.cantidad) {
                    throw new InvalidOperationException($"No hay suficiente stock para el producto: {producto.descripcion}. Stock disponible: {producto.stock}, la cantidad solicitada: {detalle.cantidad}.");
                }
                producto.stock -= detalle.cantidad;
            }
            await _posContext.SaveChangesAsync();
        }

        public async Task<Venta?> AnularVenta(int idVenta, string motivo, int idUsuario)
        {
            var venta = await _posContext.Ventas
                .Include(v => v.DetalleVentas)
                .FirstOrDefaultAsync(v => v.idVenta == idVenta);

            if(venta == null)
            {
                throw new KeyNotFoundException($"La venta con el ID: {idVenta} no existe o fue eliminada.");
            }

            // actualizamos la venta
            venta.estado = EstadoVenta.Anulada;
            venta.fechaAnulada = DateOnly.FromDateTime( DateTime.Today );
            venta.motivo = motivo;
            venta.usuarioAnula = idUsuario;

            // actualizamos stock del producto
            if(venta.DetalleVentas?.Any() == true)
            {
                foreach (var detalle in venta.DetalleVentas)
                {
                    var producto = await _posContext.Productos
                        .FindAsync(detalle.idProducto);

                    if (producto != null)
                    {
                        producto.stock += detalle.cantidad;
                    }
                }
            }
            await _posContext.SaveChangesAsync();
            return venta;
        }

        public async Task<List<Venta>> BuscarFecha(DateOnly FechaInicio, DateOnly FechaFin)
        {
            return await _posContext.Ventas
                .Where(v => v.fecha >= FechaInicio && v.fecha <= FechaFin)
                .ToListAsync();
        }

        public async Task<Venta> Create(Venta venta)
        {
            using var transaction = await _posContext.Database.BeginTransactionAsync();

            try
            {
                var numeroDocumento = await _documentoRepository.Get();
                string documentoSiguiente;

                if(numeroDocumento == null)
                {
                    documentoSiguiente = "0001";
                    var nuevoDocumento = new NumeroDocumento { documento = documentoSiguiente };
                    await _documentoRepository.Create(nuevoDocumento);
                }
                else
                {
                    var documentoActual = int.Parse(numeroDocumento.documento);
                    documentoSiguiente = (documentoActual + 1).ToString("D4"); 
                }
                venta.factura = documentoSiguiente;


                if(string.IsNullOrEmpty(venta.factura))
                {
                    throw new InvalidOperationException("No se pudo generar el numero de factura. Intentelo nuevamente.");
                }

                if(venta.DetalleVentas?.Any() == true)
                {
                    await ActualizarStock(venta.DetalleVentas.ToList());
                }

                _posContext.Add(venta);
                await _posContext.SaveChangesAsync();

                if(numeroDocumento != null)
                {
                    numeroDocumento.documento = documentoSiguiente;
                    await _documentoRepository.Update(numeroDocumento);
                }
                await transaction.CommitAsync();
                return venta;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Ocurrio un error al registrar la venta.", ex);
            }
        }

        public async Task<List<Venta>> GetAll()
        {
            return await _posContext.Ventas
                .Include(v => v.DetalleVentas)
                .ToListAsync();
        }

        public async Task<List<DetalleVenta>> GetDetallesById(int idVenta)
        {
            var detallesVenta = await _posContext.DetallesVenta
                .Where(dv => dv.idVenta == idVenta)
                .ToListAsync();

            if(detallesVenta.Count == 0)
            {
                throw new InvalidOperationException($"No se encontraron detalles de venta para el ID: {idVenta}, verifica que el ID sea el correcto.");
            }
            return detallesVenta;
        }
    }
}
