using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZenithWebsite.Data;

namespace ZenithWebsite.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _context = context;
        }

        // GET: Roles
        public IActionResult Index()
        {
            return View(_context.Roles.ToList());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Roles.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] IdentityRole @event)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole(@event.Name));
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Roles.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            var selectedUserList = _context.Users.Where(u => u.Roles.Any(r => r.RoleId == @event.Id));
            var userList = _context.Users;

            MultiSelectList selectList = new MultiSelectList(userList, "Id", "UserName");

            foreach (SelectListItem item in selectList)
            {
                if (selectedUserList.Any(s => s.Id == item.Value))
                {
                    item.Selected = true;
                }
            }

            ViewData["Id"] = new SelectList(_context.Roles, "Id", "Name", @event.Id);
            ViewData["Users"] = selectList;
            return View(@event);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Users,Roles")] IdentityRole @event)
        {

            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Check if Admin account is being removed from the Admin Role
                if (_context.Roles.Where(q => q.Name == "Admin").First().Id == id && !this.Request.Form["Users"].Any(a => a == _context.Users.Where(u => u.UserName == "a").First().Id))
                {
                    return RedirectToAction("Index");
                }

                try
                {
                    List<IdentityUserRole<string>> roles = new List<IdentityUserRole<string>>();
                    _context.UserRoles.RemoveRange(_context.UserRoles.Where(u => u.RoleId == @event.Id));
                    await _context.SaveChangesAsync();

                    foreach (string r in this.Request.Form["Users"])
                    {
                        IdentityUserRole<string> i = new IdentityUserRole<string>();
                        i.RoleId = @event.Id;
                        i.UserId = r;
                        if (!_context.UserRoles.Any(q => q.RoleId == i.RoleId && q.UserId == i.UserId))
                        {
                            _context.UserRoles.Add(i);
                        }

                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["Id"] = new SelectList(_context.Roles, "Id", "Name", @event.Id);
            return View(@event);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Roles.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //Don't delete if admin
            if (_context.Roles.Where(role => role.Name == "Admin").First().Id == id)
            {
                return RedirectToAction("Index");
            }

            var @event = await _context.Roles.SingleOrDefaultAsync(m => m.Id == id);
            _context.Roles.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RoleExists(string id)
        {
            return _context.Roles.Any(role => role.Id == id);
        }
    }
}
