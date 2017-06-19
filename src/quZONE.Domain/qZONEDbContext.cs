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

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<KBVersionHistory> KBVersionHistories { get; set; }
        public virtual DbSet<KnowledgeBase> KnowledgeBases { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        
        public virtual DbSet<RequestResponse> RequestResponses { get; set; }
        public virtual DbSet<Resolution> Resolutions { get; set; }
        public virtual DbSet<SupportRequest> SupportRequests { get; set; }
        public virtual DbSet<SupportRequestStatu> SupportRequestStatus { get; set; }


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

            modelBuilder.Entity<KnowledgeBase>()
                .HasMany(e => e.KBVersionHistories)
                .WithRequired(e => e.KnowledgeBase)
                .HasForeignKey(e => e.KnolwdgeBaseId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Organization>()
            //    .HasMany(e => e.Accounts)
            //    .WithRequired(e => e.Organization)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.PaymentMethod)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentType>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.PaymentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupportRequest>()
                .HasMany(e => e.RequestResponses)
                .WithRequired(e => e.SupportRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupportRequest>()
                .HasMany(e => e.Resolutions)
                .WithRequired(e => e.SupportRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupportRequestStatu>()
                .HasMany(e => e.SupportRequests)
                .WithRequired(e => e.SupportRequestStatu)
                .HasForeignKey(e => e.RequestStatusId)
                .WillCascadeOnDelete(false);
        }
    }
}
