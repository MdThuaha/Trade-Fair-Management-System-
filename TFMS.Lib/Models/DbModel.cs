using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFMS.Lib.Models
{
    public enum Gender { Male = 1, Female, Others }
    public enum PaymentThrough { bKash = 1, Rocket, Nagad, uPay, CreditOrDebit, Cash }
    public enum PaymentFor { Ticket, Pavilion, Parking }
    public enum BookingStatus { Confirmed = 1, Pending, Cancelled, Expired, Unavailable }
    public enum PavilionType { Corner = 1, Box, Isle }
    public enum VehicleType { Car = 1, Bike, MiniTruck, CNG }

    public class Exhibitor
    {
        public int ExhibitorId { get; set; }
        [Required(ErrorMessage = "Company Name is required"), StringLength(100), Display(Name = "Company name")]
        public string CompanyName { get; set; } = default!;
        [Required, StringLength(100), Display(Name = "Trade Licence")]
        public string TradeLicence { get; set; } = default!;
        [Required, StringLength(100), Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = default!;
        [Required, StringLength(100)]
        public string Email { get; set; } = default!;
        [Required, StringLength(100)]
        public string Website { get; set; } = default!;
        [Required, StringLength(500)]
        public string Description { get; set; } = default!;
        [Required, StringLength(50)]
        public string LogoUrl { get; set; } = default!;
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<ExhibitorPass> ExhibitorPasses { get; set; } = new List<ExhibitorPass>();
        public virtual ICollection<ParkingPass> ParkingPass { get; set; } = new List<ParkingPass>();

    }


    public class PavilionCategory
    {
        public int PavilionCategoryId { get; set; }
        [Required, StringLength(200)]
        public string CategoryName { get; set; } = default!;
        [Required, EnumDataType(typeof(PavilionType))]
        public PavilionType PavilionType { get; set; }
        public string Description { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal? FarePerSquareFeet { get; set; }
        public virtual ICollection<Pavilion> Pavilions { get; set; } = new List<Pavilion>();

    }
    public class Pavilion
    {
        public int PavilionId { get; set; }
        [Required, StringLength(100), Display(Name = "Pavilion Name")]
        public string PavilionName { get; set; } = default!;
        [Required, StringLength(10)]
        public string PavilionNumber { get; set; } = default!;
        [Required, StringLength(50)]
        public string Size { get; set; } = default!;
        [Required]
        public double SqFoot { get; set; }
        
        [Required, Column(TypeName = "money")]
        public decimal? Rent { get; set; }
       
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<ExhibitorPass> ExhibitorPasses { get; set; } = new List<ExhibitorPass>();

        //Foreign Key
        [Required, ForeignKey("PavilionCategory")]
        public int PavilionCategoryId { get; set; }
        public virtual PavilionCategory? PavilionCategory { get; set; } = default!;
    }
    public class Booking
    {
        public int BookingId { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BookingDate { get; set; }
        [Required]
        public int NumberOfPavilion { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal? BookingAmount { get; set; }
        [Required, EnumDataType(typeof(BookingStatus))]
        public BookingStatus BookingStatus { get; set; }
        //*****//
        [Required, ForeignKey("Exhibitor")]
        public int ExhibitorId { get; set; }
        public virtual Exhibitor? Exhibitor { get; set; } = default!;
        //*****//
        [Required, ForeignKey("Pavilion")]
        public int PavilionId { get; set; }
        public virtual Pavilion? Pavilion { get; set; } = default!;
    }


    ////----------------////
    public class Category
    {
        public int CategoryId { get; set; }
        [Required, StringLength(50), Display(Name = "Category name")]
        public string CategoryName { get; set; } = default!;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
    public class Product
    {
        public int ProductId { get; set; }
        [Required, StringLength(50)]
        public string ProductName { get; set; } = default!;
        [Required, StringLength(40)]
        public string Picture { get; set; } = default!;
        //Foreign Key
        [Required, ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; } = default!;
        [Required, ForeignKey("Exhibitor")]
        public int ExhibitorId { get; set; }
        public virtual Exhibitor? Exhibitor { get; set; } = default!;
    }

    //-----------------------------------------------//
    public class Visitor
    {
        public int VisitorId { get; set; }
        [Required, StringLength(100), Display(Name = "First Name")]
        public string FirstName { get; set; } = default!;
        [Required, StringLength(100), Display(Name = "Last Name")]
        public string LastName { get; set; } = default!;
        [Required, StringLength(100)]
        public string Email { get; set; } = default!;
        [Required, StringLength(20)]
        public string Phone { get; set; } = default!;
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, StringLength(50)]
        public string Nationality { get; set; } = default!;

        //*****//
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
        public virtual ICollection<Parking> Parkings { get; set; } = new List<Parking>();
        public virtual ICollection<EventReg> EventRegs { get; set; } = new List<EventReg>();
    }

    public class Ticket
    {
        public int TicketId { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? IssueDate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal? Price { get; set; }
        //
        public virtual VisitorEntry? VisitorEntry { get; set; }

        //Foreign Key
        [Required, ForeignKey("Visitor")]
        public int VisitorId { get; set; }
        public virtual Visitor? Visitor { get; set; } = default!;

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    }
    public class VisitorEntry
    {
        public int VisitorEntryId { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EntryDate { get; set; } = default!;

        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? EntryTime { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }

    }
    public class FeedBack
    {
        public int FeedBackId { get; set; }
        [Required]
        public decimal? Rating { get; set; }
        [Required, StringLength(500)]
        public string Comment { get; set; } = default!;

        //Foreign Key
        [Required, ForeignKey("Visitor")]
        public int VisitorId { get; set; }
        public virtual Visitor? Visitor { get; set; } = default!;
    }
    public class ExhibitorPass
    {
        public int ExhibitorPassId { get; set; }
        [Required, StringLength(100)]
        public string PersonName { get; set; } = default!;
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ValidFrom { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ValidUntil { get; set; }

        //Foreign Key
        [Required, ForeignKey("Exhibitor")]
        public int ExhibitorId { get; set; }
        public virtual Exhibitor? Exhibitor { get; set; } = default!;
        [Required, ForeignKey("Pavilion")]
        public int PavilionId { get; set; }
        public virtual Pavilion? Pavilion { get; set; } = default!;

        //**********//
        public virtual ICollection<ExhibitorEntryExit> ExhibitorEntryExits { get; set; } = new List<ExhibitorEntryExit>();
    }

    public class ExhibitorEntryExit
    {
        public int ExhibitorEntryExitId { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? EntryTime { get; set; } = default!;
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? ExitTime { get; set; } = default!;
        //Foreign Key
        [Required, ForeignKey("ExhibitorPass")]
        public int ExhibitorPassId { get; set; }
        public virtual ExhibitorPass? ExhibitorPass { get; set; } = default!;

    }

    //-----------------------------------------------//

    public class Parking
    {
        public int ParkingId { get; set; }
        [Required, EnumDataType(typeof(VehicleType))]
        public VehicleType VehicleType { get; set; }
        [Required, StringLength(100)]
        public string NumberPlate { get; set; } = default!;
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ParkingDate { get; set; }
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? CheckInTime { get; set; } = default!;
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? CheckOutTime { get; set; } = default!;
        [Required]
        public int Token { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal? ParkingFare { get; set; }

        //Foreign Key
        [Required, ForeignKey("Visitor")]
        public int VisitorId { get; set; }
        public virtual Visitor? Visitor { get; set; } = default!;


    }
    public class ParkingPass
    {
        public int ParkingPassId { get; set; }

        [Required, StringLength(100)]
        public string NumberPlate { get; set; } = default!;
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ParkingDate { get; set; }
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? CheckInTime { get; set; } = default!;
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? CheckOutTime { get; set; } = default!;
        [Required]
        public int Token { get; set; }

        //Foreign Key
        [Required, ForeignKey("Exhibitor")]
        public int ExhibitorId { get; set; }
        public virtual Exhibitor? Exhibitor { get; set; } = default!;


    }

    //-----------------------------------------------//
    public class Sponsor
    {
        public int SponsorId { get; set; }
        [Required, StringLength(100), Display(Name = "Company Name")]
        public string CompanyName { get; set; } = default!;

        [Required, StringLength(100)]
        public string Phone { get; set; } = default!;
        [Required, StringLength(100)]
        public string Email { get; set; } = default!;
     
        [Required, StringLength(100)]
        public string Industry { get; set; } = default!;
        [Required, StringLength(100)]
        public string City { get; set; } = default!;
        [Required, StringLength(100)]
        public string Website { get; set; } = default!;

    }
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? PaymentDate { get; set; }
        [Required, EnumDataType(typeof(PaymentThrough))]
        public PaymentThrough? PaymentThrough { get; set; }
        [Required, EnumDataType(typeof(PaymentFor))]
        public PaymentFor? PaymentFor { get; set; }
        [Required, ForeignKey("Exhibitor")]
        public int ExhibitorId { get; set; }
        public virtual Exhibitor? Exhibitor { get; set; } = default!;

        [Required, ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; } = default!;

    }

    //******************************************//
    public class Venue
    {
        public int VenueId { get; set; }
        [Required, StringLength(100), Display(Name = "Venue Name")]
        public string VenueName { get; set; } = default!;
        [Required, StringLength(100)]
        public string Location { get; set; } = default!;
        [Required]
        public int Capacity { get; set; } = default!;

        //****//
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    }
    public class Event
    {
        public int EventId { get; set; }
        [Required, StringLength(100), Display(Name = "Event Name")]
        public string EventName { get; set; } = default!;
        [Required, StringLength(500)]
        public string EventDescription { get; set; } = default!;
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EventDate { get; set; } = default!;
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? StartTime { get; set; }
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? EndTime { get; set; }
        [Required, StringLength(100), Display(Name = "Speaker Name")]
        public string SpeakerName { get; set; } = default!;
        [Required, StringLength(100), Display(Name = "Speaker Details")]
        public string SpeakerDetails { get; set; } = default!;

        //*****//
        public virtual ICollection<EventReg> EventRegs { get; set; } = new List<EventReg>();
        //Foreign Key
        [Required, ForeignKey("Venue")]
        public int VenueId { get; set; }
        public virtual Venue? Venue { get; set; } = default!;

    }
    public class EventReg
    {
        public int EventRegId { get; set; }

        //Foreign Key
        [Required, ForeignKey("Event")]
        public int EventId { get; set; }
        public virtual Event? Event { get; set; } = default!;

        [Required, ForeignKey("Visitor")]
        public int VisitorId { get; set; }
        public virtual Visitor? Visitor { get; set; } = default!;
    }

    //******************************************//

    public class StaffDetail
    {
        public int StaffDetailId { get; set; }
        [Required(ErrorMessage = "First Name is required"), StringLength(100), Display(Name = "First name")]
        public string StaffName { get; set; } = default!;
        public string Email { get; set; } = default!;
        [Required, StringLength(20)]
        public string Phone { get; set; } = default!;
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, StringLength(100)]
        public string Picture { get; set; } = default!;
        //*******///
        public virtual ICollection<Attendence> Attendences { get; set; } = new List<Attendence>();
    }

    public class Attendence
    {
        public int AttendenceId { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AttendenceDate { get; set; } = default!;

        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? StartTime { get; set; }
        [Required, Range(typeof(TimeSpan), "00:00", "23:59"), DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan? EndTime { get; set; }
        //Foreing Key
        [Required, ForeignKey("StaffDetail")]
        public int StaffDetailId { get; set; }
        public virtual StaffDetail? StaffDetail { get; set; } = default!;

    }

    public class Organizer
    {
        public int OrganizerId { get; set; }
        [Required, StringLength(100)]
        public string OrganizerName { get; set; } = default!;
        [Required, StringLength(100)]
        public string OrganizerEmail { get; set; } = default!;
        [Required, StringLength(20)]
        public string OrganizerPhone { get; set; } = default!;
        [Required, StringLength(100)]
        public string WebSiteUrl { get; set; } = default!;
        [Required, StringLength(500)]
        public string SortDescription { get; set; } = default!;
        public virtual ICollection<Fair> Fairs { get; set; } = new List<Fair>();
        public virtual ICollection<ParkingPass> ParkingPass { get; set; } = new List<ParkingPass>();

    }
    public class Fair
    {
        public int FairId { get; set; }
        [Required(ErrorMessage = "Fair Name is required"), StringLength(100), Display(Name = "Fair name")]
        public string FairName { get; set; } = default!;
     
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }
        [Required, StringLength(100)]
        public string Location { get; set; } = default!;
      
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? RegistrationDeadLine { get; set; }
 
        //Foreign Key
        [Required, ForeignKey("Organizer")]
        public int OrganizerId { get; set; }
        public virtual Organizer? Organizer { get; set; } = default!;
    }

    public class FairDbContext : DbContext
    {
        public FairDbContext(DbContextOptions<FairDbContext> options) : base(options) { }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Fair> Fairs { get; set; }
        public DbSet<Exhibitor> Exhibitors { get; set; }
        public DbSet<PavilionCategory> PavilionCategorys { get; set; }
        public DbSet<Pavilion> Pavilions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<ParkingPass> ParkingPasses { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventReg> EventsReg { get; set; }
        public DbSet<StaffDetail> StaffDetails { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<ExhibitorPass> exhibitorPasses { get; set; }
        public DbSet<ExhibitorEntryExit> ExhibitorEntryExits { get; set; }
        public DbSet<VisitorEntry> VisitorEntry { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.VisitorEntry)
                .WithOne(s => s.Ticket)
                .HasForeignKey<VisitorEntry>(s => s.TicketId);

            modelBuilder.Entity<PavilionCategory>().HasData(
             new PavilionCategory { PavilionCategoryId = 1, CategoryName = "C-1", PavilionType = PavilionType.Corner, Description = "L-Shape left Side Corner Pavilion", FarePerSquareFeet = 50 },
             new PavilionCategory { PavilionCategoryId = 2, CategoryName = "B-31", PavilionType = PavilionType.Box, Description = " left Side Box Pavilion", FarePerSquareFeet = 60 },
             new PavilionCategory { PavilionCategoryId = 3, CategoryName = "I-1", PavilionType = PavilionType.Isle, Description = "Island Pavilion", FarePerSquareFeet = 50 },
             new PavilionCategory { PavilionCategoryId = 4, CategoryName = "B-41", PavilionType = PavilionType.Box, Description = " Middle North Side Pavilion", FarePerSquareFeet = 60 },
             new PavilionCategory { PavilionCategoryId = 5, CategoryName = "B-43", PavilionType = PavilionType.Box, Description = "Middle South Side Pavilion", FarePerSquareFeet = 60 });

            modelBuilder.Entity<Pavilion>().HasData(
            new Pavilion { PavilionId = 1, PavilionName = "Conrner Pavilion", PavilionNumber = "P-101", Size = "10X12", SqFoot = 120, Rent = 6000, PavilionCategoryId = 1 },
            new Pavilion { PavilionId = 2, PavilionName = "Box Pavilion", PavilionNumber = "P-201", Size = "10X10", SqFoot = 100, Rent = 6000, PavilionCategoryId = 2 },
            new Pavilion { PavilionId = 3, PavilionName = "Island Pavilion", PavilionNumber = "P-202", Size = "8X8", SqFoot = 64, Rent = 3200, PavilionCategoryId = 3 },
            new Pavilion { PavilionId = 4, PavilionName = "Box Pavilion", PavilionNumber = "P-203", Size = "12X12", SqFoot = 144, Rent = 8640, PavilionCategoryId = 4 },
            new Pavilion { PavilionId = 5, PavilionName = "Box Pavilion", PavilionNumber = "P-204", Size = "12X12", SqFoot = 144, Rent = 8640, PavilionCategoryId = 5 });

            modelBuilder.Entity<Sponsor>().HasData(
            new Sponsor { SponsorId = 1, Phone = "01322110044", Email = "walton@gmail.com", CompanyName = "Walton", Industry = "Walton Hi-Tech Industries", City = "Dhaka", Website = "www.waltonbd.com" },
            new Sponsor { SponsorId = 2, Phone = "01422110044", Email = "akij@gmail.com", CompanyName = "Akij Group", Industry = "Akij Group of Company", City = "Gazipur", Website = "www.akijbd.com" },
            new Sponsor { SponsorId = 3, Phone = "01722110044", Email = "square@gmail.com", CompanyName = "Square Farma", Industry = "Square Pharmacitical Company", City = "Savar", Website = "www.squarebd.com" },
            new Sponsor { SponsorId = 4, Phone = "01822110044", Email = "daraz@gmail.com", CompanyName = "Daraz", Industry = "Daraz Online Shop", City = "All District", Website = "www.daraz.com" },
            new Sponsor { SponsorId = 5, Phone = "01922110044", Email = "pran@gmail.com", CompanyName = "Pran", Industry = "Pran Industry", City = "Narayanganj", Website = "www.pranbd.com" });

            modelBuilder.Entity<StaffDetail>().HasData(
            new StaffDetail { StaffDetailId = 1, StaffName = "AR Rahman", Phone = "01322110044", Email = "rahman@gmail.com", Gender = Gender.Male, Picture = "1.jpg" },
            new StaffDetail { StaffDetailId = 2, StaffName = "Runa", Phone = "01422110044", Email = "runa@gmail.com", Gender = Gender.Female, Picture = "2.jpg" },
            new StaffDetail { StaffDetailId = 3, StaffName = "Momin", Phone = "01722110044", Email = "momin@gmail.com", Gender = Gender.Male, Picture = "3.jpg" },
            new StaffDetail { StaffDetailId = 4, StaffName = "Mahmudul", Phone = "01822110044", Email = "mahmudul@gmail.com", Gender = Gender.Male, Picture = "4.jpg" },
            new StaffDetail { StaffDetailId = 5, StaffName = "Parul", Phone = "01922110044", Email = "parul@gmail.com", Gender = Gender.Female, Picture = "5.jpg" });

            modelBuilder.Entity<Attendence>().HasData(
            new Attendence { AttendenceId = 1, AttendenceDate = DateTime.Parse("2024-05-18"), StartTime = new TimeSpan(08, 55, 00), EndTime = new TimeSpan(22, 15, 00), StaffDetailId = 1 },
            new Attendence { AttendenceId = 2, AttendenceDate = DateTime.Parse("2024-05-18"), StartTime = new TimeSpan(08, 57, 00), EndTime = new TimeSpan(22, 10, 00), StaffDetailId = 2 },
            new Attendence { AttendenceId = 3, AttendenceDate = DateTime.Parse("2024-05-18"), StartTime = new TimeSpan(08, 58, 00), EndTime = new TimeSpan(22, 12, 00), StaffDetailId = 3 },
            new Attendence { AttendenceId = 4, AttendenceDate = DateTime.Parse("2024-05-18"), StartTime = new TimeSpan(08, 50, 00), EndTime = new TimeSpan(22, 05, 00), StaffDetailId = 4 },
            new Attendence { AttendenceId = 5, AttendenceDate = DateTime.Parse("2024-05-18"), StartTime = new TimeSpan(08, 53, 00), EndTime = new TimeSpan(22, 07, 00), StaffDetailId = 5 });

            modelBuilder.Entity<Organizer>().HasData(
            new Organizer { OrganizerId = 1, OrganizerName = "Progoti missino bangladesh", OrganizerEmail = "progoti123@gmial.com", OrganizerPhone = "0183157812", WebSiteUrl = "http//progogi.com", SortDescription = "Grievance Radressal Management" },
            new Organizer { OrganizerId = 2, OrganizerName = "Sebar Alo", OrganizerEmail = "Sebar78@gmial.com", OrganizerPhone = "0183157810", WebSiteUrl = "http//Saba.com", SortDescription = "sebar alo" },
            new Organizer { OrganizerId = 3, OrganizerName = "Jonogon er sohojogita", OrganizerEmail = "Jonogon21@gmial.com", OrganizerPhone = "0183857812", WebSiteUrl = "http//Jonogon21.com", SortDescription = "Jonogon er sohojogita" },
            new Organizer { OrganizerId = 4, OrganizerName = "Saba Organnizer", OrganizerEmail = "Saba23@gmial.com", OrganizerPhone = "0182357812", WebSiteUrl = "http//Saba.com", SortDescription = "" });

            modelBuilder.Entity<Fair>().HasData(
             new Fair { FairId = 1, FairName = "internatonal trade fair", StartDate = DateTime.Parse("2024-05-01"), EndDate = DateTime.Parse("2024-05-30"), Location = "Agargong", RegistrationDeadLine = DateTime.Parse("2024-04-30"), OrganizerId = 1 },
             new Fair { FairId = 2, FairName = "Book fair", StartDate = DateTime.Parse("2024-07-01"), EndDate = DateTime.Parse("2024-07-30"), Location = "Hatirjil",  RegistrationDeadLine = DateTime.Parse("2024-06-30"), OrganizerId = 2 },
             new Fair { FairId = 3, FairName = " Job fair", StartDate = DateTime.Parse("2024-06-01"), EndDate = DateTime.Parse("2024-06-30"), Location = "Dhaka",  RegistrationDeadLine = DateTime.Parse("2024-05-30"), OrganizerId = 3 },
             new Fair { FairId = 4, FairName = "Business fair", StartDate = DateTime.Parse("2024-04-01"), EndDate = DateTime.Parse("2024-04-30"), Location = "Agargong", RegistrationDeadLine = DateTime.Parse("2024-03-30"), OrganizerId = 4 });




            modelBuilder.Entity<Exhibitor>().HasData(
            new Exhibitor { ExhibitorId = 1, CompanyName = "viyla text", TradeLicence = "1254", ContactNumber = "0125486325", Email = "veyela125@gmail.com", Website = "wwwveyelatext123.com", Description = "a person whose work is being shown in an exhibiton", LogoUrl = "logo1.jpg" },
            new Exhibitor { ExhibitorId = 2, CompanyName = "Anower Group", TradeLicence = "3500", ContactNumber = "0155486325", Email = "Anower@gmail.com", Website = "wwwtext123.com", Description = "Exhibitors set up a wide variety of trade show display", LogoUrl = "logo2.jpg" },
            new Exhibitor { ExhibitorId = 3, CompanyName = "Mgf Group", TradeLicence = "1583", ContactNumber = "0121286325", Email = "Mgf@gmail.com", Website = "wwwMgf123.com", Description = "a person whose work is being shown in an exhibiton", LogoUrl = "logo3.jpg" }
      );
            modelBuilder.Entity<Booking>().HasData(

              new Booking { BookingId = 1, BookingDate = DateTime.Parse("2024-05-01"), NumberOfPavilion = 86, BookingAmount = 50000,  ExhibitorId = 1, PavilionId = 1 },
              new Booking { BookingId = 2, BookingDate = DateTime.Parse("2024-05-02"), NumberOfPavilion = 86, BookingAmount = 50000, ExhibitorId = 2, PavilionId = 2 },
              new Booking { BookingId = 3, BookingDate = DateTime.Parse("2024-05-03"), NumberOfPavilion = 86, BookingAmount = 50000, ExhibitorId = 3, PavilionId = 3 }

            );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Cosmetic" },
                new Category { CategoryId = 2, CategoryName = "Food" },
                new Category { CategoryId = 3, CategoryName = "Sanks" }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Beauty Product", Picture = "1.jpg", ExhibitorId = 1, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = " Dairy Product", Picture = "2.jpg", ExhibitorId = 2, CategoryId = 2 },
                new Product { ProductId = 3, ProductName = "Choocklet Product", Picture = "3.jpg", ExhibitorId = 3, CategoryId = 3 }

            );
            modelBuilder.Entity<ExhibitorPass>().HasData(
           new ExhibitorPass { ExhibitorPassId = 1, PersonName = "Thuaha", ValidFrom = DateTime.Parse("2024-05-01"), ValidUntil = DateTime.Parse("2024-06-01"), ExhibitorId = 1, PavilionId = 1 },
           new ExhibitorPass { ExhibitorPassId = 2, PersonName = "Fahad", ValidFrom = DateTime.Parse("2024-05-01"), ValidUntil = DateTime.Parse("2024-06-01"), ExhibitorId = 2, PavilionId = 2 },
           new ExhibitorPass { ExhibitorPassId = 3, PersonName = "Shila", ValidFrom = DateTime.Parse("2024-05-01"), ValidUntil = DateTime.Parse("2024-06-01"), ExhibitorId = 3, PavilionId = 3 }

                );
            modelBuilder.Entity<ExhibitorEntryExit>().HasData(
                new ExhibitorEntryExit { ExhibitorEntryExitId = 1, Date = DateTime.Parse("2024-05-03"), EntryTime = TimeSpan.Parse("10:10:15"), ExitTime = TimeSpan.Parse("12:10:20"), ExhibitorPassId = 1 },
                new ExhibitorEntryExit { ExhibitorEntryExitId = 2, Date = DateTime.Parse("2024-05-03"), EntryTime = TimeSpan.Parse("11:11:20"), ExitTime = TimeSpan.Parse("01:20:10"), ExhibitorPassId = 2 },
                new ExhibitorEntryExit { ExhibitorEntryExitId = 3, Date = DateTime.Parse("2024-05-03"), EntryTime = TimeSpan.Parse("10:30:15"), ExitTime = TimeSpan.Parse("11:10:30"), ExhibitorPassId = 3 }
                );
            modelBuilder.Entity<ParkingPass>().HasData(
                new ParkingPass { ParkingPassId = 1, NumberPlate = "DHAKA-D-11-9999", ParkingDate = DateTime.Parse("2024-05-09"), CheckInTime = TimeSpan.Parse("09:10:15"), CheckOutTime = TimeSpan.Parse("12:20:15"), Token = 11, ExhibitorId = 1 },
                new ParkingPass { ParkingPassId = 2, NumberPlate = "DHAKA-D-07-1234", ParkingDate = DateTime.Parse("2024-05-09"), CheckInTime = TimeSpan.Parse("11:00:00"), CheckOutTime = TimeSpan.Parse("01:30:15"), Token = 18, ExhibitorId = 2 },
                new ParkingPass { ParkingPassId = 3, NumberPlate = "DHAKA-D-20-9567", ParkingDate = DateTime.Parse("2024-05-09"), CheckInTime = TimeSpan.Parse("10:10:15"), CheckOutTime = TimeSpan.Parse("11:20:15"), Token = 13, ExhibitorId = 3 }
                );
            modelBuilder.Entity<Visitor>().HasData(
                    new Visitor { VisitorId = 1, FirstName = "A", LastName = "b", Email = "a123@gmail.com", Phone = "0214587525", Gender = Gender.Female, Nationality = "Bangladeshi" },
                     new Visitor { VisitorId = 2, FirstName = "B", LastName = "c", Email = "b123@gmail.com", Phone = "0214587525", Gender = Gender.Male, Nationality = "Indian" },
                    new Visitor { VisitorId = 3, FirstName = "C", LastName = "d", Email = "c123@gmail.com", Phone = "0214587525", Gender = Gender.Others, Nationality = "Pakistani" }
                );
            modelBuilder.Entity<VisitorEntry>().HasData(
                new VisitorEntry { VisitorEntryId = 1, EntryDate = DateTime.Parse("2024-05-05"), EntryTime = TimeSpan.Parse("10:10:10"), TicketId = 1 },
                new VisitorEntry { VisitorEntryId = 2, EntryDate = DateTime.Parse("2024-05-05"), EntryTime = TimeSpan.Parse("11:20:30"), TicketId = 2 },
                new VisitorEntry { VisitorEntryId = 3, EntryDate = DateTime.Parse("2024-05-06"), EntryTime = TimeSpan.Parse("12:11:10"), TicketId = 3 }
                );
         
            modelBuilder.Entity<Ticket>().HasData(
     new Ticket { TicketId = 1, IssueDate = DateTime.Parse("2024-5-15"), Quantity = 12, Price = 10, VisitorId = 1},
    new Ticket { TicketId = 2, IssueDate = DateTime.Parse("2024-5-10"), Quantity = 11, Price = 10, VisitorId = 2 },
     new Ticket { TicketId = 3, IssueDate = DateTime.Parse("2024-5-20"), Quantity = 1, Price = 500, VisitorId = 3}
    );
            modelBuilder.Entity<Payment>().HasData(
                new Payment { PaymentId = 1, Amount = 20000, PaymentDate = DateTime.Parse("2024-4-10"), PaymentThrough = PaymentThrough.CreditOrDebit, PaymentFor = PaymentFor.Pavilion, ExhibitorId = 1, TicketId = 1 },
                new Payment { PaymentId = 2, Amount = 110, PaymentDate = DateTime.Parse("2024-5-10"), PaymentThrough = PaymentThrough.bKash, PaymentFor = PaymentFor.Ticket, ExhibitorId = 2, TicketId = 2 },
                new Payment { PaymentId = 3, Amount = 500, PaymentDate = DateTime.Parse("2024-4-10"), PaymentThrough = PaymentThrough.Cash, PaymentFor = PaymentFor.Parking, ExhibitorId = 3, TicketId = 3 }

                );
            modelBuilder.Entity<FeedBack>().HasData(
                new FeedBack { FeedBackId = 1, Rating = 5, Comment = "very good ", VisitorId = 1 },
                new FeedBack { FeedBackId = 2, Rating = 4, Comment = " good ", VisitorId = 2 },
                new FeedBack { FeedBackId = 3, Rating = 5, Comment = "Khub valo ", VisitorId = 3 }
                );
            modelBuilder.Entity<Parking>().HasData(
                new Parking { ParkingId = 1, VehicleType = VehicleType.Car, NumberPlate = "DHAKA-D-10-9851", ParkingDate = DateTime.Parse("2024-5-10"), CheckInTime = TimeSpan.Parse("10:40:12"), CheckOutTime = TimeSpan.Parse("12:20:10"), Token = 05, ParkingFare = 100, VisitorId = 1 },
                new Parking { ParkingId = 2, VehicleType = VehicleType.Bike, NumberPlate = "DHAKA-H-15-5684", ParkingDate = DateTime.Parse("2024-5-10"), CheckInTime = TimeSpan.Parse("11:40:10"), CheckOutTime = TimeSpan.Parse("12:30:10"), Token = 07, ParkingFare = 80, VisitorId = 2 },
                new Parking { ParkingId = 3, VehicleType = VehicleType.Car, NumberPlate = "DHAKA-D-10-2354", ParkingDate = DateTime.Parse("2024-5-10"), CheckInTime = TimeSpan.Parse("10:40:12"), CheckOutTime = TimeSpan.Parse("12:20:10"), Token = 05, ParkingFare = 150, VisitorId = 3 }
                );
            modelBuilder.Entity<Venue>().HasData(
                new Venue { VenueId = 1, VenueName = "Venue 1", Location = "West South Corner", Capacity = 300 },
                new Venue { VenueId = 2, VenueName = "Venue 2", Location = "West side", Capacity = 400 },
                new Venue { VenueId = 3, VenueName = "Venue 3", Location = " South Corner", Capacity = 200 }
                );
            modelBuilder.Entity<Event>().HasData(
                new Event { EventId = 1, EventName = "New Product Launch", EventDescription = "New Product Launch", EventDate = DateTime.Parse("2024-5-5"), StartTime = TimeSpan.Parse("11:00:00"), EndTime = TimeSpan.Parse("12:00:00"), SpeakerName = "Mr. Murad", SpeakerDetails = "CEO of Walton", VenueId = 1 },
                new Event { EventId = 2, EventName = "Brand Promotion", EventDescription = "New Brand Launch", EventDate = DateTime.Parse("2024-5-5"), StartTime = TimeSpan.Parse("12:00:00"), EndTime = TimeSpan.Parse("01:00:00"), SpeakerName = "Ms. Shila", SpeakerDetails = "Md of Lorial", VenueId = 2 },
                new Event { EventId = 3, EventName = "New Product Launch", EventDescription = "New Product Launch", EventDate = DateTime.Parse("2024-5-5"), StartTime = TimeSpan.Parse("12:00:00"), EndTime = TimeSpan.Parse("12:00:00"), SpeakerName = "Mr. Fahad", SpeakerDetails = "CEO of Akij", VenueId = 3 }
                );
            modelBuilder.Entity<EventReg>().HasData(
                new EventReg { EventRegId = 1, EventId = 1, VisitorId = 1 },
                new EventReg { EventRegId = 2, EventId = 2, VisitorId = 2 },
                new EventReg { EventRegId = 3, EventId = 3, VisitorId = 3 }
                );
        }

    }
}
