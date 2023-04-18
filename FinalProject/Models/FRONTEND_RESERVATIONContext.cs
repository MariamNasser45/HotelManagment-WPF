using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalProject.Models
{
    public partial class FRONTEND_RESERVATIONContext : DbContext
    {
        public virtual DbSet<reservation> reservation { get; set; }

        public FRONTEND_RESERVATIONContext(DbContextOptions<FRONTEND_RESERVATIONContext> options) : base(options)
        {
        }

        public FRONTEND_RESERVATIONContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UUIBUUP\\SQLEXPRESS;Initial Catalog=FRONTEND_RESERVATION;Integrated Security=True;Trust Server Certificate=True;Command Timeout=300");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<reservation>(entity =>
            {
                entity.Property(e => e.card_cvc).IsFixedLength();

                entity.Property(e => e.card_type).IsFixedLength();

                entity.Property(e => e.payment_type).IsFixedLength();

                entity.Property(e => e.room_floor).IsFixedLength();

                entity.Property(e => e.room_number).IsFixedLength();

                entity.Property(e => e.room_type).IsFixedLength();

                entity.Property(e => e.zip_code).IsFixedLength();
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
