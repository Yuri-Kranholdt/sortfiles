namespace sortfiles
{
    static class DataParser
    {
        private static string Datevalidate(string date)
        {

            char signal = date[0];
            bool hassignal = false;

            if (signal == '-' || signal == '+')
            {
                date = date.Substring(1);
                hassignal = true;
            }
            if (string.IsNullOrWhiteSpace(date))
                return string.Empty;

            // Se for um ano com 4 dígitos (ex: "2025"), aceita como está
            if (date.Length == 4 && date.All(char.IsDigit))
                return hassignal ? signal + date : date;

            // Tenta converter para uma data válida
            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                var parsed = parsedDate.ToShortDateString();
                return hassignal ? signal + parsed : parsed;
            }
            // Caso nenhuma condição seja atendida, retorna vazio
            return string.Empty;
        }

        private static DateTime CorrectDate(string date)
        {
            var dmy = date.Split("/").ToList();

            while (dmy.Count < 3) { dmy.Insert(0, "01"); }

            DateTime.TryParse(string.Join("/", dmy), out DateTime dt);

            return dt;
        }

        private static bool Datenormalizer(string str_date, string filedate)
        {
            bool result;

            if (str_date[0] == '+')
            {

                result = DateTime.Parse(filedate) >= CorrectDate(str_date.Substring(1)) ? true : false;
                //Console.WriteLine(filedate);
                //Console.WriteLine(result);
            }
            else if (str_date[0] == '-')
            {
                result = DateTime.Parse(filedate) <= CorrectDate(str_date.Substring(1)) ? true : false;
            }
            else
            {
                result = filedate.Contains(str_date) ? true : false;
            }
            return result;
        }

        public static string[] LocateDate(string[] datelist)
        {
            return datelist.Select(x => Datevalidate(x)).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        public static bool GetDateMatch(string[] date, string filedate, bool funn = false)
        {
            if (!funn)
            {
                return date.Any(termo => Datenormalizer(termo, filedate));
            }
            return date.All(termo => Datenormalizer(termo, filedate));
        }

    }

}
