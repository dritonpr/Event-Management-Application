using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EventManagement.API.Services
{
    public class MySessionService : IMySessionService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public MySessionService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public long GetUserId()
        {
            long userId = 0;
            if (httpContextAccessor != null)
            {
                try
                {
                    userId = Convert.ToInt64(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                }
                catch (Exception)
                {
                }
            }
            return userId;
        }

        public string GetUsername()
        {
            string userName = "";
            if (httpContextAccessor != null)
            {
                try
                {
                    userName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                }
                catch (Exception)
                {
                }
            }
            return userName;
        }
    }
    public interface IMySessionService
    {
        long GetUserId();
        string GetUsername();
    }
}
