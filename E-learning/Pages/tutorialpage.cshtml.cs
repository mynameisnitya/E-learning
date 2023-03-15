using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Pages
{
    public class tutorialpageModel : PageModel
    {
        private readonly TutorialContext _context;
      
        public tutorialpageModel(TutorialContext context)
        {
            _context = context;
        }

        public IList<Tutorial> Tutorials { get; set; }

        public string language { get; set; }
        public async Task OnGetAsync(String Id)

        {
            language = Id;


            Tutorials = await _context.Tutorials
              .Include(t => t.QuizQuestions)
              .AsNoTracking()
              .ToListAsync();
             Page();
            
        }
     

    }


}
