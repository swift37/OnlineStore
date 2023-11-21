using OnlineStore.Application.DTOs.Subscriber;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class SubscribersMapper
    {
        public static SubscriberDTO ToDTO(this Subscriber subscriber) => new SubscriberDTO
        {
            Id = subscriber.Id,
            Email = subscriber.Email,
            SubscribeDate = subscriber.SubscribeDate
        };

        public static Subscriber FromDTO(this SubscriberDTO subscriber) => new Subscriber
        {
            Id = subscriber.Id,
            Email = subscriber.Email,
            SubscribeDate = subscriber.SubscribeDate
        };

        public static Subscriber FromDTO(this CreateSubscriberDTO subscriber) => new Subscriber
        {
            Email = subscriber.Email,
            SubscribeDate = subscriber.SubscribeDate
        };

        public static Subscriber FromDTO(this UpdateSubscriberDTO subscriber) => new Subscriber
        {
            Id = subscriber.Id,
            Email = subscriber.Email,
            SubscribeDate = subscriber.SubscribeDate
        };

        public static IEnumerable<SubscriberDTO> ToDTO(this IEnumerable<Subscriber> subscribers) => subscribers.Select(c => c.ToDTO());

        public static IEnumerable<Subscriber> FromDTO(this IEnumerable<SubscriberDTO> subscribers) => subscribers.Select(c => c.FromDTO());
    }
}
