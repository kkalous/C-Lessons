using System.Threading.Tasks;

namespace Multithreading3
{
    public class TrackerEx
    {
        public static TrackerResult Execute(int noToCreate, int noOfClicksForEach)
        {
            Tracker aTrackerInstance = null;

            //If I will want to run all tests at the same time I would need to reset the Count, so it will not count previous test call
            //Tracker.ResetCount();
            //

            Parallel.For(0, noToCreate, (indx) =>
            {
                var ourTracker = new Tracker();
                Parallel.For(0, noOfClicksForEach, (clickIndx) =>
                {
                    ourTracker.Click();
                });
                aTrackerInstance = ourTracker;
            });
            return new TrackerResult(aTrackerInstance.CreateCount, aTrackerInstance.TotalClicksAcrossAllTrackers, aTrackerInstance.TotalClicksForThisTracker);
        }
    }

    public class Tracker
    {
        private static int _createCount = 0;
        private int _totalClicksForThisTracker = 0;
        private static readonly object _lockingObj = new object();
        private static int _totalClicksAcrossAllTrackers;

        public Tracker()
        {
            lock (_lockingObj)
            {
                _createCount++;
            }
        }        

        public void Click()
        {
            lock (_lockingObj)
            {
                _totalClicksForThisTracker++;
                _totalClicksAcrossAllTrackers++;
            }

        }

        //If I will want to run all tests at the same time I would need to reset the Count, so it will not count previous test call
        //Or change the test to create new TrackerEx every single time
        //public static void ResetCount()
        //{
        //    lock(_lockingObj)
        //    {
        //        _totalClicksAcrossAllTrackers = 0;
        //    }
        //}

        public int CreateCount => _createCount;
        public int TotalClicksAcrossAllTrackers => _totalClicksAcrossAllTrackers;
        public int TotalClicksForThisTracker => _totalClicksForThisTracker;
    }

    public class TrackerResult
    {
        public TrackerResult(int createCount, int totalClicksAcrossAllTrackers, int totalClicksForThisTracker)
        {
            TotalClicksForThisTracker = totalClicksForThisTracker;
            TotalClicksAcrossAllTrackers = totalClicksAcrossAllTrackers;
            CreateCount = createCount;
        }

        public int CreateCount { get; }
        public int TotalClicksAcrossAllTrackers { get; }
        public int TotalClicksForThisTracker { get; }
    }
}

