using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GetOnIt.Data;
using GetOnIt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GetOnIt.Controllers
{
    //Make page only accessible if logged in
    [Authorize]
    public class TasksController : Controller
    {
        private readonly GetOnItContext _context;
        private readonly ApplicationDbContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public TasksController(GetOnItContext context, ApplicationDbContext identityContext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _identityContext = identityContext;
            _userManager = userManager;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            //Get the user logged in by email and then displays information based on user
            var userEmail = User.Identity.Name;
            var user = _userManager.Users.Where(a=>a.Email == userEmail).FirstOrDefault();
            var getOnItContext = _context.Tasks.Where(a=>a.UserId == user.Id); 
            return View(await getOnItContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                //.Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public async Task<IActionResult> Create()
        {
            Tasks tasks = new Tasks(); //creating a tasks model to pass back to the view

            //Find the current logged in user to create a task to THEIR account. Brings to create view and passed to post create.
            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser != null)
            {
                ViewBag.UserID = tasks.UserId = currentUser.Id;
                await _context.SaveChangesAsync();
            }
            return View(tasks);
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DateStart,DateEnd,Type,Priority,IsCompleted,UserId")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tasks);
        }

        //Get Tasks Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tasks == null)
                return NotFound();

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
                return NotFound();

            //var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser != null)
            //{
            //    ViewBag.UserID = tasks.UserId;
            //    await _context.SaveChangesAsync();
            //}
            return View(tasks);
        }

        //Post Tasks Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DateStart,DateEnd,Type,Priority,IsCompleted,UserId")] Tasks tasks)
        {
            if (id != tasks.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(tasks);
        }

        //Get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tasks == null)
                return NotFound();

            var tasks = await _context.Tasks
                //.Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
                return NotFound();
            return View(tasks);
        }

        //Post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'GetOnItContext.Tasks'  is null.");
            }
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks != null)
            {
                _context.Tasks.Remove(tasks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id) { return _context.Tasks.Any(e => e.Id == id); }
    }
}
