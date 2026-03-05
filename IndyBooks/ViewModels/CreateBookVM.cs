using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IndyBooks.Models;

namespace IndyBooks.ViewModels
{
    public class CreateBookVM
    {
        public long BookId { get; set; }
        // Properties to support the form input fields
        public string Title { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        // This field is used to create a new Author and not required, since the use may choose from the select list
        [Display(Name = "Author Name")]
        public String AuthorName { get; set; }

        //Properties to support a Writer's SelectList (Id and Writers)
        public long AuthorId { get; set; } //The Id is used
        public IEnumerable<Writer> Authors { get; set; }
    }
}
