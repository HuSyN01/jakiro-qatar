using JAKIRO_QATAR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JAKIRO_QATAR.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly JakiroDbContext _jakiroDbContext; // Replace with your actual DbContext class

        public CartController(JakiroDbContext context)
        {
            _jakiroDbContext = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            if (_jakiroDbContext.Carts == null)
            {
                return NotFound();
            }

            return await _jakiroDbContext.Carts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            if (_jakiroDbContext.Carts == null)
            {
                return NotFound();
            }

            var cart = await _jakiroDbContext.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _jakiroDbContext.Carts.Add(cart);
            await _jakiroDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        [HttpPost("{cartId}/add-product/{productId}")]
        public async Task<ActionResult<Cart>> AddProductToCart(int cartId, int productId)
        {
            var cart = await _jakiroDbContext.Carts.FindAsync(cartId);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            var product = await _jakiroDbContext.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            // Add the product to the cart or update the quantity
            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == productId);
            if (cartDetail == null)
            {
                cartDetail = new CartItem { ProductId = productId, Quantity = 1 };
                cart.CartDetails.Add(cartDetail);
            }
            else
            {
                cartDetail.Quantity++;
            }

            await _jakiroDbContext.SaveChangesAsync();
            return cart;
        }
    }
}
