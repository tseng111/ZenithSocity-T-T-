using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZenithWebsite.Models
{
    public class Event
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int EventId { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "A valid Date or Date and Time must be entered eg. January 1, 2014 12:00A)")]
        [Required(ErrorMessage = "Starting Date is required.")]
        [Display(Name = "Starting Date")]
        public DateTime EventFrom { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "A valid Date or Date and Time must be entered eg. January 1, 2014 12:00AM")]
        [Required(ErrorMessage = "Ending Date is required.")]
        [Display(Name = "Ending Date")]
        public DateTime EventTo { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        public int ActivityId { get; set; }

        [Display(Name = "Activity")]
        public Activity Activity { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
