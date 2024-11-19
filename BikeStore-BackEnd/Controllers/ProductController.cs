using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeStore_BackEnd.Dto; // Make sure this is the correct namespace for your ProductDTO
using BikeStore_BackEnd.Iservices; // Make sure this is the correct namespace for IProductService

namespace BikeStore_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct(ProductDTO productDto)
        {
            var createdProduct = await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
        }

        // PUT: api/product
        [HttpPut]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(ProductDTO productDto)
        {
            var updatedProduct = await _productService.UpdateProductAsync(productDto);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent(); // 204 No Content
        }
    }
}
