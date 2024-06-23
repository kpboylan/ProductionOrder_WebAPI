using ProductionOrder_WebAPI.DAL;
using ProductionOrder_WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ProductionOrder_WebAPI.Send;

namespace ProductionOrder_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderQueueController : ControllerBase
    {
        private readonly IProdOrderRepository _prodOrderRepository;

        public OrderQueueController(IProdOrderRepository prodOrderRepository)
        {
            _prodOrderRepository = prodOrderRepository;
        }

        [HttpPost]
        public HttpResponseMessage Post(List<Order> orders)
        {
            var send = new SendProdOrder();

            send.Send(orders.First());

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
