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
        }
    }
}
