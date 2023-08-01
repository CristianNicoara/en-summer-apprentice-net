using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TMS.Api.Exceptions;
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

            var dtoEvents = _mapper.Map<IEnumerable<EventDTO>>(events);
            for (int i = 0; i < dtoEvents.Count(); i++)
            {
                dtoEvents.ElementAt(i).Type = events.ElementAt(i).EventType?.EventTypeName ?? string.Empty;
            }

            return Ok(dtoEvents);
        }

        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetEventById(int id)
        {
            try
            {
                var @event = await _eventRepository.GetById(id);

                if (@event == null)
                    return NotFound();

                var dtoEvent = _mapper.Map<EventDTO>(@event);
                dtoEvent.Type = @event.EventType?.EventTypeName ?? string.Empty;

                return Ok(dtoEvent);
            }catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            } 
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDTO>> PatchEvent(EventPatchDTO eventPatch)
        {
            if (eventPatch == null) throw new ArgumentNullException(nameof(eventPatch));

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
