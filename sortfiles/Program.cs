using System.Text.RegularExpressions;

namespace sortfiles {
    class SortFiles
    {
        public Dictionary<string, string[]> Whitelists = new Dictionary<string, string[]>();

        public string Dir { get; set; }

        public string Pattern { get; set; }

        public SortFiles(string path, string patt = @"/.*/")
        {
            if (!Directory.Exists(path))
            {
                throw new ArgumentNullException("Directory not found in the specified path");
            }

            Dir = path[path.Length - 1] != '\\' ? path.Insert(path.Length, "\\") : path;

            Whitelists = GetWhitelist.reflection(new Config());

            Pattern = patt;
        }

        //public void SortbyCategory(Dictionary<string, string[]> dicinario, bool funnel) original
        public void SortbyCategory(List<Whitelist> Whitelists)
        {
            foreach (string Oldpath in Directory.GetFiles(Dir))
            {
                string extension = Path.GetExtension(Oldpath);
                string FileName = Path.GetFileName(Oldpath);
                long length = new FileInfo(Oldpath).Length;

                //foreach (var whitelist in dicinario.CustomWhitelist) original
                foreach (Whitelist whitelist in Whitelists)
                {
                    //string Newdir = Dir + whitelist.Key; original
                    string Newdir = Dir + whitelist.foldername;
                    string Newpath = Path.Combine(Newdir, FileName);
                    string Date = File.GetCreationTime(Oldpath).ToShortDateString();

                    var regex = RegexParser.getregex(whitelist.List1);

                    string[] datalist = DataParser.LocateDate(whitelist.List1);

                    //string[] locatesize = SizeParser.Getsize(whitelist.List1);

                    string[] sizes = SizeParser.Getsize(whitelist.List1);


                    bool Shouldmove = false;

                    if (!whitelist.Funnel)
                    {

                        if (SizeParser.match_size(sizes, length))
                        {
                            Shouldmove = true;
                        }

                        if (DataParser.GetDateMatch(datalist, Date))
                        {
                            Shouldmove = true;
                        }  //Move(Oldpath, Newpath, Newdir); }'

                        if (RegexParser.validate(regex, FileName)) { Shouldmove = true; }//Move(Oldpath, Newpath, Newdir); }

                        if (whitelist.List1.Contains(extension)) { Shouldmove = true; }//Move(Oldpath, Newpath, Newdir); }

                        if (Shouldmove)
                        {
                            Move(Oldpath, Newpath, Newdir); 
                            //debug(Oldpath, Newpath, Newdir);
                        }
                    }
                    else
                    {
                        if (!SizeParser.match_size(sizes, length, true) && sizes.Length > 0)
                        {
                            continue;
                        }

                        if (!DataParser.GetDateMatch(datalist, Date, true) && datalist.Length > 0)
                        {
                            continue;
                        }
                        if (!RegexParser.validate(regex, FileName) && regex.Length > 0)
                        {
                            continue;
                        }
                        if (validate_extension(whitelist.List1, extension) && !whitelist.List1.Contains(extension))
                        {
                            continue;
                        }

                        Move(Oldpath, Newpath, Newdir);
                        //debug(Oldpath, Newpath, Newdir);
                    }
                }

            }
        }

        private void debug(string oldpath, string newpath, string newdir)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------\n");
            Console.WriteLine($"oldpath -> {oldpath}\n");
            Console.WriteLine($"newpath -> {newpath}\n");
            Console.WriteLine($"newdir -> {newdir}\n");
            Console.WriteLine("-----------------------------------------------------------------------------------\n");
        }

        private bool validate_extension(string[] list, string extension)
        {
            return list.Any(x => Regex.Match(x, @"^(\.[a-zA-Z0-9]+)+$").Success);
        }

        private void Move(string oldpath, string newpath, string newdir)
        {
            if (!Directory.Exists(newdir))
                Directory.CreateDirectory(newdir);

            if (!File.Exists(newpath)) // evita exceção
                debug(oldpath, newpath, newdir);
                File.Move(oldpath, newpath);
        }

    }   
}