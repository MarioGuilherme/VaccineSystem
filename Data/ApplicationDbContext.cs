using VaccineSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaccineSystem.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Person> Person { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Vaccine> Vaccine { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Vaccine>(entity => {
                entity.ToTable("vaccines");
                entity.Property(v => v.vaccine_value).HasPrecision(18, 2);
                entity.HasOne(v => v.Person).WithMany(v => v.Vaccine).HasForeignKey(v => v.id_person).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Address>(entity => {
                entity.ToTable("addresses");
                entity.HasOne(a => a.Person).WithMany(a => a.Address).HasForeignKey(a => a.id_person).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            // FOI HABILITADO O CASCADE AO FAZER O DELETE E UM REGISTRO PARA EXCLUSÃO DOS REGISTROS 'FILHOS' COMO CHAVE ESTRANGEIRA
            base.OnModelCreating(modelBuilder);
        }
    }
}