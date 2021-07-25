using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLibrary.Models;
using TaskManagementAPI.Data;
using Task = ModelLibrary.Models.Task;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Task_TeamMemberController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Task_TeamMemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task_TeamMember>>> GetTask_TeamMembers()
        {
            try
            {
                return await _context.Task_TeamMember.ToListAsync();
            }
            catch (Exception ex)
            {
               
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Task_TeamMember>> GetTask_TeamMemberById(int id)
        {
            try
            {
                var Task_TeamMember = await _context.Task_TeamMember.FindAsync(id);

                if (Task_TeamMember == null)
                {
                    return NotFound();
                }
                return Task_TeamMember;
            }
            catch (Exception ex)
            {
                _context.Error_Logs.Add(new ErrorLog
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetTeamMemberTasksById/{MemberId}")]
        public async Task<ActionResult<List<Task>>> GetTeamMemberTasksById(int MemberId)
        {
            try
            {
                 //Getting tasks assigned to employee from the Task_TeamMember juntion table
                var tasks = await _context.Task_TeamMember.Where(x => x.Member_Id == MemberId).ToListAsync();
                if (tasks == null)
                {
                    return NotFound();
                }
                List<Task> empTasks = new List<Task>();

                //Getting full tasks details
                foreach (var task in tasks)
                {
                    var taskObject = _context.Tasks.Find(task.Task_Id);
                    if (taskObject != null)
                    {
                        empTasks.Add(taskObject);
                    }
                }
                return empTasks;
            }
            catch (Exception ex)
            {
                _context.Error_Logs.Add(new ErrorLog
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetTeamMemberTasksByMemberNo/{memberNo}")]
        public async Task<ActionResult<List<Task>>> GetTeamMemberTasksByMemberNo(int memberNo)
        {
            try
            {
                //Getting employee record Id using employee no
                var teamMember = await _context.TeamMembers.Where(x => x.Member_No == memberNo).FirstOrDefaultAsync();
                if (teamMember == null)
                {
                    return NotFound();
                }
                //Getting tasks assigned to employee from the Task_TeamMember juntion table
                var tasks = await _context.Task_TeamMember.Where(x => x.Member_Id == teamMember.Id).ToListAsync();
                List<Task> empTasks = new List<Task>();
                //Getting full tasks details
                foreach (var task in tasks)
                {
                    var taskObject = _context.Tasks.Find(task.Task_Id);
                    if (taskObject != null)
                    {
                        empTasks.Add(taskObject);
                    }

                }
                return empTasks;
            }
            catch (Exception ex)
            {
                _context.Error_Logs.Add(new ErrorLog
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetTaskTeamMemberByTaskId/{taskId}")]
        public async Task<ActionResult<List<TeamMember>>> GetTaskTeamMemberByTaskId(int taskId)
        {
            try
            {
                var emps = await _context.Task_TeamMember.Where(x => x.Task_Id == taskId).ToListAsync();
                if (emps == null)
                {
                    return NotFound();
                }
                List<TeamMember> employees = new List<TeamMember>();
                foreach (var empId in emps)
                {
                    var emp = _context.TeamMembers.Find(empId.Member_Id);
                    if (emp != null)
                    {
                        employees.Add(emp);
                    }
                }
                return employees;
            }
            catch (Exception ex)
            {
                _context.Error_Logs.Add(new ErrorLog
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask_TeamMember(int id, Task_TeamMember Task_TeamMember)
        {
            try
            {
                if (id != Task_TeamMember.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Task_TeamMember).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Task_TeamMemberExists(id))
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
                _context.Error_Logs.Add(new ErrorLog
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Task_TeamMember>> PostTask_TeamMember(Task_TeamMember Task_TeamMember)
        {
            try
            {
                Task task = _context.Tasks.FindAsync(Task_TeamMember.Task_Id).Result;
                TeamMember emp = _context.TeamMembers.FindAsync(Task_TeamMember.Member_Id).Result;
                Task_TeamMember.Task = task;
                Task_TeamMember.TeamMember = emp;
                _context.Task_TeamMember.Add(Task_TeamMember);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTask_TeamMember", new { id = Task_TeamMember.Id }, Task_TeamMember);
            }
            catch (Exception ex)
            {
                _context.Error_Logs.Add(new ErrorLog
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Task_TeamMember>> DeleteTask_TeamMember(int id)
        {
            try
            {
                var Task_TeamMember = await _context.Task_TeamMember.FindAsync(id);
                if (Task_TeamMember == null)
                {
                    return NotFound();
                }
                _context.Task_TeamMember.Remove(Task_TeamMember);
                await _context.SaveChangesAsync();
                return Task_TeamMember;
            }
            catch (Exception ex)
            {
                _context.Error_Logs.Add(new ErrorLog
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        private bool Task_TeamMemberExists(int id)
        {
            return _context.Task_TeamMember.Any(e => e.Id == id);
        }
    }
}
