using AutoMapper;
using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class ReviewsService : HttpClientServiceBase, IReviewsService
    {
        public ReviewsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public Task<Response<IEnumerable<ReviewViewModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<ReviewViewModel>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Exist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<int>> Create(CreateReviewViewModel createReviewViewModel)
        {
            var createReviewDTO = _mapper.Map<CreateReviewDTO>(createReviewViewModel);

            try
            {
                var response = await _client.CreateReviewAsync(_usingVersion, createReviewDTO);
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

        public async Task<Response> Update(ReviewViewModel reviewViewModel)
        {
            var updateReviewDTO = _mapper.Map<UpdateReviewDTO>(reviewViewModel);

            try
            {
                await _client.UpdateReviewAsync(_usingVersion, updateReviewDTO);
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
                await _client.DeleteReviewAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public Task<Response<IEnumerable<ReviewViewModel>>> GetReviewsByProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
