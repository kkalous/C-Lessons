using System;
using System.Collections.Generic;

namespace PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var myGeneric = new MyGenericClass<string, int>("tttta");
        }
    }

    class MyGenericClass<T1, TField>
    {
        private readonly T1 _variable;
        public MyGenericClass(T1 variable)
        {
            _variable = variable;
        }

        public TField Field { get; set; }
    }

    class MyGenericMethods
    {
        private string ToString<T>(T it)
        {
            return it.ToString();
        }

        public static T SafeGetAt<T>(List<T> list, int index)
        {
            return index < list.Count ? list[index] : default(T);
        }
    }
}
