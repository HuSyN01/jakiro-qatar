using JAKIRO_QATAR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JAKIRO_QATAR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly JakiroDbContext _jakiroDbContext;
        public ProductsController(JakiroDbContext jakiroDbContext)
        {
            _jakiroDbContext = jakiroDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_jakiroDbContext.Products == null)
            {
                return NotFound();
            }

            return await _jakiroDbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_jakiroDbContext.Products == null)
            {
                return NotFound();
            }

            var product = await _jakiroDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _jakiroDbContext.Products.Add(product);
            await _jakiroDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id=product.id}, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

            _jakiroDbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _jakiroDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (_jakiroDbContext.Products == null)
            {
                return NotFound();
            }
            var product = await _jakiroDbContext.Products.FindAsync(id);
            if (product == null) 
            { 
                return NotFound();
            }

            _jakiroDbContext.Products.Remove(product);

             await _jakiroDbContext.SaveChangesAsync();

            return Ok();
        }
    }

}
