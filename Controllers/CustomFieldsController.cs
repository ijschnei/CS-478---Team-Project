using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class CustomFieldsController : Controller
    {
        private readonly ICustomFieldsService _customFieldsService;
        private readonly IEventService _eventService;
        private readonly UserManager<Users> _userManager;
        
        public CustomFieldsController(ICustomFieldsService customFieldsService, IEventService eventService, UserManager<Users> userManager)
        {
            _customFieldsService = customFieldsService;
            _eventService = eventService;
            _userManager = userManager;
        }
       // GET: CustomFields/EventFields/5
        public async Task<IActionResult> EventFields(int eventId)
        {
            if (eventId <= 0)
                return BadRequest();
            try
            {
                //verify event exists and user has permission
                var eventItem = await _eventService.GetEventByIdAsync(eventId);
                if (eventItem == null)
                    return NotFound();
                var currentUser = await _userManager.GetUserAsync(User);
                if(currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }
                var customFields = await _customFieldsService.GetEventCustomFieldsAsync(eventId);
                ViewBag.Event = eventItem;
                return View(customFields);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to laod custom fields.";
                return RedirectToAction("Details", "Events", new { id = eventId });
            }
        }
        // GET: CustomFields/Create
        public async Task<IActionResult> Create(int eventId)
        {
            if (eventId <= 0)
                return BadRequest();
            try
            {
                //verify event exists and user has permissions
                var eventItem = await _eventService.GetEventByIdAsync(eventId);
                if (eventItem == null)
                    return NotFound();
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }
                var model = new EventCustomFields
                {
                    EventId = eventId,
                    FieldType = "text"
                };
                ViewBag.Event = eventItem;
                ViewBag.FieldTypes = GetFieldTypeSelectList();
                return View(model);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load create form.";
                return RedirectToAction("Details", "Events", new { id = eventId });
            }
        }
        // POST: CustomFields/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,FieldName,FieldType,FieldValue,DisplayOrder,IsRequired,SelectOptions")]EventCustomFields customField)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //verify event exists and user has permission
                    var eventItem = await _eventService.GetEventByIdAsync(customField.EventId);
                    if (eventItem == null)
                        return NotFound();
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }
                    await _customFieldsService.CreateCustomFieldAsync(customField);
                    TempData["Success"] = "Custom field created successfully!";
                    return RedirectToAction(nameof(EventFields), new { eventId = customField.EventId });
                }
                catch(ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch(InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while creating the custom field.";
                }
            }
            //reload event data for view
            try
            {
                var eventItem = await _eventService.GetEventByIdAsync(customField.EventId);
                ViewBag.Event = eventItem;
            }
            catch { }
            ViewBag.FieldTypes = GetFieldTypeSelectList();
            return View(customField);
        }
        // GET: CustomFields/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                var customFields = await _customFieldsService.GetEventCustomFieldsAsync(0);
                var customField = customFields.FirstOrDefault(cf => cf.Id == id);
                if (customField == null)
                    return NotFound();
                //verify event exists and user has permission
                var eventItem = await _eventService.GetEventByIdAsync(customField.EventId);
                if (eventItem == null)
                    return NotFound();
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }
                ViewBag.Event = eventItem;
                ViewBag.FieldTypes = GetFieldTypeSelectList();
                return View(customField);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load custom field for editing.";
                return RedirectToAction("Index", "Events");
            }
        }
        // POST: CustomFields/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,FieldName,FieldType,FieldValue,DisplayOrder,IsRequired,SelectOptions")] EventCustomFields customField)
        {
            if (id != customField.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    //verify event exists and user has permission
                    var eventItem = await _eventService.GetEventByIdAsync(customField.EventId);
                    if (eventItem == null)
                        return NotFound();
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }
                    var updatedField = await _customFieldsService.UpdateCustomFieldAsync(customField);
                    if (updatedField != null)
                    {
                        TempData["Success"] = "Custom field updated successfully!";
                        return RedirectToAction(nameof(EventFields), new { eventId = customField.EventId });
                    }
                    return NotFound();
                }
                catch(ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch(InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while updating the custom field.";
                }
            }
            //reload event data for view
            try
            {
                var eventItem = await _eventService.GetEventByIdAsync(customField.EventId);
                ViewBag.Event = eventItem;
            }
            catch { }
            ViewBag.FieldTypes = GetFieldTypeSelectList();
            return View(customField);
        }
        // POST: CustomFields/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int eventId)
        {
            try
            {
                //verify event exists and user has permission
                var eventItem = await _eventService.GetEventByIdAsync(eventId);
                if (eventItem == null)
                    return Json(new { success = false, message = "Event not found." });
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
                {
                    return Json(new { success = false, message = "Access denied." });
                }
                var success = await _customFieldsService.DeleteCustomFieldAsync(id);
                if (success)
                {
                    return Json(new { success = true, message = "Custom field deleted successfully!" });
                }
                return Json(new { success = false, message = "Custom field not found or could not be deleted." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while deleting the custom field." });
            }
        }
        // POST: CustomFields/UpdateValue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateValue(int fieldId, string value)
        {
            try
            {
                var success = await _customFieldsService.UpdateCustomFieldValueAsync(fieldId, value);
                if (success)
                {
                    return Json(new { success = true, message = "Field value updated successfully!" });
                }
                return Json(new { success = false, message = "Failed to update field value." });
            }
            catch(ArgumentException ex)
            {
                return Json(new {success=false,message=ex.Message});
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while updating the field value." });
            }
        }
        private static Microsoft.AspNetCore.Mvc.Rendering.SelectList GetFieldTypeSelectList()
        {
            var fieldTypes = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
            {
                new() { Value = "text", Text = "Text"},
                new() { Value = "textarea", Text = "Text Area"},
                new() { Value = "number", Text = "Number"},
                new() { Value = "date", Text = "Date"},
                new() { Value = "boolen", Text = "Yes/No"},
                new() { Value = "select", Text = "Dropdown"}
            };
            return new Microsoft.AspNetCore.Mvc.Rendering.SelectList(fieldTypes, "Value", "Text");
        }
    }
}
