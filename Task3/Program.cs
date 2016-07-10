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
            //this.secondName = new string[Length];

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

        //private int length;

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

        public void Add(string addElement)
        {
            for (int i = 0; i < peopleArray.Length; i++)
            {
                if (peopleArray[i] == null || peopleArray[i] == "")
                {
                    peopleArray[i] = addElement;
                    break;
                } 
                if (i == peopleArray.Length - 1)
                {
                    Array.Resize(ref peopleArray, peopleArray.Length * 2);
                    peopleArray[i + 1] = addElement;
                    break;
                }
            }
        }

        // 5.
        public void AddRange(PeopleContainer People)
        {
            for (int i = 0; i < peopleArray.Length; i++)
            {
                if (peopleArray[i] == null || peopleArray[i] == "")
                {
                    if (People.firstName.Length > peopleArray.Length - i - 1)
	                {
                        Array.Resize(ref peopleArray, peopleArray.Length + People.firstName.Length);
	                }
                    for (int j = 0; j < People.firstName.Length; j++)
                    {
                        peopleArray[i + j] = People.firstName[j];
                    }
                    break;
                    //peopleArray[i] = addElement;
                    //break;
                }
                if (i == peopleArray.Length - 1)
                {
                    Array.Resize(ref peopleArray, peopleArray.Length + People.firstName.Length);
                    for (int j = 0; j < People.firstName.Length; j++)
                    {
                        peopleArray[i + 1 + j] = People.firstName[j];
                    }
                    break;
                    //peopleArray[i + 1] = addElement;
                    //break;
                }
            }
        }

        // 6.
        public void Remove(int index)
        {
            for (int i = index; i < peopleArray.Length - 1; i++)
            {
                if (peopleArray[i + 1] != null || peopleArray[i + 1] != "")
                {
                    peopleArray[i] = peopleArray[i + 1];
                }
            }
        }

        // 7.
        public bool Insert(string insElt, int index)
        {
            for (int i = peopleArray.Length - 1; i >= index; i--)
            {
                if (peopleArray[i] != null && peopleArray[i] != "")
                {
                    try
                    {
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

        // 8.
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

        // 9.
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1, 2.
            DynamicArray Myo = new DynamicArray();
            Console.WriteLine(Myo.MyArray.Length);

            // 3.
            PeopleContainer musicians = new PeopleContainer();
            for (int i = 0; i < 8; i ++)
            {
                Console.WriteLine(musicians.firstName[i]);
            }
            DynamicArray dynamicArray = new DynamicArray(musicians);

            for (int i = 0; i < dynamicArray.peopleArray.Length; i++)
            {
                Console.WriteLine(dynamicArray.peopleArray[i]);
            }
            
            // 4.
            dynamicArray.Add("Александр Васильев");
            Console.WriteLine(dynamicArray.peopleArray[8]);
            Console.WriteLine(dynamicArray.peopleArray.Length);

            // 5.
            dynamicArray.AddRange(musicians);
            Console.WriteLine();
            Console.WriteLine("5:");
            for (int i = 0; i < dynamicArray.peopleArray.Length; i++)
            {
                if (dynamicArray.peopleArray[i] != null && dynamicArray.peopleArray[i] != "")
                {
                    Console.WriteLine(dynamicArray.peopleArray[i]);
                }
            }

            // 6.
            dynamicArray.Remove(8);
            Console.WriteLine();
            Console.WriteLine("6:");
            for (int i = 0; i < dynamicArray.peopleArray.Length; i++)
            {
                if (dynamicArray.peopleArray[i] != null && dynamicArray.peopleArray[i] != "")
                {
                    Console.WriteLine(dynamicArray.peopleArray[i]);
                }
            }

            // 7. 
            dynamicArray.Insert("Виктор Цой", 4);
            Console.WriteLine();
            Console.WriteLine("7:");
            for (int i = 0; i < dynamicArray.peopleArray.Length; i++)
            {
                if (dynamicArray.peopleArray[i] != null && dynamicArray.peopleArray[i] != "")
                {
                    Console.WriteLine(dynamicArray.peopleArray[i]);
                }
            }

            // 8. 
            Console.WriteLine("Длинна массива равна = {0}", dynamicArray.Length);

            // 9.
            Console.WriteLine("Реальная длинна массива равна = {0}", dynamicArray.Capacity);

            Console.ReadKey();
        }
    }
}
