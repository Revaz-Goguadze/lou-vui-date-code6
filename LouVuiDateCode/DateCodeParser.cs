using System;
using System.Globalization;

namespace LouVuiDateCode
{
    public static class DateCodeParser
    {
        /// <summary>
        /// Parses a date code and returns a <see cref="manufacturingYear"/> and <see cref="manufacturingMonth"/>.
        /// </summary>
        /// <param name="dateCode">A three or four number date code.</param>
        /// <param name="manufacturingYear">A manufacturing year to return.</param>
        /// <param name="manufacturingMonth">A manufacturing month to return.</param>
        public static void ParseEarly1980Code(string dateCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (string.IsNullOrEmpty(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode), "data code is null or empty");
            }
            else if (dateCode.Length != 3 && dateCode.Length != 4)
            {
                throw new ArgumentException("data code is not valid", nameof(dateCode));
            }

            uint year = uint.Parse("19" + dateCode[..2], CultureInfo.InvariantCulture);
            uint month = uint.Parse(dateCode[2..], CultureInfo.InvariantCulture);
            if (year > 1989 || year < 1980)
            {
                throw new ArgumentException("date code is invalid ", nameof(dateCode));
            }

            if (month <= 0 || month > 12)
            {
                throw new ArgumentException("date code is invalid ", nameof(dateCode));
            }

            manufacturingYear = year;
            manufacturingMonth = month;
        }

        /// <summary>
        /// Parses a date code and returns a <paramref name="factoryLocationCode"/>, <paramref name="manufacturingYear"/>, <paramref name="manufacturingMonth"/> and <paramref name="factoryLocationCountry"/> array.
        /// </summary>
        /// <param name="dateCode">A three or four number date code.</param>
        /// <param name="factoryLocationCountry">A factory location country array.</param>
        /// <param name="factoryLocationCode">A factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year to return.</param>
        /// <param name="manufacturingMonth">A manufacturing month to return.</param>
        public static void ParseLate1980Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (string.IsNullOrEmpty(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            factoryLocationCode = dateCode[^2 ..];
            factoryLocationCountry = CountryParser.GetCountry(factoryLocationCode);
            ParseEarly1980Code(dateCode[..^2], out manufacturingYear, out manufacturingMonth);
        }

        /// <summary>
        /// Parses a date code and returns a <paramref name="factoryLocationCode"/>, <paramref name="manufacturingYear"/>, <paramref name="manufacturingMonth"/> and <paramref name="factoryLocationCountry"/> array.
        /// </summary>
        /// <param name="dateCode">A six number date code.</param>
        /// <param name="factoryLocationCountry">A factory location country array.</param>
        /// <param name="factoryLocationCode">A factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year to return.</param>
        /// <param name="manufacturingMonth">A manufacturing month to return.</param>
        public static void Parse1990Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (string.IsNullOrEmpty(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode), "data code is null or empty");
            }
            else if (dateCode.Length != 6)
            {
                throw new ArgumentException("data code is not valid", nameof(dateCode));
            }

            factoryLocationCode = dateCode[..2];
            factoryLocationCountry = CountryParser.GetCountry(dateCode[..2]);
            uint month = uint.Parse(dateCode[2..3] + dateCode[4..5], CultureInfo.InvariantCulture);
            var year = uint.Parse(dateCode[3] == '0' ? string.Concat("20", dateCode[3], dateCode[5]) : string.Concat("19", dateCode[3], dateCode[5]), CultureInfo.InvariantCulture);

            if (year > 2006 || year < 1990)
            {
                throw new ArgumentException("date code is invalid ", nameof(dateCode));
            }

            if (month <= 0 || month > 12)
            {
                throw new ArgumentException("date code is invalid ", nameof(dateCode));
            }

            manufacturingYear = year;
            manufacturingMonth = month;
        }

        /// <summary>
        /// Parses a date code and returns a <paramref name="factoryLocationCode"/>, <paramref name="manufacturingYear"/>, <paramref name="manufacturingWeek"/> and <paramref name="factoryLocationCountry"/> array.
        /// </summary>
        /// <param name="dateCode">A six number date code.</param>
        /// <param name="factoryLocationCountry">A factory location country array.</param>
        /// <param name="factoryLocationCode">A factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year to return.</param>
        /// <param name="manufacturingWeek">A manufacturing month to return.</param>
        public static void Parse2007Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingWeek)
        {
            if (string.IsNullOrEmpty(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode), "data code is null or empty");
            }
            else if (dateCode.Length != 6)
            {
                throw new ArgumentException("data code is not valid", nameof(dateCode));
            }

            factoryLocationCode = dateCode[..2];
            factoryLocationCountry = CountryParser.GetCountry(dateCode[..2]);
            uint week = uint.Parse(dateCode[2..3] + dateCode[4..5], CultureInfo.InvariantCulture);
            uint year = uint.Parse(string.Concat("20", dateCode[3], dateCode[5]), CultureInfo.InvariantCulture);

            if (year < 2007)
            {
                throw new ArgumentException("date code is invalid ", nameof(dateCode));
            }

            if (week < 1 || week > ISOWeek.GetWeeksInYear((int)year))
            {
                throw new ArgumentException("date code is invalid ", nameof(dateCode));
            }

            manufacturingYear = year;
            manufacturingWeek = week;
        }
    }
}
