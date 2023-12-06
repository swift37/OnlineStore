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

        public Task<Response<IEnumerable<ContactRequestViewModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<ContactRequestViewModel>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Exist(int id)
        {
            throw new NotImplementedException();
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
