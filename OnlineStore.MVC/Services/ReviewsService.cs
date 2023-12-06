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

        public async Task<Response<IEnumerable<ReviewViewModel>>> GetAll()
        {
            try
            {
                var reviews = await _client.GetAllReviewsAsync(_usingVersion);
                return new Response<IEnumerable<ReviewViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<ReviewViewModel>>(reviews)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<ReviewViewModel>>(exception);
            }
        }

        public async Task<Response<ReviewViewModel>> Get(int id)
        {
            try
            {
                var review = await _client.GetReviewAsync(id, _usingVersion);
                return new Response<ReviewViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<ReviewViewModel>(review)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<ReviewViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var review = await _client.ExistReviewAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(review)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
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

        public async Task<Response<IEnumerable<ReviewViewModel>>> GetReviewsByProduct(int productId)
        {
            try
            {
                var reviews = await _client.GetReviewsByProductAsync(productId, _usingVersion);
                return new Response<IEnumerable<ReviewViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<ReviewViewModel>>(reviews)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<ReviewViewModel>>(exception);
            }
        }
    }
}
