namespace Moqups.Infrastructure.Extensions
{
    public static class HttpRequestExtension
    {
        public static string PathAndQuery(this HttpRequest request)
        { 
            return request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}" //QueryString - mevcut endpoint bilgisi
                : request.Path.ToString(); //yoksa path bilgisi dogrudan donecek
        
        }
    }
}
