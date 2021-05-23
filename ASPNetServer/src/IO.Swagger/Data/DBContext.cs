using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IO.Swagger.Models;


namespace IO.Swagger.Data
{
    /// <summary>
    /// Context of the Database
    /// </summary>
    public class DBContext : DbContext
    {
        /// <summary>
        /// Every type of Media, be it game, movie or TV Show
        /// </summary>
        public DbSet<Media> Media { get; set; }

        /// <summary>
        /// Specific entry, e.g. Level or Episode
        /// </summary>
        new public DbSet<Entry> Entry { get; set; }

        /// <summary>
        /// Needs to inherit constructor of base class
        /// </summary>
        /// <param name="options"></param>
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        { }
        /// <summary>
        /// ...
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entities to Tables
            modelBuilder.Entity<Media>().ToTable("Media");
            modelBuilder.Entity<Entry>().ToTable("Entry");

            // Primary Keys
            modelBuilder.Entity<Media>().HasKey(Media => Media.Id).HasName("MediaID");
            modelBuilder.Entity<Entry>().HasKey(Entry => Entry.ID).HasName("EntryID");

            // Columns
            modelBuilder.Entity<Media>().Property(Media => Media.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Media>().Property(Media => Media.Name).HasColumnType("nvarchar(255)").IsRequired();
            modelBuilder.Entity<Media>().Property(Media => Media.Picture).HasColumnType("text").IsRequired();
            modelBuilder.Entity<Media>().Property(Media => Media.Type).HasColumnType("nvarchar(255)").IsRequired();
            modelBuilder.Entity<Media>().Property(Media => Media.Platform).HasColumnType("nvarchar(255)").IsRequired(false);
            modelBuilder.Entity<Media>().Property(Media => Media.ReleaseDate).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<Media>().Property(Media => Media.Comments).HasColumnType("text").IsRequired(false);

            modelBuilder.Entity<Entry>().Property(Entry => Entry.Timestamp).HasColumnType("datetime").IsRequired(false);
            modelBuilder.Entity<Entry>().Property(Entry => Entry.ID).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Entry>().Property(Entry => Entry.Name).HasColumnType("nvarchar(255)").IsRequired();
            modelBuilder.Entity<Entry>().Property(Entry => Entry.Mediaid).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Entry>().Property(Entry => Entry.Comments).HasColumnType("text").IsRequired(false);
            modelBuilder.Entity<Entry>().Property(Entry => Entry.ParentID).HasColumnType("int").IsRequired(false);
            modelBuilder.Entity<Entry>().Property(Entry => Entry.Picture).HasColumnType("text").IsRequired(false);

            // Relation
            modelBuilder.Entity<Entry>().HasOne<Media>().WithMany().HasPrincipalKey(Media => Media.Id)
                .HasForeignKey(Entry => Entry.Mediaid).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_MediaID");
        }


    }
}
