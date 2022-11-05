using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public ServicesController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
          if (_context.Services == null)
          {
              return NotFound();
          }
            return await _context.Services.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var services = await _context.Services.FindAsync(id);

            if (services == null)
            {
                return NotFound();
            }

            return services;
        }
        [HttpGet("GetAllServiceOfOwner/{phone}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServiceOfOwner(string phone)
        {
          if (_context.Services == null)
          {
              return NotFound();
          }
            var services = await _context.Services.Where(s => s.UserPhone == phone).ToListAsync();

            if (services == null)
            {
                return NotFound();
            }
            return services;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service_Custom service)
        {
            Service ServiceTemp = new Service();
            ServiceTemp.ServiceId = service.ServiceId;
            ServiceTemp.ServiceName = service.ServiceName;
            ServiceTemp.ServiceDescription = service.ServiceDescription;
            ServiceTemp.UserPhone = service.UserPhone;
            _context.Entry(ServiceTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetService", new { id = ServiceTemp.ServiceId }, ServiceTemp);
        }

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service_Custom service)
        {
            var serviceTemp = new Service();
            serviceTemp.ServiceId = service.ServiceId;
            serviceTemp.ServiceName = service.ServiceName;
            serviceTemp.ServiceDescription = service.ServiceDescription;
            serviceTemp.UserPhone = service.UserPhone;
            if (_context.Services == null)
            {
                return Problem("Entity set 'LV_QLKSContext.Services'  is null.");
            }
            _context.Services.Add(serviceTemp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = serviceTemp.ServiceId }, serviceTemp);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return (_context.Services?.Any(e => e.ServiceId == id)).GetValueOrDefault();
        }
    }
}
