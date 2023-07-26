using Microsoft.EntityFrameworkCore;


namespace Cards.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=CardsDB1;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasKey(nameof(Card.Id), nameof(Card.IdSecond));
            //base.OnModelCreating(modelBuilder);
        }
    }

}
