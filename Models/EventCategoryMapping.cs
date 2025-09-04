using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS478_EventPlannerProject.Models
{
    public class EventCategoryMapping
    {
        [ForeignKey("Event")]
        public int EventId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        //Navigation Properties
        public virtual Events Event { get; set; } = null!;
        public virtual EventCategory Category { get; set; } = null!;
    }
}
