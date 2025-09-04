using CS478_EventPlannerProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CS478_EventPlannerProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //DbSets for models
        public DbSet<Events> Events { get; set; }
        public DbSet<EventAttendees> EventAttendees { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventCategoryMapping> EventCategoryMappings { get; set; }
        public DbSet<EventTheme> EventThemes { get; set; }
        public DbSet<EventCustomFields> EventCustomFields { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<UserProfiles> UserProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //configure composite key for EventCategoryMapping
            modelBuilder.Entity<EventCategoryMapping>()
                .HasKey(ecm => new { ecm.EventId, ecm.CategoryId });

            //configure relationships and constrains

            //Events relationships
            modelBuilder.Entity<Events>()
                .HasOne(e => e.Creator)
                .WithMany(u => u.CreatedEvents)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Events>()
                .HasOne(e => e.Theme)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.ThemeId)
                .OnDelete(DeleteBehavior.SetNull);

            //EventAttendees relationships
            modelBuilder.Entity<EventAttendees>()
                .HasOne(ea => ea.Event)
                .WithMany(e => e.Attendees)
                .HasForeignKey(ea => ea.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventAttendees>()
                .HasOne(ea => ea.User)
                .WithMany(u => u.EventAttendances)
                .HasForeignKey(ea => ea.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //EventCustomFields relationships
            modelBuilder.Entity<EventCustomFields>()
                .HasOne(ecf => ecf.Event)
                .WithMany(e=>e.CustomFields)
                .HasForeignKey(ecf=>ecf.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            //EventCategoryMapping relationships
            modelBuilder.Entity<EventCategoryMapping>()
                .HasOne(ecm => ecm.Event)
                .WithMany(e => e.Categories)
                .HasForeignKey(ecm => ecm.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventCategoryMapping>()
                .HasOne(ecm =>ecm.Category)
                .WithMany(c=>c.EventMappings)
                .HasForeignKey(ecm=>ecm.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            //Messages relationships
            modelBuilder.Entity<Messages>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Messages>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Messages>()
                .HasOne(m => m.RelatedEvent)
                .WithMany(e => e.RelatedMessages)
                .HasForeignKey(m => m.RelatedEventId)
                .OnDelete(DeleteBehavior.SetNull);

            //UserProfiles relationships
            modelBuilder.Entity<UserProfiles>()
                .HasOne(up => up.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfiles>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Indexes for performance
            modelBuilder.Entity<Events>()
                .HasIndex(e => e.StartDateTime);

            modelBuilder.Entity<Events>()
                .HasIndex(e => e.CreatorId);

            modelBuilder.Entity<EventAttendees>()
                .HasIndex(ea => new { ea.EventId, ea.Status });

            modelBuilder.Entity<Messages>()
                .HasIndex(m => m.ConversationId);

            modelBuilder.Entity<Messages>()
                .HasIndex(m => new { m.ReceiverId, m.IsRead });

            //Seed data
            SeedData(modelBuilder);

        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            //seed default event categories
            modelBuilder.Entity<EventCategory>().HasData(
                new EventCategory { Id = 1, Name = "Business", Description = "Corporate and professional events", ColorCode = "#1f77b4" },
                new EventCategory { Id = 2, Name = "Social", Description = "Social gatherings and parties", ColorCode = "#ff7f0e" },
                new EventCategory { Id = 3, Name = "Educational", Description = "Learning and training events", ColorCode = "#2ca02c" },
                new EventCategory { Id = 4, Name = "Sports", Description = "Athletic and fitness events", ColorCode = "#d62728" },
                new EventCategory { Id = 5, Name = "Entertainment", Description = "Shows, concerts, and entertainment", ColorCode = "#9467bd" }
                );

            //seed default themes
            modelBuilder.Entity<EventTheme>().HasData(
                new EventTheme { Id = 1, Name = "Classic", Description = "Clean and professional theme", IsActive = true },
                new EventTheme { Id = 2, Name = "Modern", Description = "Contemporary design with bold colors", IsActive = true },
                new EventTheme { Id = 3, Name = "Elegant", Description = "Sophisticated and refined styling", IsPremium = true, IsActive = true }
                );
        }

    }
}
