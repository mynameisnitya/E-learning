using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Areas.Identity.Data;
using E_learning.Data;
using E_learning.Pages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace E_learning.Pages
{
    public class tutorialpageModel : PageModel
    {
        private readonly TutorialContext _context;
        private readonly E_learningContext _contextuser;
        public IdentityUser CurrentUser { get; set; }
       
        private readonly UserManager<IdentityUser> _userManager;
        public tutorialpageModel(TutorialContext context, UserManager<IdentityUser> userManager, E_learningContext contextuser)
        {
            _context = context;
            _userManager = userManager;
            _contextuser = contextuser;
        }
        public List<QuizScore> QuizScores { get; set; } = new List<QuizScore>();

        public IList<Tutorial> Tutorials { get; set; }
        public string language { get; set; }
        public int CurrentIndex { get; set; }
        public Tutorial CurrentTutorial { get; set; }
        public int NextIndex { get; set; }
        public bool HasNextTutorial => NextIndex < Tutorials.Count;
        public int PreviousIndex { get; set; }
        public bool HasPreviousTutorial => PreviousIndex >= 0;

        public async Task<IActionResult> OnGetAsync(string id, int index = 0)
        {

            var user = await _userManager.GetUserAsync(User);
            CurrentUser = user;


            language = id;
            Tutorials = await _context.Tutorials
                .Where(t => t.ProgrammingLanguage == id)


                .Include(t => t.QuizQuestions)
                .AsNoTracking()
                .ToListAsync();

            if (Tutorials.Count == 0)
            {
                return NotFound();
            }

            CurrentIndex = index;
            CurrentTutorial = Tutorials.ElementAtOrDefault(CurrentIndex);

            if (CurrentTutorial == null)
            {
                return NotFound();
            }

            NextIndex = CurrentIndex + 1;
            PreviousIndex = CurrentIndex - 1;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int quizid)
        {
            int score=0;
            //string correctanswer="";

            var user = await _userManager.GetUserAsync(User);
          
            var result = new QuizScore();

            result.QuizId = quizid;
            if (Request.Query["answer"].ToString()==Request.Query["correctanswer"].ToString())
            {
              score = 1;
            }
            else
            {
                score = 0;
            }
            result.Score = score ;

            if (user == null || user.Id == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else {
                result.UserId = user.Id;
                _contextuser.QuizScores.Add(result);
                _contextuser.SaveChanges();
                TempData["Message"] = "Your score has been saved successfully!";
                return Page();//add redirect to scores page
               
            }
        }





    }

  
}