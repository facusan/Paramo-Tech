using System;

namespace Sat.Recruitment.Api
{
    public class EmailNormalizer
    {
        public static string Normalize(string email)
        {
            var emailSplittedByAt = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var firstPartOfEmailBeforeAt = emailSplittedByAt[0];
            var plusCharIndex = firstPartOfEmailBeforeAt.IndexOf("+", StringComparison.Ordinal);

            firstPartOfEmailBeforeAt = plusCharIndex < 0 ? firstPartOfEmailBeforeAt.Replace(".", "") : firstPartOfEmailBeforeAt.Replace(".", "").Remove(plusCharIndex);

            return string.Join("@", new string[] { firstPartOfEmailBeforeAt, emailSplittedByAt[1] });
        }
    }
}
