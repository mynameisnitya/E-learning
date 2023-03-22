using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Data
{
    public class QuizScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int QuizId { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }

    public class E_learningContext : IdentityDbContext<IdentityUser>
    {
        public E_learningContext(DbContextOptions<E_learningContext> options)
            : base(options)
        {
        }

        public DbSet<QuizScore> QuizScores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure QuizScore entity
            builder.Entity<QuizScore>()
                .HasKey(qs => qs.Id);

            builder.Entity<QuizScore>()
                .HasOne(qs => qs.User)
                .WithMany()
                .HasForeignKey(qs => qs.UserId);
        }
    }
}
