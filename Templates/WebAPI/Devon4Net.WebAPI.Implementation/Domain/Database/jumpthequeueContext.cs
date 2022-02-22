using System;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Devon4Net.WebAPI.Implementation
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=jumpthequeue;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessCode>(entity =>
            {
                entity.ToTable("access_code");

                entity.HasIndex(e => e.QueueId);

                entity.HasIndex(e => e.VisitorUid);

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

                entity.Property(e => e.VisitorUid).HasColumnName("visitor_uid");

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.AccessCode)
                    .HasForeignKey(d => d.QueueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("access_code_queue_id_fkey");

                entity.HasOne(d => d.VisitorU)
                    .WithMany(p => p.AccessCode)
                    .HasForeignKey(d => d.VisitorUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("access_code_visitor_uid_fk");
            });

            modelBuilder.Entity<Queue>(entity =>
            {
                entity.ToTable("queue");

                entity.HasIndex(e => e.UserClientid);

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
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<User>().HasData(
                        new User { Clientid = "OP1", Role = Role_t.Owner },
                        new User { Clientid = "OP2", Role = Role_t.Owner },
                        new User { Clientid = "OP3", Role = Role_t.Owner }
            );

            modelBuilder.HasPostgresExtension("uuid-ossp");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
