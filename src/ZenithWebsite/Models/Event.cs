using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebsite.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime EventFrom { get; set; }
        public DateTime EventTo { get; set; }
        public string CreatedBy { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
