using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpContext.Session
{
    public static class GetSetObject
    {
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
        public static void SetObject<T>(this ISession session, T obj, string key)
        {
            session.SetString(key, JsonSerializer.Serialize<T>(obj));
        }
    }
}
