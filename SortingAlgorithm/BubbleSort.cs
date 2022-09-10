using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithm
{
    public class BubbleSort
    {
        public int[] BubbleSortMethod(int[] arr)
        {           
            int temp;
            for (int j = 0; j <= arr.Length - 2; j++)
            {
                for (int i = 0; i <= arr.Length - 2; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        temp = arr[i + 1];
                        arr[i + 1] = arr[i];
                        arr[i] = temp;
                    }
                }
            }

            return arr;
            
        }
    }
}
