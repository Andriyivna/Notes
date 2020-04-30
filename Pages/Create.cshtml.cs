using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Notes.Models;

namespace Notes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationContext _context;
        [BindProperty]
        public List<Note> NotesList { get; set; }
        [BindProperty]
        public Note NewNote { get; set; }
        public CreateModel(ApplicationContext db)
        {
            _context = db;
        }
        public void OnGet()
        {
            NotesList = _context.NotesDb.AsNoTracking().ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.NotesDb.Add(NewNote);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }

     
    }
}




