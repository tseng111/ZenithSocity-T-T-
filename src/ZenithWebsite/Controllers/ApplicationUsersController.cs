using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZenithWebsite.Data;
using ZenithWebsite.Models;

namespace ZenithWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUsersController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();
            
            IList<string> roleNames = new List<string>();

            foreach (var role in roles)
            {
                roleNames.Add(role.Name);

                //Grabs users in role
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                ViewData[role.Name] = usersInRole;
            }
            ViewData["Roles"] = roleNames;

            return View(await _context.ApplicationUser.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user.UserName == "a" && role == "Admin")
            {
                return RedirectToAction("Index");
            }
            else
            {
                IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role);
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddToRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            IdentityResult result = await _userManager.AddToRoleAsync(user, role);
            return RedirectToAction("Index");
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
