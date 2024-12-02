using System.Collections;
using System.Collections.Generic;
using FastBudjet.Data.Models;

namespace FastBudjet.Web.Models.CategoryViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Category> IncomingCategories { get; set; }
        public IEnumerable<Category> ExpenseCategories { get; set; }

        public CreateViewModel CreateViewModel { get; set; }
        public UpdateViewModel UpdateViewModel{ get; set; }
    }
}