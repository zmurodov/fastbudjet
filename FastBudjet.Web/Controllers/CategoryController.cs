using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastBudjet.Core.Interfaces;
using FastBudjet.Data.Models;
using FastBudjet.Web.Models.CategoryViewModel;

namespace FastBudjet.Web.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var incomingCategories = _categoryService.GetIncomingParentCategories();
            var expenseCategories = _categoryService.GetExpenseParentCategories();
            
            return View(new IndexViewModel
            {
                IncomingCategories = incomingCategories,
                ExpenseCategories = expenseCategories,
                CreateViewModel = new CreateViewModel(),
                UpdateViewModel = new UpdateViewModel()
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int? id)
        {
            if (id == null) return NotFound();

            var category = _categoryService.GetCategory(id.Value);
            
            var categoryViewModel = new CategoryViewModel();
            categoryViewModel.Name = category.Name;
            categoryViewModel.CategoryId = category.CategoryId;
            categoryViewModel.Image = category.Image;
            categoryViewModel.Income = category.Income;
            if (category.ParentId != null)
            {
                var parent = _categoryService.GetCategory(category.ParentId.Value);
                categoryViewModel.ParentId = category.ParentId;
                categoryViewModel.Parent = parent.Name;
            }
            else
            {
                categoryViewModel.ParentId = null;
                categoryViewModel.Parent = null;
            }


            return Ok(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateViewModel createViewModel)
        {
            var category = new Category();
            category.Name = createViewModel.Name;
            category.Income = createViewModel.Income;
            category.Image = createViewModel.Image;
            if(createViewModel.ParentId > 0)
                category.ParentId = createViewModel.ParentId;
            else
                category.ParentId = null;


            await _categoryService.AddCategory(category);
            return RedirectToAction("Index");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCategory([FromForm]int? id, UpdateViewModel updateViewModel)
        {
            if(id == null)
            {
                return NotFound();
            }
            var category = _categoryService.GetCategory(id.Value);

            category.Name = updateViewModel.Name ?? category.Name;
            category.Image = updateViewModel.Image ?? category.Image;
            category.ParentId = updateViewModel.ParentId ?? category.ParentId;

            await _categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null) return NotFound();

            await _categoryService.DeleteCategory(id.Value);

            return Ok();
        }
    }
}
