using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Task2
{
    class Program
    {
        // Регулярное выражение разделитель.
        const string separator = @"\.+\s+|\s+\.+|[\.+\s+]";

        static void Main(string[] args)
        {
            string sentence="",
                    answer;

            // Пользовательский ввод предложения или использование примера.
            Console.WriteLine("Хотите ввести свой текст (наберите [y/Y]) или использовать пример?(наберите [n/N])");
            answer = Console.ReadLine().ToLower();
            if (answer == "y")
            {
                Console.WriteLine("Введите текст: \n");
                sentence = Console.ReadLine();
            }
            else if (answer == "n")
            {
                Console.WriteLine("Пример: \n");
                sentence = "Hello hi hello buenos dias Hi Bonjorno Helllo.bye-bye HI ok. Lo siente. privet.maniama .priVet SiEnTe. bonjorno privet ok";
                Console.WriteLine(sentence);
            }
            else
            {
                Console.WriteLine("Не правильный ввод, будет использован пример");
                sentence = "Hello hi hello buenos dias Hi Bonjorno Helllo.bye-bye HI ok. Lo siente. privet.maniama .priVet SiEnTe. bonjorno privet ok";
            }
            

            // Резбиение предложения на слова.
            String[] elements = Regex.Split(sentence, separator);
            
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i].ToLower();
            }

            Dictionary<string, int> resultDict = new Dictionary<string, int>();

            // Запись в словарь колличества совпадений слов.
            foreach (string item in elements)
            {
                if (resultDict.ContainsKey(item))
                {
                    resultDict[item]++;
                }
                else
                {
                    resultDict.Add(item, 1);
                }
            }

            // Вывод результата.
            Console.WriteLine("\nРезультат: ");
            foreach (KeyValuePair<string, int> item in resultDict)
            {
                Console.WriteLine("слово {0} встречается {1} раз", item.Key, item.Value);
            }

            Console.ReadKey();
        }
    }
}
