using AutoMapper;
using OnlineStore.MVC.Models.Specification;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class SpecificationsService : HttpClientServiceBase, ISpecificationsService
    {
        public SpecificationsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<SpecificationViewModel>>> GetAll()
        {
            try
            {
                var specifications = await _client.GetAllSpecificationsAsync(_usingVersion);
                return new Response<IEnumerable<SpecificationViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<SpecificationViewModel>>(specifications)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<SpecificationViewModel>>(exception);
            }
        }

        public async Task<Response<SpecificationViewModel>> Get(int id)
        {
            try
            {
                var specification = await _client.GetSpecificationAsync(id, _usingVersion);
                return new Response<SpecificationViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<SpecificationViewModel>(specification)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<SpecificationViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var specification = await _client.ExistSpecificationAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(specification)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreateSpecificationViewModel createSpecificationViewModel)
        {
            var createSpecificationDTO = _mapper.Map<CreateSpecificationDTO>(createSpecificationViewModel);

            try
            {
                var response = await _client.CreateSpecificationAsync(_usingVersion, createSpecificationDTO);
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

        public async Task<Response> Update(SpecificationViewModel specificationViewModel)
        {
            var updateSpecificationDTO = _mapper.Map<UpdateSpecificationDTO>(specificationViewModel);

            try
            {
                await _client.UpdateSpecificationAsync(_usingVersion, updateSpecificationDTO);
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
                await _client.DeleteSpecificationAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public async Task<Response<IEnumerable<SpecificationViewModel>>> GetMany(IEnumerable<int> ids)
        {
            try
            {
                var specifications = await _client.GetSpecificationsAsync(ids, _usingVersion);
                return new Response<IEnumerable<SpecificationViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<SpecificationViewModel>>(specifications)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<SpecificationViewModel>>(exception);
            }
        }
    }
}
