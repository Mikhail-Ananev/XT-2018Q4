using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task04.NumberArraySum
{
    public static class MyArrayExtension
    {
        public static sbyte ArrCount(this sbyte[] arr)
        {
            sbyte sum = 0;
            foreach (sbyte a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static byte ArrCount(this byte[] arr)
        {
            byte sum = 0;
            foreach (byte a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static short ArrCount(this short[] arr)
        {
            short sum = 0;
            foreach (short a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static ushort ArrCount(this ushort[] arr)
        {
            ushort sum = 0;
            foreach (ushort a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static int ArrCount(this int[] arr)
        {
            int sum = 0;
            foreach (int a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static uint ArrCount(this uint[] arr)
        {
            uint sum = 0;
            foreach (uint a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static long ArrCount(this long[] arr)
        {
            long sum = 0;
            foreach (long a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static ulong ArrCount(this ulong[] arr)
        {
            ulong sum = 0;
            foreach (ulong a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static float ArrCount(this float[] arr)
        {
            float sum = 0;
            foreach (float a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static double ArrCount(this double[] arr)
        {
            double sum = 0;

            foreach (double a in arr)
            {
                sum += a;
            }

            return sum;
        }

        public static decimal ArrCount(this decimal[] arr)
        {
            decimal sum = 0;

            foreach (decimal a in arr)
            {
                sum += a;
            }

            return sum;
        }
    }
}
