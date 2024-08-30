using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class AllFilter
    {
        public PaginatedList<Book>? PaginatedList{ get; set; }

        public List<Book> Book { get; set; }
        public List<Genre>? Genre { get; set; }

        public List<Author>? Author { get; set; }

        public List<Year>? Year { get; set; }
        public List<Publisher>? Publisher { get; set; }

        public List<Employee>? Employee { get; set; }

        public int? Id { get; set; }
        public string? Username { get; set; }

    }
}
