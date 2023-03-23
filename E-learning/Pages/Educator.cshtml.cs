using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_learning.Pages
{
    public class EducatorModel : PageModel
    {
        private readonly E_learningContext _contextuser;
        private readonly UserManager<IdentityUser> _userManager;
        public EducatorModel ( UserManager<IdentityUser> userManager, E_learningContext contextuser)
        {
            
            _userManager = userManager;
            _contextuser = contextuser;
        }
        public void OnGet()
        {

        }
        public async Task OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            await _userManager.AddToRoleAsync(user, "AdminManager");
        }
    }
}
