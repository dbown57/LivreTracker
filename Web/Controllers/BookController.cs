using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(LivreTrackerContext livreTrackerContext) : ControllerBase
    {

        // GET: api/BookItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookItemDTO>>> GetBookItems()
        {
            return await livreTrackerContext.BookItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/BookItems/5
        // <snippet_GetByID>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookItemDTO>> GetBookItem(long id)
        {
            var BookItem = await livreTrackerContext.BookItems.FindAsync(id);

            if (BookItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(BookItem);
        }
        // </snippet_GetByID>

        // PUT: api/BookItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Update>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookItem(long id, BookItemDTO todoDTO)
        {
            if (id != todoDTO.Id)
            {
                return BadRequest();
            }

            var BookItem = await livreTrackerContext.BookItems.FindAsync(id);
            if (BookItem == null)
            {
                return NotFound();
            }

            BookItem.Name = todoDTO.Name;
            BookItem.IsReaded = todoDTO.IsReaded;

            try
            {
                await livreTrackerContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BookItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // </snippet_Update>

        // POST: api/BookItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Create>
        [HttpPost]
        public async Task<ActionResult<BookItemDTO>> PostBookItem(BookItemDTO bookDTO)
        {
            var BookItem = new BookItem
            {
                IsReaded = bookDTO.IsReaded,
                Name = bookDTO.Name
            };

            livreTrackerContext.BookItems.Add(BookItem);
            await livreTrackerContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBookItem),
                new { id = BookItem.Id },
                ItemToDTO(BookItem));
        }
        // </snippet_Create>

        // DELETE: api/BookItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookItem(long id)
        {
            var BookItem = await livreTrackerContext.BookItems.FindAsync(id);
            if (BookItem == null)
            {
                return NotFound();
            }

            livreTrackerContext.BookItems.Remove(BookItem);
            await livreTrackerContext.SaveChangesAsync();

            return NoContent();
        }

        private bool BookItemExists(long id)
        {
            return livreTrackerContext.BookItems.Any(e => e.Id == id);
        }

        private static BookItemDTO ItemToDTO(BookItem BookItem) =>
           new BookItemDTO
           {
               Id = BookItem.Id,
               Name = BookItem.Name,
               IsReaded = BookItem.IsReaded
           };
    }
}
