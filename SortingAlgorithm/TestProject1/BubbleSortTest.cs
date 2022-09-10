using NUnit.Framework;
using SortingAlgorithm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithmTests
{
    public class BubbleSortTest
    {
        private BubbleSort _sortService;


        [SetUp]
        public void Setup()
        {
            _sortService = new BubbleSort();
        }

        [Test]
        public void SortArrayTrue()
        {
            int[] array = { 34, 56, 2, 64, 35, 98, 4 }; // {2, 4, 34, 35, 56, 64, 98}

            var returnedArray = _sortService.BubbleSortMethod(array);

            int[] manuallySortArray = { 2, 4, 34, 35, 56, 64, 98 };

            Assert.AreEqual(returnedArray, manuallySortArray);
        }

        [Test]
        public void SortArrayFalse()
        {

            int[] array = { 34, 56, 2, 64, 35, 98, 4 }; // {2, 4, 34, 35, 56, 64, 98}

            var returnedArray = _sortService.BubbleSortMethod(array);

            int[] manuallySortArray = { 4, 2, 34, 35, 56, 64, 98 };

            Assert.AreNotEqual(returnedArray, manuallySortArray);
        }
    }
}
