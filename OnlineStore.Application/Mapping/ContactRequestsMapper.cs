using OnlineStore.Application.DTOs.ContactRequest;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class ContactRequestsMapper
    {
        public static ContactRequestDTO ToDTO(this ContactRequest contactRequest) => new ContactRequestDTO
        {
            Id = contactRequest.Id,
            ContactName = contactRequest.ContactName,
            Email = contactRequest.Email,
            Message = contactRequest.Message,
            CreationDate = contactRequest.CreationDate,
            ResponseDate = contactRequest.ResponseDate
        };

        public static ContactRequest FromDTO(this ContactRequestDTO contactRequest) => new ContactRequest
        {
            Id = contactRequest.Id,
            ContactName = contactRequest.ContactName,
            Email = contactRequest.Email,
            Message = contactRequest.Message,
            CreationDate = contactRequest.CreationDate,
            ResponseDate = contactRequest.ResponseDate
        };

        public static IEnumerable<ContactRequestDTO> ToDTO(this IEnumerable<ContactRequest> contactRequests) => contactRequests.Select(c => c.ToDTO());

        public static IEnumerable<ContactRequest> FromDTO(this IEnumerable<ContactRequestDTO> contactRequests) => contactRequests.Select(c => c.FromDTO());
    }
}
