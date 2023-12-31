﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoitemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoitemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Todoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todoitem>>> GetTodoItems()
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Todoitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todoitem>> GetTodoitem(long id)
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            var todoitem = await _context.TodoItems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return todoitem;
        }

        // PUT: api/Todoitems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoitem(long id, Todoitem todoitem)
        {
            if (id != todoitem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoitem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoitemExists(id))
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

        // POST: api/Todoitems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todoitem>> PostTodoitem(Todoitem todoitem)
        {
          if (_context.TodoItems == null)
          {
              return Problem("Entity set 'TodoContext.TodoItems'  is null.");
          }
            _context.TodoItems.Add(todoitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoitem", new { id = todoitem.Id }, todoitem);
        }

        // DELETE: api/Todoitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoitem(long id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var todoitem = await _context.TodoItems.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoitemExists(long id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
