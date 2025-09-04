using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Implementation
{
    public class ThemeService : IThemeService
    {
        private readonly ApplicationDbContext _context;
        public ThemeService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<EventTheme>> GetAllThemesAsync()
        {
            try
            {
                return await _context.EventThemes
                    .Include(t => t.Events.Where(e => !e.IsDeleted)) //only active events
                    .OrderBy(t => t.IsPremium) //free themes first
                    .ThenBy(t => t.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve themes", ex);
            }
        }
        public async Task<IEnumerable<EventTheme>> GetActiveThemesAsync()
        {
            try 
            {
                return await _context.EventThemes
                    .Include(t=>t.Events.Where(e=>!e.IsDeleted))
                    .Where(t=>t.IsActive)
                    .OrderBy(t=>t.IsPremium)//free themes first
                    .ThenBy(t=>t.Name)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve active themes", ex);
            }
        }
        public async Task<EventTheme?> GetThemeByIdAsync(int id)
        {
            if(id<=0) return null;
            try
            {
                return await _context.EventThemes
                    .Include(t => t.Events.Where(e => !e.IsDeleted))
                    .FirstOrDefaultAsync();
            }
            catch(Exception ex) 
            {
                throw new InvalidOperationException($"Failed to retrieve theme {id}", ex);
            }
        }
        public async Task<EventTheme> CreateThemeAsync(EventTheme theme)
        {
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));
            if (string.IsNullOrWhiteSpace(theme.Name))
                throw new ArgumentException("Theme name is required");
            try
            {
                //check if theme name already exists
                var existingTheme = await _context.EventThemes
                    .AnyAsync(t => t.Name.ToLower() == theme.Name.ToLower());
                if (existingTheme)
                    throw new InvalidOperationException("A theme with this name already exists");
                //audit fields
                theme.CreatedAt = DateTime.UtcNow;
                theme.IsActive = true;
                //validate CSS if provided
                if (!string.IsNullOrEmpty(theme.CssTemplate))
                {
                    //basic CSS validation for malicious content
                    var dangerousPatterns = new[] { "javascript:", "expression(", "import", "@import" };
                    var cssLower = theme.CssTemplate.ToLower();

                    if (dangerousPatterns.Any(pattern => cssLower.Contains(pattern)))
                    {
                        throw new ArgumentException("CSS template contains potentially unsafe content");
                    }
                }
                //validate thumbnail url if provided
                if (!string.IsNullOrEmpty(theme.ThumbnailUrl))
                {
                    if(!Uri.TryCreate(theme.ThumbnailUrl, UriKind.Absolute, out _))
                    {
                        throw new ArgumentException("Thumbnail URL is not valid");
                    }
                }
                _context.EventThemes.Add(theme);
                await _context.SaveChangesAsync();

                return theme;
            }
            catch(Exception ex) when(!(ex is ArgumentException || ex is InvalidOperationException)) 
            {
                throw new InvalidOperationException("Failed to create theme", ex);
            }
        }
        public async Task<EventTheme?> UpdateThemeAsync(EventTheme theme)
        {
            if (theme == null) return null;
            try
            {
                var existingTheme = await _context.EventThemes.FindAsync(theme.Id);
                if (existingTheme == null) return null;
                //check if new name conflicts with existing themes
                var nameConflict = await _context.EventThemes
                    .AnyAsync(t => t.Name.ToLower() == theme.Name.ToLower() && t.Id != theme.Id);
                if (nameConflict)
                    throw new InvalidOperationException("A theme with this name already exists");
                //validate CSS if provided
                if(!string.IsNullOrEmpty(theme.CssTemplate))
                {
                    var dangerousPatterns = new[] { "javascript:", "expression(", "import", "@import" };
                    var cssLower = theme.CssTemplate.ToLower();

                    if (dangerousPatterns.Any(pattern => cssLower.Contains(pattern)))
                    {
                        throw new ArgumentException("CSS template contains potentially unsafe content");
                    }
                }
                //validate thumbnail URL if provided
                if (!string.IsNullOrEmpty(theme.ThumbnailUrl))
                {
                    if (!Uri.TryCreate(theme.ThumbnailUrl, UriKind.Absolute, out _))
                    {
                        throw new ArgumentException("Thumbnail URL is not valid");
                    }
                }
                //update fields
                existingTheme.Name = theme.Name;
                existingTheme.Description = theme.Description;
                existingTheme.CssTemplate = theme.CssTemplate;
                existingTheme.ThumbnailUrl = theme.ThumbnailUrl;
                existingTheme.IsPremium = theme.IsPremium;
                existingTheme.IsActive = theme.IsActive;
                await _context.SaveChangesAsync();
                return existingTheme;
            }
            catch(Exception ex) when (!(ex is ArgumentException||ex is InvalidOperationException))
            {
                throw new InvalidOperationException($"Failed to update theme {theme.Id}", ex);
            }
        }
        public async Task<bool> DeleteThemeAsync(int id)
        {
            if (id <= 0) return false;
            try 
            {
                var theme = await _context.EventThemes
                    .Include(t => t.Events)
                    .FirstOrDefaultAsync(t => t.Id == id);
                if (theme == null) return false;
                //check if theme is in use
                var isInUse = theme.Events.Any(e => !e.IsDeleted);
                if (isInUse)
                {
                    //soft delete-mark as inactive instead of removing
                    theme.IsActive = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //hard delete if not in use
                    _context.EventThemes.Remove(theme);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Failed to delete theme {id}", ex);
            }
        }
    }
}
