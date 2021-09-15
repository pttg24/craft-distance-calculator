namespace craft.Utils
{
    using System.Linq;

    public static class StringUtils
    {
        public static string ToSnakeCase(this string self) =>
            string.Concat(self.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()))
                .ToLower();
    }
}