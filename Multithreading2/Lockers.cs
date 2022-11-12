using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Multithreading2
{
    public class LockersExamples1
    {

        object locker1 = new object(); //Note: these can be accesed by anyone - should be private 
        object locker2 = new object(); //Note: these can be accesed by anyone - should be private 
        //Note: We should have just one lock instead of two

        //Note: We shouldn't access two lockers at the same time. We should access just one lock at the same time

        //Thread example = new Thread(() =>  
        //{
        //    lock (locker1)
        //    {
        //        Thread.Sleep(1000);
        //        lock (locker2) ;
        //    }
        //}).Start();

        // lock (locker2)
        //{
        //    Thread.Sleep(1000);
        //    lock (locker1); 

        //}

    }

    public class PhoneBook
    {
        private Dictionary<string, long> _phonebook;
        private object _lock = new object();

        public void AddNumber(string name, long number)
        {
            //Note: Problem: we should not use locks on strings ( in this case name)
            lock (_lock)
            {
                if (!_phonebook.ContainsKey(name))
                {
                    _phonebook.Add(name, number);
                }
                else
                {
                    _phonebook[name] = number;
                }
            }
        }

        public void Clear()
        {
            _phonebook.Clear();
        }

    }
}


