using System;
using System.Collections.Generic;
using System.Text;

namespace Multithreading2
{
    public class Randomness
    {
        /// <summary>
        /// Moves characters to a new random location on the console.
        /// This gets called by a multithreaded method that randomly picks a Loc from a list and moves it. But the characters keep get duplicated. But I'm locking on the Loc to make sure that while its moving another thread can't move it!
        /// </summary>
        /// <param name="state"></param>
        /// 

        ///Note: Should we have lock in Write at method? As I can pass the same Loc state to WriteMethod at exactly same time???
        
        public Loc MoveCharacter(Loc state)
        {
            // We make a copy as we're going to change X and Y in a sec and don't want to have side effects on original
            var copy = new Loc(state.X, state.Y, state.Value);

            //Note: - I would get rid off this lock as we would have it in the WriteAt method
           // lock (copy)
            //{
                // erase the previous position
                WriteAt(" ", copy.X, copy.Y);
           // }

            // pick a new random position
            copy.X = new Random().Next(1000);
            copy.Y = new Random().Next(1000);

            //Note: - I would get rid off this lock as we would have it in the WriteAt method
           // lock (copy)
           // {
                WriteAt(copy.Value, copy.X, copy.Y);
            //}

            return copy;
        }

        public void WriteAt(string s, int x, int y)
        {
            try
            {
                //Note: idea - KK
                var locker = new Loc(x, y, s);

                lock (locker)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(s);
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        // This is the definition for the Loc class. Don't make changes to this class
        public class Loc
        {
            public Loc(int x, int y, string value)
            {
                X = x;
                Y = y;
                Value = value;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public string Value { get; set; }  // Will just be one character
        }
    }
}
