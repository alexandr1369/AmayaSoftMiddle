using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Editor.Tests
{
    [Serializable]
    public class InfinityCount
    {
        private const int FLOATING_DIGITS = 2;
        private const double MIN_FLOATING_RATE = 1D;
        private const double MAX_FLOATING_RATE = 10D;
        private const string DEFAULT_OUTPUT_FORMAT = "{0:F0}{1}";
        private const string FLOATING_POINT_VALUE_OUTPUT_FORMAT = "{0:N2}{1}";

        private static readonly List<KeyValuePair<int, string>> Pairs = new()
        {
            new KeyValuePair<int, string>(3, "K"),
            new KeyValuePair<int, string>(6, "M"),
            new KeyValuePair<int, string>(9, "B"),
            new KeyValuePair<int, string>(12, "T"),
            new KeyValuePair<int, string>(15, "AA"),
            new KeyValuePair<int, string>(18, "AB")
        };

        [field: SerializeField, Min(0)] public double Rate { get; set; }
        [field: SerializeField, Min(0)] public int PowerOfTen { get; set; }

        public bool IsEmpty => Rate <= 0 && PowerOfTen <= 0;

        public InfinityCount()
        {
            Rate = 0;
            PowerOfTen = 0;
        }

        public InfinityCount(double rate = 0, int powerOfTen = 0)
        {
            Rate = Math.Clamp(rate, 0, double.MaxValue);
            PowerOfTen = Math.Clamp(powerOfTen, 0, int.MaxValue);
            this.Clamp();
        }

        public InfinityCount(InfinityCount count)
            : this(count.Rate, count.PowerOfTen)
        {
        }

        public static InfinityCount operator +(InfinityCount count1, InfinityCount count2)
        {
            ReduceToCommonDenominator(count1, count2,
                out var newCount1, out var newCount2);

            newCount1.Rate += newCount2.Rate;

            return newCount1.Clamp();
        }

        public static InfinityCount operator +(InfinityCount count1, int count2)
        {
            ReduceToCommonDenominator(count1, new InfinityCount(count2),
                out var newCount1, out var newCount2);

            newCount1.Rate += newCount2.Rate;

            return newCount1.Clamp();
        }

        public static InfinityCount operator -(InfinityCount count1, InfinityCount count2)
        {
            ReduceToCommonDenominator(count1, count2,
                out var newCount1, out var newCount2);

            newCount1.Rate -= newCount2.Rate;

            return newCount1.Clamp();
        }

        public static InfinityCount operator -(InfinityCount count1, int count2)
        {
            ReduceToCommonDenominator(count1, new InfinityCount(count2),
                out var newCount1, out var newCount2);

            newCount1.Rate -= newCount2.Rate;

            return newCount1.Clamp();
        }

        public static InfinityCount operator *(InfinityCount count, float ratio)
        {
            count = new InfinityCount(count);
            count.Rate *= ratio;

            return count.Clamp();
        }

        public static InfinityCount operator /(InfinityCount count, float ratio)
        {
            count = new InfinityCount(count);
            count.Rate /= ratio;

            return count.Clamp();
        }

        public static InfinityCount operator ++(InfinityCount count)
        {
            ReduceToCommonDenominator(count, new InfinityCount(1),
                out var newCount1, out var newCount2);

            newCount1.Rate += newCount2.Rate;

            return newCount1.Clamp();
        }

        public static InfinityCount operator --(InfinityCount count)
        {
            ReduceToCommonDenominator(count, new InfinityCount(1),
                out var newCount1, out var newCount2);

            newCount1.Rate -= newCount2.Rate;

            return newCount1.Clamp();
        }

        public static bool operator <(InfinityCount count1, InfinityCount count2)
        {
            return count1.PowerOfTen < count2.PowerOfTen
                   || count1.PowerOfTen.Equals(count2.PowerOfTen)
                   && count1.Rate < count2.Rate;
        }

        public static bool operator >(InfinityCount count1, InfinityCount count2)
        {
            return count1.PowerOfTen > count2.PowerOfTen
                   || count1.PowerOfTen.Equals(count2.PowerOfTen)
                   && count1.Rate > count2.Rate;
        }

        public static bool operator <=(InfinityCount count1, InfinityCount count2)
        {
            return count1.PowerOfTen < count2.PowerOfTen
                   || count1.PowerOfTen.Equals(count2.PowerOfTen)
                   && count1.Rate <= count2.Rate;
        }

        public static bool operator >=(InfinityCount count1, InfinityCount count2)
        {
            return count1.PowerOfTen > count2.PowerOfTen
                   || count1.PowerOfTen.Equals(count2.PowerOfTen)
                   && count1.Rate >= count2.Rate;
        }

        public static bool operator <(InfinityCount count1, int count2) =>
            count1 < new InfinityCount(count2);

        public static bool operator >(InfinityCount count1, int count2) =>
            count1 > new InfinityCount(count2);

        public static bool operator <=(InfinityCount count1, int count2) =>
            count1 <= new InfinityCount(count2);

        public static bool operator >=(InfinityCount count1, int count2) =>
            count1 >= new InfinityCount(count2);

        public static bool operator <(int count1, InfinityCount count2) =>
            new InfinityCount(count1) < count2;

        public static bool operator >(int count1, InfinityCount count2) =>
            new InfinityCount(count1) > count2;

        public static bool operator <=(int count1, InfinityCount count2) =>
            new InfinityCount(count1) <= count2;

        public static bool operator >=(int count1, InfinityCount count2) =>
            new InfinityCount(count1) >= count2;

        public long GetLongValue() => (long)Rate * PowerOfTen;

        private static void ReduceToCommonDenominator(
            InfinityCount count1,
            InfinityCount count2,
            out InfinityCount newCount1,
            out InfinityCount newCount2)
        {
            newCount1 = new InfinityCount(count1);
            newCount2 = new InfinityCount(count2);

            if (newCount1.PowerOfTen < newCount2.PowerOfTen)
            {
                var multiplierDifference = newCount2.PowerOfTen - newCount1.PowerOfTen;
                newCount1.PowerOfTen += multiplierDifference;
                newCount1.Rate /= Math.Pow(10, multiplierDifference);
            }
            else if (newCount1.PowerOfTen > newCount2.PowerOfTen)
            {
                var multiplierDifference = newCount1.PowerOfTen - newCount2.PowerOfTen;
                newCount2.PowerOfTen += multiplierDifference;
                newCount2.Rate /= Math.Pow(10, multiplierDifference);
            }
        }

        private string OutputFormat()
        {
            if (PowerOfTen > 0)
            {
                return Rate is < MAX_FLOATING_RATE and > MIN_FLOATING_RATE
                    ? FLOATING_POINT_VALUE_OUTPUT_FORMAT
                    : DEFAULT_OUTPUT_FORMAT;
            }

            return DEFAULT_OUTPUT_FORMAT;
        }

        public override string ToString()
        {
            var availabilityState = PowerOfTen <= 0 || Pairs.Exists(t => t.Key == PowerOfTen);

            if (!availabilityState)
                throw new Exception("[InfinityCount] Power of ten search has failed!");

            var powerOfTenConvertedChar = Pairs.Find(t => t.Key == PowerOfTen).Value;
            var rate = Math.Round(PowerOfTen > 0
                ? Rate > MAX_FLOATING_RATE
                    ? Math.Floor(Rate)
                    : Rate
                : Math.Floor(Rate), FLOATING_DIGITS);

            return string.Format(
                OutputFormat(),
                rate.ToString(new CultureInfo("en-US")),
                powerOfTenConvertedChar);
        }
    }
}