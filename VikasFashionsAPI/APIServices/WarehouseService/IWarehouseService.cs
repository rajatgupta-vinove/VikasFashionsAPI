namespace VikasFashionsAPI.APIServices.WarehouseService
{ 
    public interface IWarehouseService
    {        
           Task<IEnumerable<Warehouse>> GetAllWarehouseAsync();
        Task<Warehouse?> GetByWarehouseIdAsync(int warehouseId);
        Task<Warehouse> AddWarehouseAsync(Warehouse warehouse);
        Task<Warehouse?> UpdateWarehouseAsync(Warehouse warehouse);
        Task<bool> DeleteWarehouseAsync(int warehouseId);
        Task<bool> CheckWarehouseStatusAsync(int WarehouseId, string Warehousecode);
        Task<Warehouse?> ChangeWarehouseStatusAsync(int warehouseId, int updatedBy, DateTime updatedOn);
    }
}
