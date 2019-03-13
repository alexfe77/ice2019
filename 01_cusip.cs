using System;
using System.IO;

namespace IceTest01
{
    class Program
    {
        public static bool IsCUSIP(string line)
        {
            /*
            if (!string.IsNullOrEmpty(line))
            {
                if ((line.Length == 8) && (!line.Contains(".")))
                {
                    return true;
                }
            }
            */
            decimal result;
            return !Decimal.TryParse(line, out result);            
        }


        public static void Main(string[] args)
        {
            var fs = new FileStream(@".\\icesample01.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                var reader = new StreamReader(fs);
                try
                {
                    var curBond = string.Empty;
                    decimal curPrice = (decimal)-1;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (IsCUSIP(line))
                        {
                            if (!string.IsNullOrEmpty(curBond) && (curPrice >= 0))
                            {
                                Console.WriteLine($"{curBond} {curPrice}");
                                curBond = line;
                                curPrice = -1;
                            }
                            else
                            {
                                curBond = line;
                            }
                        }
                        else
                        {
                            decimal.TryParse(line, out curPrice);
                        }
                    }
                    if (!string.IsNullOrEmpty(curBond) && (curPrice >= 0))
                    {
                        Console.WriteLine($"{curBond} {curPrice}");
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                fs.Close();
            }

            Console.WriteLine("Done. Press <enter> to stop...");
            Console.ReadLine();

        }
    }
}
