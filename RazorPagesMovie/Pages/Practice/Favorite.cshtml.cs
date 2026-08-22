using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages.Practice
{
    public class FavoriteModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; } = "";
        public string ResultMessage { get; private set; } = "";
        public void OnGet() { }
        public void OnPost()
        {
            ResultMessage = $"{Name}さん、Post成功しましたyo🎉";
            Console.WriteLine("ResultMessage: " + ResultMessage);
        }
    }
}