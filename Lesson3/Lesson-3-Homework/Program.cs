using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson_3_Homework
{
    class Program
    {
        public static void Main(string[] args)
        {
            var people = new List<Person>()
            {
                new Person("Bill", "Smith", 41),
                new Person("Sarah", "Jones", 22),
                new Person("Stacy","Baker", 21),
                new Person("Vivianne","Dexter", 19 ),
                new Person("Bob","Smith", 49 ),
                new Person("Brett","Baker", 51 ),
                new Person("Mark","Parker", 19),
                new Person("Alice","Thompson", 18),
                new Person("Evelyn","Thompson", 58 ),
                new Person("Mort","Martin", 58),
                new Person("Eugene","deLauter", 84 ),
                new Person("Gail","Dawson", 19 ),
            };

            Console.WriteLine("Number 0\n");
            //0. write linq display every name ordered alphabetically
            var alphabeticalOrder = people.OrderBy(s=> s.LastName).ThenBy(s => s.FirstName).ToList();
            foreach (var person in alphabeticalOrder)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}, {person.Age}");
            }

            Console.WriteLine("\nNumber 1\n");
            //1. write linq statement for the people with last name that starts with the letter D
            //Console.WriteLine("Number of people who's last name starts with the letter D " + people1.Count());
            var startWithD = people.Where(s => s.LastName.StartsWith('d')).ToList();
            foreach (var person in startWithD)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}, {person.Age}");
            }

            Console.WriteLine("\nNumber 2\n");
            //2. write linq statement for all the people who are have the surname Thompson and Baker. Write all the first names to the console
            var lastNameEquals = people.Where(p => p.LastName == "Thompson" || p.LastName == "Baker").ToList();
            foreach (var person in lastNameEquals)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}, {person.Age}");
            }

            Console.WriteLine("\nNumber 3\n");
            //3. write linq to convert the list of people to a dictionary keyed by first name
            var dictionary = people.ToDictionary(p => p.FirstName);
            foreach (var person in dictionary)
            {
                Console.WriteLine($"{person.Key} {person.Value.LastName}, {person.Value.Age}");
            }

            Console.WriteLine("\nNumber 4\n");
            // 4. Write linq statement for first Person Older Than 40 In Descending Alphabetical Order By First Name
            //Console.WriteLine("First Person Older Than 40 in Descending Order by First Name " + person2.ToString());
            var olderThan40 = people.Where(a => a.Age > 40).ToList().OrderByDescending(a => a.FirstName).ToList();
            Console.WriteLine($"People Older than 40 in Descending Order By First Name");
            foreach (var person in olderThan40)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}, {person.Age}");
            }
            Console.WriteLine("\nNumber 5\n");
            //5. write a linq statement that finds all the people who are part of a family. (aka there is at least one other person with the same surname.
            var familyMembers = people.GroupBy(f => f.LastName).Where(g => g.Count() > 1).ToList();
            foreach (var group in familyMembers)
            {
                foreach (var person in group)
                {
                    Console.WriteLine($"{person.LastName} {person.FirstName}, {person.Age}");
                }
            }


            Console.WriteLine("\nNumber 6\n");
            //6. Write a linq statement that finds which of the following numbers are multiples of 4 or 6
            List<int> mixedNumbers = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };

            var multiples = mixedNumbers.Where(n => n % 4 == 0 || n % 6 == 0).ToList();
            foreach (var number in multiples)
            {
                Console.WriteLine($"{number}");
            }


            Console.WriteLine("\nNumber 7\n");
            // 7. How much money have we made?
            List<double> purchases = new List<double>()
            {
                2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
            };

            var sum = purchases.Sum();
            Console.WriteLine($"{sum}");
        }


        public class Person
        {
            public Person(string firstName, string lastName, int age)
            {
                FirstName = firstName;
                LastName = lastName;
                Age = age;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }

            //override ToString to return the person's FirstName LastName Age

        }
    }
}
