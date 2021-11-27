
using Newtonsoft.Json;

public static class ExtensionMethods
{
    public static T ToEnum<T>(this string value)
    {
        if (!System.Enum.IsDefined(typeof(T), value))
            return default(T);

        return (T)System.Enum.Parse(typeof(T), value, true);
    }

    public static T ToJosnData<T>(this string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }
    public static string ToJosnString<T>(this T data)
    {
        return JsonConvert.SerializeObject(data);
    }
}
