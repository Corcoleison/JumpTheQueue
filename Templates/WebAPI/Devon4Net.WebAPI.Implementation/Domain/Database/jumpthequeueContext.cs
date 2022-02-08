using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Domain.Database
{
    public partial class jumpthequeueContext : DbContext
    {
        public jumpthequeueContext()
        {
        }

        public jumpthequeueContext(DbContextOptions<jumpthequeueContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessCode> AccessCode { get; set; }
        public virtual DbSet<Queue> Queue { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Visitor> Visitor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=jumpthequeue;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessCode>(entity =>
            {
                entity.ToTable("access_code");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("character varying");

                entity.Property(e => e.Createdtime)
                    .HasColumnName("createdtime")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Endtime)
                    .HasColumnName("endtime")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.QueueId).HasColumnName("queue_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.Property(e => e.VisitorUid)
                    .HasColumnName("visitor_uid")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.AccessCode)
                    .HasForeignKey(d => d.QueueId)
                    .HasConstraintName("access_code_queue_id_fkey");

                entity.HasOne(d => d.VisitorU)
                    .WithMany(p => p.AccessCode)
                    .HasForeignKey(d => d.VisitorUid)
                    .HasConstraintName("fk_visitor");
            });

            modelBuilder.Entity<Queue>(entity =>
            {
                entity.ToTable("queue");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Accesslink)
                    .HasColumnName("accesslink")
                    .HasColumnType("character varying");

                entity.Property(e => e.Closed).HasColumnName("closed");

                entity.Property(e => e.Closetime)
                    .HasColumnName("closetime")
                    .HasColumnType("character varying");

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasColumnType("character varying");

                entity.Property(e => e.Minattentiontime).HasColumnName("minattentiontime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.Opentime)
                    .HasColumnName("opentime")
                    .HasColumnType("character varying");

                entity.Property(e => e.Started).HasColumnName("started");

                entity.Property(e => e.UserClientid)
                    .IsRequired()
                    .HasColumnName("user_clientid")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.UserClient)
                    .WithMany(p => p.Queue)
                    .HasForeignKey(d => d.UserClientid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Clientid)
                    .HasName("user_pkey");

                entity.ToTable("user");

                entity.Property(e => e.Clientid)
                    .HasColumnName("clientid")
                    .HasColumnType("character varying");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasConversion<string>()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("visitor_pkey");

                entity.ToTable("visitor");

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
