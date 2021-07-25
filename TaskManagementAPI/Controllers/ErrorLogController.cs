using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using ModelLibrary.Models;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ErrorLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorLog>>> GetError_Logs()
        {
            return await _context.Error_Logs.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ErrorLog>> PostError_Logs(ErrorLog error_Logs)
        {
            _context.Error_Logs.Add(error_Logs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetError_Logs", new { id = error_Logs.ID }, error_Logs);
        }
    }
}
