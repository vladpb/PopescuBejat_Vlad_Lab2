using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PopescuBejat_Vlad_Lab2.Data;
using PopescuBejat_Vlad_Lab2.Models;
using PopescuBejat_Vlad_Lab2.Models.ViewModels;

namespace PopescuBejat_Vlad_Lab2.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly PopescuBejat_Vlad_Lab2.Data.PopescuBejat_Vlad_Lab2Context _context;

        public IndexModel(PopescuBejat_Vlad_Lab2.Data.PopescuBejat_Vlad_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        public CategoryIndexData CategoryIndexData { get; set; }


        public async Task OnGetAsync()
        {
            if (_context.Category != null)
            {
                Category = await _context.Category.ToListAsync();
            }
        }
        public int CategoryID { get; set; }

        public BookData BookD { get; set; }
        public string CurrentFilter { get; set; }

        public string PublisherNameSort { get; set; }
        public string PublishDateSort { get; set; }

        public async Task OnGetAsync(int? PublisherName, string sortOrder, string searchString)
        {
            BookD = new BookData();

            PublisherNameSort = sortOrder == "publisher_name" ? "publisher_name_desc" : "publisher_name";
            PublishDateSort = sortOrder == "publish_date" ? "publish_date_desc" : "publish_date";

            CurrentFilter = searchString;

            BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();


            switch(sortOrder)
            {
                case "publisher_name_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Publisher.PublisherName);
                    break;
                case "publisher_name":
                    BookD.Books = BookD.Books.OrderBy(s => s.Publisher.PublisherName);
                    break;
            }
        }

    }
}
