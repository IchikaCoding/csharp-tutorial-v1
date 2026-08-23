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
            // Nameプロパティに入力値が入るのは、OnPostが実行される前。
            // だからここが実行される頃にはNameが入っています
            ResultMessage = $"{Name}さん、Post成功しましたyo🎉";
            Console.WriteLine("ResultMessage: " + ResultMessage);
        }
    }
}