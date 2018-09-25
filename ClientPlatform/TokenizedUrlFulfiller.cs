using System.Text.RegularExpressions;

namespace ClientPlatform
{
    public sealed class TokenizedUrlFulfiller : ITokenizedUrlFulfiller
    {
        public string Fill(TokenizedUrl tokenizedUrl)
        {
            var editableUrl = tokenizedUrl.FullUrl;

            foreach(var property in tokenizedUrl.ResourceNames)
            {
                var value = tokenizedUrl.GetResourceValue(property);

                editableUrl = Regex.Replace(editableUrl, $@"(\{{){property}(}})", value);
            }

            return editableUrl;
        }
    }
}
