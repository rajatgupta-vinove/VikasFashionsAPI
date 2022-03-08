namespace VikasFashionsAPI.APIServices.WarehouseService
{
    public class WarehouseService : IWarehouseService
    {
        private readonly ILogger<WarehouseService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        /// <summary>
        /// Default Warehouse Service class constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        public WarehouseService(IConfiguration config, ILogger<WarehouseService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        /// <summary>
        /// Add New warehouse
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns>warehouse</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Warehouse> AddWarehouseAsync(Warehouse warehouse)
        {
            try
            {
                if (warehouse == null)
                    throw new ArgumentNullException("AddWarehouse");
                _context.Warehouses.Add(warehouse);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding warehouse", ex);
            }
            return warehouse;
        }

        /// <summary>
        /// Delete Warehouse based on Warehouse Id
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns>isDeleted</returns>
        public async Task<bool> DeleteWarehouseAsync(int warehouseId)
        {
            bool isDeleted = false;
            try
            {
                if (warehouseId == 0)
                    return isDeleted;
                var Warehouse = await _context.Warehouses.FirstOrDefaultAsync(m => m.WarehouseId == warehouseId);
                if (Warehouse == null)
                    return isDeleted;
                _context.Warehouses.Remove(Warehouse);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting warehouse", ex);
            }
            return isDeleted;
        }

        /// <summary>
        /// Get all Warehouse Details.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Warehouse>> GetAllWarehouseAsync()
        {
            _log.LogInformation("Warehouse GetAll Called!");
            return await _context.Warehouses.ToListAsync();
        }

        /// <summary>
        /// Get warehouse details based on warehouse Id.
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns>Warehouse</returns>
        public async Task<Warehouse?> GetByWarehouseIdAsync(int warehouseId)
        {
            Warehouse? Warehouse = null;
            try
            {
                if (warehouseId == 0)
                    return Warehouse;
                Warehouse = await _context.Warehouses.FirstOrDefaultAsync(m => m.WarehouseId == warehouseId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting warehouse", ex);
            }
            return Warehouse;
        }

        /// <summary>
        /// Update existing warehouse details based on Warehouse ID.
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns>Warehouse</returns>
        public async Task<Warehouse?> UpdateWarehouseAsync(Warehouse warehouse)
        {
            try
            {
                var exisingWarehouse = await _context.Warehouses.FirstOrDefaultAsync(m => m.WarehouseId == warehouse.WarehouseId);               
                if (exisingWarehouse == null)
                    return null;
                exisingWarehouse.WarehouseId = warehouse.WarehouseId;
                exisingWarehouse.WarehouseCode = warehouse.WarehouseCode;
                exisingWarehouse.WarehouseName = warehouse.WarehouseName;
                exisingWarehouse.PlantId = warehouse.PlantId;
                exisingWarehouse.Address1 = warehouse.Address1;
                exisingWarehouse.Address2 = warehouse.Address2;
                exisingWarehouse.CityId = warehouse.CityId; 
                exisingWarehouse.PinCode = warehouse.PinCode;
                exisingWarehouse.PANNo = warehouse.PANNo;
                exisingWarehouse.Remark = warehouse.Remark;


                exisingWarehouse.IsDropShip = warehouse.IsDropShip;
                exisingWarehouse.IsBinLoc = warehouse.IsBinLoc;
                exisingWarehouse.UpdatedBy = warehouse.UpdatedBy;
                exisingWarehouse.UpdatedOn = warehouse.UpdatedOn;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating warehouse", ex);
            }
            return warehouse;
        }
        public async Task<Warehouse?> ChangeWarehouseStatusAsync(int warehouseId, int updatedBy, DateTime updatedOn)
        {
            Warehouse? exisingWarehouse = null;
            try
            {
                exisingWarehouse = await _context.Warehouses.FirstOrDefaultAsync(m => m.WarehouseId == warehouseId);
                if (exisingWarehouse == null)
                    return null;
                exisingWarehouse.IsActive = !exisingWarehouse.IsActive;
                exisingWarehouse.UpdatedBy = updatedBy;
                exisingWarehouse.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exisingWarehouse = null;
                _log.LogError("Error while updating country", ex);
            }
            return exisingWarehouse;
        }
    }
}
