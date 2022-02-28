﻿using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.APIServices.MaterialTypeService
{
    public interface IMaterialTypeService
    {
        Task<IEnumerable<MaterialType>> GetAllAsync();
        Task<MaterialType?> GetByIdAsync(int materialTypeId);
        Task<MaterialType> AddMaterialTypeAsync(MaterialType materialType);
        Task<MaterialType?> UpdateMaterialTypeAsync(MaterialType materialType);
        Task<bool> DeleteMaterialTypeAsync(int materialTypeId);
    }
}
