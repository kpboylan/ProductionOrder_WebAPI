using ProductionOrder_WebAPI.Model;

namespace ProductionOrder_WebAPI.DAL
{
    public interface IProdOrderRepository
    {
        void AddProdOrder(Order order);
        List<Plant> GetPlantLocations();
        List<Material> GetMaterials();
        List<UOM> GetUOM();
        List<Order> GetOrders();
    }
}
