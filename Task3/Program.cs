using System;
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

    class DynamicArray : ConfigurationSection
    {
        public int[] MyArray;

        // Конструктор.
        public DynamicArray(int Capacity = DefaultCapacity)
        {
            this.MyArray = new int[Capacity];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DynamicArray Myo = new DynamicArray();
            Console.WriteLine(Myo.MyArray.Length);
            Console.ReadKey();
        }
    }
}
