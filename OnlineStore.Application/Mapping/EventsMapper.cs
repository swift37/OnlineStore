using OnlineStore.Application.DTOs.Event;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class EventsMapper
    {
        public static EventDTO ToDTO(this Event @event) => new EventDTO
        {
            Id = @event.Id,
            Name = @event.Name,
            StartDate = @event.StartDate,
            FinishDate = @event.FinishDate,
            Description = @event.Description,
            Image = @event.Image
        };

        public static Event FromDTO(this EventDTO @event) => new Event
        {
            Id = @event.Id,
            Name = @event.Name,
            StartDate = @event.StartDate,
            FinishDate = @event.FinishDate,
            Description = @event.Description,
            Image = @event.Image
        };

        public static Event FromDTO(this CreateEventDTO @event) => new Event
        {
            Name = @event.Name,
            StartDate = @event.StartDate,
            FinishDate = @event.FinishDate,
            Description = @event.Description,
            Image = @event.Image
        };

        public static Event FromDTO(this UpdateEventDTO @event) => new Event
        {
            Id = @event.Id,
            Name = @event.Name,
            StartDate = @event.StartDate,
            FinishDate = @event.FinishDate,
            Description = @event.Description,
            Image = @event.Image
        };

        public static IEnumerable<EventDTO> ToDTO(this IEnumerable<Event> events) => events.Select(e => e.ToDTO());

        public static IEnumerable<Event> FromDTO(this IEnumerable<EventDTO> events) => events.Select(e => e.FromDTO());
    }
}
