using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Swashbuckle.Examples;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Dtos;
using WebApplicationExercise.Exceptions;
using WebApplicationExercise.SwaggerExamples;

namespace WebApplicationExercise.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderRepository _repository;
        
        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get order by Id
        /// </summary>
        /// <remarks>
        /// Get order by Id
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(OrderExamples))]
        public async Task<IHttpActionResult> GetOrder(Guid id)
        {
            try
            {
                var order = await _repository.Get(id);
                return Ok(order);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get orders with filters
        /// </summary>
        /// <remarks>
        /// Allows to get orders with "from-to" date filter and/or by customer name
        /// </remarks>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        [HttpGet]
        [System.Web.Mvc.HandleError]
        public async Task<IEnumerable<Order>> GetOrders(
            [FromUri]DateTime? from = null,
            [FromUri]DateTime? to = null,
            [FromUri]string customerName = null)
        {
            var orders = await _repository.GetOrders(from, to, customerName);

            return orders;
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <remarks>
        /// Create a new order
        /// </remarks>
        /// <param name="orderProductsDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> CreateOrder([FromBody]OrderProductsDto orderProductsDto)
        {
            try
            {
                var result = await _repository.Create(orderProductsDto.Order, orderProductsDto.ProductIds);
                return Ok(result);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update an existing order
        /// </summary>
        /// <remarks>
        /// Update an existing order
        /// </remarks>
        /// <param name="order"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateOrder([FromBody] Order order, Guid id)
        {
            try
            {
                order.Id = id;
                var result = await _repository.Update(order);
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("assignProducts")]
        public async Task<IHttpActionResult> AssignProducts([FromBody] OrderProductIdsDto orderProductIdsDto)
        {
            try
            {
                var result = await _repository.AssignProducts(orderProductIdsDto);
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete an existing order
        /// </summary>
        /// <remarks>
        /// Delete an existing order
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteOrder(Guid id)
        {
            try
            {
                var order = await _repository.Delete(id);
                return Ok(order);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }
    }
}