using System.CommandLine;

namespace sortfiles
{
    public class Whitelist
    {
        public string[] List1 { get; set; }
        public bool Funnel { get; set; }

        public string foldername { get; set; }

        public Whitelist(string folder, string[] valores, bool ativo) 
        {
            foldername = folder;
            List1 = valores;
            Funnel = ativo;
        }
    }

    class CustomSort
    {
        public string path { get; set; }
        public List<Whitelist> Wlinstances = new List<Whitelist>();
        public bool funnel = true;

        public CustomSort(string path) //base(path) { }
        {
            this.path = path;
        }

        public CustomSort add_whitelist(string foldername, string[] whitelist, bool fun=false)
        {
            List<string> list = new List<string>();
            foreach (var item in whitelist) 
            {
                var value = GetWhitelist.GetPropByName(new Config(), item);
                if (value != null)
                {
                    //list = list.Concat(value).ToArray();
                    list.AddRange(value);
                    continue;
                }
                list.Add(item);

            }
            //CustomWhitelist.Add(foldername, list.ToArray());
            Whitelist instance = new Whitelist(foldername, list.ToArray(), fun);
            Wlinstances.Add(instance);
            //customwhitelist[foldername] = instance;
            return this;

        }

        public void launch()
        {
            new SortFiles(path).SortbyCategory(Wlinstances);
        }

        public void parser(string express)
        {
            if (string.IsNullOrWhiteSpace(express))
            {
                throw new Exception("expressão vazia ou nula");
            }

            string[] subexpress = express.Split(';');

            for (int i = 0; i < subexpress.Length-1; i++)
            {
                string item = subexpress[i].Trim();

                string[] key_value = item.Split('=');

                string key = key_value[0];

                string value = key_value[1];


                if (key_value.Length != 2) 
                {
                    throw new Exception($"Expressão malformada: '{item}'");
                }
                

                if (!value.StartsWith('[') || !value.EndsWith(']'))
                {
                    throw new Exception($"Expressão malformada: '{item}'");
                }


                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                {
                    throw new Exception($"Chave ou valor vazio: '{item}'");
                }

                bool funnel = key[0] == '-' ? true : false;

                string clean_key = funnel ? key.Substring(1) : key;

                string[] parsed_value = value.Trim('[', ']').Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(v => v.Trim()).ToArray();

                add_whitelist(clean_key, parsed_value, funnel);
            }

            launch();
        }
        
    }

    class Org
    {
        static int Main(string[] args) {

            string custom_path = System.IO.Directory.GetCurrentDirectory(); ;
            Option<FileInfo> pathOption = new("-p")
            {
                Description = "caminho da pasta a ser organizada.",
            };

            Option<string> stringOption = new("-s")
            {
                Description = "expressão usada como receita para organizar o diretório, segue a seguinte estrutura",
                Required = true
            }; 

            var rootCommand = new RootCommand("Exemplo com -s");
            rootCommand.Options.Add(pathOption);
            rootCommand.Options.Add(stringOption);

            ParseResult parseResult = rootCommand.Parse(args);

            if (parseResult.GetValue(pathOption) is FileInfo path)
            {
                custom_path = path.FullName;
            }
            

            if (parseResult.GetValue(stringOption) is string parsedstr) 
            { 
                new CustomSort(custom_path).parser(parsedstr);
            }

            return 1;
        }
    }
}
