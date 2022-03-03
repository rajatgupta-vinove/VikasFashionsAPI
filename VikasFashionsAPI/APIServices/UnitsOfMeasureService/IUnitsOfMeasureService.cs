namespace VikasFashionsAPI.APIServices.UnitsOfMeasureService
{
    public interface IUnitsOfMeasureService 
    {
        Task<IEnumerable<UnitsOfMeasure>> GetAllAsync();
        Task<UnitsOfMeasure?> GetByIdAsync(int UnitsofMeasureId);
        Task<UnitsOfMeasure> AddUnitsOfMeasureAsync(UnitsOfMeasure unitsOfMeasure);
        Task<UnitsOfMeasure?> UpdateUnitsOfMeasureAsync(UnitsOfMeasure unitsOfMeasure);
        Task<bool> DeleteUnitsOfMeasureAsync(int UnitsofMeasureId);


    }
}
