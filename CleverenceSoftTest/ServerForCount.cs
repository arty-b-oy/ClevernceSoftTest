using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleverenceSoftTest
{
    public static class ServerForCount
    {
        private static int count;
        private static readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        public static int GetCount() 
        {
            locker.EnterReadLock();
            try
            {

                return count;
            }
            finally
            {
                locker.ExitReadLock();
            }
        }
        public static void AddToCount(int value)
        {
            locker.EnterWriteLock();
            try
            {
                count += value;
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }
    }
}
