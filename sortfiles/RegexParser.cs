using System.Text.RegularExpressions;

namespace sortfiles
{
    static class RegexParser
    {
        public static string[] getregex(string[] regex)
        {
            return regex.Select(x => Regex.Match(x, @"(?<=\\).*(?=\\)").ToString()).Where(x => x.Length > 0).ToArray();
            // .Where filtrou os valores nulls na busca pelo elemento q desse match com a regex
        }

        public static bool validate(string[] regex, string filename)
        {
            return regex.Any(termo => Regex.Match(filename, termo).Success);
            // Any: retorna true se pelo menos um dos elementos satisfazer uma dada condição
        }

    }
}
