using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class ThemesController : Controller
    {
        private readonly IThemeService _themeService;
        public ThemesController(IThemeService themeService)
        {
            _themeService = themeService;
        }
        // GET: Themes
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<EventTheme> themes;
                if (User.IsInRole("Admin"))
                {
                    themes = await _themeService.GetAllThemesAsync();
                }
                else
                {
                    themes = await _themeService.GetActiveThemesAsync();
                }
                return View(themes);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load themes.";
                return View(new List<EventTheme>());
            }
        }
        // GET: Themes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                var theme = await _themeService.GetThemeByIdAsync(id);
                if(theme == null)
                {
                    return NotFound();
                }
                //non-admin users can only view active themes
                if(!User.IsInRole("Admin") && !theme.IsActive)
                {
                    return NotFound();
                }
                return View(theme);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load theme details.";
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: Themes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new EventTheme();
            return View(model);
        }
        // POST: Themes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([Bind("Name,Description,CssTemplate,ThumbnailUrl,IsPremium")] EventTheme theme)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _themeService.CreateThemeAsync(theme);
                    TempData["Success"] = "Theme created successfully!";
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
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while creating the theme.";
                }
            }
            return View(theme);
        }
        // GET: Themes/Edit/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                var theme = await _themeService.GetThemeByIdAsync(id);
                if(theme == null)
                {
                    return NotFound();
                }
                return View(theme);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load theme for editing.";
                return RedirectToAction(nameof(Index));
            }
        }
        // POST: Themes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CssTemplate,ThumbnailUrl,IsPremium,IsActive")]EventTheme theme) 
        {
            if (id != theme.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedTheme = await _themeService.UpdateThemeAsync(theme);
                    if (updatedTheme != null)
                    {
                        TempData["Success"] = "Theme updated successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                    return NotFound();
                }
                catch(InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch(ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while updating the theme.";
                }
            }
            return View(theme);
        }
        // GET: Themes/Delete/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                var theme = await _themeService.GetThemeByIdAsync(id);
                if (theme == null)
                {
                    return NotFound();
                }
                return View(theme);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load theme for deletion.";
                return RedirectToAction(nameof(Index));
            }
        }
        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteConfirmed (int id) 
        {
            try
            {
                var success = await _themeService.DeleteThemeAsync(id);
                if (success)
                {
                    TempData["Success"] = "Theme deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Theme not found or could not be deleted.";
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred whiled deleting the theme.";
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Themes/Preview/5
        public async Task<IActionResult> Preview(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                var theme = await _themeService.GetThemeByIdAsync(id);
                if(theme == null)
                {
                    return NotFound();
                }
                //non-admin users can only preview active themes
                if(!User.IsInRole("Admin") && !theme.IsActive)
                {
                    return NotFound();
                }
                //create a sample event for preview
                var sampleEvent = new Events
                {
                    EventId = 0,
                    EventName = "Sample Event - Theme Preview",
                    EventDescription = "This is a preview of how your event would look with this theme.",
                    StartDateTime = DateTime.Now.AddDays(7),
                    EndDateTime = DateTime.Now.AddDays(7).AddHours(3),
                    VenueName = "Sample Venue",
                    City = "Sample City",
                    State = "Sample State",
                    Theme = theme
                };
                ViewBag.Theme = theme;
                return View(sampleEvent);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load theme preview.";
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: Themes/GetActiveThemes (AJAX endpoint for event creation/editing)
        [HttpGet]
        public async Task<IActionResult> GetActiveThemes()
        {
            try
            {
                var themes = await _themeService.GetActiveThemesAsync();
                var themeList = themes.Select(t=>new
                {
                    id = t.Id,
                    name = t.Name,
                    description = t.Description,
                    thumbnailUrl = t.ThumbnailUrl,
                    isPremium = t.IsPremium
                });
                return Json(themeList);
            }
            catch (Exception)
            {
                return Json(new List<object>());
            }
        }
        // GET: Themes/GetThemeCSS/5 (AJAX endpoint to get theme CSS)
        [HttpGet]
        public async Task<IActionResult> GetThemeCss(int id)
        {
            try
            {
                var theme = await _themeService.GetThemeByIdAsync(id);
                if (theme == null || (!User.IsInRole("Admin") && !theme.IsActive))
                {
                    return Json(new { success = false, css = "" });
                }
                return Json(new { success = true, css = theme.CssTemplate ?? "" });
            }
            catch (Exception)
            {
                return Json(new { success = false, css = "" });
            }
        }
    }
}
