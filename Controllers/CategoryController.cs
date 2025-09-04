using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load categories.";
                return View(new List<EventCategory>());
            }
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load category details.";
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new EventCategory
            {
                ColorCode = "#1f77b4" //Default blue color
            };
            return View(model);
        }
       
        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Description,IconUrl,ColorCode")] EventCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.CreateCategoryAsync(category);
                    TempData["Success"] = "Category created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch(InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch(ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch(Exception)
                {
                    TempData["Error"] = "An error occurred while creating the category.";
                }
            }
            return View(category);
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to laod category for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IconUrl,ColorCode")] EventCategory category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
                    if(updatedCategory != null)
                    {
                        TempData["Success"] = "Category updated successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                    return NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while updating the category.";
                }
            }
            return View(category);           
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load category for deletion.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _categoryService.DeleteCategoryAsync(id);
                if (success)
                {
                    TempData["Success"] = "Category deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Category not found or could not be deleted.";
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while deleting the category.";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Category/AssignToEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToEvent(int eventId, int categoryId)
        {
            try
            {
                var success = await _categoryService.AssignCategoryToEventAsync(eventId, categoryId);
                if (success)
                {
                    return Json(new { success = true, message = "Category assigned successfully!" });
                }
                return Json(new { success = false, message = "Failed to assign category. It may already be assigned." });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while assigning the category." });
            }
        }

        // POST: Category/RemoveFromEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromEvent(int eventId, int categoryId)
        {
            try
            {
                var success = await _categoryService.RemoveCategoryFromEventAsync(eventId, categoryId);
                if (success)
                {
                    return Json(new { success = true, message = "Category removed successfully!" });
                }
                return Json(new { success = false, message = "Failed to remove category." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while removing the category." });
            }
        }
    }
}
