using Newtonsoft.Json;
using OnlineStore.MVC.Models.Cart;
using OnlineStore.MVC.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineStore.MVC.Services
{
    public class CookieCartStorage : ICartStorage
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private HttpRequest Request => _httpContextAccessor.HttpContext!.Request!;

        private HttpResponse Response => _httpContextAccessor.HttpContext!.Response!;

        public CartViewModel? Cart
        {
            get
            {
                var cookies = Response.Cookies;
                var cartCookies = Request.Cookies[_cartName];
                if (cartCookies is null)
                {
                    var cart = new CartViewModel();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookies(cookies, cartCookies);
                return JsonConvert.DeserializeObject<CartViewModel>(cartCookies);
            }

            set => ReplaceCookies(Response.Cookies, JsonConvert.SerializeObject(value));
        }

        public CookieCartStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext!.User;
            var username = (user.Identity?.IsAuthenticated ?? false) ? 
                $"-{user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value}" : 
                null;
            _cartName = Constants.Cart.CookieCartName + username;
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookiesString)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookiesString);
        }
    }
}
