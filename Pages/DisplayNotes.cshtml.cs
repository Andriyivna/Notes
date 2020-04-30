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
    public class DisplayNotesModel : PageModel
    {
        private readonly ApplicationContext _context;
        [BindProperty]
        public Note note { get; set; }

        public List<Note> NotesList { get; set; }

        public DisplayNotesModel(ApplicationContext db)
        {
            _context = db;
        }
      
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            NotesList = _context.NotesDb.AsNoTracking().ToList();
            if (id == null)
            {
                return NotFound();
            }

            note = await _context.NotesDb.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!_context.NotesDb.Any(e => e.Id == note.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("Index");
        }

       
    }
}