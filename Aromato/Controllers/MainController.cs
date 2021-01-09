using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aromato.Models;
using Aromato.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public MainController(ApplicationDbContext _db)
        {
            db = _db;
        }

        [HttpPost("subscriptions")]
        public async Task<ActionResult> PostSubscriptions(Subscription subscription)
        {
            if (subscription == null)
                return NotFound();

            db.Subscriptions.Add(subscription);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("subscriptions/{password}")]
        public async Task<ActionResult<Subscription>> GetSubscriptions(string password)
        {
            if (password == "VitaVitaChikaChika")
            {
                var subs = await db.Subscriptions.OrderBy(_ => _.Id).ToArrayAsync();
                return Ok(subs);
            }

            return NotFound();
        }

        [HttpPost("counter")]
        public async Task<ActionResult> PostCounter(Counter counter)
        {
            if (counter == null)
                return NotFound();

            db.Counters.Add(counter);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("counter/{password}")]
        public async Task<ActionResult<Counter>> GetCounter(string password)
        {
            if (password == "VitaVitaChikaChika")
            {
                var subs = await db.Counters.OrderBy(_ => _.Id).ToArrayAsync();
                return Ok(subs);
            }

            return NotFound();
        }

        [HttpGet("counter/value")]
        public async Task<ActionResult<Counter>> GetCounterValue()
        {
            int val = 0;
            val = await db.Counters.CountAsync();
            return Ok(val);
        }
    }
}
