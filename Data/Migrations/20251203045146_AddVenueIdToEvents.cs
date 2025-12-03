using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CS478_EventPlannerProject.Migrations
{
    /// <inheritdoc />
    public partial class AddVenueIdToEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserProfiles",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "UserProfiles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                table: "Messages",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReadAt",
                table: "Messages",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EventThemes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Events",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateTime",
                table: "Events",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "Events",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Events",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BannerImageUrl",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VenueId",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VenueTimeSlotId",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                table: "EventGroupMessages",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReadAt",
                table: "EventGroupMessageReads",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RSVP_Date",
                table: "EventAttendees",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInTime",
                table: "EventAttendees",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginAt",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VenueName = table.Column<string>(type: "text", nullable: false),
                    VenueType = table.Column<string>(type: "text", nullable: true),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Amenities = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "VenueTimeSlots",
                columns: table => new
                {
                    TimeSlotId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VenueId = table.Column<int>(type: "integer", nullable: false),
                    SlotDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    BookedEventId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueTimeSlots", x => x.TimeSlotId);
                    table.ForeignKey(
                        name: "FK_VenueTimeSlots_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "EventThemes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 4, 51, 44, 759, DateTimeKind.Utc).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "EventThemes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 4, 51, 44, 759, DateTimeKind.Utc).AddTicks(9012));

            migrationBuilder.UpdateData(
                table: "EventThemes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 4, 51, 44, 759, DateTimeKind.Utc).AddTicks(9015));

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueId", "Address", "Amenities", "Capacity", "City", "Country", "CreatedAt", "Description", "IsActive", "PostalCode", "State", "UpdatedAt", "VenueName", "VenueType" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "Stage, Sound System, Lighting, Parking", 500, "Downtown", "USA", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(1649), "Beautiful outdoor plaza in the heart of downtown", true, "12345", "State", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(1798), "Downtown Plaza", "Plaza" },
                    { 2, "456 River Road", "Open Space, Pavilion, Restrooms, Playground", 1000, "Eastside", "USA", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(1988), "Spacious park along the riverside", true, "12346", "State", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(1989), "Riverside Park", "Park" },
                    { 3, "789 Community Ave", "A/C, Kitchen, Tables & Chairs, WiFi", 300, "Midtown", "USA", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(1995), "Climate-controlled community hall", true, "12347", "State", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(1996), "Central Community Hall", "Indoor Hall" },
                    { 4, "321 Lakefront Dr", "Stage, Professional Sound, Lighting, Seating", 2000, "Northside", "USA", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(2000), "Premier entertainment venue by the lake", true, "12348", "State", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(2001), "Lakeside Amphitheater", "Amphitheater" },
                    { 5, "555 Heritage Blvd", "Gazebo, Fountain, Benches, Street Access", 750, "Historic District", "USA", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(2005), "Historic plaza in the city center", true, "12349", "State", new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(2005), "Heritage Square", "Plaza" }
                });

            migrationBuilder.InsertData(
                table: "VenueTimeSlots",
                columns: new[] { "TimeSlotId", "BookedEventId", "CreatedAt", "EndTime", "IsAvailable", "SlotDate", "StartTime", "UpdatedAt", "VenueId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3473), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3587), 1 },
                    { 2, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3713), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3714), 1 },
                    { 3, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3718), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3719), 1 },
                    { 4, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3723), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3723), 1 },
                    { 5, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3725), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3725), 1 },
                    { 6, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3729), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3730), 1 },
                    { 7, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3731), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3732), 1 },
                    { 8, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3733), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3734), 1 },
                    { 9, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3735), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3736), 1 },
                    { 10, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3739), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3739), 1 },
                    { 11, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3741), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3741), 1 },
                    { 12, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3743), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3743), 1 },
                    { 13, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3745), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3745), 1 },
                    { 14, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3747), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3747), 1 },
                    { 15, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3749), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3749), 1 },
                    { 16, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3750), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3751), 1 },
                    { 17, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3752), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3753), 1 },
                    { 18, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3755), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3756), 1 },
                    { 19, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3757), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3758), 1 },
                    { 20, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3759), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3759), 1 },
                    { 21, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3761), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3761), 1 },
                    { 22, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3762), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3763), 1 },
                    { 23, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3764), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3764), 1 },
                    { 24, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3766), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3766), 1 },
                    { 25, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3767), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3768), 1 },
                    { 26, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3769), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3769), 1 },
                    { 27, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3771), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3771), 1 },
                    { 28, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3773), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3773), 1 },
                    { 29, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3774), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3775), 1 },
                    { 30, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3776), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3776), 1 },
                    { 31, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3778), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3778), 1 },
                    { 32, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3779), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3780), 1 },
                    { 33, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3781), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3781), 1 },
                    { 34, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3784), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3784), 1 },
                    { 35, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3786), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3786), 1 },
                    { 36, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3787), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3788), 1 },
                    { 37, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3789), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3789), 1 },
                    { 38, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3791), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3791), 1 },
                    { 39, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3792), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3793), 1 },
                    { 40, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3794), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3794), 1 },
                    { 41, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3796), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3796), 1 },
                    { 42, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3797), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3798), 1 },
                    { 43, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3799), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3799), 1 },
                    { 44, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3801), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3801), 1 },
                    { 45, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3802), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3803), 1 },
                    { 46, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3804), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3804), 1 },
                    { 47, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3806), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3806), 1 },
                    { 48, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3807), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3808), 1 },
                    { 49, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3809), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3809), 1 },
                    { 50, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3811), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3811), 1 },
                    { 51, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3812), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3813), 1 },
                    { 52, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3814), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3814), 1 },
                    { 53, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3816), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3816), 1 },
                    { 54, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3817), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3818), 1 },
                    { 55, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3819), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3820), 1 },
                    { 56, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3821), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3821), 1 },
                    { 57, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3822), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3823), 1 },
                    { 58, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3824), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3825), 1 },
                    { 59, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3826), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3826), 1 },
                    { 60, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3828), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3828), 1 },
                    { 61, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3829), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3830), 1 },
                    { 62, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3831), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3831), 1 },
                    { 63, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3833), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3833), 1 },
                    { 64, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3834), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3835), 1 },
                    { 65, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3882), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3882), 1 },
                    { 66, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3884), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3885), 1 },
                    { 67, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3886), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3887), 1 },
                    { 68, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3888), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3889), 1 },
                    { 69, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3890), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3891), 1 },
                    { 70, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3892), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3892), 1 },
                    { 71, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3894), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3894), 1 },
                    { 72, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3896), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3896), 1 },
                    { 73, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3897), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3898), 1 },
                    { 74, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3899), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3899), 1 },
                    { 75, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3901), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3901), 1 },
                    { 76, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3903), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3903), 1 },
                    { 77, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3904), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3905), 1 },
                    { 78, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3906), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3906), 1 },
                    { 79, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3908), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3908), 1 },
                    { 80, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3910), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3910), 1 },
                    { 81, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3911), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3912), 1 },
                    { 82, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3913), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3913), 1 },
                    { 83, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3915), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3915), 1 },
                    { 84, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3916), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3917), 1 },
                    { 85, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3918), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3919), 1 },
                    { 86, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3920), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3920), 1 },
                    { 87, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3922), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3922), 1 },
                    { 88, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3923), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3924), 1 },
                    { 89, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3925), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3925), 1 },
                    { 90, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3927), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3927), 1 },
                    { 91, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3929), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3929), 2 },
                    { 92, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3931), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3931), 2 },
                    { 93, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3932), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3933), 2 },
                    { 94, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3934), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3935), 2 },
                    { 95, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3936), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3936), 2 },
                    { 96, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3938), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3938), 2 },
                    { 97, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3939), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3940), 2 },
                    { 98, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3941), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3941), 2 },
                    { 99, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3943), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3943), 2 },
                    { 100, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3944), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3945), 2 },
                    { 101, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3946), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3947), 2 },
                    { 102, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3948), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3948), 2 },
                    { 103, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3950), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3950), 2 },
                    { 104, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3951), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3952), 2 },
                    { 105, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3953), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3953), 2 },
                    { 106, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3955), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3955), 2 },
                    { 107, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3956), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3957), 2 },
                    { 108, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3958), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3958), 2 },
                    { 109, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3960), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3960), 2 },
                    { 110, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3961), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3962), 2 },
                    { 111, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3963), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3964), 2 },
                    { 112, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3965), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3965), 2 },
                    { 113, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3967), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3967), 2 },
                    { 114, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3968), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3969), 2 },
                    { 115, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3970), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3970), 2 },
                    { 116, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3972), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3972), 2 },
                    { 117, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3973), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3974), 2 },
                    { 118, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3975), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3976), 2 },
                    { 119, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3977), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3977), 2 },
                    { 120, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3978), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3979), 2 },
                    { 121, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3980), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3981), 2 },
                    { 122, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3982), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3982), 2 },
                    { 123, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3984), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3984), 2 },
                    { 124, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3985), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3986), 2 },
                    { 125, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3987), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3987), 2 },
                    { 126, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3989), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3989), 2 },
                    { 127, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3990), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3991), 2 },
                    { 128, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3992), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3992), 2 },
                    { 129, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3994), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(3994), 2 },
                    { 130, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4022), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4022), 2 },
                    { 131, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4024), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4024), 2 },
                    { 132, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4026), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4026), 2 },
                    { 133, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4028), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4028), 2 },
                    { 134, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4030), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4030), 2 },
                    { 135, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4032), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4032), 2 },
                    { 136, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4033), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4034), 2 },
                    { 137, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4035), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4036), 2 },
                    { 138, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4037), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4037), 2 },
                    { 139, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4039), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4039), 2 },
                    { 140, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4040), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4041), 2 },
                    { 141, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4042), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4042), 2 },
                    { 142, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4044), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4044), 2 },
                    { 143, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4046), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4046), 2 },
                    { 144, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4047), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4048), 2 },
                    { 145, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4049), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4049), 2 },
                    { 146, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4051), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4051), 2 },
                    { 147, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4052), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4053), 2 },
                    { 148, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4054), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4055), 2 },
                    { 149, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4056), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4056), 2 },
                    { 150, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4058), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4058), 2 },
                    { 151, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4059), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4060), 2 },
                    { 152, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4061), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4062), 2 },
                    { 153, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4063), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4063), 2 },
                    { 154, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4065), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4065), 2 },
                    { 155, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4066), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4067), 2 },
                    { 156, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4068), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4069), 2 },
                    { 157, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4070), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4070), 2 },
                    { 158, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4072), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4072), 2 },
                    { 159, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4073), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4074), 2 },
                    { 160, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4075), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4075), 2 },
                    { 161, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4077), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4077), 2 },
                    { 162, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4078), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4079), 2 },
                    { 163, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4080), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4081), 2 },
                    { 164, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4082), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4082), 2 },
                    { 165, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4084), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4084), 2 },
                    { 166, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4085), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4086), 2 },
                    { 167, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4087), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4087), 2 },
                    { 168, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4089), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4089), 2 },
                    { 169, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4090), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4091), 2 },
                    { 170, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4092), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4092), 2 },
                    { 171, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4094), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4094), 2 },
                    { 172, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4095), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4096), 2 },
                    { 173, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4097), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4098), 2 },
                    { 174, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4099), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4099), 2 },
                    { 175, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4101), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4101), 2 },
                    { 176, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4102), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4103), 2 },
                    { 177, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4104), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4104), 2 },
                    { 178, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4106), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4106), 2 },
                    { 179, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4107), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4108), 2 },
                    { 180, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4109), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4109), 2 },
                    { 181, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4111), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4111), 3 },
                    { 182, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4113), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4113), 3 },
                    { 183, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4114), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4115), 3 },
                    { 184, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4116), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4117), 3 },
                    { 185, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4118), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4118), 3 },
                    { 186, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4120), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4120), 3 },
                    { 187, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4121), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4122), 3 },
                    { 188, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4123), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4123), 3 },
                    { 189, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4125), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4125), 3 },
                    { 190, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4126), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4127), 3 },
                    { 191, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4128), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4128), 3 },
                    { 192, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4130), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4130), 3 },
                    { 193, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4131), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4132), 3 },
                    { 194, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4133), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4134), 3 },
                    { 195, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4135), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4135), 3 },
                    { 196, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4137), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4137), 3 },
                    { 197, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4138), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4139), 3 },
                    { 198, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4140), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4140), 3 },
                    { 199, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4142), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4142), 3 },
                    { 200, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4210), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4210), 3 },
                    { 201, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4212), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4213), 3 },
                    { 202, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4214), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4215), 3 },
                    { 203, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4216), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4216), 3 },
                    { 204, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4218), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4218), 3 },
                    { 205, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4219), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4220), 3 },
                    { 206, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4221), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4222), 3 },
                    { 207, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4223), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4224), 3 },
                    { 208, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4225), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4225), 3 },
                    { 209, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4227), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4227), 3 },
                    { 210, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4228), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4229), 3 },
                    { 211, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4230), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4231), 3 },
                    { 212, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4232), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4232), 3 },
                    { 213, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4234), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4234), 3 },
                    { 214, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4235), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4236), 3 },
                    { 215, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4237), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4237), 3 },
                    { 216, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4239), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4239), 3 },
                    { 217, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4241), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4241), 3 },
                    { 218, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4242), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4243), 3 },
                    { 219, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4244), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4244), 3 },
                    { 220, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4246), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4246), 3 },
                    { 221, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4247), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4248), 3 },
                    { 222, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4249), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4249), 3 },
                    { 223, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4251), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4251), 3 },
                    { 224, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4253), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4253), 3 },
                    { 225, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4254), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4255), 3 },
                    { 226, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4256), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4256), 3 },
                    { 227, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4258), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4258), 3 },
                    { 228, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4259), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4260), 3 },
                    { 229, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4261), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4261), 3 },
                    { 230, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4263), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4263), 3 },
                    { 231, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4264), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4265), 3 },
                    { 232, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4266), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4266), 3 },
                    { 233, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4268), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4268), 3 },
                    { 234, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4269), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4270), 3 },
                    { 235, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4271), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4272), 3 },
                    { 236, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4273), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4273), 3 },
                    { 237, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4274), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4275), 3 },
                    { 238, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4276), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4277), 3 },
                    { 239, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4278), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4278), 3 },
                    { 240, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4280), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4280), 3 },
                    { 241, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4281), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4282), 3 },
                    { 242, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4283), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4283), 3 },
                    { 243, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4285), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4285), 3 },
                    { 244, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4286), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4287), 3 },
                    { 245, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4288), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4288), 3 },
                    { 246, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4290), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4290), 3 },
                    { 247, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4292), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4292), 3 },
                    { 248, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4293), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4294), 3 },
                    { 249, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4295), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4295), 3 },
                    { 250, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4297), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4297), 3 },
                    { 251, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4298), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4299), 3 },
                    { 252, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4300), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4300), 3 },
                    { 253, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4302), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4302), 3 },
                    { 254, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4303), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4304), 3 },
                    { 255, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4305), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4305), 3 },
                    { 256, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4307), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4307), 3 },
                    { 257, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4308), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4309), 3 },
                    { 258, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4368), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4369), 3 },
                    { 259, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4370), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4371), 3 },
                    { 260, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4372), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4372), 3 },
                    { 261, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4374), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4374), 3 },
                    { 262, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4375), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4376), 3 },
                    { 263, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4377), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4378), 3 },
                    { 264, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4379), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4379), 3 },
                    { 265, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4381), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4381), 3 },
                    { 266, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4382), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4383), 3 },
                    { 267, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4384), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4384), 3 },
                    { 268, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4386), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4386), 3 },
                    { 269, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4388), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4388), 3 },
                    { 270, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4389), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4390), 3 },
                    { 271, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4391), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4392), 4 },
                    { 272, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4393), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4393), 4 },
                    { 273, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4395), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4395), 4 },
                    { 274, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4396), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4397), 4 },
                    { 275, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4398), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4398), 4 },
                    { 276, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4400), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4400), 4 },
                    { 277, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4401), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4402), 4 },
                    { 278, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4403), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4403), 4 },
                    { 279, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4405), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4405), 4 },
                    { 280, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4407), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4407), 4 },
                    { 281, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4408), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4409), 4 },
                    { 282, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4410), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4410), 4 },
                    { 283, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4412), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4412), 4 },
                    { 284, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4413), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4414), 4 },
                    { 285, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4415), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4415), 4 },
                    { 286, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4417), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4417), 4 },
                    { 287, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4418), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4419), 4 },
                    { 288, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4420), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4420), 4 },
                    { 289, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4422), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4422), 4 },
                    { 290, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4423), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4424), 4 },
                    { 291, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4425), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4425), 4 },
                    { 292, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4427), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4427), 4 },
                    { 293, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4429), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4429), 4 },
                    { 294, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4430), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4431), 4 },
                    { 295, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4432), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4432), 4 },
                    { 296, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4434), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4434), 4 },
                    { 297, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4435), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4436), 4 },
                    { 298, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4437), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4438), 4 },
                    { 299, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4439), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4440), 4 },
                    { 300, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4441), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4441), 4 },
                    { 301, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4443), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4443), 4 },
                    { 302, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4444), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4445), 4 },
                    { 303, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4446), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4446), 4 },
                    { 304, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4448), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4448), 4 },
                    { 305, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4449), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4450), 4 },
                    { 306, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4451), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4451), 4 },
                    { 307, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4453), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4453), 4 },
                    { 308, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4454), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4455), 4 },
                    { 309, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4456), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4456), 4 },
                    { 310, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4458), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4458), 4 },
                    { 311, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4459), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4460), 4 },
                    { 312, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4461), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4461), 4 },
                    { 313, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4463), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4463), 4 },
                    { 314, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4464), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4465), 4 },
                    { 315, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4466), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4467), 4 },
                    { 316, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4468), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4468), 4 },
                    { 317, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4470), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4470), 4 },
                    { 318, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4471), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4472), 4 },
                    { 319, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4473), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4473), 4 },
                    { 320, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4475), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4475), 4 },
                    { 321, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4476), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4477), 4 },
                    { 322, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4478), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4478), 4 },
                    { 323, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4480), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4480), 4 },
                    { 324, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4481), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4482), 4 },
                    { 325, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4483), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4484), 4 },
                    { 326, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4485), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4485), 4 },
                    { 327, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4486), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4487), 4 },
                    { 328, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4526), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4527), 4 },
                    { 329, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4528), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4528), 4 },
                    { 330, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4530), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4530), 4 },
                    { 331, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4532), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4533), 4 },
                    { 332, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4534), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4534), 4 },
                    { 333, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4536), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4536), 4 },
                    { 334, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4537), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4538), 4 },
                    { 335, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4539), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4540), 4 },
                    { 336, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4541), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4541), 4 },
                    { 337, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4543), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4543), 4 },
                    { 338, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4545), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4545), 4 },
                    { 339, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4546), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4547), 4 },
                    { 340, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4548), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4549), 4 },
                    { 341, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4550), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4550), 4 },
                    { 342, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4552), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4552), 4 },
                    { 343, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4553), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4554), 4 },
                    { 344, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4555), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4555), 4 },
                    { 345, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4557), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4557), 4 },
                    { 346, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4558), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4559), 4 },
                    { 347, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4560), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4561), 4 },
                    { 348, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4562), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4562), 4 },
                    { 349, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4564), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4564), 4 },
                    { 350, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4565), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4566), 4 },
                    { 351, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4567), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4567), 4 },
                    { 352, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4569), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4569), 4 },
                    { 353, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4570), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4571), 4 },
                    { 354, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4572), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4572), 4 },
                    { 355, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4574), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4574), 4 },
                    { 356, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4575), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4576), 4 },
                    { 357, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4577), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4578), 4 },
                    { 358, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4579), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4579), 4 },
                    { 359, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4581), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4581), 4 },
                    { 360, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4582), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4583), 4 },
                    { 361, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4584), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4585), 5 },
                    { 362, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4586), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4586), 5 },
                    { 363, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4588), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4588), 5 },
                    { 364, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4589), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4590), 5 },
                    { 365, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4591), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4591), 5 },
                    { 366, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4593), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4593), 5 },
                    { 367, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4595), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4595), 5 },
                    { 368, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4596), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4597), 5 },
                    { 369, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4598), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4598), 5 },
                    { 370, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4600), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4600), 5 },
                    { 371, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4601), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4602), 5 },
                    { 372, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4603), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4603), 5 },
                    { 373, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4605), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4605), 5 },
                    { 374, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4606), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4607), 5 },
                    { 375, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4608), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4608), 5 },
                    { 376, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4610), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4610), 5 },
                    { 377, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4611), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4612), 5 },
                    { 378, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4613), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4614), 5 },
                    { 379, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4615), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4615), 5 },
                    { 380, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4617), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4617), 5 },
                    { 381, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4618), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4619), 5 },
                    { 382, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4620), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4620), 5 },
                    { 383, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4622), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4622), 5 },
                    { 384, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4624), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4624), 5 },
                    { 385, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4625), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4626), 5 },
                    { 386, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4627), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4627), 5 },
                    { 387, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4629), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4629), 5 },
                    { 388, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4630), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4631), 5 },
                    { 389, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4632), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4632), 5 },
                    { 390, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4634), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4634), 5 },
                    { 391, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4636), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4636), 5 },
                    { 392, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4637), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4638), 5 },
                    { 393, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4639), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4639), 5 },
                    { 394, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4641), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4641), 5 },
                    { 395, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4642), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4643), 5 },
                    { 396, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4644), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4644), 5 },
                    { 397, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4646), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4646), 5 },
                    { 398, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4647), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4648), 5 },
                    { 399, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4649), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4649), 5 },
                    { 400, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4651), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4651), 5 },
                    { 401, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4652), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4653), 5 },
                    { 402, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4654), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4654), 5 },
                    { 403, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4656), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4656), 5 },
                    { 404, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4658), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4658), 5 },
                    { 405, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4660), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4660), 5 },
                    { 406, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4661), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4662), 5 },
                    { 407, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4663), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4663), 5 },
                    { 408, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4665), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4665), 5 },
                    { 409, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4666), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4667), 5 },
                    { 410, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4668), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4668), 5 },
                    { 411, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4670), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4670), 5 },
                    { 412, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4671), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4672), 5 },
                    { 413, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4673), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4673), 5 },
                    { 414, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4715), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4716), 5 },
                    { 415, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4717), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4718), 5 },
                    { 416, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4719), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4719), 5 },
                    { 417, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4721), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4721), 5 },
                    { 418, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4723), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4723), 5 },
                    { 419, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4724), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4725), 5 },
                    { 420, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4726), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4727), 5 },
                    { 421, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4728), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4728), 5 },
                    { 422, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4730), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4730), 5 },
                    { 423, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4732), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4732), 5 },
                    { 424, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4733), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4734), 5 },
                    { 425, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4735), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4735), 5 },
                    { 426, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4737), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4737), 5 },
                    { 427, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4738), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4739), 5 },
                    { 428, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4740), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4741), 5 },
                    { 429, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4742), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4742), 5 },
                    { 430, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4744), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4744), 5 },
                    { 431, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4745), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4746), 5 },
                    { 432, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4747), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4747), 5 },
                    { 433, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4749), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4749), 5 },
                    { 434, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4750), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4751), 5 },
                    { 435, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4752), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4752), 5 },
                    { 436, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4754), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4754), 5 },
                    { 437, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4755), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4756), 5 },
                    { 438, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4757), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4758), 5 },
                    { 439, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4759), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4759), 5 },
                    { 440, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4761), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4761), 5 },
                    { 441, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4762), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4763), 5 },
                    { 442, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4764), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4765), 5 },
                    { 443, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4766), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4766), 5 },
                    { 444, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4768), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4768), 5 },
                    { 445, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4769), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4770), 5 },
                    { 446, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4771), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4771), 5 },
                    { 447, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4773), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4773), 5 },
                    { 448, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4774), new TimeSpan(0, 12, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 9, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4775), 5 },
                    { 449, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4776), new TimeSpan(0, 17, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4776), 5 },
                    { 450, null, new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4778), new TimeSpan(0, 22, 0, 0, 0), true, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 12, 3, 4, 51, 44, 760, DateTimeKind.Utc).AddTicks(4778), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueTimeSlotId",
                table: "Events",
                column: "VenueTimeSlotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VenueTimeSlots_BookedEventId",
                table: "VenueTimeSlots",
                column: "BookedEventId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueTimeSlots_VenueId_SlotDate_IsAvailable",
                table: "VenueTimeSlots",
                columns: new[] { "VenueId", "SlotDate", "IsAvailable" });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_VenueTimeSlots_VenueTimeSlotId",
                table: "Events",
                column: "VenueTimeSlotId",
                principalTable: "VenueTimeSlots",
                principalColumn: "TimeSlotId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Venues_VenueId",
                table: "Events",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_VenueTimeSlots_VenueTimeSlotId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Venues_VenueId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "VenueTimeSlots");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Events_VenueId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_VenueTimeSlotId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VenueTimeSlotId",
                table: "Events");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReadAt",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EventThemes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Events",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateTime",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Events",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Events",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Events",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BannerImageUrl",
                table: "Events",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Events",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                table: "EventGroupMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReadAt",
                table: "EventGroupMessageReads",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RSVP_Date",
                table: "EventAttendees",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInTime",
                table: "EventAttendees",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "EventThemes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "EventThemes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "EventThemes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
