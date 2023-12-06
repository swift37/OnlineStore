using AutoMapper;
using OnlineStore.MVC.Models.ContactRequest;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class ContactRequetsService : HttpClientServiceBase, IContactRequestsService
    {
        public ContactRequetsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<ContactRequestViewModel>>> GetAll()
        {
            try
            {
                var contactRequests = await _client.GetAllContactRequestsAsync(_usingVersion);
                return new Response<IEnumerable<ContactRequestViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<ContactRequestViewModel>>(contactRequests)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<ContactRequestViewModel>>(exception);
            }
        }

        public async Task<Response<ContactRequestViewModel>> Get(int id)
        {
            try
            {
                var contactRequest = await _client.GetContactRequestAsync(id, _usingVersion);
                return new Response<ContactRequestViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<ContactRequestViewModel>(contactRequest)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<ContactRequestViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var contactRequest = await _client.ExistContactRequestAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(contactRequest)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreateContactRequestViewModel createContactRequestViewModel)
        {
            var createContactRequestDTO = _mapper.Map<CreateContactRequestDTO>(createContactRequestViewModel);

            try
            {
                var response = await _client.CreateContactRequestAsync(_usingVersion, createContactRequestDTO);
                return new Response<int>
                {
                    Success = true,
                    Data = response
                };
            }
            catch (ApiException e)
            {
                var generatedResponse = GenerateResponse(e);
                return new Response<int>(generatedResponse);
            }
        }

        public async Task<Response> Update(ContactRequestViewModel contactRequestViewModel)
        {
            var updateContactRequestDTO = _mapper.Map<UpdateContactRequestDTO>(contactRequestViewModel);

            try
            {
                await _client.UpdateContactRequestAsync(_usingVersion, updateContactRequestDTO);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                await _client.DeleteContactRequestAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
