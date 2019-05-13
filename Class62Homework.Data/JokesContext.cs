using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Class62Homework.Data
{
    public class JokesContext : DbContext
    {
        private string _connectionString;

        public JokesContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLikedJokes>()
                .HasKey(ul => new { ul.UserId, ul.JokeId });
            
            modelBuilder.Entity<UserLikedJokes>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserLikedJokes)
                .HasForeignKey(u => u.UserId);
            
            modelBuilder.Entity<UserLikedJokes>()
                .HasOne(ul => ul.Joke)
                .WithMany(j => j.UserLikedJokes)
                .HasForeignKey(j => j.JokeId);
        }

        public DbSet<Joke> Jokes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLikedJokes> UserLikedJokes { get; set; }
    }
}
