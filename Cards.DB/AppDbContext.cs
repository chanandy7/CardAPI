using Microsoft.EntityFrameworkCore;


namespace Cards.DB
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Server=(localdb)\mssqllocaldb;Database=CardsDB2;Trusted_Connection=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasKey(nameof(Card.Id), nameof(Card.IdSecond));
            base.OnModelCreating(modelBuilder);
        }
    }

}
