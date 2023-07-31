using System;

namespace ShrtLy.BLL.Validators
{
    public static class UrlValidator
    {
        public static bool IsValid(string url)
        {
            bool isValid = IsUrlValid(url);

            return isValid;
        }

        private static bool IsUrlValid(string url) =>
             Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
