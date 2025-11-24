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

                // Define dummy events - ALL WITH FUTURE DATES (30 TOTAL EVENTS)
                var eventsToSeed = new List<Events>
                {
                    // ORIGINAL 5 EVENTS
                    
                    // Tech Conference by Bob
                    new Events
                    {
                        EventName = "Tech Innovation Summit 2025",
                        EventDescription = "Join industry leaders for cutting-edge tech discussions and networking.",
                        EventDetails = "Featuring keynote speakers, workshops, and tech demos. Lunch and refreshments provided.",
                        StartDateTime = DateTime.Now.AddDays(30),
                        EndDateTime = DateTime.Now.AddDays(30).AddHours(8),
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
                        StartDateTime = DateTime.Now.AddDays(25),
                        EndDateTime = DateTime.Now.AddDays(25).AddHours(2),
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
                        StartDateTime = DateTime.Now.AddDays(34),
                        EndDateTime = DateTime.Now.AddDays(34).AddHours(3),
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
                        StartDateTime = DateTime.Now.AddDays(21),
                        EndDateTime = DateTime.Now.AddDays(21).AddHours(4),
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
                        StartDateTime = DateTime.Now.AddDays(45),
                        EndDateTime = DateTime.Now.AddDays(45).AddHours(3),
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

                    // SECOND SET OF 10 EVENTS

                    // Music Festival by Alice
                    new Events
                    {
                        EventName = "Summer Music Festival 2025",
                        EventDescription = "Three-day outdoor music festival featuring local and national artists.",
                        EventDetails = "Food trucks, craft beer garden, and camping available. Rain or shine event.",
                        StartDateTime = DateTime.Now.AddDays(60),
                        EndDateTime = DateTime.Now.AddDays(62),
                        TimeZone = "PST",
                        IsAllDay = true,
                        VenueName = "Golden Gate Park",
                        Address = "501 Stanyan St",
                        City = "San Francisco",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "94117",
                        IsVirtual = false,
                        MaxAttendees = 5000,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = alice.Id,
                        ThemeId = 1
                    },

                    // Charity Run by Bob
                    new Events
                    {
                        EventName = "5K Charity Run for Education",
                        EventDescription = "Annual charity run to support local schools and educational programs.",
                        EventDetails = "All proceeds go to local schools. T-shirt included with registration.",
                        StartDateTime = DateTime.Now.AddDays(25),
                        EndDateTime = DateTime.Now.AddDays(25).AddHours(3),
                        TimeZone = "EST",
                        IsAllDay = false,
                        VenueName = "Central Park",
                        Address = "Central Park West",
                        City = "New York",
                        State = "NY",
                        Country = "United States",
                        PostalCode = "10024",
                        IsVirtual = false,
                        MaxAttendees = 1000,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = bob.Id,
                        ThemeId = 2
                    },

                    // Cooking Class by Carol
                    new Events
                    {
                        EventName = "Italian Cooking Masterclass",
                        EventDescription = "Learn to make authentic Italian pasta and sauces from a professional chef.",
                        EventDetails = "All ingredients and equipment provided. Wine pairing included.",
                        StartDateTime = DateTime.Now.AddDays(40),
                        EndDateTime = DateTime.Now.AddDays(40).AddHours(3),
                        TimeZone = "CST",
                        IsAllDay = false,
                        VenueName = "Culinary Institute Chicago",
                        Address = "225 West Jackson Blvd",
                        City = "Chicago",
                        State = "IL",
                        Country = "United States",
                        PostalCode = "60606",
                        IsVirtual = false,
                        MaxAttendees = 20,
                        IsPrivate = false,
                        RequiresApproval = true,
                        AllowGuestList = true,
                        CreatorId = carol.Id,
                        ThemeId = 3
                    },

                    // Art Gallery Opening by David
                    new Events
                    {
                        EventName = "Contemporary Art Gallery Opening",
                        EventDescription = "Opening night of new contemporary art exhibition featuring emerging artists.",
                        EventDetails = "Meet the artists, live music, wine and cheese reception.",
                        StartDateTime = DateTime.Now.AddDays(32),
                        EndDateTime = DateTime.Now.AddDays(32).AddHours(4),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Modern Art Gallery Seattle",
                        Address = "1400 1st Ave",
                        City = "Seattle",
                        State = "WA",
                        Country = "United States",
                        PostalCode = "98101",
                        IsVirtual = false,
                        MaxAttendees = 150,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = david.Id,
                        ThemeId = 1
                    },

                    // Virtual Gaming Tournament by Emma
                    new Events
                    {
                        EventName = "Virtual Gaming Championship 2025",
                        EventDescription = "Competitive gaming tournament with cash prizes for top players.",
                        EventDetails = "Open to all skill levels. Multiple game categories. Stream available.",
                        StartDateTime = DateTime.Now.AddDays(35),
                        EndDateTime = DateTime.Now.AddDays(35).AddHours(8),
                        TimeZone = "EST",
                        IsAllDay = false,
                        IsVirtual = true,
                        VirtualMeetingUrl = "https://twitch.tv/gamingchampionship",
                        MaxAttendees = 500,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = emma.Id,
                        ThemeId = 2
                    },

                    // Book Club Meetup by Alice
                    new Events
                    {
                        EventName = "Monthly Book Club Discussion",
                        EventDescription = "Join us for a discussion of this month's selected book and light refreshments.",
                        EventDetails = "This month: 'The Midnight Library' by Matt Haig. Coffee and pastries provided.",
                        StartDateTime = DateTime.Now.AddDays(28),
                        EndDateTime = DateTime.Now.AddDays(28).AddHours(2),
                        TimeZone = "EST",
                        IsAllDay = false,
                        VenueName = "Pages & Coffee Bookstore",
                        Address = "567 Newbury St",
                        City = "Boston",
                        State = "MA",
                        Country = "United States",
                        PostalCode = "02115",
                        IsVirtual = false,
                        MaxAttendees = 25,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = alice.Id,
                        ThemeId = 1
                    },

                    // Yoga Retreat by Carol
                    new Events
                    {
                        EventName = "Weekend Yoga & Wellness Retreat",
                        EventDescription = "Two-day retreat focused on yoga, meditation, and holistic wellness.",
                        EventDetails = "Accommodation, meals, and all yoga sessions included. Beginner-friendly.",
                        StartDateTime = DateTime.Now.AddDays(50),
                        EndDateTime = DateTime.Now.AddDays(52),
                        TimeZone = "MST",
                        IsAllDay = true,
                        VenueName = "Mountain View Wellness Center",
                        Address = "789 Scenic Drive",
                        City = "Boulder",
                        State = "CO",
                        Country = "United States",
                        PostalCode = "80302",
                        IsVirtual = false,
                        MaxAttendees = 40,
                        IsPrivate = false,
                        RequiresApproval = true,
                        AllowGuestList = true,
                        CreatorId = carol.Id,
                        ThemeId = 3
                    },

                    // Science Fair by Bob
                    new Events
                    {
                        EventName = "Regional Science & Innovation Fair",
                        EventDescription = "Annual science fair showcasing student projects and innovations.",
                        EventDetails = "Free admission. Prizes for top projects. Interactive demonstrations.",
                        StartDateTime = DateTime.Now.AddDays(40),
                        EndDateTime = DateTime.Now.AddDays(40).AddHours(6),
                        TimeZone = "EST",
                        IsAllDay = false,
                        VenueName = "Convention Center Boston",
                        Address = "415 Summer St",
                        City = "Boston",
                        State = "MA",
                        Country = "United States",
                        PostalCode = "02210",
                        IsVirtual = false,
                        MaxAttendees = 2000,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = bob.Id,
                        ThemeId = 2
                    },

                    // Film Screening by David
                    new Events
                    {
                        EventName = "Independent Film Festival Screening",
                        EventDescription = "Premiere screening of award-winning independent films followed by Q&A.",
                        EventDetails = "Meet the filmmakers. Popcorn and drinks available for purchase.",
                        StartDateTime = DateTime.Now.AddDays(28),
                        EndDateTime = DateTime.Now.AddDays(28).AddHours(4),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Historic Theater Los Angeles",
                        Address = "1332 Hollywood Blvd",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "90028",
                        IsVirtual = false,
                        MaxAttendees = 300,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = david.Id,
                        ThemeId = 1
                    },

                    // Career Fair by Emma
                    new Events
                    {
                        EventName = "Tech Career Fair 2025",
                        EventDescription = "Connect with top tech companies looking to hire talented professionals.",
                        EventDetails = "Bring resumes. Dress professionally. On-site interviews available.",
                        StartDateTime = DateTime.Now.AddDays(22),
                        EndDateTime = DateTime.Now.AddDays(22).AddHours(5),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Silicon Valley Conference Center",
                        Address = "2901 Tasman Dr",
                        City = "San Jose",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "95134",
                        IsVirtual = false,
                        MaxAttendees = 1000,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = emma.Id,
                        ThemeId = 2
                    },

                    // NEW 15 EVENTS ADDED BELOW

                    // Wine Tasting by Alice
                    new Events
                    {
                        EventName = "Napa Valley Wine Tasting Tour",
                        EventDescription = "Guided tour of three premium wineries with wine tasting and gourmet lunch.",
                        EventDetails = "Transportation provided. Must be 21+. Souvenir wine glass included.",
                        StartDateTime = DateTime.Now.AddDays(55),
                        EndDateTime = DateTime.Now.AddDays(55).AddHours(6),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Napa Valley Wine Country",
                        Address = "1234 Vineyard Lane",
                        City = "Napa",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "94558",
                        IsVirtual = false,
                        MaxAttendees = 50,
                        IsPrivate = false,
                        RequiresApproval = true,
                        AllowGuestList = true,
                        CreatorId = alice.Id,
                        ThemeId = 3
                    },

                    // Photography Workshop by Bob
                    new Events
                    {
                        EventName = "Urban Photography Workshop",
                        EventDescription = "Learn professional photography techniques while exploring the city.",
                        EventDetails = "Bring your camera. All skill levels welcome. Photo critique session included.",
                        StartDateTime = DateTime.Now.AddDays(38),
                        EndDateTime = DateTime.Now.AddDays(38).AddHours(4),
                        TimeZone = "EST",
                        IsAllDay = false,
                        VenueName = "Manhattan Photography Studio",
                        Address = "890 Broadway",
                        City = "New York",
                        State = "NY",
                        Country = "United States",
                        PostalCode = "10003",
                        IsVirtual = false,
                        MaxAttendees = 15,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = bob.Id,
                        ThemeId = 1
                    },

                    // Halloween Party by Carol
                    new Events
                    {
                        EventName = "Spooky Halloween Costume Party",
                        EventDescription = "Come dressed in your best costume for a night of fun, food, and prizes!",
                        EventDetails = "Costume contest with cash prizes. DJ, photo booth, and themed cocktails.",
                        StartDateTime = DateTime.Now.AddDays(90),
                        EndDateTime = DateTime.Now.AddDays(90).AddHours(5),
                        TimeZone = "CST",
                        IsAllDay = false,
                        VenueName = "Downtown Event Hall",
                        Address = "456 Party Street",
                        City = "Chicago",
                        State = "IL",
                        Country = "United States",
                        PostalCode = "60602",
                        IsVirtual = false,
                        MaxAttendees = 200,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = carol.Id,
                        ThemeId = 1
                    },

                    // Hiking Adventure by David
                    new Events
                    {
                        EventName = "Mountain Sunrise Hiking Adventure",
                        EventDescription = "Early morning guided hike to catch the sunrise from the mountain peak.",
                        EventDetails = "Moderate difficulty. Bring water and snacks. Coffee and breakfast at summit.",
                        StartDateTime = DateTime.Now.AddDays(20),
                        EndDateTime = DateTime.Now.AddDays(20).AddHours(4),
                        TimeZone = "MST",
                        IsAllDay = false,
                        VenueName = "Rocky Mountain Trailhead",
                        Address = "Mountain Road 15",
                        City = "Denver",
                        State = "CO",
                        Country = "United States",
                        PostalCode = "80202",
                        IsVirtual = false,
                        MaxAttendees = 30,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = david.Id,
                        ThemeId = 2
                    },

                    // Virtual Meditation by Emma
                    new Events
                    {
                        EventName = "Guided Meditation & Mindfulness Session",
                        EventDescription = "Virtual mindfulness meditation session for stress relief and mental clarity.",
                        EventDetails = "Suitable for beginners. Join from the comfort of your home.",
                        StartDateTime = DateTime.Now.AddDays(23),
                        EndDateTime = DateTime.Now.AddDays(23).AddHours(1),
                        TimeZone = "PST",
                        IsAllDay = false,
                        IsVirtual = true,
                        VirtualMeetingUrl = "https://zoom.us/j/meditation2025",
                        MaxAttendees = 100,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = emma.Id,
                        ThemeId = 3
                    },

                    // Comedy Night by Alice
                    new Events
                    {
                        EventName = "Stand-Up Comedy Night",
                        EventDescription = "Enjoy an evening of laughter with local and touring comedians.",
                        EventDetails = "21+ event. Two-drink minimum. Dinner menu available.",
                        StartDateTime = DateTime.Now.AddDays(33),
                        EndDateTime = DateTime.Now.AddDays(33).AddHours(3),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Laugh Factory Los Angeles",
                        Address = "8001 Sunset Blvd",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "90046",
                        IsVirtual = false,
                        MaxAttendees = 250,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = alice.Id,
                        ThemeId = 1
                    },

                    // Farmers Market by Bob
                    new Events
                    {
                        EventName = "Weekend Farmers Market & Craft Fair",
                        EventDescription = "Local farmers, artisans, and food vendors gather for a community market.",
                        EventDetails = "Fresh produce, handmade crafts, live music, and food trucks.",
                        StartDateTime = DateTime.Now.AddDays(26),
                        EndDateTime = DateTime.Now.AddDays(26).AddHours(5),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "City Center Plaza",
                        Address = "100 Main Street",
                        City = "Portland",
                        State = "OR",
                        Country = "United States",
                        PostalCode = "97204",
                        IsVirtual = false,
                        MaxAttendees = 1000,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = bob.Id,
                        ThemeId = 2
                    },

                    // Dance Class by Carol
                    new Events
                    {
                        EventName = "Salsa Dancing Beginners Workshop",
                        EventDescription = "Learn the basics of salsa dancing in a fun and friendly environment.",
                        EventDetails = "No partner needed. Comfortable shoes recommended. All ages welcome.",
                        StartDateTime = DateTime.Now.AddDays(36),
                        EndDateTime = DateTime.Now.AddDays(36).AddHours(2),
                        TimeZone = "EST",
                        IsAllDay = false,
                        VenueName = "Miami Dance Studio",
                        Address = "234 Ocean Drive",
                        City = "Miami",
                        State = "FL",
                        Country = "United States",
                        PostalCode = "33139",
                        IsVirtual = false,
                        MaxAttendees = 40,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = carol.Id,
                        ThemeId = 1
                    },

                    // Coding Bootcamp by David
                    new Events
                    {
                        EventName = "Introduction to Python Programming",
                        EventDescription = "One-day intensive coding workshop for absolute beginners.",
                        EventDetails = "Laptop required. Course materials provided. Certificate of completion.",
                        StartDateTime = DateTime.Now.AddDays(27),
                        EndDateTime = DateTime.Now.AddDays(27).AddHours(6),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Tech Learning Center",
                        Address = "567 Innovation Way",
                        City = "San Francisco",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "94107",
                        IsVirtual = false,
                        MaxAttendees = 25,
                        IsPrivate = false,
                        RequiresApproval = true,
                        AllowGuestList = true,
                        CreatorId = david.Id,
                        ThemeId = 2
                    },

                    // Food Festival by Emma
                    new Events
                    {
                        EventName = "International Food & Culture Festival",
                        EventDescription = "Taste dishes from around the world at this multicultural food celebration.",
                        EventDetails = "50+ food vendors, cultural performances, cooking demonstrations, and kids zone.",
                        StartDateTime = DateTime.Now.AddDays(70),
                        EndDateTime = DateTime.Now.AddDays(70).AddHours(8),
                        TimeZone = "EST",
                        IsAllDay = false,
                        VenueName = "Brooklyn Bridge Park",
                        Address = "334 Furman St",
                        City = "Brooklyn",
                        State = "NY",
                        Country = "United States",
                        PostalCode = "11201",
                        IsVirtual = false,
                        MaxAttendees = 3000,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = emma.Id,
                        ThemeId = 3
                    },

                    // Pet Adoption Event by Alice
                    new Events
                    {
                        EventName = "Pet Adoption Day & Animal Rescue Fair",
                        EventDescription = "Meet adoptable pets and learn about animal rescue organizations.",
                        EventDetails = "Free event. Bring the family. Pet supplies vendors and veterinary information.",
                        StartDateTime = DateTime.Now.AddDays(38),
                        EndDateTime = DateTime.Now.AddDays(38).AddHours(4),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Community Park",
                        Address = "789 Park Avenue",
                        City = "San Diego",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "92101",
                        IsVirtual = false,
                        MaxAttendees = 500,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = alice.Id,
                        ThemeId = 1
                    },

                    // Business Conference by Bob
                    new Events
                    {
                        EventName = "Women in Business Leadership Summit",
                        EventDescription = "Empowering women leaders with networking, workshops, and keynote speakers.",
                        EventDetails = "Continental breakfast and lunch included. CEU credits available.",
                        StartDateTime = DateTime.Now.AddDays(48),
                        EndDateTime = DateTime.Now.AddDays(48).AddHours(7),
                        TimeZone = "EST",
                        IsAllDay = false,
                        VenueName = "Boston Conference Center",
                        Address = "900 Boylston St",
                        City = "Boston",
                        State = "MA",
                        Country = "United States",
                        PostalCode = "02115",
                        IsVirtual = false,
                        MaxAttendees = 300,
                        IsPrivate = false,
                        RequiresApproval = true,
                        AllowGuestList = true,
                        CreatorId = bob.Id,
                        ThemeId = 2
                    },

                    // Pottery Class by Carol
                    new Events
                    {
                        EventName = "Pottery Making & Ceramics Workshop",
                        EventDescription = "Create your own ceramic pottery piece with guidance from expert instructors.",
                        EventDetails = "All materials provided. Pieces will be fired and ready for pickup in 2 weeks.",
                        StartDateTime = DateTime.Now.AddDays(24),
                        EndDateTime = DateTime.Now.AddDays(24).AddHours(3),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Arts & Crafts Studio",
                        Address = "456 Creative Lane",
                        City = "Seattle",
                        State = "WA",
                        Country = "United States",
                        PostalCode = "98104",
                        IsVirtual = false,
                        MaxAttendees = 12,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = carol.Id,
                        ThemeId = 1
                    },

                    // Beach Cleanup by David
                    new Events
                    {
                        EventName = "Community Beach Cleanup Day",
                        EventDescription = "Join us in keeping our beaches clean and protecting marine life.",
                        EventDetails = "Gloves and trash bags provided. Free T-shirt for participants. Refreshments after cleanup.",
                        StartDateTime = DateTime.Now.AddDays(31),
                        EndDateTime = DateTime.Now.AddDays(31).AddHours(3),
                        TimeZone = "PST",
                        IsAllDay = false,
                        VenueName = "Santa Monica Beach",
                        Address = "1550 Pacific Coast Hwy",
                        City = "Santa Monica",
                        State = "CA",
                        Country = "United States",
                        PostalCode = "90401",
                        IsVirtual = false,
                        MaxAttendees = 200,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = david.Id,
                        ThemeId = 2
                    },

                    // Trivia Night by Emma
                    new Events
                    {
                        EventName = "Pub Quiz & Trivia Championship",
                        EventDescription = "Test your knowledge in this fun team trivia competition with prizes!",
                        EventDetails = "Teams of 4-6 people. Food and drink specials. Grand prize: $500 gift card.",
                        StartDateTime = DateTime.Now.AddDays(29),
                        EndDateTime = DateTime.Now.AddDays(29).AddHours(3),
                        TimeZone = "CST",
                        IsAllDay = false,
                        VenueName = "The Irish Pub",
                        Address = "123 Main Street",
                        City = "Austin",
                        State = "TX",
                        Country = "United States",
                        PostalCode = "78701",
                        IsVirtual = false,
                        MaxAttendees = 100,
                        IsPrivate = false,
                        RequiresApproval = false,
                        AllowGuestList = true,
                        CreatorId = emma.Id,
                        ThemeId = 3
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

                // Now add attendees to events (abbreviated for brevity - add more as needed)
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
            var musicFest = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Summer Music Festival 2025");
            var charityRun = await context.Events.FirstOrDefaultAsync(e => e.EventName == "5K Charity Run for Education");
            var cookingClass = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Italian Cooking Masterclass");
            var comedyNight = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Stand-Up Comedy Night");
            var foodFestival = await context.Events.FirstOrDefaultAsync(e => e.EventName == "International Food & Culture Festival");

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

            // Music Festival
            if (musicFest != null)
            {
                await AddAttendeeIfNotExists(context, musicFest.EventId, alice.Id, "organizer");
                await AddAttendeeIfNotExists(context, musicFest.EventId, bob.Id, "accepted");
                await AddAttendeeIfNotExists(context, musicFest.EventId, emma.Id, "accepted");
            }

            // Charity Run
            if (charityRun != null)
            {
                await AddAttendeeIfNotExists(context, charityRun.EventId, bob.Id, "organizer");
                await AddAttendeeIfNotExists(context, charityRun.EventId, alice.Id, "accepted");
                await AddAttendeeIfNotExists(context, charityRun.EventId, carol.Id, "accepted");
                await AddAttendeeIfNotExists(context, charityRun.EventId, david.Id, "accepted");
            }

            // Cooking Class
            if (cookingClass != null)
            {
                await AddAttendeeIfNotExists(context, cookingClass.EventId, carol.Id, "organizer");
                await AddAttendeeIfNotExists(context, cookingClass.EventId, emma.Id, "accepted");
                await AddAttendeeIfNotExists(context, cookingClass.EventId, alice.Id, "pending");
            }

            // Comedy Night
            if (comedyNight != null)
            {
                await AddAttendeeIfNotExists(context, comedyNight.EventId, alice.Id, "organizer");
                await AddAttendeeIfNotExists(context, comedyNight.EventId, bob.Id, "accepted");
                await AddAttendeeIfNotExists(context, comedyNight.EventId, david.Id, "accepted");
            }

            // Food Festival
            if (foodFestival != null)
            {
                await AddAttendeeIfNotExists(context, foodFestival.EventId, emma.Id, "organizer");
                await AddAttendeeIfNotExists(context, foodFestival.EventId, alice.Id, "accepted");
                await AddAttendeeIfNotExists(context, foodFestival.EventId, carol.Id, "accepted");
                await AddAttendeeIfNotExists(context, foodFestival.EventId, bob.Id, "accepted");
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