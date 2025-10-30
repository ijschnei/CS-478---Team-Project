using CS478_EventPlannerProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CS478_EventPlannerProject.Data
{
    public static class EventSeeder
    {
        public static async Task SeedEventsAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();

                // Get existing users (seeded by UserSeeder)
                var alice = await userManager.FindByEmailAsync("alice.smith@example.com");
                var bob = await userManager.FindByEmailAsync("bob.jones@example.com");
                var carol = await userManager.FindByEmailAsync("carol.williams@example.com");
                var david = await userManager.FindByEmailAsync("david.brown@example.com");
                var emma = await userManager.FindByEmailAsync("emma.davis@example.com");

                if (alice == null || bob == null || carol == null || david == null || emma == null)
                {
                    Console.WriteLine("Users not found. Please ensure UserSeeder has run first.");
                    return;
                }

                // Define dummy events
                var eventsToSeed = new List<Events>
                {
                    // Tech Conference by Bob
                    new Events
                    {
                        EventName = "Tech Innovation Summit 2025",
                        EventDescription = "Join industry leaders for cutting-edge tech discussions and networking.",
                        EventDetails = "Featuring keynote speakers, workshops, and tech demos. Lunch and refreshments provided.",
                        StartDateTime = DateTime.UtcNow.AddDays(30),
                        EndDateTime = DateTime.UtcNow.AddDays(30).AddHours(8),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "San Francisco Convention Center",
                        Address = "747 Howard St",
                        City = "San Francisco",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "94103",
                        IsVirtual = false,
                        MaxAttendees = 500,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = bob.Id,
                        ThemeId = 2
                    },

                    // Virtual Webinar by Alice
                    new Events
                    {
                        EventName = "Event Planning Masterclass",
                        EventDescription = "Learn the secrets of successful event planning from industry experts.",
                        EventDetails = "Interactive online workshop with Q&A session. Recording will be available.",
                        StartDateTime = DateTime.UtcNow.AddDays(7),
                        EndDateTime = DateTime.UtcNow.AddDays(7).AddHours(2),
                        TimeZone = "EST",
                        IsAllDay = false,
                        IsVirtual = true,
                        VirtualMeetingUrl = "https://zoom.us/j/123456789",
                        MaxAttendees = 100,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = alice.Id,
                        ThemeId = 1
                    },

                    // Networking Event by Carol
                    new Events
                    {
                        EventName = "Professional Networking Mixer",
                        EventDescription = "Connect with professionals from various industries in a relaxed setting.",
                        EventDetails = "Complimentary appetizers and drinks. Business casual attire.",
                        StartDateTime = DateTime.UtcNow.AddDays(14),
                        EndDateTime = DateTime.UtcNow.AddDays(14).AddHours(3),
                        TimeZone = "CST",
                        IsAllDay = false,
                        VenueName = "The Grand Hotel Chicago",
                        Address = "123 Michigan Ave",
                        City = "Chicago",
                        State = "IL",
                        Country = "United States",
                        PostalCode = "60601",
                        IsVirtual = false,
                        MaxAttendees = 75,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = carol.Id,
                        ThemeId = 3
                    },

                    // Community Event by David (Private)
                    new Events
                    {
                        EventName = "Private Community Gathering",
                        EventDescription = "Exclusive meetup for community leaders and organizers.",
                        EventDetails = "RSVP required. Agenda will be shared with confirmed attendees.",
                        StartDateTime = DateTime.UtcNow.AddDays(21),
                        EndDateTime = DateTime.UtcNow.AddDays(21).AddHours(4),
                        TimeZone = "CST",
                        IsAllDay = false,
                        VenueName = "Austin Community Center",
                        Address = "456 Congress Ave",
                        City = "Austin",
                        State = "TX",
                        Country = "United States",
                        PostalCode = "78701",
                        IsVirtual = false,
                        MaxAttendees = 30,
                        IsPrivate = true,
                        RequiresApproval = true,
                        AllowGuestList = false,
                        CreatorId = david.Id,
                        ThemeId = 1
                    },

                    // Startup Pitch Event by Emma
                    new Events
                    {
                        EventName = "Startup Pitch Night",
                        EventDescription = "Watch innovative startups pitch their ideas to investors.",
                        EventDetails = "5 startups presenting, followed by networking reception.",
                        StartDateTime = DateTime.UtcNow.AddDays(45),
                        EndDateTime = DateTime.UtcNow.AddDays(45).AddHours(3),
                        TimeZone = "EST",
                        IsAllDay = false,
                        VenueName = "Innovation Hub Boston",
                        Address = "789 Boylston St",
                        City = "Boston",
                        State = "MA",
                        Country = "United States",
                        PostalCode = "02199",
                        IsVirtual = false,
                        MaxAttendees = 200,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = emma.Id,
                        ThemeId = 2
                    },

                    // Past Event for testing
                    new Events
                    {
                        EventName = "Annual Tech Meetup 2024",
                        EventDescription = "Last year's successful tech meetup.",
                        StartDateTime = DateTime.UtcNow.AddDays(-30),
                        EndDateTime = DateTime.UtcNow.AddDays(-30).AddHours(5),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Tech Hub SF",
                        Address = "100 Market St",
                        City = "San Francisco",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "94105",
                        IsVirtual = false,
                        MaxAttendees = 150,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = bob.Id,
                        ThemeId = 1
                    }
                };

                // Add events if they don't exist
                foreach (var eventItem in eventsToSeed)
                {
                    var exists = await context.Events.AnyAsync(e => e.EventName == eventItem.EventName);
                    if (!exists)
                    {
                        context.Events.Add(eventItem);
                    }
                }

                await context.SaveChangesAsync();

                // Now add attendees to events
                await SeedEventAttendeesAsync(context, alice, bob, carol, david, emma);
            }
        }

        private static async Task SeedEventAttendeesAsync(
            ApplicationDbContext context,
            Users alice, Users bob, Users carol, Users david, Users emma)
        {
            // Get the events
            var techSummit = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Tech Innovation Summit 2025");
            var masterclass = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Event Planning Masterclass");
            var networking = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Professional Networking Mixer");
            var privateMeeting = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Private Community Gathering");
            var pitchNight = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Startup Pitch Night");
            var pastEvent = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Annual Tech Meetup 2024");

            // Tech Summit - Multiple attendees
            if (techSummit != null)
            {
                await AddAttendeeIfNotExists(context, techSummit.EventId, bob.Id, "organizer");
                await AddAttendeeIfNotExists(context, techSummit.EventId, alice.Id, "accepted");
                await AddAttendeeIfNotExists(context, techSummit.EventId, carol.Id, "accepted");
                await AddAttendeeIfNotExists(context, techSummit.EventId, emma.Id, "accepted");
            }

            // Masterclass - Virtual event
            if (masterclass != null)
            {
                await AddAttendeeIfNotExists(context, masterclass.EventId, alice.Id, "organizer");
                await AddAttendeeIfNotExists(context, masterclass.EventId, bob.Id, "accepted");
                await AddAttendeeIfNotExists(context, masterclass.EventId, carol.Id, "accepted");
                await AddAttendeeIfNotExists(context, masterclass.EventId, david.Id, "pending");
            }

            // Networking Event
            if (networking != null)
            {
                await AddAttendeeIfNotExists(context, networking.EventId, carol.Id, "organizer");
                await AddAttendeeIfNotExists(context, networking.EventId, alice.Id, "accepted");
                await AddAttendeeIfNotExists(context, networking.EventId, emma.Id, "accepted");
            }

            // Private Meeting - Requires approval
            if (privateMeeting != null)
            {
                await AddAttendeeIfNotExists(context, privateMeeting.EventId, david.Id, "organizer");
                await AddAttendeeIfNotExists(context, privateMeeting.EventId, carol.Id, "accepted");
                await AddAttendeeIfNotExists(context, privateMeeting.EventId, alice.Id, "pending");
            }

            // Pitch Night
            if (pitchNight != null)
            {
                await AddAttendeeIfNotExists(context, pitchNight.EventId, emma.Id, "organizer");
                await AddAttendeeIfNotExists(context, pitchNight.EventId, bob.Id, "accepted");
                await AddAttendeeIfNotExists(context, pitchNight.EventId, alice.Id, "accepted");
                await AddAttendeeIfNotExists(context, pitchNight.EventId, carol.Id, "accepted");
            }

            // Past Event
            if (pastEvent != null)
            {
                await AddAttendeeIfNotExists(context, pastEvent.EventId, bob.Id, "organizer");
                await AddAttendeeIfNotExists(context, pastEvent.EventId, alice.Id, "accepted");
                await AddAttendeeIfNotExists(context, pastEvent.EventId, carol.Id, "accepted");
                await AddAttendeeIfNotExists(context, pastEvent.EventId, emma.Id, "accepted");
            }


            await context.SaveChangesAsync();
        }

        private static async Task AddAttendeeIfNotExists(ApplicationDbContext context, int eventId, string userId, string status)
        {
            var exists = await context.EventAttendees
                .AnyAsync(ea => ea.EventId == eventId && ea.UserId == userId);

            if (!exists)
            {
                var attendee = new EventAttendees
                {
                    EventId = eventId,
                    UserId = userId,
                    Status = status,
                    AttendeeType = status == "organizer" ? "organizer" : "attendee",
                    RSVP_Date = DateTime.UtcNow,
                    CheckedIn = false
                };
                context.EventAttendees.Add(attendee);
            }
        }
    }
}