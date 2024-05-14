using System.Text.Json;

namespace Moqups.Infrastructure.Extensions
{
    public static class SessionExtension //Extensions ifadeler genelde static class'lara yazilir.
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value)); //Ilgili nesne string olarak hafizaya alinir.
        }
        //Objeye bagli olaral calisir.

        public static void SetJson<T>(this ISession session, string key, T value)
        { 
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        //Tip ister. Herhangi bir obje degildir, tipin guvenligi saglanir.

        public static T? GetJson<T>(this ISession session, string key)
        { 
            var data = session.GetString(key);
            return data is null
                ? default(T)
                : JsonSerializer.Deserialize<T>(data);
        }
    }
}
