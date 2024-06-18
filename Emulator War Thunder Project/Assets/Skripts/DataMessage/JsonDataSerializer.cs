using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public static class JsonDataSerializer
{
    public static string SerializeToJsonString<T>(T data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All // Указываем сохранение информации о типах
        });
        return json;
    }

    public static T DeserializeJson<T>(string json)
    {
        var knownTypes = new List<Type>
        {
            typeof(Package),
            typeof(PackageValueFloat),
            typeof(PackageValueInt),
            typeof(PackageValueBool),
            typeof(PackageValueVector2),
            typeof(PackageValueString),
            typeof(PackageProjectile),
        };
        var binder = new CustomSerializationBinder(knownTypes);

        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            SerializationBinder = binder
        };

        T deserializedData = JsonConvert.DeserializeObject<T>(json, settings);

        return deserializedData;
    }
}
