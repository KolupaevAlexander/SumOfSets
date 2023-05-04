using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Практика_18
{
    internal class Program
    {
        
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            short m;
            Console.Write("Введите число: ");
            while (!Int16.TryParse(Console.ReadLine(), out m))
            {
                Console.Write("Неверный формат ввода. Повторите ввод: ");
            }

            string path = "data.txt";

            short[] data = new short[33554432];
            if (File.Exists(path))
            {
                Fill();
                try
                {
                    Console.WriteLine("Наборы");
                    data = File.ReadAllText(path)
                               .Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(x => Int16.Parse(x))
                               .ToArray(); 
                    var list = Count(data);

                    foreach ( var x in list ) { Console.WriteLine($"{x}"); }
                    //Console.WriteLine($"{list.Capacity}");
                    if (list.Values.Contains(m))
                    {
                        foreach (var item in list)
                        {
                            if (item.Value == m)
                            {
                                foreach (var i in SetFromCode(data, item.Key))
                                    Console.Write($"{i} ");
                                Console.WriteLine();
                            }
                        }
                    }
                    else Console.WriteLine("Нет таких комбинаций"); 
                }
                catch
                {
                    Console.WriteLine("Информация записана в файле неверно");
                }                            
            }
            else 
            {
                Console.WriteLine("Файл не найден");
            }
            Console.ReadKey();
        }

        static SortedList<string, int> Count(short[] data)
        {
            var list = new SortedList<string, int>(33554432);

            for (int i = 0; i < Math.Pow(2, data.Length); i++) 
            {
                list.Add(GenerateCode(i, data.Length), SetSum(data, GenerateCode(i, data.Length)));
            }
            return list;
        }
        static int SetSum(short[] set, string code)
        {
            int sum = 0;
            for (int i = 0; i < set.Length; i++)
            {
                if (code[i] == '1')
                    sum += set[i];
            }
            return sum;
        }
        static string GenerateCode(int number, int length)
        {
            string code = Convert.ToString(number, 2);
                while (code.Length != length)
                {
                    code = '0' + code;
                }
            return code;
        }
        static List<int> SetFromCode(short[] set, string code)
        {
            List<int> list = new List<int>(33554432);
            for (int i = 0; i < code.Length; i++)
            { 
                if (code[i] == '1')
                        list.Add(set[i]);
            }
            return list;
        }

        //Генерация данных
        static void Fill()
        {
            int[] set = new int[25];
            Random random = new Random();
            for (int i=0;i<25;i++)
                set[i] = random.Next(-100,100);

            File.WriteAllText("data.txt", String.Join(" ", set));
        }

    }
}
