using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Swashbuckle.Examples;
using Unity;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Models;
using WebApplicationExercise.Dtos;
using WebApplicationExercise.Exceptions;
using WebApplicationExercise.SwaggerExamples;

namespace WebApplicationExercise.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
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
        /// Get order by id
        /// </remarks>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getOrder/{orderId}")]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(OrderExamples))]
        public async Task<IHttpActionResult> GetOrder(Guid orderId)
        {
            try
            {
                var order = await _repository.Get(orderId);
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
        [Route("getOrders")]
        public async Task<IEnumerable<Order>> GetOrders(
            [FromUri]DateTime? from = null,
            [FromUri]DateTime? to = null,
            [FromUri]string customerName = null)
        {
            var orders = await _repository.GetOrders(from, to, customerName);

            return orders.ToList();
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <remarks>
        /// Create a new order
        /// </remarks>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveOrder")]
        public async Task<IHttpActionResult> SaveOrder([FromBody]Order order)
        {
            try
            {
                var result = await _repository.Create(order);
                return Ok(result);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updateOrder")]
        public async Task<IHttpActionResult> UpdateOrder([FromBody] Order order)
        {
            try
            {
                /*var result =*/
                await _repository.Update(order);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("assignProducts")]
        public async Task<IHttpActionResult> AssignProducts([FromBody] OrderProductsDto orderProductsDto)
        {
            try
            {
                var result = await _repository.AssignProducts(orderProductsDto);
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
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteOrder/{orderId}")]
        public async Task<IHttpActionResult> DeleteOrder(Guid orderId)
        {
            try
            {
                var order = await _repository.Delete(orderId);
                return Ok(order);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }
    }
}