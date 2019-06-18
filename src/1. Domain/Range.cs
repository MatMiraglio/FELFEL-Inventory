using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL_Inventory.Domain
{

    public class Range<T> where T : IComparable<T>
    {
        public Range(T start, T end)
        {
            this.Start = start;
            this.End = end;
        }
        /// <summary>Minimum value of the range.</summary>
        public T Start { get; set; }

        /// <summary>Maximum value of the range.</summary>
        public T End { get; set; }

        /// <summary>Presents the Range in readable format.</summary>
        /// <returns>String representation of the Range</returns>
        public override string ToString()
        {
            return string.Format("[{0} - {1}]", this.Start, this.End);
        }

        /// <summary>Determines if the range is valid.</summary>
        /// <returns>True if range is valid, else false</returns>
        public bool IsValid()
        {
            return this.Start.CompareTo(this.End) <= 0;
        }

        /// <summary>Determines if the provided value is inside the range.</summary>
        /// <param name="value">The value to test</param>
        /// <returns>True if the value is inside Range, else false</returns>
        public bool ContainsValue(T value)
        {
            return (this.Start.CompareTo(value) <= 0) && (value.CompareTo(this.End) <= 0);
        }

        /// <summary>Determines if this Range is inside the bounds of another range.</summary>
        /// <param name="Range">The parent range to test on</param>
        /// <returns>True if range is inclusive, else false</returns>
        public bool IsInsideRange(Range<T> range)
        {
            return this.IsValid() && range.IsValid() && range.ContainsValue(this.Start) && range.ContainsValue(this.End);
        }

        /// <summary>Determines if another range is inside the bounds of this range.</summary>
        /// <param name="Range">The child range to test</param>
        /// <returns>True if range is inside, else false</returns>
        public bool ContainsRange(Range<T> range)
        {
            return this.IsValid() && range.IsValid() && this.ContainsValue(range.Start) && this.ContainsValue(range.End);
        }
    }
}

