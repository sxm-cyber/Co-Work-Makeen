using MakeenCo_Work.Application.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MakeenCo_Work.Application.Services
{
	public class OtpService : IOtpService
	{
		private readonly IMemoryCache _cache;
		private readonly IConfiguration _configuration;
		private readonly ILogger<OtpService> _logger;


		public OtpService(IMemoryCache cache , IConfiguration configuration , ILogger<OtpService> logger)
		{
			_cache = cache;
			_configuration = configuration;
			_logger = logger;
		}


        public async Task<string> GenerateOtpAsync(string phoneNumber)
        {
            //Generate Otp
            var random = new Random();
            var otp = random.Next(10000, 99999).ToString();


            //Store In Cache
            var expirySeconds = _configuration.GetValue<int>("Otp:ExpiryInSeconds", 120);
            var cacheKey = $"OTP _{phoneNumber}";

            _cache.Set(cacheKey, otp, TimeSpan.FromSeconds(expirySeconds));

            _logger.LogInformation($"OTP Generated For {phoneNumber}: {otp}");

            return await Task.FromResult(otp);
        }


        public async Task<bool> ValidateOtpAsync(string phoneNumber, string otp)
        {
            var cacheKey = $"OTP _{phoneNumber}";

            if(_cache.TryGetValue(cacheKey, out string cachedOtp))
            {
                if(cachedOtp == otp)
                {
                    _cache.Remove(cacheKey);
                    return await Task.FromResult(true);
                }
            }

            return await Task.FromResult(false);
        }


        public async Task SendSmsAsync(string phoneNumber, string otp)
        {
            // Sms Service Should Add Here

            _logger.LogInformation($"SMS Sent To {phoneNumber}: Your Verification Code Is {otp}");

            await Task.CompletedTask;
        }


        public async Task MarkPhoneAsVerifiedAsync(string phoneNumber)
        {
            var cacheKey = $"VERIFIED _{phoneNumber}";

            _cache.Set(cacheKey, true, TimeSpan.FromMinutes(10));

            await Task.CompletedTask;
        }


        public async Task<bool> IsPhoneVerifiedAsync(string phoneNumber)
        {
            var cacheKey = $"VERIFIED _{phoneNumber}";

            return await Task.FromResult(_cache.TryGetValue(cacheKey, out bool _));
        }


        public async Task ClearPhoneVerificationAsync(string phoneNumber)
        {
            var cacheKey = $"VERIFIED _{phoneNumber}";

            _cache.Remove(cacheKey);

            await Task.CompletedTask;
        }
    }
}

