using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Models.CategoryViewModel
{
    public class CreateViewModel
    {
        public string Name { get; set; }
        public bool Income { get; set; }

        public string Image { get; set; }

        public int ParentId { get; set; }
    }
}
