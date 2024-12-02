using FastBudjet.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastBudjet.Core.Interfaces
{
    public interface ICategoryService
    {
        Category GetCategory(int categoryId);

        Task<Category> AddCategory(Category category);

        Task<Category> UpdateCategory(Category category);

        Task DeleteCategory(int categoryId);

        IEnumerable<Category> GetIncomingCategories();
        
        IEnumerable<Category> GetExpenseCategories();
        
        IEnumerable<Category> GetIncomingParentCategories();
        IEnumerable<Category> GetExpenseParentCategories();


    }
}
