using System.Text;
using MakeenCo_Work.Application.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MakeenCo_Work.Application.Services
{
	public class OtpService : IOtpService
	{
		private readonly IDistributedCache _cache;
		private readonly IConfiguration _configuration;
		private readonly ILogger<OtpService> _logger;


		public OtpService(IDistributedCache cache , IConfiguration configuration , ILogger<OtpService> logger)
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
            var cacheKey = $"OTP_{phoneNumber}";

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirySeconds)
            };

            var otpBytes = Encoding.UTF8.GetBytes(otp);
            await _cache.SetAsync(cacheKey, otpBytes, options);

            _logger.LogInformation($"OTP Generated For {phoneNumber}: {otp} ");

            return otp;
        }


        public async Task<bool> ValidateOtpAsync(string phoneNumber, string otp)
        {
            var cacheKey = $"OTP_{phoneNumber}";

            var cachedOtpBytes = await _cache.GetAsync(cacheKey);

            if(cachedOtpBytes is not null)
            {
                var cachedOtp = Encoding.UTF8.GetString(cachedOtpBytes);

                if(cachedOtp == otp)
                {
                    await _cache.RemoveAsync(cacheKey);

                    return true;
                }
            }

            return false;
        }


        public async Task SendSmsAsync(string phoneNumber, string otp)
        {
            // Sms Service Should Add Here

            _logger.LogInformation($"SMS Sent To {phoneNumber}: Your Verification Code Is {otp}");

            await Task.CompletedTask;
        }


        public async Task MarkPhoneAsVerifiedAsync(string phoneNumber)
        {
            var cacheKey = $"VERIFIED_{phoneNumber}";

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            var valueBytes = Encoding.UTF8.GetBytes("true");

            await _cache.SetAsync(cacheKey, valueBytes, options);

            await Task.CompletedTask;
        }


        public async Task<bool> IsPhoneVerifiedAsync(string phoneNumber)
        {
            var cacheKey = $"VERIFIED_{phoneNumber}";

            var cachedBytes = await _cache.GetAsync(cacheKey);

            return cachedBytes != null;
        }


        public async Task ClearPhoneVerificationAsync(string phoneNumber)
        {
            var cacheKey = $"VERIFIED_{phoneNumber}";

            _cache.Remove(cacheKey);

            await Task.CompletedTask;
        }
    }
}

