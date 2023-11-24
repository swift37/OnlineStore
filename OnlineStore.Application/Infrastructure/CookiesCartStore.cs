using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineStore.Application.Interfaces.Infrastructure;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Infrastructure
{
    public class CookiesCartStore : ICartStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private HttpRequest Request => _httpContextAccessor.HttpContext.Request!;

        private HttpResponse Response => _httpContextAccessor.HttpContext.Response!;

        public Cart? Cart
        {
            get
            {
                var cookies = Response.Cookies;
                var cartCookies = Request.Cookies[_cartName];
                if (cartCookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookies(cookies, cartCookies);
                return JsonConvert.DeserializeObject<Cart>(cartCookies);
            }

            set => ReplaceCookies(Response.Cookies, JsonConvert.SerializeObject(value));
        }

        public CookiesCartStore(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User;
            var username = (user.Identity?.IsAuthenticated ?? false) ? $"{user.Identity.Name}" : null;
            _cartName = $"OnlineStore.Cart-{username}";
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookiesString)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookiesString);
        }
    }
}
