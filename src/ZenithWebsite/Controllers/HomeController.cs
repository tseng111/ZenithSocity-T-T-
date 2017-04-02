using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZenithWebsite.Data;
using ZenithWebsite.Models;

namespace ZenithWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser currentUser;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


         public async Task<IActionResult> Index()
        {
            currentUser = await _userManager.GetUserAsync(HttpContext.User);

            DateTime today = DateTime.Today;
            DateTime start = today.Date.AddDays(-(int)today.DayOfWeek + 1);
            DateTime end = start.AddDays(7);

            var events = _context.Events.Where(e => e.EventFrom >= start && e.EventTo <= end).ToList();

            if(currentUser == null)
            {
                events.RemoveAll(e => e.IsActive == false);
            }

            foreach(var eventItem in events)
            {
                eventItem.Activity = _context.Activities.FirstOrDefault(a => a.ActivityId == eventItem.ActivityId);
            }

            return View(events);
        }
    }
}
