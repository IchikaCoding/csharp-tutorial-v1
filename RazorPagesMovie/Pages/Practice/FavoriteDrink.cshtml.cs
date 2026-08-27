using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Practice
{
    public class FavoriteDrinkModel : PageModel
    {
        [BindProperty]
        public FavoriteDrinkInput Input { get; set; } = new();
        public bool Submitted { get; private set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // 初期値falseだからこれいらない↓
                //Submitted = false;
                return Page();
            }
            Console.WriteLine("OnPost()が実行できました！");
            Submitted = true;
            return Page();
        }
    }
}
