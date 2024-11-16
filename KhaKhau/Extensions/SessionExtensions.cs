﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
namespace KhaKhau.Extensions
{

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string
        key, object value)
        {
            session.SetString(key,System.Text.Json.JsonSerializer.Serialize(value));
        }
        public static T GetObjectFromJson<T>(this ISession session,
        string key)
        {
            var value = session.GetString(key);
            return value == null ? default :
            System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
}
    
