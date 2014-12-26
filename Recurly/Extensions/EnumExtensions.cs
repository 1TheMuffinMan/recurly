using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Extensions;

namespace Recurly.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts the given <see cref="T:System.String"/> as if it were the name of an enumeration member to the syntax Recurly's API expects (lowercase, words separated by underscores).
        /// </summary>
        /// <param name="enumName">The <see cref="T:System.String"/> that is to be converted.</param>
        /// <returns>The result of the attempted conversion.</returns>
        public static string ToTransportCase(this Enum theEnum)
        {
            var enumName = theEnum.ToString();
            if (string.IsNullOrWhiteSpace(enumName))
                throw new ArgumentException("enumName cannot be null or whitespace!", "enumName");

            var words = enumName.Split(' ');
            var result = new StringBuilder();

            foreach (var name in words)
            {
                var word = new StringBuilder();
                if (name.IsUpperCase())
                {
                    word.Append(name.ToLowerInvariant());
                }
                else
                {
                    foreach (var c in name)
                    {
                        if (char.IsUpper(c))
                            word.Append('_').Append(char.ToLowerInvariant(c));
                        else
                            word.Append(c);
                    }
                }
                result.Append(word.ToString().Trim('_'));
            }

            return result.ToString();
        }
    }
}
