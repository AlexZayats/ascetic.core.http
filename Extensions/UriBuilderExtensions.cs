namespace Ascetic.Core.Http.Extensions
{
    public static class UriBuilderExtensions
    {
        public static string AddOptionalParameter(this string uri, string name, object value)
        {
            if (value != null)
            {
                return AddParameter(uri, name, value);
            }
            return uri;
        }

        public static string AddParameter(this string uri, string name, object value)
        {
            uri += (uri.Contains("?") ? "&" : "?") + $"{name}={value}";
            return uri;
        }
    }
}
