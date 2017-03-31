using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebsite.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime CreationDate { get; set; }

        public List<Event> Events { get; set; }
    }
}
