using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QASite.Data
{
    public class QADataContext : DbContext
    {
        private string _connectionString;
        public QADataContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<QuestionTags>()
                .HasKey(qt => new { qt.QuestionId, qt.TagID });

            modelBuilder.Entity<Likes>()
                .HasKey(l => new { l.UserId, l.QuestionID });

            modelBuilder.Entity<LikedAnswers>()
                .HasKey(l => new { l.UserId, l.AnswerId });
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTags> QuestionsTags { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<LikedAnswers> LikedAnswers { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<User> User { get; set; }
    }
}
