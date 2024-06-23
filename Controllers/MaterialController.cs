using ProductionOrder_WebAPI.DAL;
using ProductionOrder_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProductionOrder_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly IProdOrderRepository _prodOrderRepository;

        public MaterialController(IProdOrderRepository prodOrderRepository)
        {
            _prodOrderRepository = prodOrderRepository;
        }

        [HttpGet]
        public List<Material> GetMaterials()
        {
            var materials = _prodOrderRepository.GetMaterials();

            return materials;

        }
    }
}
