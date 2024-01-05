using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Mini_project_API.Models;

namespace Mini_project_API.Data
{
    public partial class PersonInterestContext : DbContext
    {
        public PersonInterestContext()
        {
        }

        public PersonInterestContext(DbContextOptions<PersonInterestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Interest> Interests { get; set; } = null!;
        public virtual DbSet<Link> Links { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<PersonsInterest> PersonsInterests { get; set; } = null!;
        public virtual DbSet<PersonsInterestsLink> PersonsInterestsLinks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=PersonInterestProject;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interest>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Title).HasMaxLength(35);
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.Property(e => e.LinkToPage)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(35);

                entity.Property(e => e.LastName).HasMaxLength(35);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PersonsInterest>(entity =>
            {
                entity.HasKey(e => e.PersonInterestId)
                    .HasName("PK__PersonsI__E3E70328FD0379A0");

                entity.Property(e => e.InterestIdFk).HasColumnName("InterestId_FK");

                entity.Property(e => e.PersonIdFk).HasColumnName("PersonId_FK");

                entity.HasOne(d => d.InterestIdFkNavigation)
                    .WithMany(p => p.PersonsInterests)
                    .HasForeignKey(d => d.InterestIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonsIn__Inter__29572725");

                entity.HasOne(d => d.PersonIdFkNavigation)
                    .WithMany(p => p.PersonsInterests)
                    .HasForeignKey(d => d.PersonIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonsIn__Perso__286302EC");
            });

            modelBuilder.Entity<PersonsInterestsLink>(entity =>
            {
                entity.HasKey(e => e.PersonInterestLinkId)
                    .HasName("PK__PersonsI__E738390E06A33C05");

                entity.Property(e => e.LinkIdFk).HasColumnName("LinkId_FK");

                entity.Property(e => e.PersonsInterestsIdFk).HasColumnName("PersonsInterestsId_FK");

                entity.HasOne(d => d.LinkIdFkNavigation)
                    .WithMany(p => p.PersonsInterestsLinks)
                    .HasForeignKey(d => d.LinkIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonsIn__LinkI__2E1BDC42");

                entity.HasOne(d => d.PersonsInterestsIdFkNavigation)
                    .WithMany(p => p.PersonsInterestsLinks)
                    .HasForeignKey(d => d.PersonsInterestsIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonsIn__Perso__4AB81AF0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
