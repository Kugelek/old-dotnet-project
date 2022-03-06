using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMVC.Data;
using ProjectMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace ProjectMVC.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [Route("Projects")]
        public async Task<IActionResult> Index()
        {
            var projects = from a in _context.Projects
                           orderby a.deadline descending
                           select a;
            return View(await projects.ToListAsync());
        }

     
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            
            var chosenProject = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);

            if (chosenProject == null)
                return NotFound();
            
            return View(chosenProject);
        }
        

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,name,description,deadline")] Project project)
        {
            if (ModelState.IsValid){
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
      
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null){
                return NotFound();
            }
            var oldChosenProject = await _context.Projects.FindAsync(id);

            if (oldChosenProject == null){
                return NotFound();
            }
            return View(oldChosenProject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,description,deadline")] Project candidateProject)
        {
            
            if (id == null || id != candidateProject.Id)
                return NotFound();
            
            if (ModelState.IsValid){
                try{
                    _context.Update(candidateProject);
                    await _context.SaveChangesAsync();
                }catch (DbUpdateConcurrencyException){
                    //404 jak nie ma, a jak scatchowalo inny case to 500
                    var projectExists = _context.Projects.Any(e => e.Id == candidateProject.Id);
                    if (!projectExists)
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(candidateProject);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var chosenProject = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (chosenProject == null)
                return NotFound();
            return View(chosenProject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete == null)
                return NotFound();

            _context.Projects.Remove(projectToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
