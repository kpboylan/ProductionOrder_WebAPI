using ProductionOrder_WebAPI.DAL;
using ProductionOrder_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProductionOrder_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UOMController : ControllerBase
    {
        private readonly IProdOrderRepository _prodOrderRepository;

        public UOMController(IProdOrderRepository prodOrderRepository)
        {
            _prodOrderRepository = prodOrderRepository;
        }

        [HttpGet]
        public List<UOM> GetUom()
        {
            var uom = _prodOrderRepository.GetUOM();

            return uom;

        }
    }
}
