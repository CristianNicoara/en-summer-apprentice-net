using TMS.Api.Models;

namespace TMS.Api.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetEvents();

        Task<Event> GetById(int id);

        void Update(Event @event);

        void Delete(Event @event);

    }
}
