using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Models;

namespace EventHorizon.Data;

public class EventHorizonDbContext : IdentityDbContext<IdentityUser>
{
  // DbSet properties for our models
  public DbSet<UserProfile> UserProfiles { get; set; }
  public DbSet<Event> Events { get; set; }
  public DbSet<Venue> Venues { get; set; }
  public DbSet<EventCategory> EventCategories { get; set; }
  public DbSet<Registration> Registrations { get; set; }

  public EventHorizonDbContext(DbContextOptions<EventHorizonDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Event>()
      .Property(e => e.DateTime)
      .HasColumnType("timestamp without time zone");
    
    base.OnModelCreating(modelBuilder);

    // Configure entity relationships
    ConfigureRelationships(modelBuilder);

    // Seed data
    SeedData(modelBuilder);
  }

  private void ConfigureRelationships(ModelBuilder modelBuilder)
  {
    // Configure UserProfile to IdentityUser relationship (one-to-one)
    modelBuilder.Entity<UserProfile>()
        .HasOne(up => up.IdentityUser)
        .WithOne()
        .HasForeignKey<UserProfile>(up => up.IdentityUserId);

    // Configure Event to Venue relationship (one-to-many)
    modelBuilder.Entity<Event>()
        .HasOne(e => e.Venue)
        .WithMany(v => v.Events)
        .HasForeignKey(e => e.VenueId)
        .OnDelete(DeleteBehavior.Restrict);

    // Configure Event to EventCategory relationship (one-to-many)
    modelBuilder.Entity<Event>()
        .HasOne(e => e.EventCategory)
        .WithMany(ec => ec.Events)
        .HasForeignKey(e => e.EventCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    // Configure Registration to Event relationship (one-to-many)
    modelBuilder.Entity<Registration>()
        .HasOne(r => r.Event)
        .WithMany(e => e.Registrations)
        .HasForeignKey(r => r.EventId)
        .OnDelete(DeleteBehavior.Restrict);

    // Configure Registration to UserProfile relationship (one-to-many)
    modelBuilder.Entity<Registration>()
        .HasOne(r => r.UserProfile)
        .WithMany(up => up.Registrations)
        .HasForeignKey(r => r.UserProfileId)
        .OnDelete(DeleteBehavior.Restrict);
  }

  private void SeedData(ModelBuilder modelBuilder)
  {
    // Seed roles
    SeedRoles(modelBuilder);

    // Seed users
    SeedUsers(modelBuilder);

    // Seed user profiles
    SeedUserProfiles(modelBuilder);

    // Assign users to roles
    SeedUserRoles(modelBuilder);

    // Seed products
    SeedVenues(modelBuilder);
    SeedEvents(modelBuilder);
    SeedEventCategories(modelBuilder);
  }

  private void SeedRoles(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole
        {
          Id = "fab4fac1-c546-41de-aebc-a14da6895711",
          Name = "Admin",
          NormalizedName = "ADMIN"
        },
        new IdentityRole
        {
          Id = "c7b013f0-5201-4317-abd8-c211f91b7330",
          Name = "User",
          NormalizedName = "USER"
        }
    );
  }

