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
    public class AccountsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public AccountsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, Account_Custom account)
        {
            Account accountTemp = new Account();
            accountTemp.ToaId = account.ToaId;
            accountTemp.AccountUsername = account.AccountUsername;
            accountTemp.AccountPassword = account.AccountPassword;

            if (id != account.AccountUsername)
            {
                return BadRequest();
            }

            _context.Entry(accountTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccount", new { id = accountTemp.AccountUsername }, accountTemp);
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'LV_QLKSContext.Accounts'  is null.");
            }
            account.Toa = null;
            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (AccountExists(account.AccountUsername))
                {
                    return Conflict();
                }
                else
                {
                    Console.Write(ex.ToString());
                    throw;
                }
            }

            return CreatedAtAction("GetAccount", new { id = account.AccountUsername }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(string id)
        {
            return (_context.Accounts?.Any(e => e.AccountUsername == id)).GetValueOrDefault();
        }

        // GET: api/Accounts/5
        [HttpGet("GetAccountLogin")]
        public async Task<ActionResult<Account>> GetAccountLogin(string id, string pwd)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.Include(a => a.Toa).Where(acc => acc.AccountUsername == id && acc.AccountPassword == pwd).SingleOrDefaultAsync();

            if (account == null)
            {
                return new Account();
            }

            return account;
        }
    }
}
