using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Models;
using Project.Api.ViewModels.PeopleViewModels.CreateViewModels;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Getasync()
        {
            return Ok(await _context.People.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreatePersonRequest personRequest,
            CancellationToken cancellation = default)
        {
            Person person = new(personRequest.FirstName, personRequest.LastName);

            await _context.AddAsync(person, cancellation);
            await _context.SaveChangesAsync(cancellation);

            return Ok(person);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id,
            [FromBody] UpatePersonRequest personRequest)
        {
            if (id != personRequest.Id)
            {
                return NotFound();
            }
            try
            {
                Person? person = await _context.People.FirstOrDefaultAsync(m => m.Id == id);
                if (person == null)
                {
                    return NotFound();
                }

                person.Update(personRequest.FirstName, personRequest.LastName);
                await _context.SaveChangesAsync();

                return Ok(person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(personRequest.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();

            return Ok(id);
        }

        private bool PersonExists(Guid id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
