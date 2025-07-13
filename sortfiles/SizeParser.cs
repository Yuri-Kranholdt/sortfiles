using System.Text.RegularExpressions;


namespace sortfiles
{
    static class SizeParser
    {

        public static string[] Getsize(string[] list)
        {
            return list.Select(x => Regex.Match(x, @"^[\-\+]?\d+\w{1}$").ToString()).Where(x => x.Length > 0).ToArray();

        }

        private static bool validate_size(string strsize, long size)
        {

            Match size_match = Regex.Match(strsize, @"(\d+)");

            char unit = strsize[strsize.Length - 1];

            long size_int = long.Parse(size_match.Groups[1].Value);

            //long size_int = long.Parse(size_match.Groups[1].Value);

            size = convert_to_bytes(size, unit);

            //size = convert_to_bytes(size, unit);

            char signal = strsize[0];

            bool result;

            if (signal == '+')
            {
                result = size >= size_int ? true : false;
            }
            else if (signal == '-')
            {
                result = size <= size_int ? true : false;
            }
            else
            {
                result = size_int == size ? true : false;
            }

            return result;

        }

        private static long convert_to_bytes(long size_int, char unit)
        {

            switch (char.ToUpper(unit))
            {
                case 'M':
                    //size_int = size_int * 1024 * 1024; break;
                    size_int = (size_int / 1024) / 1024; break;

                case 'K':
                    //size_int = size_int * 1024; break;
                    size_int = size_int / 1024; break;

                case 'G':
                    //size_int = size_int * 1024 * 1024 * 1024; break;
                    size_int = ((size_int / 1024) / 1024) / 1024; break;

                case 'B':
                    break;

                default:
                    break;
            }

            return size_int;

        }

        public static bool match_size(string[] size, long file_size, bool funn = false)
        {
            if (!funn) { return size.Any(termo => validate_size(termo, file_size)); }
            return size.All(termo => validate_size(termo, file_size));
        }
    }
}
