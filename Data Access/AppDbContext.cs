using Microsoft.EntityFrameworkCore;
using ConsultorioSeguros.Models;
namespace ConsultorioSeguros.Data_Access
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }

        public DbSet<Usuario> usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(tb=>
            {
                tb.HasKey(ent => ent.IdUsuario);
                tb.Property(ent => ent.IdUsuario)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired();
                tb.Property(ent => ent.NombreCompleto).HasMaxLength(50);
                tb.Property(ent => ent.Correo).HasMaxLength(50);
                tb.Property(ent => ent.User).HasMaxLength(50);
                tb.Property(ent => ent.Password).HasMaxLength(50);
                
            });
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
        }



    }
}
