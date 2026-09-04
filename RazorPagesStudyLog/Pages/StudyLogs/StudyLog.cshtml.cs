using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesStudyLog.Models;

namespace RazorPagesStudyLog.Pages.StudyLogs
{
    public class StudyLogModel : PageModel
    {
        [BindProperty]
        // 入力値全部　Inputクラスから取ってくる
        public StudyLogInput studyLogInput { get; set; } = new StudyLogInput();
        // 送信した時刻とか
        public bool Submited { get; private set; }
        public DateTime SubmitedAt { get; private set; }
        // 送信したかどうかのbool
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost()が実行されました");
            SubmitedAt = DateTime.Now;
            Submited = true;
            Console.WriteLine("SubmitedAt" + SubmitedAt);
            Console.WriteLine("Submited" + Submited);
            return Page();
        }
    }
}
