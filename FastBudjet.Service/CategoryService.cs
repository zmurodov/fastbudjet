using FastBudjet.Data;
using FastBudjet.Data.Models;
using FastBudjet.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FastBudjet.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public IEnumerable<Category> GetIncomingCategories()
        {
            return _context.Categories
                .Where(category => category.Income == true)
                .ToList();
        }
        public IEnumerable<Category> GetExpenseCategories()
        {
            return _context.Categories
                .Where(category => category.Income == false)
                .ToList();
        }

        public IEnumerable<Category> GetExpenseParentCategories()
        {
            return _context.Categories
                .Include(category => category.Children)
                .Where(category => category.Parent == null && category.Income == false);
        }

        public IEnumerable<Category> GetIncomingParentCategories()
        {
            return _context.Categories
                .Include(category => category.Children)
                .Where(category => category.Parent == null && category.Income == true);
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories
                .FirstOrDefault(category => category.CategoryId == categoryId);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);

            _context.Remove(category);

            await _context.SaveChangesAsync();
        }
    }
}
