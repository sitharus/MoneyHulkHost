using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MoneyHulkHost.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyHulkHost.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private MHContext _context;

        public AccountsController(MHContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Account> Get() => _context.Accounts;

        // GET api/values/5
        [HttpGet("{id}")]
        public Account Get(int id) => _context.Accounts.First(a => a.AccountId == id);

        // POST api/values
        [HttpPost]
        public async Task<Account> Post([FromBody]string name)
        {
            var account = new Account
            {
                Name = name
                
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            var account = _context.Accounts.First(a => a.AccountId == id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
