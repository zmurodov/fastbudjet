using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastBudjet.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public bool Income{ get; set; }

        public virtual ICollection<Category> Children { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
