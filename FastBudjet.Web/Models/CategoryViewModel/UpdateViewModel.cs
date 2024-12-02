using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Web.Models.CategoryViewModel
{
    public class UpdateViewModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Image { get; set; }
    }
}
