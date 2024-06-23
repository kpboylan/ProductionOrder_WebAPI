using ProductionOrder_WebAPI.DAL;
using ProductionOrder_WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProductionOrder_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IProdOrderRepository _prodOrderRepository;

        public OrderController(IProdOrderRepository prodOrderRepository)
        {
            _prodOrderRepository = prodOrderRepository;
        }

        [HttpPost]
        public HttpResponseMessage Post(List<Order> orders)
        {
            _prodOrderRepository.AddProdOrder(orders.First());

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            var orders = _prodOrderRepository.GetOrders();

            return orders;

        }
    }
}
