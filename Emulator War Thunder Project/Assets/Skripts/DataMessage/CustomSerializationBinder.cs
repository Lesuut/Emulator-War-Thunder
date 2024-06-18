using Newtonsoft.Json.Serialization; // For ISerializationBinder
using System;
using System.Collections.Generic;
using System.Linq; // For ToDictionary

public class CustomSerializationBinder : ISerializationBinder
{
    private readonly IDictionary<string, Type> _typeCache;

    public CustomSerializationBinder(IEnumerable<Type> knownTypes)
    {
        _typeCache = knownTypes.ToDictionary(t => t.FullName);
    }

    public Type BindToType(string assemblyName, string typeName)
    {
        _typeCache.TryGetValue(typeName, out var type);
        return type;
    }

    public void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
        assemblyName = null;
        typeName = serializedType.FullName;
    }
}
