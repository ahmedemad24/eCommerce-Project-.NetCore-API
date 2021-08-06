using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbCRUD.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HousePhoto> HousePhotos { get; set; }
        public DbSet<HouseFeedback> HouseFeedbacks { get; set; }
        public DbSet<PersonFeedback> PersonFeedbacks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add your own configuration here
            modelBuilder.Entity<PersonFeedback>().HasOne(p => p.PersonAsHost).WithMany(p => p.FeedbacksAsHost).HasForeignKey("PersonHostId").IsRequired();
            modelBuilder.Entity<PersonFeedback>().HasOne(p => p.PersonAsCustomer).WithMany(p => p.FeedbacksAsCustomer).HasForeignKey("PersonCustomerId").IsRequired();
            modelBuilder.Entity<HousePhoto>().HasKey(p => new { p.HouseId, p.HousePhotos });
;
            ;
        }
    }
}
