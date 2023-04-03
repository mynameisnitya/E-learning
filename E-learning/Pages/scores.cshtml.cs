using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Areas.Identity.Data;
using E_learning.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Pages
{
    public class scoresModel : PageModel
    {
     
        private readonly E_learningContext _contextuser;
        private readonly TutorialContext _context;
       

        private readonly UserManager<IdentityUser> _userManager;
        public IdentityUser CurrentUser { get; set; }
        public List<QuizScore> QuizScores { get; set; } = new List<QuizScore>();

        public IList<Tutorial> Tutorials { get; set; }
        public scoresModel(UserManager<IdentityUser> userManager, E_learningContext contextuser, TutorialContext context)
        {
            _context = context;
            _userManager = userManager;
            _contextuser = contextuser;
        }

        public async Task<IActionResult> OnGetAsync()
        {

           


            Tutorials = await _context.Tutorials
 
             .Include(t => t.QuizQuestions)
             .AsNoTracking()
             .ToListAsync();
            var user = await _userManager.GetUserAsync(User);
            
          
      
            if (user == null || user.Id == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {

                var roles = await _userManager.GetRolesAsync(user);
                var rolesString = string.Join(", ", roles);
                Console.WriteLine("Roles: {0}", rolesString);

                CurrentUser = user;



                QuizScores = await _contextuser.QuizScores
                    .Where(t => t.UserId == user.Id)



                    .AsNoTracking()
                    .ToListAsync();
                return Page();
            }
        }
    }
}
