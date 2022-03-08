namespace VikasFashionsAPI.APIServices.StateService
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetAllAsync();
        Task<IEnumerable<State>> GetAllByStatusAsync(bool? isActive);
        Task<IEnumerable<State>> GetAllByCountryIdAsync(int countryId);
        Task<IEnumerable<State>> GetAllByCountryAndStatusAsync(int countryId, bool? isActive);
        Task<State?> GetByIdAsync(int StateId);
        Task<State> AddStateAsync(State State);
        Task<State?> UpdateStateAsync(State State);
        Task<bool> DeleteStateAsync(int StateId);
        Task<State?> ChangeStateStatusAsync(int StateId, int updatedBy, DateTime updatedOn);

    }
}
