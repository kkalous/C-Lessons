using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithm
{
    public class QuickSort
    {
        public int[] QuickSortMethod(int[] array, int leftIndex, int rightIndex)
        {          
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }

                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                QuickSortMethod(array, leftIndex, j);
            if (i < rightIndex)
                QuickSortMethod(array, i, rightIndex);
            return array;
        }
    }
}
