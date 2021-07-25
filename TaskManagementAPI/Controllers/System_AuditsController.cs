using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLibrary.Models;
using TaskManagementAPI.Data;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class System_AuditsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public System_AuditsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<System_Audits>>> Getsystem_Audits()
        {
            return await _context.System_Audits.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<System_Audits>> PostSystem_Audits(System_Audits system_Audits)
        {
            _context.System_Audits.Add(system_Audits);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSystem_Audits", new { id = system_Audits.Id }, system_Audits);
        }

        private bool System_AuditsExists(int id)
        {
            return _context.System_Audits.Any(e => e.Id == id);
        }
    }
}
