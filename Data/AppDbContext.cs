using Microsoft.EntityFrameworkCore;
using SistemaRestaurante.Models;
using static SistemaRestaurante.Models.PedidoItemModel;

namespace SistemaRestaurante.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<PedidoItemModel> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento Pedido -> PedidoItem
            modelBuilder.Entity<PedidoItemModel>()
                .HasOne(pi => pi.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(pi => pi.PedidoId);

            modelBuilder.Entity<PedidoItemModel>()
                .HasOne(pi => pi.Produto)
                .WithMany()
                .HasForeignKey(pi => pi.ProdutoId);
        }
    }
}
