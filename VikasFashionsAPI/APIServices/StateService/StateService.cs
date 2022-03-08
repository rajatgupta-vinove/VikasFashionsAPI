namespace VikasFashionsAPI.APIServices.StateService
{
    public class StateService : IStateService
    {
        private readonly ILogger<StateService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public StateService(IConfiguration config, ILogger<StateService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<State> AddStateAsync(State State)
        {
            try
            {
                if (State == null)
                    throw new ArgumentNullException("AddState");
                _context.States.Add(State);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding State", ex);
            }
            return State;
        }

        public async Task<bool> DeleteStateAsync(int StateId)
        {
            bool isDeleted = false;
            try
            {
                if (StateId == 0)
                    return isDeleted;
                var State = await _context.States.FirstOrDefaultAsync(m => m.StateId == StateId);
                if (State == null)
                    return isDeleted;
                _context.States.Remove(State);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting State", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            _log.LogInformation("State GetAll Called!");
            return await _context.States.ToListAsync();
        }

        public async Task<IEnumerable<State>> GetAllByStatusAsync(bool? isActive)
        {
            if (isActive == null) return await GetAllAsync();
            return await _context.States.Where(m => m.IsActive == isActive).ToListAsync();
        }
        public async Task<IEnumerable<State>> GetAllByCountryIdAsync(int countryId)
        {
            _log.LogInformation("State GetAllByCountryIdAsync Called!");
            return await _context.States.Where(m => m.CountryId == countryId).ToListAsync();
        }

        public async Task<IEnumerable<State>> GetAllByCountryAndStatusAsync(int countryId, bool? isActive)
        {
            if (isActive == null) return await GetAllByCountryIdAsync(countryId);
            return await _context.States.Where(m => m.IsActive == isActive && m.CountryId == countryId).ToListAsync();
        }

        public async Task<State?> GetByIdAsync(int StateId)
        {
            State? State = null;
            try
            {
                if (StateId == 0)
                    return State;
                State = await _context.States.FirstOrDefaultAsync(m => m.StateId == StateId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting State", ex);
            }
            return State;
        }

        public async Task<State?> UpdateStateAsync(State State)
        {
            try
            {
                var exisingState = await _context.States.FirstOrDefaultAsync(m => m.StateId == State.StateId);
                if (exisingState == null)
                    return null;
                exisingState.StateCode = State.StateCode;
                exisingState.StateId = State.StateId;
                exisingState.StateName = State.StateName;
                exisingState.UpdatedOn = State.UpdatedOn;
                exisingState.UpdatedBy = State.UpdatedBy;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating State", ex);
            }
            return State;
        }

        public async Task<State?> ChangeStateStatusAsync(int StateId, int updatedBy, DateTime updatedOn)
        {
            State? exisingState = null;
            try
            {
                exisingState = await _context.States.FirstOrDefaultAsync(m => m.StateId == StateId);
                if (exisingState == null)
                    return null;
                exisingState.IsActive = !exisingState.IsActive;
                exisingState.UpdatedBy = updatedBy;
                exisingState.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exisingState = null;
                _log.LogError("Error while updating State", ex);
            }
            return exisingState;
        }
    }
}
