using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TFMS.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Exhibitors",
                columns: table => new
                {
                    ExhibitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TradeLicence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibitors", x => x.ExhibitorId);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    OrganizerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WebSiteUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.OrganizerId);
                });

            migrationBuilder.CreateTable(
                name: "PavilionCategorys",
                columns: table => new
                {
                    PavilionCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PavilionType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarePerSquareFeet = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PavilionCategorys", x => x.PavilionCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Sponsors",
                columns: table => new
                {
                    SponsorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.SponsorId);
                });

            migrationBuilder.CreateTable(
                name: "StaffDetails",
                columns: table => new
                {
                    StaffDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffDetails", x => x.StaffDetailId);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.VisitorId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ExhibitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Exhibitors_ExhibitorId",
                        column: x => x.ExhibitorId,
                        principalTable: "Exhibitors",
                        principalColumn: "ExhibitorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fairs",
                columns: table => new
                {
                    FairId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FairName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegistrationDeadLine = table.Column<DateTime>(type: "date", nullable: false),
                    OrganizerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fairs", x => x.FairId);
                    table.ForeignKey(
                        name: "FK_Fairs_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "OrganizerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingPasses",
                columns: table => new
                {
                    ParkingPassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberPlate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParkingDate = table.Column<DateTime>(type: "date", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CheckOutTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Token = table.Column<int>(type: "int", nullable: false),
                    ExhibitorId = table.Column<int>(type: "int", nullable: false),
                    OrganizerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingPasses", x => x.ParkingPassId);
                    table.ForeignKey(
                        name: "FK_ParkingPasses_Exhibitors_ExhibitorId",
                        column: x => x.ExhibitorId,
                        principalTable: "Exhibitors",
                        principalColumn: "ExhibitorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingPasses_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "OrganizerId");
                });

            migrationBuilder.CreateTable(
                name: "Pavilions",
                columns: table => new
                {
                    PavilionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PavilionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PavilionNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SqFoot = table.Column<double>(type: "float", nullable: false),
                    Rent = table.Column<decimal>(type: "money", nullable: false),
                    PavilionCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pavilions", x => x.PavilionId);
                    table.ForeignKey(
                        name: "FK_Pavilions_PavilionCategorys_PavilionCategoryId",
                        column: x => x.PavilionCategoryId,
                        principalTable: "PavilionCategorys",
                        principalColumn: "PavilionCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendences",
                columns: table => new
                {
                    AttendenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendenceDate = table.Column<DateTime>(type: "date", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StaffDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendences", x => x.AttendenceId);
                    table.ForeignKey(
                        name: "FK_Attendences_StaffDetails_StaffDetailId",
                        column: x => x.StaffDetailId,
                        principalTable: "StaffDetails",
                        principalColumn: "StaffDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EventDate = table.Column<DateTime>(type: "date", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SpeakerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpeakerDetails = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedBacks",
                columns: table => new
                {
                    FeedBackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBacks", x => x.FeedBackId);
                    table.ForeignKey(
                        name: "FK_FeedBacks_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    ParkingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    NumberPlate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParkingDate = table.Column<DateTime>(type: "date", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CheckOutTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Token = table.Column<int>(type: "int", nullable: false),
                    ParkingFare = table.Column<decimal>(type: "money", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.ParkingId);
                    table.ForeignKey(
                        name: "FK_Parkings_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "date", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "date", nullable: false),
                    NumberOfPavilion = table.Column<int>(type: "int", nullable: false),
                    BookingAmount = table.Column<decimal>(type: "money", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    ExhibitorId = table.Column<int>(type: "int", nullable: false),
                    PavilionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Exhibitors_ExhibitorId",
                        column: x => x.ExhibitorId,
                        principalTable: "Exhibitors",
                        principalColumn: "ExhibitorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Pavilions_PavilionId",
                        column: x => x.PavilionId,
                        principalTable: "Pavilions",
                        principalColumn: "PavilionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exhibitorPasses",
                columns: table => new
                {
                    ExhibitorPassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "date", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "date", nullable: false),
                    ExhibitorId = table.Column<int>(type: "int", nullable: false),
                    PavilionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exhibitorPasses", x => x.ExhibitorPassId);
                    table.ForeignKey(
                        name: "FK_exhibitorPasses_Exhibitors_ExhibitorId",
                        column: x => x.ExhibitorId,
                        principalTable: "Exhibitors",
                        principalColumn: "ExhibitorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exhibitorPasses_Pavilions_PavilionId",
                        column: x => x.PavilionId,
                        principalTable: "Pavilions",
                        principalColumn: "PavilionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventsReg",
                columns: table => new
                {
                    EventRegId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsReg", x => x.EventRegId);
                    table.ForeignKey(
                        name: "FK_EventsReg_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsReg_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: false),
                    PaymentThrough = table.Column<int>(type: "int", nullable: false),
                    PaymentFor = table.Column<int>(type: "int", nullable: false),
                    ExhibitorId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Exhibitors_ExhibitorId",
                        column: x => x.ExhibitorId,
                        principalTable: "Exhibitors",
                        principalColumn: "ExhibitorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitorEntry",
                columns: table => new
                {
                    VisitorEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDate = table.Column<DateTime>(type: "date", nullable: false),
                    EntryTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorEntry", x => x.VisitorEntryId);
                    table.ForeignKey(
                        name: "FK_VisitorEntry_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExhibitorEntryExits",
                columns: table => new
                {
                    ExhibitorEntryExitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    EntryTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ExitTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ExhibitorPassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExhibitorEntryExits", x => x.ExhibitorEntryExitId);
                    table.ForeignKey(
                        name: "FK_ExhibitorEntryExits_exhibitorPasses_ExhibitorPassId",
                        column: x => x.ExhibitorPassId,
                        principalTable: "exhibitorPasses",
                        principalColumn: "ExhibitorPassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Cosmetic" },
                    { 2, "Food" },
                    { 3, "Sanks" }
                });

            migrationBuilder.InsertData(
                table: "Exhibitors",
                columns: new[] { "ExhibitorId", "CompanyName", "ContactNumber", "Description", "Email", "LogoUrl", "TradeLicence", "Website" },
                values: new object[,]
                {
                    { 1, "viyla text", "0125486325", "a person whose work is being shown in an exhibiton", "veyela125@gmail.com", "logo1.jpg", "1254", "wwwveyelatext123.com" },
                    { 2, "Anower Group", "0155486325", "Exhibitors set up a wide variety of trade show display", "Anower@gmail.com", "logo2.jpg", "3500", "wwwtext123.com" },
                    { 3, "Mgf Group", "0121286325", "a person whose work is being shown in an exhibiton", "Mgf@gmail.com", "logo3.jpg", "1583", "wwwMgf123.com" }
                });

            migrationBuilder.InsertData(
                table: "Organizers",
                columns: new[] { "OrganizerId", "OrganizerEmail", "OrganizerName", "OrganizerPhone", "SortDescription", "WebSiteUrl" },
                values: new object[,]
                {
                    { 1, "progoti123@gmial.com", "Progoti missino bangladesh", "0183157812", "Grievance Radressal Management", "http//progogi.com" },
                    { 2, "Sebar78@gmial.com", "Sebar Alo", "0183157810", "sebar alo", "http//Saba.com" },
                    { 3, "Jonogon21@gmial.com", "Jonogon er sohojogita", "0183857812", "Jonogon er sohojogita", "http//Jonogon21.com" },
                    { 4, "Saba23@gmial.com", "Saba Organnizer", "0182357812", "", "http//Saba.com" }
                });

            migrationBuilder.InsertData(
                table: "PavilionCategorys",
                columns: new[] { "PavilionCategoryId", "CategoryName", "Description", "FarePerSquareFeet", "PavilionType" },
                values: new object[,]
                {
                    { 1, "C-1", "L-Shape left Side Corner Pavilion", 50m, 1 },
                    { 2, "B-31", " left Side Box Pavilion", 60m, 2 },
                    { 3, "I-1", "Island Pavilion", 50m, 3 },
                    { 4, "B-41", " Middle North Side Pavilion", 60m, 2 },
                    { 5, "B-43", "Middle South Side Pavilion", 60m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Sponsors",
                columns: new[] { "SponsorId", "City", "CompanyName", "Email", "Industry", "Phone", "Website" },
                values: new object[,]
                {
                    { 1, "Dhaka", "Walton", "walton@gmail.com", "Walton Hi-Tech Industries", "01322110044", "www.waltonbd.com" },
                    { 2, "Gazipur", "Akij Group", "akij@gmail.com", "Akij Group of Company", "01422110044", "www.akijbd.com" },
                    { 3, "Savar", "Square Farma", "square@gmail.com", "Square Pharmacitical Company", "01722110044", "www.squarebd.com" },
                    { 4, "All District", "Daraz", "daraz@gmail.com", "Daraz Online Shop", "01822110044", "www.daraz.com" },
                    { 5, "Narayanganj", "Pran", "pran@gmail.com", "Pran Industry", "01922110044", "www.pranbd.com" }
                });

            migrationBuilder.InsertData(
                table: "StaffDetails",
                columns: new[] { "StaffDetailId", "Email", "Gender", "Phone", "Picture", "StaffName" },
                values: new object[,]
                {
                    { 1, "rahman@gmail.com", 1, "01322110044", "1.jpg", "AR Rahman" },
                    { 2, "runa@gmail.com", 2, "01422110044", "2.jpg", "Runa" },
                    { 3, "momin@gmail.com", 1, "01722110044", "3.jpg", "Momin" },
                    { 4, "mahmudul@gmail.com", 1, "01822110044", "4.jpg", "Mahmudul" },
                    { 5, "parul@gmail.com", 2, "01922110044", "5.jpg", "Parul" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueId", "Capacity", "Location", "VenueName" },
                values: new object[,]
                {
                    { 1, 300, "West South Corner", "Venue 1" },
                    { 2, 400, "West side", "Venue 2" },
                    { 3, 200, " South Corner", "Venue 3" }
                });

            migrationBuilder.InsertData(
                table: "Visitors",
                columns: new[] { "VisitorId", "Email", "FirstName", "Gender", "LastName", "Nationality", "Phone" },
                values: new object[,]
                {
                    { 1, "a123@gmail.com", "A", 2, "b", "Bangladeshi", "0214587525" },
                    { 2, "b123@gmail.com", "B", 1, "c", "Indian", "0214587525" },
                    { 3, "c123@gmail.com", "C", 3, "d", "Pakistani", "0214587525" }
                });

            migrationBuilder.InsertData(
                table: "Attendences",
                columns: new[] { "AttendenceId", "AttendenceDate", "EndTime", "StaffDetailId", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 15, 0, 0), 1, new TimeSpan(0, 8, 55, 0, 0) },
                    { 2, new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 10, 0, 0), 2, new TimeSpan(0, 8, 57, 0, 0) },
                    { 3, new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 12, 0, 0), 3, new TimeSpan(0, 8, 58, 0, 0) },
                    { 4, new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 5, 0, 0), 4, new TimeSpan(0, 8, 50, 0, 0) },
                    { 5, new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 7, 0, 0), 5, new TimeSpan(0, 8, 53, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "EndTime", "EventDate", "EventDescription", "EventName", "SpeakerDetails", "SpeakerName", "StartTime", "VenueId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 12, 0, 0, 0), new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Product Launch", "New Product Launch", "CEO of Walton", "Mr. Murad", new TimeSpan(0, 11, 0, 0, 0), 1 },
                    { 2, new TimeSpan(0, 1, 0, 0, 0), new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Brand Launch", "Brand Promotion", "Md of Lorial", "Ms. Shila", new TimeSpan(0, 12, 0, 0, 0), 2 },
                    { 3, new TimeSpan(0, 12, 0, 0, 0), new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Product Launch", "New Product Launch", "CEO of Akij", "Mr. Fahad", new TimeSpan(0, 12, 0, 0, 0), 3 }
                });

            migrationBuilder.InsertData(
                table: "Fairs",
                columns: new[] { "FairId", "EndDate", "FairName", "Location", "OrganizerId", "RegistrationDeadLine", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "internatonal trade fair", "Agargong", 1, new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book fair", "Hatirjil", 2, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), " Job fair", "Dhaka", 3, new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business fair", "Agargong", 4, new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "FeedBacks",
                columns: new[] { "FeedBackId", "Comment", "Rating", "VisitorId" },
                values: new object[,]
                {
                    { 1, "very good ", 5m, 1 },
                    { 2, " good ", 4m, 2 },
                    { 3, "Khub valo ", 5m, 3 }
                });

            migrationBuilder.InsertData(
                table: "ParkingPasses",
                columns: new[] { "ParkingPassId", "CheckInTime", "CheckOutTime", "ExhibitorId", "NumberPlate", "OrganizerId", "ParkingDate", "Token" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 9, 10, 15, 0), new TimeSpan(0, 12, 20, 15, 0), 1, "DHAKA-D-11-9999", null, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 2, new TimeSpan(0, 11, 0, 0, 0), new TimeSpan(0, 1, 30, 15, 0), 2, "DHAKA-D-07-1234", null, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 3, new TimeSpan(0, 10, 10, 15, 0), new TimeSpan(0, 11, 20, 15, 0), 3, "DHAKA-D-20-9567", null, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 }
                });

            migrationBuilder.InsertData(
                table: "Parkings",
                columns: new[] { "ParkingId", "CheckInTime", "CheckOutTime", "NumberPlate", "ParkingDate", "ParkingFare", "Token", "VehicleType", "VisitorId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 10, 40, 12, 0), new TimeSpan(0, 12, 20, 10, 0), "DHAKA-D-10-9851", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m, 5, 1, 1 },
                    { 2, new TimeSpan(0, 11, 40, 10, 0), new TimeSpan(0, 12, 30, 10, 0), "DHAKA-H-15-5684", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 80m, 7, 2, 2 },
                    { 3, new TimeSpan(0, 10, 40, 12, 0), new TimeSpan(0, 12, 20, 10, 0), "DHAKA-D-10-2354", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m, 5, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Pavilions",
                columns: new[] { "PavilionId", "PavilionCategoryId", "PavilionName", "PavilionNumber", "Rent", "Size", "SqFoot" },
                values: new object[,]
                {
                    { 1, 1, "Conrner Pavilion", "P-101", 6000m, "10X12", 120.0 },
                    { 2, 2, "Box Pavilion", "P-201", 6000m, "10X10", 100.0 },
                    { 3, 3, "Island Pavilion", "P-202", 3200m, "8X8", 64.0 },
                    { 4, 4, "Box Pavilion", "P-203", 8640m, "12X12", 144.0 },
                    { 5, 5, "Box Pavilion", "P-204", 8640m, "12X12", 144.0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "ExhibitorId", "Picture", "ProductName" },
                values: new object[,]
                {
                    { 1, 1, 1, "1.jpg", "Beauty Product" },
                    { 2, 2, 2, "2.jpg", " Dairy Product" },
                    { 3, 3, 3, "3.jpg", "Choocklet Product" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "IssueDate", "Price", "Quantity", "VisitorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, 12, 1 },
                    { 2, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, 11, 2 },
                    { 3, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 500m, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "BookingAmount", "BookingDate", "BookingStatus", "ExhibitorId", "NumberOfPavilion", "PavilionId" },
                values: new object[,]
                {
                    { 1, 50000m, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 86, 1 },
                    { 2, 50000m, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2, 86, 2 },
                    { 3, 50000m, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3, 86, 3 }
                });

            migrationBuilder.InsertData(
                table: "EventsReg",
                columns: new[] { "EventRegId", "EventId", "VisitorId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "ExhibitorId", "PaymentDate", "PaymentFor", "PaymentThrough", "TicketId" },
                values: new object[,]
                {
                    { 1, 20000m, 1, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 1 },
                    { 2, 110m, 2, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 2 },
                    { 3, 500m, 3, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, 3 }
                });

            migrationBuilder.InsertData(
                table: "VisitorEntry",
                columns: new[] { "VisitorEntryId", "EntryDate", "EntryTime", "TicketId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 10, 10, 0), 1 },
                    { 2, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 20, 30, 0), 2 },
                    { 3, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 11, 10, 0), 3 }
                });

            migrationBuilder.InsertData(
                table: "exhibitorPasses",
                columns: new[] { "ExhibitorPassId", "ExhibitorId", "PavilionId", "PersonName", "ValidFrom", "ValidUntil" },
                values: new object[,]
                {
                    { 1, 1, 1, "Thuaha", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, "Fahad", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, "Shila", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ExhibitorEntryExits",
                columns: new[] { "ExhibitorEntryExitId", "Date", "EntryTime", "ExhibitorPassId", "ExitTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 10, 15, 0), 1, new TimeSpan(0, 12, 10, 20, 0) },
                    { 2, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 11, 20, 0), 2, new TimeSpan(0, 1, 20, 10, 0) },
                    { 3, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 30, 15, 0), 3, new TimeSpan(0, 11, 10, 30, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendences_StaffDetailId",
                table: "Attendences",
                column: "StaffDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ExhibitorId",
                table: "Bookings",
                column: "ExhibitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PavilionId",
                table: "Bookings",
                column: "PavilionId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsReg_EventId",
                table: "EventsReg",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsReg_VisitorId",
                table: "EventsReg",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExhibitorEntryExits_ExhibitorPassId",
                table: "ExhibitorEntryExits",
                column: "ExhibitorPassId");

            migrationBuilder.CreateIndex(
                name: "IX_exhibitorPasses_ExhibitorId",
                table: "exhibitorPasses",
                column: "ExhibitorId");

            migrationBuilder.CreateIndex(
                name: "IX_exhibitorPasses_PavilionId",
                table: "exhibitorPasses",
                column: "PavilionId");

            migrationBuilder.CreateIndex(
                name: "IX_Fairs_OrganizerId",
                table: "Fairs",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_VisitorId",
                table: "FeedBacks",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPasses_ExhibitorId",
                table: "ParkingPasses",
                column: "ExhibitorId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPasses_OrganizerId",
                table: "ParkingPasses",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_VisitorId",
                table: "Parkings",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pavilions_PavilionCategoryId",
                table: "Pavilions",
                column: "PavilionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ExhibitorId",
                table: "Payments",
                column: "ExhibitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TicketId",
                table: "Payments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ExhibitorId",
                table: "Products",
                column: "ExhibitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorEntry_TicketId",
                table: "VisitorEntry",
                column: "TicketId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendences");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "EventsReg");

            migrationBuilder.DropTable(
                name: "ExhibitorEntryExits");

            migrationBuilder.DropTable(
                name: "Fairs");

            migrationBuilder.DropTable(
                name: "FeedBacks");

            migrationBuilder.DropTable(
                name: "ParkingPasses");

            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sponsors");

            migrationBuilder.DropTable(
                name: "VisitorEntry");

            migrationBuilder.DropTable(
                name: "StaffDetails");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "exhibitorPasses");

            migrationBuilder.DropTable(
                name: "Organizers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Exhibitors");

            migrationBuilder.DropTable(
                name: "Pavilions");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "PavilionCategorys");
        }
    }
}
