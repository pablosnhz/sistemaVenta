using Microsoft.EntityFrameworkCore;
using Pos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Model.Context
{
    public class PosContext : DbContext
    {
        public PosContext(DbContextOptions<PosContext> options) : base(options) { }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Negocio> Negocios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rol>(static entity =>
            {
                entity.HasKey(r => r.idRol);
                entity.Property(r => r.descripcion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(r => r.estado)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(r => r.fechaRegistro)
                .IsRequired()
                .HasDefaultValueSql("Now()");

                entity.HasIndex(r => r.descripcion).IsUnique();
            });

            modelBuilder.Entity<Usuario>(static entity =>
            {
                entity.HasKey(u => u.idUsuario);

                entity.Property(u => u.nombres)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

                entity.Property(u => u.apellidos)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

                entity.HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .IsRequired()
                .HasForeignKey(u => u.idRol);

                entity.Property(u => u.Telefono)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

                entity.Property(u => u.estado)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(u => u.fechaRegistro)
                .IsRequired()
                .HasDefaultValueSql("Now()");

                entity.HasIndex(u => u.Telefono).IsUnique();
            });

            modelBuilder.Entity<Negocio>(static entity =>
            {
                entity.HasKey(n => n.idNegocio);

                entity.Property(n => n.ruc)
                .IsRequired()
                .HasMaxLength (20)
                .IsUnicode(false);

                entity.Property(n => n.razonSocial)
                .IsRequired()
                .HasMaxLength (50)
                .IsUnicode(false);

                entity.Property(n => n.email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property (n => n.telefono)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

                entity.Property(n => n.direccion)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

                entity.Property(n => n.propietario) 
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(n => n.descuento)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(4,2)
                .IsUnicode (false);

                entity.Property(n => n.fechaRegistro)
                .IsRequired()
                .HasDefaultValueSql("Now()");
            });

            modelBuilder.Entity<Categoria>(static entity =>
            {
                entity.HasKey(c => c.idCategoria);

                entity.Property(c => c.descripcion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(c => c.estado)
                .IsRequired ()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(c => c.fechaRegistro)
                .IsRequired()
                .HasDefaultValueSql("Now()");

                // relacion con producto
                Object value = entity.HasMany(c => c.Productos)
                .WithOne(p => p.categoria)
                .HasForeignKey(p => p.idCategoria)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(c => c.descripcion)
                .IsUnique();
            });

            modelBuilder.Entity<Producto>(static entity =>
            {
                entity.HasKey(p => p.idProducto);

                entity.Property (p => p.codigoBarra)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

                entity.Property(p => p.descripcion)
                .IsRequired()
                .HasMaxLength (50)
                .IsUnicode(false);

                entity.HasOne(p => p.categoria)
                .WithMany(c => c.Productos)
                .IsRequired()
                .HasForeignKey(p => p.idCategoria);

                entity.Property(c => c.precioVenta)
                .IsRequired()
                .HasPrecision(18,2)
                .IsUnicode (false);

                entity.Property(p => p.stock)
                .IsRequired()
                .HasDefaultValue(0)
                .IsUnicode(false);

                entity.Property(p => p.stockMinimo)
                .IsRequired()
                .HasDefaultValue(5)
                .IsUnicode(false);

                entity.Property(p => p.estado)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(p => p.fechaRegistro)
                .IsRequired()
                .HasDefaultValueSql("Now()");

                entity.HasIndex(c => c.codigoBarra).IsUnique();
                entity.HasIndex(c => c.descripcion).IsUnique();

            });

            modelBuilder.Entity<Venta>(static entity =>
            {
                entity.HasKey(v => v.idVenta);

                entity.Property(v => v.factura)
                .HasMaxLength(10)
                .IsUnicode(false);

                entity.Property(v => v.fecha)
                .IsRequired()
                .HasDefaultValueSql("Now()");

                entity.Property(v => v.dni)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode (false);

                entity.Property(v => v.cliente)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(v => v.descuento)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(4, 2)
                .IsUnicode();

                entity.Property(v => v.total)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(18, 2)
                .IsUnicode();

                // hicimos la conversion de estado
                entity.Property(v => v.estado)
                .IsRequired()
                .HasConversion<string>();

                entity.Property(v => v.fechaAnulada)
                .IsRequired(false);

                entity.Property(v => v.motivo)
                .IsRequired(false)
                .HasColumnType("TEXT");

                entity.Property(v => v.usuarioAnula)
                .IsRequired(false);

                // especificamos que en venta puede haber muchos detalles de venta
                object value = entity.HasMany(v => v.DetalleVentas)
                .WithOne(dv => dv.Venta)
                .HasForeignKey(dv => dv.idVenta)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(v => v.factura).IsUnique();
            });

            modelBuilder.Entity<DetalleVenta>(static entity =>
            {
                entity.HasKey(dv => dv.idDetalleVenta);

                entity.HasOne(dv => dv.Venta)
                .WithMany(v => v.DetalleVentas)
                .HasForeignKey(dv => dv.idVenta)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(dv => dv.Producto)
                .WithMany()
                .HasForeignKey(dv => dv.idProducto)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(dv => dv.nombreProducto)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(dv => dv.precio)
                .IsRequired()
                .HasPrecision(18, 2)
                .IsUnicode(false);

                entity.Property(dv => dv.cantidad)
                .IsRequired()
                .HasDefaultValue(1)
                .IsUnicode(false);

                entity.Property(dv => dv.descuento)
                .IsRequired()
                .HasDefaultValue(0)
                .IsUnicode(false);

                entity.Property(dv => dv.total)
                .IsRequired()
                .IsUnicode (false);
            });
        }
    }
}
