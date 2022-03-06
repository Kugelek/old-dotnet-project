using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Controllers
{
    public class PlannedTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlannedTasksController(ApplicationDbContext context){
             _context = context;
         }

     
        [AllowAnonymous]
        [Route("PlannedTasks")]
        public async Task<IActionResult> Index(string name)
        {
            var plannedTasks = from task in _context.PlannedTasks
                                  orderby task.importance descending
                                  select task;

            if (!String.IsNullOrEmpty(name))
            {
                var tasks = from a in _context.PlannedTasks
                               select a;
                tasks = tasks.Where(s => s.name.Contains(name));

                return View(await tasks.ToListAsync());
            }

            return View(await plannedTasks.ToListAsync());
        }

        
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
  
            var plannedTask = await _context.PlannedTasks
                .Include(task => task.project)
                .FirstOrDefaultAsync(proj => proj.Id == id);
            if (plannedTask == null)
                return NotFound();
            
            return View(plannedTask);
        }
        
     
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["projectId"] = new SelectList(_context.Projects, "Id", "Id");
            var test = new SelectList(_context.Projects, "Id", "Id");
            Console.Write("testowanie");
            Console.Write(test);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,name,description,taskType,importance,projectId")] PlannedTask plannedTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plannedTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["projectId"] = new SelectList(_context.Projects, "Id", "Id", plannedTask.projectId);
            return View(plannedTask);
        }

       
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var plannedTaskCandidate = await _context.PlannedTasks.FindAsync(id);

            if (plannedTaskCandidate == null) return NotFound();
            
            ViewData["projectId"] = new SelectList(_context.Projects, "Id", "Id", plannedTaskCandidate.projectId);
            return View(plannedTaskCandidate);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,description,taskType,importance,projectId")] PlannedTask plannedTask)
        {
            if (id != plannedTask.Id) return NotFound();
           
            if (ModelState.IsValid){
                try{
                    _context.Update(plannedTask);
                    await _context.SaveChangesAsync();
                }catch (DbUpdateConcurrencyException){
                    var taskExists = _context.PlannedTasks.Any(e => e.Id == plannedTask.Id);
                    if (!taskExists)
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["projectId"] = new SelectList(_context.Projects, "Id", "Id", plannedTask.projectId);
            return View(plannedTask);
        }

      
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            
            var plannedTask = await _context.PlannedTasks
                .Include(task => task.project)
                .FirstOrDefaultAsync(proj => proj.Id == id);
            if (plannedTask == null)
                return NotFound();
        

            return View(plannedTask);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.PlannedTasks.FindAsync(id);
            _context.PlannedTasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
