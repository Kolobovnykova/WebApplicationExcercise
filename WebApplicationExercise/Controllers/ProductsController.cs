using System;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Exceptions;

namespace WebApplicationExercise.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getProduct/{productId}")]
        public async Task<IHttpActionResult> GetOrder(Guid productId)
        {
            try
            {
                var product = await _repository.Get(productId);
                return Ok(product);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("saveProduct")]
        public async Task<IHttpActionResult> SaveOrder([FromBody]Product product)
        {
            try
            {
                await _repository.Create(product);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
        }
    }
}
