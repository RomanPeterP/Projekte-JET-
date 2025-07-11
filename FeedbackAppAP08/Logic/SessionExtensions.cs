using Microsoft.AspNetCore.Http;
using System.Text.Json;
namespace FeedbackAppAP08.Logic
{
    public static class SessionExtension
    {
        public static T? GetObject<T>(this ISession session, string key)
        {
            if (session == null || string.IsNullOrWhiteSpace(key))
            {
                return default;
            }
            var value = session.GetString(key);
            try
            {
                return string.IsNullOrWhiteSpace(value) ? default : JsonSerializer.Deserialize<T>(value);
            }
            catch
            {
                return default;
            }
        }
        public static void SetObject<T>(this ISession session, string key, T t) where T : class
        {
            if (session == null || string.IsNullOrWhiteSpace(key) || t == null)
            {
                return;
            }
            session.SetString(key, JsonSerializer.Serialize(t));
        }
    }
}
