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
    public class Producto_Repository : IRepository<Producto>
    {
        private readonly PosContext _posContext;
        
        public Producto_Repository(PosContext posContext)
        {
            _posContext = posContext;
        }
        public async Task<Producto> Create(Producto entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _posContext.Productos.Add(entity);
            await _posContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var producto = await _posContext.Productos.FindAsync(id);
            if (producto == null)
            {
                return false;
            }
            _posContext.Productos.Remove(producto);
            await _posContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Producto>> GetAll()
        {
            return await _posContext.Productos.Include(p => p.categoria)
                .ToListAsync();
        }

        public async Task<Producto?> GetById(int id)
        {
            return await _posContext.Productos.Include(p => p.categoria).FirstOrDefaultAsync(p => p.idProducto == id);
        }

        public async Task<Producto> Update(Producto entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var productoExistente = await _posContext.Productos
                .Include(p => p.categoria)
                .FirstOrDefaultAsync(p => p.idProducto == entity.idProducto);
            if (productoExistente == null)
            {
                throw new KeyNotFoundException($"No se encontro el producto con el ID: {entity.idProducto}");
            }
            productoExistente.codigoBarra = entity.codigoBarra;
            productoExistente.descripcion = entity.descripcion;
            productoExistente.idCategoria = entity.idCategoria;
            productoExistente.precioVenta = entity.precioVenta;
            productoExistente.stock = entity.stock;
            productoExistente.stockMinimo = entity.stockMinimo;
            productoExistente.estado = entity.estado;

            productoExistente.estado = entity.estado;

            _posContext.Productos.Update(productoExistente);
            await _posContext.SaveChangesAsync();
            return productoExistente;
        }
    }
}
