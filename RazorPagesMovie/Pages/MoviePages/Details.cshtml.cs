using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.MoviePages;

public class DetailsModel : PageModel
{
    private readonly RazorPagesMovieContext _context;
    public DetailsModel(RazorPagesMovieContext context)
    {
        _context = context;
    }

    public Movie Movie { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        if (movie is null)
        {
            return NotFound();
        }
        else
        {
            Movie = movie;
        }

        return Page();
    }
}
