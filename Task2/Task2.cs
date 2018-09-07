using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Step1
            Console.WriteLine("Входной файл:");
            String nameoffile1 = Console.ReadLine();
            FileStream file = new FileStream(nameoffile1, FileMode.Create, FileAccess.Write);
            StreamWriter WRITER1 = new StreamWriter(file);
            Console.WriteLine("Текст в файле:");
            String text = Console.ReadLine();
            WRITER1.Write(text);
            WRITER1.Close();
            file.Close();
            FileStream file2 = new FileStream(nameoffile1, FileMode.Open, FileAccess.Read);
            StreamReader READER = new StreamReader(file2);
            String text2 = READER.ReadToEnd();
            READER.Close();
            file2.Close();
            #endregion
            #region Step2

            String[] lines = text.Split('\n');
            String[][] words = new string[lines.Length][];

            char[] simbol = { ' ', ',', '.', '?', '!' };

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = lines[i].Split(simbol, StringSplitOptions.RemoveEmptyEntries);
            }

            //printMas(words);

            #endregion
            #region Step3

            Dictionary<string, List<int>> dic = new Dictionary<string, List<int>>();

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    String word = words[i][j].Trim().ToLower();

                    if (dic.ContainsKey(word))
                    {
                        int count = ++dic[word][0];
                        dic[word][0] = -1;
                        if (!dic[word].Contains(i + 1)) dic[word].Add(i + 1);
                        dic[word][0] = count;
                    }
                    else
                    {
                        dic[word] = new List<int>();
                        dic[word].Add(1);

                        dic[word].Add(i + 1);
                    }
                }
            }

            printTable(dic);

            #endregion
            Console.ReadKey();
        }

        static void printTable(Dictionary<string, List<int>> temp)
        {
            IEnumerable<KeyValuePair<string, List<int>>> pair = temp.OrderBy(key => key.Key);
            Dictionary<string, List<int>> dic = pair.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            Dictionary<char, bool> letter = new Dictionary<char, bool>();
            Console.WriteLine("Выходной файл:");
            String nameoffile2 = Console.ReadLine();
            FileStream FILE = new FileStream(nameoffile2, FileMode.Create, FileAccess.Write);
            StreamWriter WRITER = new StreamWriter(FILE);
            for (char i = 'A'; i <= 'Z'; i++)
            {
                letter[i] = true;
            }

            foreach (KeyValuePair<string, List<int>> data in dic)
            {
                for (char i = 'A'; i <= 'Z'; i++)
                {
                    if (letter[i] && data.Key.ToUpper().StartsWith(i.ToString()))
                    {
                        Console.WriteLine(i);
                        letter[i] = false;
                    }
                }
                Console.Write("\t{0} - {1} : ", data.Key.Trim(), data.Value[0]);
                WRITER.Write("\t{0} - {1} : ", data.Key.Trim(), data.Value[0]);
                for (int i = 1; i < data.Value.Count; i++)
                {
                    Console.Write("{0} ", data.Value[i]);
                    WRITER.Write("{0} ", data.Value[i]);
                }
                Console.WriteLine();
                WRITER.WriteLine();
            }
            WRITER.Close();
            FILE.Close();
          
        }

        static void printMas(String[][] mas)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = 0; j < mas[i].Length; j++)
                {
                    Console.Write("{0} ", mas[i][j].TrimEnd());
                }
                Console.WriteLine();
            }
        }
    }
}
