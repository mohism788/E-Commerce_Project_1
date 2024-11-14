using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Core.Specificatioon;
using Microsoft.AspNetCore.Mvc;



namespace API.Controllers
{

    
    public class ProductsController(IGenericRepository<Product> _ProductRepo) : BaseApiController
    {



        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {

            var spec = new ProductSpecification(specParams);

           
            return await CreatePageResult(_ProductRepo,spec,specParams.PageIndex, specParams.PageSize);

        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById([FromRoute] int id)
        {

            var product = await _ProductRepo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound("Sorry, product is not found in our database!");
            }

            return product;

        }

        [HttpPost]

        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            _ProductRepo.Add(product);

            if (await _ProductRepo.SaveAllAsync())
            {
                return CreatedAtAction("GetProductById", new { id = product.Id }, product);
            }

            return BadRequest("Problem in creating product!");
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {


            if (product.Id != id || !ProductExists(id))
            {
                return BadRequest("Cannot update this product");
            }

            _ProductRepo.Update(product);
            if (await _ProductRepo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem updating the product!");
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _ProductRepo.GetByIdAsync(id);

            if (product == null) return NotFound();

            _ProductRepo.Remove(product);
            if (await _ProductRepo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the product!");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            return Ok(await _ProductRepo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await _ProductRepo.ListAsync(spec));
        }

        private bool ProductExists(int id)
        {
            return _ProductRepo.Exists(id);
        }
        /*
                private bool ProductExists(int id)
                {
                    return _context.Products.Any(x => x.Id == id);
                }*/
    }
}
