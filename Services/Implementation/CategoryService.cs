using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<EventCategory>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.EventCategories
                    .Include(c => c.EventMappings)
                        .ThenInclude(em => em.Event)
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve categories", ex);
            }
        }
        public async Task<EventCategory?> GetCategoryByIdAsync(int id)
        {
            if (id <= 0) return null;
            try
            {
                return await _context.EventCategories
                    .Include(c => c.EventMappings)
                        .ThenInclude(em => em.Event)
                    .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve category {id}", ex);
            }
        }
        public async Task<EventCategory> CreateCategoryAsync(EventCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name is required");
            try
            {
                //check if category name already exists
                var existingCategory = await _context.EventCategories
                    .AnyAsync(c=>c.Name.ToLower()==category.Name.ToLower() && c.IsActive);
                if (existingCategory)
                    throw new InvalidOperationException("A category with this name already exists");
                //validate color code format (if provided)
                if (!string.IsNullOrEmpty(category.ColorCode))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(category.ColorCode, @"^#[0-9A-Fa-f]{6}$"))
                        throw new ArgumentException("Color code must be in hex format (#RRGGBB)");
                }
                else
                {
                    //assign default color if none provided
                    category.ColorCode = "#1f77b4"; //default blue
                }
                category.IsActive = true;
                _context.EventCategories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex) when (!(ex is ArgumentException || ex is InvalidOperationException)) 
            {
                throw new InvalidOperationException("Failed to create category", ex);
            }
        }
        public async Task<EventCategory?> UpdateCategoryAsync(EventCategory category)
        {
            if (category == null) return null;
            try
            {
                var existingCategory = await _context.EventCategories.FindAsync(category.Id);
                if (existingCategory == null || !existingCategory.IsActive)
                    return null;
                //check if new name conflicts with existing categories
                var nameConflict = await _context.EventCategories
                    .AnyAsync(c => c.Name.ToLower() == category.Name.ToLower() &&
                              c.Id != category.Id && c.IsActive);
                if (nameConflict)
                    throw new InvalidOperationException("A category with this name already exists");
                //validate color code format (if provided)
                if (!string.IsNullOrEmpty(category.ColorCode))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(category.ColorCode, @"^#[0-9A-Fa-f]{6}$"))
                        throw new ArgumentException("Color code must be in hex format (#RRGGBB)");
                }
                //update fields
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                existingCategory.IconUrl = category.IconUrl;
                existingCategory.ColorCode = category.ColorCode;

                await _context.SaveChangesAsync();
                return existingCategory;
            }
            catch (Exception ex) when (!(ex is ArgumentException || ex is InvalidOperationException)) 
            {
                throw new InvalidOperationException($"Failed to update category {category.Id}", ex);
            }

        }
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id <= 0) return false;
            try 
            {
                var category = await _context.EventCategories.FindAsync(id);
                if (category == null || !category.IsActive)
                    return false;
                //check if category is in use
                var isInUse = await _context.EventCategoryMappings
                    .AnyAsync(ecm => ecm.CategoryId == id);
                if (isInUse)
                {
                    //soft delete - mark as inactive instead of removing
                    category.IsActive = false;
                }
                else
                {
                    //hard delete if not in use
                    _context.EventCategories.Remove(category);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"Failed to delete category {id}", ex);
            }
        }
        public async Task<bool> AssignCategoryToEventAsync(int eventId, int categoryId)
        {
            if (eventId <= 0 || categoryId <= 0) return false;
            try
            {
                //check if event exists
                var eventExists = await _context.Events
                    .AnyAsync(e => e.EventId == eventId && !e.IsDeleted && e.IsActive);
                if (!eventExists) return false;
                //check if category exists and is active
                var categoryExists = await _context.EventCategories
                    .AnyAsync(c => c.Id == categoryId && c.IsActive);
                if (!categoryExists) return false;
                //check if mapping already exists
                var mappingExists = await _context.EventCategoryMappings
                    .AnyAsync(ecm => ecm.EventId == eventId && ecm.CategoryId == categoryId);
                if (mappingExists) return false; //already assigned
                var mapping = new EventCategoryMapping
                {
                    EventId = eventId,
                    CategoryId = categoryId
                };
                _context.EventCategoryMappings.Add(mapping);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Failed to assign category {categoryId} to event {eventId}", ex);
            }
        }
        public async Task<bool> RemoveCategoryFromEventAsync(int eventId, int categoryId)
        {
            if (eventId <= 0 || categoryId <=0) return false;
            try 
            {
                var mapping = await _context.EventCategoryMappings
                    .FirstOrDefaultAsync(ecm => ecm.EventId == eventId && ecm.CategoryId == categoryId);
                if (mapping == null) return false;
                _context.EventCategoryMappings.Remove(mapping);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Failed to remove category {categoryId} from event {eventId}", ex);
            }
        }


    }
}
