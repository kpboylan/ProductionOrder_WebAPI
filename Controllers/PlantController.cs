using ProductionOrder_WebAPI.DAL;
using ProductionOrder_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProductionOrder_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantController : ControllerBase
    {
        private readonly IProdOrderRepository _prodOrderRepository;

        public PlantController(IProdOrderRepository prodOrderRepository)
        {
            _prodOrderRepository = prodOrderRepository;
        }

        [HttpGet]
        public List<Plant> GetPlants()
        {
            var plants = _prodOrderRepository.GetPlantLocations();

            return plants;

        }
    }
}
