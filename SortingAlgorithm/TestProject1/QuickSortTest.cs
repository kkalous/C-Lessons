using NUnit.Framework;
using SortingAlgorithm;

namespace TestProject1
{
    public class QuickSortTest
    {
        private QuickSort _sortService;


        [SetUp]
        public void Setup()
        {             
            _sortService = new QuickSort();
        }

        [Test]
        public void SortArrayTrue()
        {
            int[] array = { 34, 56, 2, 64, 35, 98, 4 }; // {2, 4, 34, 35, 56, 64, 98}

            var returnedArray = _sortService.QuickSortMethod(array, 0, array.Length -  1);

            int[] manuallySortArray = { 2, 4, 34, 35, 56, 64, 98 };

            Assert.AreEqual(returnedArray, manuallySortArray);
        }

        [Test]
        public void SortArrayFalse()
        {
            int[] array = { 34, 56, 2, 64, 35, 98, 4 }; // {2, 4, 34, 35, 56, 64, 98}

            var returnedArray = _sortService.QuickSortMethod(array, 0, array.Length - 1);

            int[] manuallyNotSortArray = { 4, 2, 34, 35, 56, 64, 98 };

            Assert.AreNotEqual(returnedArray, manuallyNotSortArray);
        }
    }
}