using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Donkey
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var argLookup = args.ToLookup(x => x[0] == '-');
                var normalArgs = argLookup[false].ToList();
                var options = argLookup[true].SelectMany(x=>x.Skip(1)).ToHashSet();

                foreach (var filename in normalArgs)
                {
                    var newFileName = filename.EndsWith(".uu") ? filename.Substring(0, filename.Length - 3) : $"{filename}.uu";

                    var ba = File.ReadAllBytes(filename);
                    for (int i = 0; i < ba.Length; i++)
                    {
                        ba[i] ^= 0xAD;
                    }

                    File.WriteAllBytes(newFileName, ba);
                }
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine($"{progname} Error: {ex.Message}");
            }

        }
    }
}
