using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TMS.Api.Models;
using TMS.Api.Models.DTOs;
using TMS.Api.Repositories;

namespace TMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<EventDTO>> GetEvents() {
            var events = _eventRepository.GetEvents().ToList();

            var dtoEvents = events.Select(e => new EventDTO()
            {
                EventId = e.EventId,
                Venue = _mapper.Map<VenueDTO>(e.Venue),
                Type = e.EventType?.EventTypeName ?? string.Empty,
                EventDescription = e.EventDescription ?? string.Empty,
                EventName = e.EventName ?? string.Empty,
                EventStartDate = e.EventStartDate ?? DateTime.MinValue,
                EventEndDate = e.EventEndDate ?? DateTime.MinValue
            });

            return Ok(dtoEvents);
        }

        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetEventById(int id)
        {
            var @event = await _eventRepository.GetById(@id);

            if (@event == null)
                return NotFound();

            var dtoEvent = _mapper.Map<EventDTO>(@event);
            dtoEvent.Type = @event.EventType?.EventTypeName ?? string.Empty;

            return Ok(dtoEvent);
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDTO>> PatchEvent(EventPatchDTO eventPatch)
        {
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);
            
            if (eventEntity == null)
                return NotFound();

            if (!eventPatch.EventName.IsNullOrEmpty())
                eventEntity.EventName = eventPatch.EventName;

            if (!eventPatch.EventDescription.IsNullOrEmpty())
                eventEntity.EventDescription = eventPatch.EventDescription;

            _eventRepository.Update(eventEntity);
            return NoContent(); 
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);

            if (eventEntity == null)
                return NotFound();

            _eventRepository.Delete(eventEntity);
            return NoContent();
        }
    }
}
