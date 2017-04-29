using quZONE.Domain.Entities;

namespace quZONE.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class qZONEDbContext : DbContext
    {
        static qZONEDbContext()
        {
            Database.SetInitializer<qZONEDbContext>(null);
        }

        public qZONEDbContext()
            : base("name=qZONEDbContext")
        {
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<WalkInWaitList> WalkInWaitLists { get; set; }
        public virtual DbSet<GuestTable> GuestTables { get; set; }
        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<TrialRequest> TrialRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                 .HasMany(e => e.Organizations)
                 .WithRequired(e => e.Address)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Guest>()
                .HasMany(e => e.WalkInWaitLists)
                .WithRequired(e => e.Guest)
                .WillCascadeOnDelete(false);
        }
    }
}
