using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Pages;
using Microsoft.EntityFrameworkCore;
namespace E_learning.Areas.Identity.Data
{
   

    public class TutorialContext : DbContext
    {

        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
       

        public TutorialContext(DbContextOptions<TutorialContext> options)
     : base(options)
        { }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TutorialDb;Trusted_Connection=True;");
        }
    }

}
