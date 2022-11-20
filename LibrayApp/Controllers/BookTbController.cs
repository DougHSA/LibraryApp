
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Services;
using LibraryApp.Data;
using LibraryApp.Models;
using LibraryApp.Dto.Book;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookTbController : ControllerBase
    {

        private readonly ILogger<BookTbController> _logger;
        private readonly LibraryAppContext _context;
        private readonly BookServices bookService;

        public BookTbController(ILogger<BookTbController> logger, LibraryAppContext context)
        {
            _logger = logger;
            _context = context;
            bookService = new BookServices(_context);
        }
       
        [AllowAnonymous]
        [HttpGet("GetAllBooks")]
        public async Task<ActionResult> GetAll()
        {
            _logger.LogInformation("GetAllBooks");
            try
            {
                var books = bookService.GetAllBooks().ToList();
                if (books == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("GetBooksFromName/{name}")]
        public async Task<ActionResult> GetBooksFromName([FromRoute] string name)
        {
            _logger.LogInformation("GetBooksFromName");
            try
            {
                var books = bookService.GetBooksFromName(name);
                if (books == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("GetBooksFromId/{id}")]
        public async Task<ActionResult> GetBooksFromId([FromRoute] int id)
        {
            _logger.LogInformation("GetBooksFromId");

            try
            {
                var books = bookService.GetBooksFromId(id);
                if (books == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(books);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] BookTbDto book)
        {
            _logger.LogInformation("CreateBook");

            try
            {
                var bookReturn = await bookService.CreateBook(book);
                if (bookReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return CreatedAtAction("PostBook", new { name = bookReturn.Name }, bookReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook([FromRoute] int id, [FromBody] BookTb book)
        {
            _logger.LogInformation("UpdateBook");

            if (id != book.IdBook)
            {
                _logger.LogWarning("No Content");
                return BadRequest();
            }
            try
            {
                var bookReturn = await bookService.UpdateBook(book);
                if (bookReturn == null)
                {
                    return NotFound();
                }
                return CreatedAtAction("UpdateBook", new { id = book.IdBook, name = bookReturn.Name }, bookReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook([FromRoute] int id)
        {
            _logger.LogInformation("DeleteBook");

            try
            {
                var bookReturn = await bookService.DeleteBook(id);
                if (bookReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return CreatedAtAction("DeleteBook", new { id = bookReturn.IdBook, name = bookReturn.Name }, bookReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}
