using System.Reflection;

namespace sortfiles
{
    public class Config
    {

        public string[] pdf { get; } = { ".pdf", ".pdft", ".pdfa", ".pdfx", "pdf" };
        public string[] img { get; } = { ".png", ".jpeg", ".webp", ".jpg", "img" };
        public string[] videos { get; } = { ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".flv", ".webm",
                                            ".mpeg", ".mpg", ".3gp", ".ts", ".m4v", ".ogv",
                                            ".mts", ".m2ts", ".f4v", ".divx", ".vob", "videos" };
        public string[] sound { get; } = { ".mp3", ".wav", "sound" };
        public string[] excel { get; } = { ".csv", ".xlsx", "excel" };
        public string[] word { get; } = { ".doc", ".docx", "word" };
        public string[] jar { get; } = { ".jar", "jar" };
        public string[] exe { get; } = { ".exe", "exe" };
        public string[] txt { get; } = { ".txt", "txt" };
        public string[] powerpoint { get; } = { ".pptx", ".ppt", ".ppsx", "pp" };
        public string[] zipeds { get; } = {".zip", ".rar", "zip"};
        
    }

    public class GetWhitelist { 
        public static Dictionary<string, string[]> reflection(Config config)
        {
            Type configType = config.GetType();

            //List<string[]> list = new List<string[]>();
            Dictionary<string, string[]> list = new Dictionary<string, string[]>();

            foreach (PropertyInfo property in configType.GetProperties())
            {
                var prop = property.GetValue(config, null);

                if (prop != null) { list.Add(property.Name, (string[])prop); }
                
            }
            return list;
        }  
        
        public static string[]? GetPropByName(Config config, string name)
        {
                Type configType = config.GetType();
            
                var prop = configType.GetProperty(name);
                var v = prop?.GetValue(config);                                            // call GetValue() only if GetProperty() return is not null;

                if (v != null)
                {
                    return (string[])v;
                }
            
                return null;

        }
    }

}
