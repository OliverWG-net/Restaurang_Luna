using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Restaurang_luna.Models;

namespace Restaurang_luna.Data
{
    public class LunaDbContext : DbContext
    {
        public LunaDbContext(DbContextOptions<LunaDbContext> options) : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = Guid.Parse("8f4f1a1e-3a08-4f6a-9a8b-6b2a7d2c4f10"),
                    UserName = "Oliver",
                    PasswordHash = "G/ZurCUC4QMRDqXwZwQRsA==;ULAG2GRXijaJasIKRhSJUTZtZQGTsAm3UTjPE7xsEBY="
                }
            );

            modelBuilder.Entity<Booking>()
            .Property(b => b.RowVersion)
            .IsRowVersion();


            modelBuilder.Entity<Booking>()
            .Property(b => b.Status)
            .HasConversion<string>();

            modelBuilder.Entity<Booking>()
            .Property<bool>("IsActive")
            .HasColumnName("IsActive")
            .ValueGeneratedOnAddOrUpdate()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<Booking>()
                .HasIndex("TableId_FK", nameof(Booking.StartAt))
                .HasDatabaseName("UX_Bookings_TableId_StartAt_Active")
                .IsUnique()
                .HasFilter("[IsActive] = 1");

            modelBuilder.Entity<Table>().HasData(
             new Table { TableId = 1, TableNr = "T1", Capacity = 2 },
               new Table { TableId = 2, TableNr = "T2", Capacity = 4 },
               new Table { TableId = 3, TableNr = "T3", Capacity = 4 },
               new Table { TableId = 4, TableNr = "T4", Capacity = 6 },
               new Table { TableId = 5, TableNr = "T5", Capacity = 8 }
              );

            modelBuilder.Entity<Menu>().HasData(
            new Menu
            {
                MenuId = 1,
                MenuItem = "Spaghetti Carbonara",
                Price = 120,
                Description = "Classic pasta with creamy egg sauce, pancetta, and parmesan.",
                IsPopular = true,
                PicutreUrl = "https://vasterbottensost.com/wp-content/uploads/2025/03/carbonara.jpg"
            },
                new Menu
                {
                    MenuId = 2,
                    MenuItem = "Penne Arrabbiata",
                    Price = 100,
                    Description = "Spicy tomato sauce with garlic, chili, and fresh parsley.",
                    IsPopular = false,
                    PicutreUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-3mx7DCimLIn1_B7a5JbY8SvzPT4jpahzeQ&s"
                },
                new Menu
                {
                    MenuId = 3,
                    MenuItem = "Fettuccine Alfredo",
                    Price = 140,
                    Description = "Rich butter and parmesan sauce with silky fettuccine pasta.",
                    IsPopular = true,
                    PicutreUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQpfJYxux157r0GogK-XhDhxjnTvmQYVgaPUg&s"
                },
                new Menu
                {
                    MenuId = 4,
                    MenuItem = "Lasagna Bolognese",
                    Price = 160,
                    Description = "Layered pasta with beef ragù, béchamel, and parmesan.",
                    IsPopular = true,
                    PicutreUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkkjn3cMbzsNByc3-WkOob9H0c3ioINWgE5g&s"
                },
                new Menu
                {
                    MenuId = 5,
                    MenuItem = "Margherita Pizza",
                    Price = 110,
                    Description = "Wood-fired pizza with tomato, mozzarella, and fresh basil.",
                    IsPopular = true,
                    PicutreUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSpBDIHxbtMe_C5SJhO2AujarCzMCKXQpO6Qw&s"
                },
                new Menu
                {
                    MenuId = 6,
                    MenuItem = "Red Wine (Glass)",
                    Price = 75,
                    Description = "Full-bodied Italian red wine, served by the glass.",
                    IsPopular = false,
                    PicutreUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTLeLeVxyjldu8sSuMuPdL3EgzMT8Y68uWECg&s"
                },
                new Menu
                {
                    MenuId = 7,
                    MenuItem = "White Wine (Glass)",
                    Price = 75,
                    Description = "Crisp Italian white wine, served chilled by the glass.",
                    IsPopular = false,
                    PicutreUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSiRqs99I_sHPJ6eWHmEKrMYo66ZWYNLDNnqg&s"
                },
                new Menu
                {
                    MenuId = 8,
                    MenuItem = "Espresso",
                    Price = 40,
                    Description = "Strong Italian coffee served in a small cup.",
                    IsPopular = true,
                    PicutreUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRqpOw-kv9-J1OzjJICx_KZEU-KhhLYfYKF1w&s"
                },
                new Menu
                {
                    MenuId = 9,
                    MenuItem = "Tiramisu",
                    Price = 90,
                    Description = "Classic Italian dessert with mascarpone and espresso.",
                    IsPopular = true,
                    PicutreUrl = "https://staticcookist.akamaized.net/wp-content/uploads/sites/22/2024/09/THUMB-VIDEO-2_rev1-56.jpeg"
                }
            );


            modelBuilder.Entity<Customer>().HasData(
        new Customer
        {
            CustomerId = Guid.Parse("b5b55e03-2d42-4b64-9a1d-3a4fd0a9e6a1"),
            FirstName = "Alice",
            LastName = "Johansson",
            PhoneNumber = "+46701234567",
            Email = "alice@example.com"
        },
               new Customer
               {
                   CustomerId = Guid.Parse("a1c7d3a6-8b10-4a1f-90a4-7f2f0d9b1234"),
                   FirstName = "Bob",
                   LastName = "Svensson",
                   PhoneNumber = "+46709876543",
                   Email = "bob@example.com"
               },
               new Customer
               {
                   CustomerId = Guid.Parse("c9e2f4b1-7a27-4c9c-8b3b-0b4c8a7f55e2"),
                   FirstName = "Klara",
                   LastName = null,
                   PhoneNumber = "+46855500010",
                   Email = null
               }
           );

            var createdAt = new DateTimeOffset(2025, 08, 01, 12, 00, 00, TimeSpan.Zero);


            modelBuilder.Entity<Booking>().HasData(
                 new
                 {
                     BookingId = Guid.Parse("d6a6d2f0-1f44-4b77-9b6c-0c9d1a2b3c4d"),
                     StartAt = new DateTimeOffset(2025, 09, 01, 17, 00, 00, TimeSpan.Zero),
                     Duration = 90,
                     GuestAmount = 2,
                     TableId_FK = 1,
                     CustomerId_FK = Guid.Parse("b5b55e03-2d42-4b64-9a1d-3a4fd0a9e6a1"),

                     // private-set properties
                     Status = BookingStatus.Confirmed,
                     SnapshotName = "Alice Johansson",
                     SnapshotPhone = "+46701234567",
                     SnapshotEmail = "alice@example.com",
                     SnapshotNotes = (string?)null,
                     IsSnapshotLocked = true,

                     CheckedInAt = (DateTimeOffset?)null,
                     CompletedAt = (DateTimeOffset?)null,
                     CancelledAt = (DateTimeOffset?)null,

                     CreatedAt = new DateTimeOffset(2025, 08, 01, 12, 00, 00, TimeSpan.Zero),
                     UpdatedAt = (DateTimeOffset?)null
                 },
                 new
                 {
                     BookingId = Guid.Parse("a2b3c4d5-e6f7-4a89-9b01-23456789abcd"),
                     StartAt = new DateTimeOffset(2025, 09, 02, 18, 30, 00, TimeSpan.Zero),
                     Duration = 90,
                     GuestAmount = 4,
                     TableId_FK = 2,
                     CustomerId_FK = Guid.Parse("a1c7d3a6-8b10-4a1f-90a4-7f2f0d9b1234"),

                     Status = BookingStatus.Confirmed,
                     SnapshotName = "Bob Svensson",
                     SnapshotPhone = "+46709876543",
                     SnapshotEmail = "bob@example.com",
                     SnapshotNotes = (string?)null,
                     IsSnapshotLocked = true,

                     CheckedInAt = (DateTimeOffset?)null,
                     CompletedAt = (DateTimeOffset?)null,
                     CancelledAt = (DateTimeOffset?)null,

                     CreatedAt = new DateTimeOffset(2025, 08, 01, 12, 00, 00, TimeSpan.Zero),
                     UpdatedAt = (DateTimeOffset?)null
                 },
                 new
                 {
                     BookingId = Guid.Parse("f1e2d3c4-b5a6-4789-8123-9abcdeff0011"),
                     StartAt = new DateTimeOffset(2025, 09, 03, 19, 00, 00, TimeSpan.Zero),
                     Duration = 90,
                     GuestAmount = 6,
                     TableId_FK = 4,
                     CustomerId_FK = Guid.Parse("c9e2f4b1-7a27-4c9c-8b3b-0b4c8a7f55e2"),

                     Status = BookingStatus.Confirmed,
                     SnapshotName = "Klara",
                     SnapshotPhone = "+46855500010",
                     SnapshotEmail = (string?)null,
                     SnapshotNotes = (string?)null,
                     IsSnapshotLocked = true,

                     CheckedInAt = (DateTimeOffset?)null,
                     CompletedAt = (DateTimeOffset?)null,
                     CancelledAt = (DateTimeOffset?)null,

                     CreatedAt = new DateTimeOffset(2025, 08, 01, 12, 00, 00, TimeSpan.Zero),
                     UpdatedAt = (DateTimeOffset?)null
                 }
             );
        }
    }
}