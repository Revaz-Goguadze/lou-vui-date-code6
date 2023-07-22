using System;

namespace LouVuiDateCode
{
    public static class CountryParser
    {
        private static readonly string[] France = new[]
            {
                "A0", "A1", "A2", "AA", "AH", "AN", "AR", "AS", "BA", "BJ", "BU", "DR", "DU", "DR", "DT", "CO", "CT", "CX",
                "ET", "MB", "MI", "NO", "RA", "RI", "SF", "SL", "SN", "SP", "SR", "TJ", "TH", "TR", "TS", "VI", "VX",
            };

        private static readonly string[] FranceUsa = new[] { "FL", "SD" };
        private static readonly string[] FranceSpain = new[] { "LW" };
        private static readonly string[] Germany = new[] { "LP", "OL" };
        private static readonly string[] Italy = new[] { "BC", "BO", "CE", "FO", "MA", "OB", "RC", "RE", "SA", "TD", };
        private static readonly string[] Spain = new[] { "CA", "LO", "LB", "LM", "GI" };
        private static readonly string[] Switzerland = new[] { "DI", "FA" };
        private static readonly string[] Usa = new[] { "FC", "FH", "LA", "OS" };

        /// <summary>
        /// Gets a an array of <see cref="Country"/> enumeration values for a specified factory location code. One location code can belong to many countries.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <returns>An array of <see cref="Country"/> enumeration values.</returns>
        public static Country[] GetCountry(string factoryLocationCode)
        {
            if (string.IsNullOrEmpty(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode), "string is null or empty ");
            }

            if (Array.IndexOf(France, factoryLocationCode) > -1)
            {
                return new[] { Country.France };
            }

            if (Array.IndexOf(Italy, factoryLocationCode) > -1)
            {
                return new[] { Country.Italy };
            }

            if (Array.IndexOf(Spain, factoryLocationCode) > -1)
            {
                return new[] { Country.Spain };
            }

            if (Array.IndexOf(Usa, factoryLocationCode) > -1)
            {
                return new[] { Country.USA };
            }

            if (Array.IndexOf(FranceUsa, factoryLocationCode) > -1)
            {
                return new[] { Country.France, Country.USA };
            }

            if (Array.IndexOf(Germany, factoryLocationCode) > -1)
            {
                return new[] { Country.Germany };
            }

            if (Array.IndexOf(Switzerland, factoryLocationCode) > -1)
            {
                return new[] { Country.Switzerland };
            }

            if (Array.IndexOf(FranceSpain, factoryLocationCode) > -1)
            {
                return new[] { Country.France, Country.Spain };
            }

            throw new ArgumentException("factory location code is invalid ", nameof(factoryLocationCode));
        }
    }
}
