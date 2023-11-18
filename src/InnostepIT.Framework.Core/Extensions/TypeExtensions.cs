namespace InnostepIT.Framework.Core.Extensions;

public static class TypeExtensions
{
    public static string ToLogFriendlyName(this Type type)
    {
        if (!type.IsGenericType)
            return type.Name;

        var baseName = type.Name.Split('`').First();
        var genericArgs = type.GetGenericArguments().Select(ToLogFriendlyName);
        var genericNames = string.Join(", ", genericArgs);
        return $"{baseName}<{genericNames}>";
    }
}