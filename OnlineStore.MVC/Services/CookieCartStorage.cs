using Newtonsoft.Json;
using OnlineStore.MVC.Models.Cart;
using OnlineStore.MVC.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                var cartCookies = Request.Cookies[_cartName];
                if (string.IsNullOrEmpty(cartCookies)) TransferCookies(out cartCookies);
                ReplaceCookies(cartCookies);
                return JsonConvert.DeserializeObject<CartViewModel>(cartCookies);
            }

            set => ReplaceCookies(JsonConvert.SerializeObject(value));
        }

        public CookieCartStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext!.User;
            var username = user.Identity?.IsAuthenticated is true ? 
                $"-{user.FindFirstValue(JwtRegisteredClaimNames.Sub)}" : 
                null;
            _cartName = Constants.Cart.CookieCartName + username;
        }

        public CartViewModel? GetUnauthCart()
        {
            TransferCookies(out var cartCookies);
            return JsonConvert.DeserializeObject<CartViewModel>(cartCookies);
        }

        private void TransferCookies(out string cartCookies)
        {
            var unauthCartCookies = Request.Cookies[Constants.Cart.CookieCartName];
            if (string.IsNullOrEmpty(unauthCartCookies))
                unauthCartCookies = JsonConvert.SerializeObject(new CartViewModel());

            cartCookies = unauthCartCookies;
        }

        private void ReplaceCookies(string cookiesString)
        {
            Response.Cookies.Delete(_cartName);
            Response.Cookies.Append(_cartName, cookiesString);
        }
    }
}
