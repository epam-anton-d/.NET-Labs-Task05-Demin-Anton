using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация коллекции человек.
            ArrayList HumanList = new ArrayList();
            // Инициализация временной коллекции.
            ArrayList Temp = new ArrayList();
            int count = 2,
                roundNum = 0,
                n;
            bool flag = false;
            
            // ВВод пользователем колличества человек в круге.
            Console.WriteLine("Введите колличество человек в круге: ");
            while (!Int32.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Неверный ввод, попробуйте еще раз: ");
            }

            // Заполнение коллекции.
            for (int i = 0; i < n; i++)
            {
                HumanList.Add("Human " + (i + 1));
            }

            // Уменьшаем людей пока не останется один.
            while (count > 1)
            {
                // Убираем каждого второго, "выживших" отправляем в коллекцию temp.
                foreach (string item in HumanList)
                {
                    flag = !flag;
                    if (flag && item != null)
                    {
                        Temp.Add(item);
                    }
                }

                HumanList.Clear();
                HumanList.Capacity = 1;

                // Обновляем коллекцию человек из temp с учетом "вычеркнутых".
                count = 0;
                foreach (string item in Temp)
                {
                    count++;
                    HumanList.Add(item);
                }

                Temp.Clear();
                Temp.Capacity = 1;
                roundNum++;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Круг {0}", roundNum);
                Console.ForegroundColor = ConsoleColor.Gray;
                // Вывод результата каждый круг.
                foreach (string item in HumanList)
                {
                    Console.WriteLine(item);
                }

            }
                        
            Console.ReadKey();
        }
    }
}
