using GeekShopping.Web.Extensions;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.ViewModels;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services;

public class CouponService : ICouponService
{
    private const string BasePath = "api/v1/coupon";
    private readonly HttpClient _httpClient;

    public CouponService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CouponViewModel?> GetCouponByCouponCodeAsync(string couponCode, string token, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BasePath}/get-coupon-by-code/{couponCode}", HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        if(response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return new CouponViewModel();
        }

        return await response.ReadContentAs<CouponViewModel>();
    }
}
