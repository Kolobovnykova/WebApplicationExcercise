using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Exceptions;

namespace WebApplicationExercise.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : BaseApiController
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <remarks>
        /// Get product by Id
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetProduct(Guid id)
        {
            try
            {
                var product = await _repository.Get(id);
                return Ok(product);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <remarks>
        /// Create a new product
        /// </remarks>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct([FromBody]Product product)
        {
            try
            {
                var result = await _repository.Create(product);
                return Ok(result);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
        }
    }
}
