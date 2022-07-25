using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ToDoAPI.Data;
using ToDoAPI.Entities;

namespace ToDoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.ToDos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var toDo = await _context.ToDos.FirstOrDefaultAsync(x => x.Id == id);

            if(toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToDoEntity entity)
        {
            if (ModelState.IsValid)
            {
                entity.CreatedAt = DateTime.Now;

                _context.ToDos.Add(entity);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = entity.Id}, entity);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] ToDoEntity entity)
        {
            var toDo = await _context.ToDos.FirstOrDefaultAsync(x => x.Id == id);

            if(toDo == null)
            {
                return NotFound();
            }

            toDo.Title = entity.Title;
            toDo.Done = entity.Done;

            _context.Update(toDo);
            await _context.SaveChangesAsync();  

            return Ok(toDo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var toDo = await _context.ToDos.FirstOrDefaultAsync(x => x.Id == id);

            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
