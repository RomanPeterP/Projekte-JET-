using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystem.Logic
{
    public class MiscLogic: IMiscLogic
    {
        private readonly IResponse<Country> _response;
        private readonly IMiscRepository _repository;

        public MiscLogic(IMiscRepository repository, IResponse<Country> response)
        {
            _response = response;
            _repository = repository;
        }

        public IResponse<Country> CountriesData()
        {
            try
            {
                _response.Data = _repository.Countries;
                _response.StatusCode = Enums.StatusCode.Success;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
