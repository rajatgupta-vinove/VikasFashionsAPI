﻿using VikasFashionsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VikasFashionsAPI.APIServices.PlantBranchService
{
    public class PlantBranchService : IPlantBranchService
    {
        private readonly ILogger<PlantBranchService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        /// <summary>
        /// Default Plant Branch Service class constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        public PlantBranchService(IConfiguration config, ILogger<PlantBranchService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        /// <summary>
        /// Add New Plant Branch
        /// </summary>
        /// <param name="plantBranch"></param>
        /// <returns>PlantBranch</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<PlantBranch> AddPlantBranchAsync(PlantBranch plantBranch)
        {
            try
            {
                if (plantBranch == null)
                    throw new ArgumentNullException("AddPlantBranch");
                _context.PlantBranches.Add(plantBranch);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding plant branch", ex);
            }
            return plantBranch;
        }

        /// <summary>
        /// Delete Plant Branch based on PlantBranche Id
        /// </summary>
        /// <param name="plantId"></param>
        /// <returns>isDeleted</returns>
        public async Task<bool> DeletePlantBranchAsync(int plantId)
        {
            bool isDeleted = false;
            try
            {
                if (plantId == 0)
                    return isDeleted;
                var plantBranch = await _context.PlantBranches.FirstOrDefaultAsync(m => m.PlantId == plantId);
                if (plantBranch == null)
                    return isDeleted;
                _context.PlantBranches.Remove(plantBranch);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting plant branch", ex);
            }
            return isDeleted;
        }

        /// <summary>
        /// Get all Plant Branch Details.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PlantBranch>> GetAllPlantBranchAsync()
        {
            _log.LogInformation("PlantBranch GetAll Called!");
            return await _context.PlantBranches.ToListAsync();
        }

        /// <summary>
        /// Get Plant Branch details based on Plant Branch Id.
        /// </summary>
        /// <param name="plantId"></param>
        /// <returns>plantBranch</returns>
        public async Task<PlantBranch?> GetByPlantBranchIdAsync(int plantId)
        {
            PlantBranch? plantBranch = null;
            try
            {
                if (plantId == 0)
                    return plantBranch;
                plantBranch = await _context.PlantBranches.FirstOrDefaultAsync(m => m.PlantId == plantId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting plant branch", ex);
            }
            return plantBranch;
        }

        /// <summary>
        /// Update existing Plant Branch details based on Plant Branch ID.
        /// </summary>
        /// <param name="PlantBranch"></param>
        /// <returns>plantBranch</returns>
        public async Task<PlantBranch?> UpdatePlantBranchAsync(PlantBranch plantBranch)
        {
            try
            {
                var exisingPlantBranch = await _context.PlantBranches.FirstOrDefaultAsync(m => m.PlantId == plantBranch.PlantId);               
                if (exisingPlantBranch == null)
                    return null;
                exisingPlantBranch.PlantId = plantBranch.PlantId;
                exisingPlantBranch.PlantName = plantBranch.PlantName;
                exisingPlantBranch.Address1 = plantBranch.Address1;
                exisingPlantBranch.Address2 = plantBranch.Address2;
                exisingPlantBranch.CityId = plantBranch.CityId; 
                exisingPlantBranch.PinCode = plantBranch.PinCode;
                exisingPlantBranch.PANNo = plantBranch.PANNo;
                exisingPlantBranch.CSTNo = plantBranch.CSTNo;
                exisingPlantBranch.GSTIN = plantBranch.GSTIN;
                exisingPlantBranch.GSTType = plantBranch.GSTType;
                exisingPlantBranch.Remark = plantBranch.Remark;
                exisingPlantBranch.Logo = plantBranch.Logo;
                exisingPlantBranch.CompanyId = plantBranch.CompanyId;
                exisingPlantBranch.BranchType= plantBranch.BranchType;

               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating plant branch", ex);
            }
            return plantBranch;
        }

     
    }
}