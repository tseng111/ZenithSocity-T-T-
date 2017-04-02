using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebsite.Models
{
    public class Activity
    {
        [Key]
        [Display(Name = "Activity ID")]
        public int ActivityId { get; set; }

        [Display(Name = "Activity Description")]
        [Required(ErrorMessage = "Activity Description is required.")]
        public string ActivityDescription { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        public List<Event> Events { get; set; }
    }
}