  private void SeedUsers(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<IdentityUser>().HasData(
        // Admin user
        new IdentityUser
        {
          Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
          UserName = "admin1",
          Email = "admin1@eventhorizon.com",
          NormalizedEmail = "ADMIN1@EVENTHORIZON.COM",
          NormalizedUserName = "ADMIN1",
          EmailConfirmed = true,
          PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin123!")
        },

        // Baker users
        new IdentityUser
        {
          Id = "e2cfe4e6-5437-4efb-9a66-8d1371796bda",
          UserName = "user1",
          Email = "user1@eventhorizon.com",
          NormalizedEmail = "USER1@EVENTHORIZON.COM",
          NormalizedUserName = "USER1",
          EmailConfirmed = true,
          PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "User123!")
        }
    );
  }

  private void SeedUserProfiles(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<UserProfile>().HasData(
        // Admin profile
        new UserProfile
        {
          Id = 1,
          IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
          FirstName = "Admin",
          LastName = "Profile"
        },

        // User profiles
        new UserProfile
        {
          Id = 2,
          IdentityUserId = "e2cfe4e6-5437-4efb-9a66-8d1371796bda",
          FirstName = "User",
          LastName = "Profile"
        }
    );
  }

  private void SeedUserRoles(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        // Admin role assignment
        new IdentityUserRole<string>
        {
          RoleId = "fab4fac1-c546-41de-aebc-a14da6895711",
          UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
        },

        // User role assignments
        new IdentityUserRole<string>
        {
          RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330",
          UserId = "e2cfe4e6-5437-4efb-9a66-8d1371796bda"
        }
    );
  }

  private void SeedVenues(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Venue>().HasData(
      new Venue
      {
        Id = 1,
        Name = "Red Rocks Amphitheater",
        Address = "18300 W. Alameda Parkway",
        City = "Morrison",
        State = "CO",
        ZipCode = "80465",
        MaxCapacity = 9525,
        Description = "Open-air amphitheater in the western United States near Morrison, Colorado"
      },
      new Venue
      {
        Id = 2,
        Name = "The Chattanooga Convention Center",
        Address = "1 Carter Plaza",
        City = "Chattanooga",
        State = "TN",
        ZipCode = "37402",
        MaxCapacity = 50000,
        Description = "Convention Center with 100,000 square feet of open exhibit-space and a tried-and-true process for facilitating large-scale events"
      },
      new Venue
      {
        Id = 3,
        Name = "Ten Forward",
        Address = "Deck 10",
        City = "USS Enterprise D",
        State = "Forward Section 1",
        ZipCode = "101010",
        MaxCapacity = 30,
        Description = "lounge and recreation facility for Starfleet passengers"
      }
    );
  }

  private void SeedEvents(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Event>().HasData(
      new Event
      {
        Id = 1,
        Name = "MANÁ: VIVIR SIN AIRE TOUR",
        Description = "Live Nation is thrilled to announce MANÁ live at Red Rocks Amphitheatre",
        DateTime = new DateTime(2025,9,10,0,0,0),
        MaxAttendees = 9525,
        VenueId = 1,
        EventCategoryId = 3
      },
      new Event
      {
        Id = 2,
        Name = "Billy Idol and Joan Jett & the Blackhearts",
        Description = "Live Nation & AEG are thrilled to announce BILLY IDOL live at Red Rocks Amphitheatre",
        DateTime = new DateTime(2025,9,3,0,0,0),
        MaxAttendees = 9525,
        VenueId = 1,
        EventCategoryId = 3
      },
      new Event
      {
        Id = 3,
        Name = "Open mic night with Jean-Luc",
        Description = "Come experience the musical stylings of Starfleet's finest captain",
        DateTime = new DateTime(2365,2,22,0,0,0),
        MaxAttendees = 30,
        VenueId = 3,
        EventCategoryId = 3
      },
      new Event
      {
        Id = 4,
        Name = "Warp Drives and You!",
        Description = "Learn how space warp has changed our lives for the better (and worse)",
        DateTime = new DateTime(2365,11,3,0,0,0),
        MaxAttendees = 30,
        VenueId = 3,
        EventCategoryId = 5
      },
      new Event
      {
        Id = 5,
        Name = "How to treat your Tribble",
        Description = "A hands-on workshop focused on tribble care and maintenance",
        DateTime = new DateTime(2366,6,1,0,0,0),
        MaxAttendees = 9525,
        VenueId = 3,
        EventCategoryId = 2
      },
      new Event
      {
        Id = 6,
        Name = "Rotary Club Chattanooga",
        Description = "Club designed to help the up-building of Chattanooga and vicinity, to encourage the exchange of business ideas and methods",
        DateTime = new DateTime(2025,9,4,0,0,0),
        MaxAttendees = 1000,
        VenueId = 2,
        EventCategoryId = 4
      },
      new Event
      {
        Id = 7,
        Name = "Chattanooga Comic Con",
        Description = "Chattanooga's newest comic and pop culture convention",
        DateTime = new DateTime(2025,9,27,0,0,0),
        MaxAttendees = 40000,
        VenueId = 2,
        EventCategoryId = 1
      },
      new Event
      {
        Id = 8,
        Name = "Erlanger Neuroscience Conference",
        Description = "Symposium offering neuroscience professionals an opportunity to network and grow in their fields",
        DateTime = new DateTime(2025,9,26,0,0,0),
        MaxAttendees = 10000,
        VenueId = 2,
        EventCategoryId = 1
      },
      new Event
      {
        Id = 9,
        Name = "American Grappling Federation",
        Description = "Come get yo ass whooped",
        DateTime = new DateTime(2025,9,6,0,0,0),
        MaxAttendees = 1000,
        VenueId = 2,
        EventCategoryId = 1
      },
      new Event
      {
        Id = 10,
        Name = "Dare to Dance benefiting the Kidney Foundation",
        Description = "Time to dance that kidney failure out the door!",
        DateTime = new DateTime(2025,10,25,0,0,0),
        MaxAttendees = 2000,
        VenueId = 2,
        EventCategoryId = 2
      }
    );
  }
      private void SeedEventCategories(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<EventCategory>().HasData(
      new EventCategory
      {
        Id = 1,
        Name = "Conference",
        Description = "A business or entertainment conference"
      },
      new EventCategory
      {
        Id = 2,
        Name = "Workshop",
        Description = "A workshop for learning new skills"
      },
      new EventCategory
      {
        Id = 3,
        Name = "Concert",
        Description = "A musical performance"
      },
      new EventCategory
      {
        Id = 4,
        Name = "Networking",
        Description = "A chance to meet busisness professionals and peers"
      },
      new EventCategory
      {
        Id = 5,
        Name = "Seminar",
        Description = "An informative lecture for education"
      }
    );
  }
}
