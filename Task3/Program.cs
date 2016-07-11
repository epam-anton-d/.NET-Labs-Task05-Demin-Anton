using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Task3
{
    class ConfigurationSection
    {
        public const int DefaultCapacity = 8;
    }

    class PeopleContainer : IEnumerable
    {
        public string[] firstName;
        private int length;

        public int Length 
        {
            get 
            { 
                return length; 
            }
            set
            {
                length = value;
            }
        }

        public PeopleContainer(int Length = 8)
        {
            this.Length = 8;

            this.firstName = new string[Length];

            firstName[0] = "Эрос Ромазотти";
            firstName[1] = "Петр Чайковский";
            firstName[2] = "Иоган Бах";
            firstName[3] = "Сергей Прокофьев";
            firstName[4] = "Людвиг Бетховен";
            firstName[5] = "Рихард Вагнер";
            firstName[6] = "Карл Вебер";
            firstName[7] = "Джужеппе Верди";

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        private IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }




    }

    class DynamicArray : ConfigurationSection, IEnumerable
    {
        public int[] MyArray;
        public string[] peopleArray;

        // Конструктор (Задание 3.2. Создание массива заданной емкости, по-умолчанию 8).
        public DynamicArray(int Capacity = DefaultCapacity)
        {
            this.MyArray = new int[Capacity];
        }

        // Задание 3.3. Кипирование в массив из коллекции.
        public DynamicArray(PeopleContainer People)
        {
            this.peopleArray = new string [People.Length];

            for (int i = 0; i < People.Length; i++)
            {
                peopleArray[i] = People.firstName[i];
            }
        }

        // Задание 3.4. Добавить один элемент в конец массива.
        public void Add(string addElement)
        {
            for (int i = 0; i < peopleArray.Length; i++)
            {
                // Если места достаточно не расширяем массив.
                if (peopleArray[i] == null || peopleArray[i] == "")
                {
                    peopleArray[i] = addElement;
                    break;
                }
                // Если места не достаточно - расширяем;
                if (i == peopleArray.Length - 1)
                {
                    Array.Resize(ref peopleArray, peopleArray.Length * 2);
                    peopleArray[i + 1] = addElement;
                    break;
                }
            }
        }

        // Задание 3.5. Добавление в конец массива содержимое коллекции.
        public void AddRange(PeopleContainer People)
        {
            for (int i = 0; i < peopleArray.Length; i++)
            {
                if (peopleArray[i] == null || peopleArray[i] == "")
                {
                    // Если достаточно места вставляем коллекцию в массив.
                    if (People.firstName.Length > peopleArray.Length - i - 1)
	                {
                        Array.Resize(ref peopleArray, peopleArray.Length + People.firstName.Length);
	                }
                    for (int j = 0; j < People.firstName.Length; j++)
                    {
                        peopleArray[i + j] = People.firstName[j];
                    }
                    break;
                }
                if (i == peopleArray.Length - 1)
                {
                    // Если не достаточно - увеличиваем размер.
                    Array.Resize(ref peopleArray, peopleArray.Length + People.firstName.Length);
                    for (int j = 0; j < People.firstName.Length; j++)
                    {
                        peopleArray[i + 1 + j] = People.firstName[j];
                    }
                    break;
                }
            }
        }

        // Задание 3.6. Удаление из коллекции указанного элемента.
        public void Remove(int index)
        {
            for (int i = index; i < peopleArray.Length - 1; i++)
            {
                if (peopleArray[i + 1] != null || peopleArray[i + 1] != "")
                {
                    // Сдвигаем элементы, чтобыне было пустот.
                    peopleArray[i] = peopleArray[i + 1];
                }
            }
        }

        // Задание 3.7. Добавляем элемент в произвольную позицию.
        public bool Insert(string insElt, int index)
        {
            for (int i = peopleArray.Length - 1; i >= index; i--)
            {
                if (peopleArray[i] != null && peopleArray[i] != "")
                {
                    try
                    {
                        // Освобождаем место под элемент.
                        peopleArray[i + 1] = peopleArray[i];
                    }
                    catch (System.IndexOutOfRangeException e)  
                    {
                        System.Console.WriteLine(e.Message);
                        //Set IndexOutOfRangeException to the new exception's InnerException.
                        throw new System.ArgumentOutOfRangeException("index parameter is out of range.", e);
                    }
                }
            }
            peopleArray[index] = insElt;
            return true;
        }

        // Задание 3.8. Получение длинны массива.
        public int Length
        {
            get
            {
                int count = 0;
                for (int i = 0; i < peopleArray.Length; i++)
                {
                    if (peopleArray[i] != null && peopleArray[i] != "")
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        // Задание 3.9. Получение реальной длины массива.
        public int Capacity
        {
            get
            {
                return peopleArray.Length;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        private IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // Задание 3.10. Реализация методов интерфейсов.
        private int position = 0;

        // Перемещение "указателя" на следущий элемент
        public bool MoveNext()
        {
            if (position < peopleArray.Length)
            {
                position++;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Сброс указателя на нулевой элемент.
        public void Reset()
        {
            position = 0;
        }

        // Возврашение текущего элемента.
        public object Current
        {
            get
            {
                return peopleArray[position];
            }
        }

        // Задание 3.11. Индексатор.
        public string this[int index]
        {
            get
            {
                try
                {
                    // Обращение к объекту как к массиву.
                    return peopleArray[index];
                }
                catch (System.IndexOutOfRangeException e)
                {
                    System.Console.WriteLine(e.Message);
                    throw new System.ArgumentOutOfRangeException("Вышли за пределы допустимых значений.", e);
                }
            }

            set
            {
                try
                {
                    peopleArray[index] = value;
                }
                catch (System.IndexOutOfRangeException e)
                {
                    System.Console.WriteLine(e.Message);
                    throw new System.ArgumentOutOfRangeException("Вышли за пределы допустимых значений.", e);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Задание 3.1, 3.2.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.1, 3.2.");
            Console.ForegroundColor = ConsoleColor.Gray;
            DynamicArray Myo = new DynamicArray();
            Console.WriteLine("Длина массива, с заданной длинной по умолчанию = {0}",Myo.MyArray.Length);

            // Задание 3.3.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.3.");         
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Выводим передаваемую коллекцию:");
            PeopleContainer musicians = new PeopleContainer();
            for (int i = 0; i < 8; i ++)
            {
                Console.WriteLine(musicians.firstName[i]);
            }
            DynamicArray dynamicArray = new DynamicArray(musicians);
            Console.WriteLine("\nВыводим полученную коллекцию:");
            for (int i = 0; i < dynamicArray.peopleArray.Length; i++)
            {
                Console.WriteLine(dynamicArray.peopleArray[i]);
            }
            
            // Задание 3.4.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.4.");
            Console.ForegroundColor = ConsoleColor.Gray;
            // Добавляем эллемент в коллекцию.
            Console.WriteLine("Добавили эллемент:");
            dynamicArray.Add("Александр Васильев");
            Console.WriteLine(dynamicArray.peopleArray[8]);
            Console.WriteLine("Вместимость коллекции после добавления: {0}", dynamicArray.peopleArray.Length);

            // Задание 3.5.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.5.");
            Console.ForegroundColor = ConsoleColor.Gray;
            dynamicArray.AddRange(musicians);
            Console.WriteLine();
            Console.WriteLine("Полученная коллекция:");
            for (int i = 0; i < dynamicArray.peopleArray.Length; i++)
            {
                if (dynamicArray.peopleArray[i] != null && dynamicArray.peopleArray[i] != "")
                {
                    Console.WriteLine(dynamicArray.peopleArray[i]);
                }
            }

            // Задание 3.6.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.6.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Удаляем элемент синдексом 8.");
            dynamicArray.Remove(8);
            Console.WriteLine();
            Console.WriteLine("Результат:");
            for (int i = 0; i < dynamicArray.peopleArray.Length; i++)
            {
                if (dynamicArray.peopleArray[i] != null && dynamicArray.peopleArray[i] != "")
                {
                    Console.WriteLine(dynamicArray.peopleArray[i]);
                }
            }

            // Задание 3.7.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.7.");
            Console.ForegroundColor = ConsoleColor.Gray;
            dynamicArray.Insert("Виктор Цой", 4);
            Console.WriteLine("Вставили в массив элемент. Результат:");
            for (int i = 0; i < dynamicArray.peopleArray.Length; i++)
            {
                if (dynamicArray.peopleArray[i] != null && dynamicArray.peopleArray[i] != "")
                {
                    Console.WriteLine(dynamicArray.peopleArray[i]);
                }
            }

            // Задание 3.8.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.8.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Длинна массива равна = {0}", dynamicArray.Length);

            // Задание 3.9.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.9.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Реальная длинна массива равна = {0}", dynamicArray.Capacity);

            // Задание 3.10.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.10.");
            Console.ForegroundColor = ConsoleColor.Gray;
            // Получаем третий элемент колекции.
            dynamicArray.Reset();
            dynamicArray.MoveNext();
            dynamicArray.MoveNext();
            Console.WriteLine("Текущий элемент: {0}", dynamicArray.Current);
            dynamicArray.Reset();

            // Задание 3.11.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 3.11.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Пятый элемент коллекции (осуществляем достут с применением индексатора): {0}", dynamicArray[5]);

            Console.ReadKey();
        }
    }
}
