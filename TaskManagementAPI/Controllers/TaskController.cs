using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using Task = ModelLibrary.Models.Task;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            try { 
            return await _context.Tasks.ToListAsync();
            }
            catch (Exception ex)
            {
               
                return StatusCode(500);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            try { 
            var tasks = await _context.Tasks.FindAsync(id);

            if (tasks == null)
            {
                return NotFound();
            }

            return tasks;
            }
            catch (Exception ex)
            {
               
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Task task)
        {
            try { 
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
            }
            catch (Exception ex)
            {
               
                return StatusCode(500);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(Task task)
        {
            try
            {
                _context.Tasks.Add(task);
                var r = await _context.SaveChangesAsync();

                return CreatedAtAction("GetTasks", new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500);
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Task>> DeleteTask(int id)
        {
            try { 
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return tasks;
            }
            catch (Exception ex)
            {
               
                return StatusCode(500);
            }
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
