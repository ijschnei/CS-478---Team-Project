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
        public DbSet<EventGroupMessages> EventGroupMessages { get; set; }
        public DbSet<EventGroupMessageReads> EventGroupMessageReads { get; set; }
        public DbSet<UserProfiles> UserProfiles { get; set; }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<VenueTimeSlot> VenueTimeSlots { get; set; }

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

            modelBuilder.Entity<Events>()
                .HasOne(e => e.Venue)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Events>()
                .HasOne(e => e.VenueTimeSlot)
                .WithOne(ts => ts.BookedEvent)
                .HasForeignKey<Events>(e => e.VenueTimeSlotId)
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
                .WithMany(e => e.CustomFields)
                .HasForeignKey(ecf => ecf.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            //EventCategoryMapping relationships
            modelBuilder.Entity<EventCategoryMapping>()
                .HasOne(ecm => ecm.Event)
                .WithMany(e => e.Categories)
                .HasForeignKey(ecm => ecm.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventCategoryMapping>()
                .HasOne(ecm => ecm.Category)
                .WithMany(c => c.EventMappings)
                .HasForeignKey(ecm => ecm.CategoryId)
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

            //EventGroupMessages configuration
            modelBuilder.Entity<EventGroupMessages>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Event)
                    .WithMany()
                    .HasForeignKey(e => e.EventId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Sender)
                    .WithMany()
                    .HasForeignKey(e => e.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.EventId, e.SentAt });
                entity.HasIndex(e => e.IsPinned);
            });

            //EventGroupMessageReads configuration
            modelBuilder.Entity<EventGroupMessageReads>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Message)
                    .WithMany(m => m.MessageReads)
                    .HasForeignKey(e => e.MessageId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.MessageId, e.UserId })
                    .IsUnique();
            });

            //UserProfiles relationships
            modelBuilder.Entity<UserProfiles>()
                .HasOne(up => up.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfiles>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VenueTimeSlot>()
                .HasOne(ts => ts.Venue)
                .WithMany(v => v.TimeSlots)
                .HasForeignKey(ts => ts.VenueId)
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

            modelBuilder.Entity<VenueTimeSlot>()
                .HasIndex(ts => new { ts.VenueId, ts.SlotDate, ts.IsAvailable });

            modelBuilder.Entity<VenueTimeSlot>()
                .HasIndex(ts => ts.BookedEventId);

            //Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // ---- STATIC TIMESTAMPS ----
            var staticNow = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            // ---- EVENT CATEGORIES ----
            modelBuilder.Entity<EventCategory>().HasData(
                new EventCategory { Id = 1, Name = "Business", Description = "Corporate and professional events", ColorCode = "#1f77b4" },
                new EventCategory { Id = 2, Name = "Social", Description = "Social gatherings and parties", ColorCode = "#ff7f0e" },
                new EventCategory { Id = 3, Name = "Educational", Description = "Learning and training events", ColorCode = "#2ca02c" },
                new EventCategory { Id = 4, Name = "Sports", Description = "Athletic and fitness events", ColorCode = "#d62728" },
                new EventCategory { Id = 5, Name = "Entertainment", Description = "Shows, concerts, and entertainment", ColorCode = "#9467bd" }
            );

            // ---- THEMES ----
            modelBuilder.Entity<EventTheme>().HasData(
                new EventTheme { Id = 1, Name = "Classic", Description = "Clean and professional theme", IsActive = true },
                new EventTheme { Id = 2, Name = "Modern", Description = "Contemporary design with bold colors", IsActive = true },
                new EventTheme { Id = 3, Name = "Elegant", Description = "Sophisticated and refined styling", IsPremium = true, IsActive = true }
            );

            // ---- VENUES ----
            modelBuilder.Entity<Venue>().HasData(
                new Venue
                {
                    VenueId = 1,
                    VenueName = "Downtown Plaza",
                    VenueType = "Plaza",
                    Capacity = 500,
                    Address = "123 Main Street",
                    City = "Downtown",
                    State = "State",
                    PostalCode = "12345",
                    Country = "USA",
                    Amenities = "Stage, Sound System, Lighting, Parking",
                    Description = "Beautiful outdoor plaza in the heart of downtown",
                    IsActive = true,
                    CreatedAt = staticNow,
                    UpdatedAt = staticNow
                },
                new Venue
                {
                    VenueId = 2,
                    VenueName = "Riverside Park",
                    VenueType = "Park",
                    Capacity = 1000,
                    Address = "456 River Road",
                    City = "Eastside",
                    State = "State",
                    PostalCode = "12346",
                    Country = "USA",
                    Amenities = "Open Space, Pavilion, Restrooms, Playground",
                    Description = "Spacious park along the riverside",
                    IsActive = true,
                    CreatedAt = staticNow,
                    UpdatedAt = staticNow
                },
                new Venue
                {
                    VenueId = 3,
                    VenueName = "Central Community Hall",
                    VenueType = "Indoor Hall",
                    Capacity = 300,
                    Address = "789 Community Ave",
                    City = "Midtown",
                    State = "State",
                    PostalCode = "12347",
                    Country = "USA",
                    Amenities = "A/C, Kitchen, Tables & Chairs, WiFi",
                    Description = "Climate-controlled community hall",
                    IsActive = true,
                    CreatedAt = staticNow,
                    UpdatedAt = staticNow
                },
                new Venue
                {
                    VenueId = 4,
                    VenueName = "Lakeside Amphitheater",
                    VenueType = "Amphitheater",
                    Capacity = 2000,
                    Address = "321 Lakefront Dr",
                    City = "Northside",
                    State = "State",
                    PostalCode = "12348",
                    Country = "USA",
                    Amenities = "Stage, Professional Sound, Lighting, Seating",
                    Description = "Premier entertainment venue by the lake",
                    IsActive = true,
                    CreatedAt = staticNow,
                    UpdatedAt = staticNow
                },
                new Venue
                {
                    VenueId = 5,
                    VenueName = "Heritage Square",
                    VenueType = "Plaza",
                    Capacity = 750,
                    Address = "555 Heritage Blvd",
                    City = "Historic District",
                    State = "State",
                    PostalCode = "12349",
                    Country = "USA",
                    Amenities = "Gazebo, Fountain, Benches, Street Access",
                    Description = "Historic plaza in the city center",
                    IsActive = true,
                    CreatedAt = staticNow,
                    UpdatedAt = staticNow
                }
            );

            // ---- STATIC TIME SLOTS ----
            var startDate = new DateTime(2025, 01, 01);
            var timeSlots = new List<VenueTimeSlot>();
            int slotId = 1;

            for (int venueId = 1; venueId <= 5; venueId++)
            {
                for (int dayOffset = 0; dayOffset < 30; dayOffset++)
                {
                    var date = startDate.AddDays(dayOffset);

                    timeSlots.Add(new VenueTimeSlot
                    {
                        TimeSlotId = slotId++,
                        VenueId = venueId,
                        SlotDate = date,
                        StartTime = new TimeSpan(9, 0, 0),
                        EndTime = new TimeSpan(12, 0, 0),
                        IsAvailable = true,
                        CreatedAt = staticNow,
                        UpdatedAt = staticNow
                    });

                    timeSlots.Add(new VenueTimeSlot
                    {
                        TimeSlotId = slotId++,
                        VenueId = venueId,
                        SlotDate = date,
                        StartTime = new TimeSpan(13, 0, 0),
                        EndTime = new TimeSpan(17, 0, 0),
                        IsAvailable = true,
                        CreatedAt = staticNow,
                        UpdatedAt = staticNow
                    });

                    timeSlots.Add(new VenueTimeSlot
                    {
                        TimeSlotId = slotId++,
                        VenueId = venueId,
                        SlotDate = date,
                        StartTime = new TimeSpan(18, 0, 0),
                        EndTime = new TimeSpan(22, 0, 0),
                        IsAvailable = true,
                        CreatedAt = staticNow,
                        UpdatedAt = staticNow
                    });
                }
            }

            modelBuilder.Entity<VenueTimeSlot>().HasData(timeSlots);
        }

    }
}