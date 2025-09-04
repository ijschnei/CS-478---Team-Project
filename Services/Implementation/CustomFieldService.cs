using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace CS478_EventPlannerProject.Services.Implementation
{
    public class CustomFieldService : ICustomFieldsService
    {
        private readonly ApplicationDbContext _context;
        public CustomFieldService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<EventCustomFields>> GetEventCustomFieldsAsync(int eventId)
        {
            if (eventId <= 0)
                return Enumerable.Empty<EventCustomFields>();
            try
            {
                return await _context.EventCustomFields
                    .Include(cf => cf.Event)
                    .Where(cf => cf.EventId == eventId)
                    .OrderBy(cf => cf.DisplayOrder)
                    .ThenBy(cf => cf.FieldName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve custom fields for event {eventId}", ex);
            }
        }
        public async Task<EventCustomFields> CreateCustomFieldAsync(EventCustomFields customField)
        {
            if (customField == null)
                throw new ArgumentNullException(nameof(customField));
            if (string.IsNullOrWhiteSpace(customField.FieldName))
                throw new ArgumentException("Field name is required");
            if (string.IsNullOrWhiteSpace(customField.FieldType))
                throw new ArgumentException("Field type is required");
            try
            {
                //validate field type
                var validFieldTypes = new[] { "text", "number", "date", "boolean", "select", "textarea" };
                if (!validFieldTypes.Contains(customField.FieldType.ToLower()))
                {
                    throw new ArgumentException($"Invalid field type. Valid types are: {string.Join(", ", validFieldTypes)}");
                }
                //validate that event exists
                var eventExists = await _context.Events
                    .AnyAsync(e => e.EventId == customField.EventId && !e.IsDeleted && e.IsActive);
                if (!eventExists)
                    throw new ArgumentException("Event does not exists or is not active");
                //check if field name already exists for this event
                var fieldNameExists = await _context.EventCustomFields
                    .AnyAsync(cf => cf.EventId == customField.EventId &&
                            cf.FieldName.ToLower() == customField.FieldName.ToLower());
                if (fieldNameExists)
                    throw new InvalidOperationException("A field with this name already exists for this event");
                //validate select options for select field type
                if (customField.FieldType.ToLower() == "select")
                {
                    if (string.IsNullOrWhiteSpace(customField.SelectOptions))
                        throw new ArgumentException("Select options are required to select field type");
                    try
                    {
                        //validate that SelectOptions is valid JSON array
                        var options = JsonSerializer.Deserialize<string[]>(customField.SelectOptions);
                        if (options == null || options.Length == 0)
                            throw new ArgumentException("Select options must contain at least one option");
                    }
                    catch (JsonException)
                    {
                        throw new ArgumentException("Select options must be a valid JSON array of strings");
                    }
                }
                //set display order if not provided
                if (customField.DisplayOrder == 0)
                {
                    var maxOrder = await _context.EventCustomFields
                        .Where(cf => cf.EventId == customField.EventId)
                        .MaxAsync(cf => (int?)cf.DisplayOrder) ?? 0;
                    customField.DisplayOrder = maxOrder + 1;
                }
                _context.EventCustomFields.Add(customField);
                await _context.SaveChangesAsync();
                return customField;
            }
            catch(Exception ex) when (!(ex is ArgumentException||ex is InvalidOperationException))
            {
                throw new InvalidOperationException("Failed to create custom field", ex);
            }
        }
        public async Task<EventCustomFields?> UpdateCustomFieldAsync(EventCustomFields customField)
        {
            if (customField == null) return null;
            try
            {
                var existingField = await _context.EventCustomFields.FindAsync(customField.Id);
                if (existingField == null) return null;
                //validate field type
                var validFieldTypes = new[] { "text", "number", "date", "boolean", "select", "textarea" };
                if (!validFieldTypes.Contains(customField.FieldType.ToLower()))
                {
                    throw new ArgumentException($"Invalid field type. Valid types are: {string.Join(", ", validFieldTypes)}");
                }
                //check if new field name conflicts with existing fields(excluding current field)
                var fieldNameExists = await _context.EventCustomFields
                    .AnyAsync(cf => cf.EventId == customField.EventId &&
                            cf.FieldName.ToLower() == customField.FieldName.ToLower()
                            && cf.Id != customField.Id);
                if (fieldNameExists)
                    throw new InvalidOperationException("A field with this name already exists for this event");
                //validate select options for select field type
                if (customField.FieldType.ToLower() == "select")
                {
                    if (string.IsNullOrWhiteSpace(customField.SelectOptions))
                        throw new ArgumentException("Select options are required to select field type");
                    try
                    {
                        //validate that SelectOptions is valid JSON array
                        var options = JsonSerializer.Deserialize<string[]>(customField.SelectOptions);
                        if (options == null || options.Length == 0)
                            throw new ArgumentException("Select options must contain at least one option");
                    }
                    catch (JsonException)
                    {
                        throw new ArgumentException("Select options must be a valid JSON array of strings");
                    }
                }
                //update fields
                existingField.FieldName = customField.FieldName;
                existingField.FieldType = customField.FieldType.ToLower();
                existingField.FieldValue = customField.FieldValue;
                existingField.DisplayOrder = customField.DisplayOrder;
                existingField.IsRequired = customField.IsRequired;
                existingField.SelectOptions = customField.SelectOptions;

                await _context.SaveChangesAsync();
                return existingField;
            }
            catch(Exception ex) when (!(ex is ArgumentException|| ex is InvalidOperationException))
            {
                throw new InvalidOperationException($"Failed to update custom field {customField.Id}", ex);
            }
        }
        public async Task<bool> DeleteCustomFieldAsync(int id)
        {
            if (id <= 0) return false;
            try
            {
                var customField = await _context.EventCustomFields.FindAsync(id);
                if (customField == null) return false;
                _context.EventCustomFields.Remove(customField);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Failed to delete custom field {id}", ex);
            }
        }
        public async Task<bool> UpdateCustomFieldValueAsync(int fieldId, string value)
        {
            if (fieldId <= 0) return false;
            try
            {
                var customField = await _context.EventCustomFields.FindAsync(fieldId);
                if (customField == null) return false;
                //validate value based on field type
                if (!ValidateFieldValue(customField.FieldType, value, customField.SelectOptions))
                {
                    throw new ArgumentException($"Invalid value for field type {customField.FieldType}");
                }
                customField.FieldValue = value;
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) when (!(ex is ArgumentException))
            {
                throw new InvalidOperationException($"Failed to update custom field value for field {fieldId}.", ex);
            }
        }
        private static bool ValidateFieldValue(string fieldType, string value, string? selectOptions)
        {
            if (string.IsNullOrEmpty(value)) return true; //empty values are allowed (unless field is required)
            return fieldType.ToLower() switch
            {
                "text" or "textarea" => true, //any string is valid
                "number" => decimal.TryParse(value, out _),
                "date" => DateTime.TryParse(value, out _),
                "boolen" => bool.TryParse(value, out _),
                "select" => ValidateSelectValue(value, selectOptions),
                _ => false
            };
        }
        private static bool ValidateSelectValue(string value, string? selectOptions)
        {
            if (string.IsNullOrWhiteSpace(selectOptions)) return false;
            try
            {
                var options = JsonSerializer.Deserialize<string[]>(selectOptions);
                return options != null && options.Contains(value, StringComparer.OrdinalIgnoreCase);
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
