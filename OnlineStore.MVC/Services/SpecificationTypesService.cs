using AutoMapper;
using OnlineStore.MVC.Models.SpecificationType;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class SpecificationTypesService : HttpClientServiceBase, ISpecificationTypesService
    {
        public SpecificationTypesService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<SpecificationTypeViewModel>>> GetAll()
        {
            try
            {
                var specifications = await _client.GetAllSpecificationTypesAsync(_usingVersion);
                return new Response<IEnumerable<SpecificationTypeViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<SpecificationTypeViewModel>>(specifications)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<SpecificationTypeViewModel>>(exception);
            }
        }

        public async Task<Response<SpecificationTypeViewModel>> Get(int id)
        {
            try
            {
                var specification = await _client.GetSpecificationTypeAsync(id, _usingVersion);
                return new Response<SpecificationTypeViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<SpecificationTypeViewModel>(specification)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<SpecificationTypeViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var specification = await _client.ExistSpecificationTypeAsync(id, _usingVersion);
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

        public async Task<Response<int>> Create(CreateSpecificationTypeViewModel createSpecificationTypeViewModel)
        {
            var createSpecificationTypeDTO = _mapper.Map<CreateSpecificationTypeDTO>(createSpecificationTypeViewModel);

            try
            {
                var response = await _client.CreateSpecificationTypeAsync(_usingVersion, createSpecificationTypeDTO);
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

        public async Task<Response> Update(SpecificationTypeViewModel specificationViewModel)
        {
            var updateSpecificationTypeDTO = _mapper.Map<UpdateSpecificationTypeDTO>(specificationViewModel);

            try
            {
                await _client.UpdateSpecificationTypeAsync(_usingVersion, updateSpecificationTypeDTO);
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
                await _client.DeleteSpecificationTypeAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
