using GameZone.Models;

namespace GameZone
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        { }

        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Device> Devices { get; set; } = default!;
        public DbSet<GameDevice> GameDevices { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Adventure" },
                new Category { Id = 3, Name = "Indie" },
                new Category { Id = 4, Name = "RPG" },
                new Category { Id = 5, Name = "Simulation" },
                new Category { Id = 6, Name = "Strategy" },
                new Category { Id = 7, Name = "Sports" },
                new Category { Id = 8, Name = "Racing" },
                new Category { Id = 9, Name = "Fighting" }
            );

            // Seed Data 
            modelBuilder.Entity<Device>().HasData(
                new Device { Id = 1, Name = "PC" ,Icon = "pc.png" },
                new Device { Id = 2, Name = "Xbox" ,Icon = "xbox.png" },
                new Device { Id = 3, Name = "Playstation" ,Icon = "playstation.png" },
                new Device { Id = 4, Name = "Mobile" , Icon = "mobile.png" }
            );

            
            modelBuilder.Entity<GameDevice>()
                .HasKey(e => new { e.GameId, e.DeviceId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
