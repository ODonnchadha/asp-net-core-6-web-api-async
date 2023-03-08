using Microsoft.AspNetCore.Mvc;

namespace Books.API.Extensions.Filters
{
    public static class ObjectResultExtensions
    {
        public static bool IsValidRequest(this ObjectResult? result) 
        { 
            if (result == null)
            {
                return false;
            }

            if (result.StatusCode < 200 || result.StatusCode >= 300)
            {
                return false;
            }

            return true;
        }
    }
}
