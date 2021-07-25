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
    public class TeamMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeamMembers()
        {
            try
            {
                return await _context.TeamMembers.ToListAsync();
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

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMember>> GetTeamMember(int id)
        {
            try
            {
                var teamMember = await _context.TeamMembers.Where(x=>x.Id==id).FirstOrDefaultAsync();

                if (teamMember == null)
                {
                    return NotFound();
                }
                return teamMember;
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
        [Route("GetTeamMemberByMemberNo/{MemberNo}")]
        public async Task<ActionResult<TeamMember>> GetTeamMemberByMemberNo(int MemberNo)
        {
            try
            {
                var teamMember = await _context.TeamMembers.Where(x => x.Member_No == MemberNo).FirstOrDefaultAsync();

                if (teamMember == null)
                {
                    return NotFound();
                }
                return teamMember;
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
        [Route("GetNextAvailableMemberNumber")]
        public async Task<ActionResult<int>> GetNextAvailableMemberNumber()
        {
            try
            {
                //Getting the last added casual employee no and giving the next to be added employee and increment of one of that number
                var lastteamMember = await _context.TeamMembers.OrderBy(x=>x.Id).LastOrDefaultAsync();
                if (lastteamMember != null) {
                    //Separating employee no flag and actual value
                    var lastNoValue = lastteamMember.Member_No + 1;
                    return lastNoValue;
                }
                return 1001;
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
        public async Task<IActionResult> PutTeamMember(int id, TeamMember teamMember)
        {
            try
            {
                if (id != teamMember.Id)
                {
                    return BadRequest();
                }

                _context.Entry(teamMember).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamMemberExists(id))
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
        public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember teamMember)
        {
            try
            {
 
                teamMember.Member_No = GetNextAvailableMemberNumber().Result.Value;
                _context.TeamMembers.Add(teamMember);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTeamMember", new { id = teamMember.Id }, teamMember);
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
        public async Task<ActionResult<TeamMember>> DeleteTeamMember(int id)
        {
            try
            {
                var casualEmployee = await _context.TeamMembers.FindAsync(id);
                if (casualEmployee == null)
                {
                    return NotFound();
                }
                _context.TeamMembers.Remove(casualEmployee);
                await _context.SaveChangesAsync();
                return casualEmployee;
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

        private bool TeamMemberExists(int id)
        {
            return _context.TeamMembers.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("GetTeamMemberTasksByMemberId/{MemberId}")]
        public async Task<ActionResult<List<Task>>> GetTeamMemberTasksByMemberId(int MemberId)
        {
            return await new Task_TeamMemberController(_context).GetTeamMemberTasksByMemberId(MemberId);
        }

        [HttpGet]
        [Route("GetTeamMemberTasksByMemberNo/{memberNo}")]
        public async Task<ActionResult<List<Task>>> GetTeamMemberTasksByMemberNo(int memberNo)
        {
            return await new Task_TeamMemberController(_context).GetTeamMemberTasksByMemberNo(memberNo);
        }
    }
}
