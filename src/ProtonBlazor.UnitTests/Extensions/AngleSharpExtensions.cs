using AngleSharp.Dom;

namespace ProtonBlazor.UnitTests
{
    public static class AngleSharpExtensions
    {
        public static string TrimmedText(this IElement self)
        {
            return self.TextContent?.Trim();
        }
    }
}
