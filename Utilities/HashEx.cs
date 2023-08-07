using System.Security.Cryptography;
namespace com.portfolio.website.Utilities
{
    public static class HashEx
    {
        public static string ToHashString(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Use SHA256 to create hash

            using var sha = new SHA256Managed();

            //Convet the string to byte array first to be processed
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = sha.ComputeHash(textBytes);

            //Convert back to string removing '-' that BitConverter adds
            String hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", String.Empty);
    
            return hash;
        }
    }
}
    