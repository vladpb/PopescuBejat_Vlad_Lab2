using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PopescuBejat_Vlad_Lab2.Models;

namespace PopescuBejat_Vlad_Lab2.Data
{
    public class PopescuBejat_Vlad_Lab2Context : DbContext
    {
        public PopescuBejat_Vlad_Lab2Context (DbContextOptions<PopescuBejat_Vlad_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<PopescuBejat_Vlad_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<PopescuBejat_Vlad_Lab2.Models.Publisher>? Publisher { get; set; }

        public DbSet<PopescuBejat_Vlad_Lab2.Models.Author>? Author { get; set; }
    }
}
