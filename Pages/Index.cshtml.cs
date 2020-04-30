using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Notes.Models;
using System.Threading.Tasks;

namespace Notes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;
        public List<Note> NotesList { get; set; }
        public IndexModel(ApplicationContext db)
        {
            _context = db;
        }
        public void OnGet()
        {
            NotesList = _context.NotesDb.AsNoTracking().ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _context.NotesDb.FindAsync(id);

            if (product != null)
            {
                _context.NotesDb.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }


    }
}