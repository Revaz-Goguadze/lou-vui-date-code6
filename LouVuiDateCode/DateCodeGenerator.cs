using System;
using System.Globalization;

namespace LouVuiDateCode
{
    public static class DateCodeGenerator
    {
        /// <summary>
        /// Generates a date code using rules from early 1980s.
        /// </summary>
        /// <param name="manufacturingYear">A manufacturing year.</param>
        /// <param name="manufacturingMonth">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string GenerateEarly1980Code(uint manufacturingYear, uint manufacturingMonth)
        {
            if (manufacturingYear > 1989 || manufacturingYear < 1980)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

            if (manufacturingMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingMonth));
            }

            return manufacturingYear.ToString(CultureInfo.InvariantCulture)[2..] + manufacturingMonth.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Generates a date code using rules from early 1980s.
        /// </summary>
        /// <param name="manufacturingDate">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string GenerateEarly1980Code(DateTime manufacturingDate)
        {
            return GenerateEarly1980Code((uint)manufacturingDate.Year, (uint)manufacturingDate.Month);
        }

        /// <summary>
        /// Generates a date code using rules from late 1980s.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year.</param>
        /// <param name="manufacturingMonth">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string GenerateLate1980Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingMonth)
        {
            return GenerateEarly1980Code(manufacturingYear, manufacturingMonth) + GetfactoryLocationCode(factoryLocationCode);
        }

        /// <summary>
        /// Generates a date code using rules from late 1980s.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingDate">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string GenerateLate1980Code(string factoryLocationCode, DateTime manufacturingDate)
        {
            return GenerateLate1980Code(factoryLocationCode, (uint)manufacturingDate.Year, (uint)manufacturingDate.Month);
        }

        /// <summary>
        /// Generates a date code using rules from 1990 to 2006 period.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year.</param>
        /// <param name="manufacturingMonth">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string Generate1990Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingMonth)
        {
            if (manufacturingYear > 2006 || manufacturingYear < 1990)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

            if (manufacturingMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingMonth));
            }

            string srtManufacturingMonth = manufacturingMonth.ToString("00", CultureInfo.InvariantCulture);
            string strManufacturingYear = manufacturingYear.ToString(CultureInfo.InvariantCulture);
            return GetfactoryLocationCode(factoryLocationCode) + srtManufacturingMonth[0] + strManufacturingYear[^2] + srtManufacturingMonth[1] + strManufacturingYear[^1];
        }

        /// <summary>
        /// Generates a date code using rules from 1990 to 2006 period.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingDate">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string Generate1990Code(string factoryLocationCode, DateTime manufacturingDate)
        {
            return Generate1990Code(factoryLocationCode, (uint)manufacturingDate.Year, (uint)manufacturingDate.Month);
        }

        /// <summary>
        /// Generates a date code using rules from post 2007 period.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year.</param>
        /// <param name="manufacturingWeek">A manufacturing week number.</param>
        /// <returns>A generated date code.</returns>
        public static string Generate2007Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingWeek)
        {
            if (manufacturingWeek < 1 || manufacturingWeek > ISOWeek.GetWeeksInYear((int)manufacturingYear))
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingWeek));
            }

            return Generate2007Code(factoryLocationCode, ISOWeek.ToDateTime((int)manufacturingYear, (int)manufacturingWeek, DayOfWeek.Monday));
        }

        /// <summary>
        /// Generates a date code using rules from post 2007 period.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingDate">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string Generate2007Code(string factoryLocationCode, DateTime manufacturingDate)
        {
            if (manufacturingDate.Year < 2007)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingDate));
            }

            string srtManufacturingWeek = ISOWeek.GetWeekOfYear(manufacturingDate).ToString("00", CultureInfo.InvariantCulture);
            string strManufacturingYear = ISOWeek.GetYear(manufacturingDate).ToString(CultureInfo.InvariantCulture);
            return GetfactoryLocationCode(factoryLocationCode) + srtManufacturingWeek[0] + strManufacturingYear[^2] + srtManufacturingWeek[1] + strManufacturingYear[^1];
        }

        /// <summary>
        /// Generates a factory location code.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <returns>Factory location code.</returns>
        public static string GetfactoryLocationCode(string factoryLocationCode)
        {
            if (string.IsNullOrEmpty(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            bool isLetter = true;
            foreach (char s in factoryLocationCode)
            {
                if (!char.IsLetter(s))
                {
                    isLetter = false;
                    break;
                }
            }

            if (!isLetter || factoryLocationCode.Length != 2)
            {
                throw new ArgumentException("invalid format factory locationCode", nameof(factoryLocationCode));
            }

            return factoryLocationCode.ToUpper(CultureInfo.InvariantCulture);
        }
    }
}
