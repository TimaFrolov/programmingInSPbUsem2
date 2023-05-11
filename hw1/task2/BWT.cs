namespace Task2
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>Provides a set of static methods for encode or decode BWT.</summary>
    public static class BWT
    {
        /// <summary>Encodes the string using BWT.</summary>
        /// <param name="str">String to encode.</param>
        /// <param name="indexOfLastChar">Index of last character from input string in encoded string.</param>
        /// <returns>Encoded string.</returns>
        public static string Encode(string str, out int indexOfLastChar)
        {
            // Generating suffix array using algorithm from: 
            // https://www.geeksforgeeks.org/suffix-array-set-2-a-nlognlogn-algorithm/
            // https://web.stanford.edu/class/cs97si/suffix-array.pdf
            Suffix[] suffixes = str.Select((char c, int index) => new Suffix(index, c, 0)).ToArray();

            for (var i = 0; i < str.Length - 1; i++)
            {
                suffixes[i].SecondaryRank = suffixes[i + 1].MainRank;
            }

            Array.Sort(suffixes);

            var locationOfSuffix = new int[str.Length];

            for (var step = 2; step < str.Length; step *= 2)
            {
                int newRank = 0;
                int rankOfPreviousSuffix = suffixes[0].MainRank;
                suffixes[0].MainRank = newRank;
                locationOfSuffix[suffixes[0].Index] = 0;
                for (var i = 1; i < str.Length; i++)
                {
                    if (suffixes[i].MainRank == rankOfPreviousSuffix
                        && suffixes[i].SecondaryRank == suffixes[i - 1].SecondaryRank)
                    {
                        rankOfPreviousSuffix = suffixes[i].MainRank;
                        suffixes[i].MainRank = newRank;
                    }
                    else
                    {
                        rankOfPreviousSuffix = suffixes[i].MainRank;
                        suffixes[i].MainRank = ++newRank;
                    }

                    locationOfSuffix[suffixes[i].Index] = i;
                }

                for (var i = 0; i < str.Length; i++)
                {
                    int nextCharPosition = suffixes[i].Index + step;
                    suffixes[i].SecondaryRank = nextCharPosition < str.Length ?
                       suffixes[locationOfSuffix[nextCharPosition]].MainRank : 0;
                }

                Array.Sort(suffixes);
            }

            int[] suffixArray = suffixes.Select(s => s.Index).ToArray();
            indexOfLastChar = Array.IndexOf(suffixArray, 0);
            return string.Join(string.Empty, suffixArray.Select(ind => ind != 0 ? str[ind - 1] : str[str.Length - 1]));
        }

        /// <summary>Decodes the string encoded using BWT.</summary>
        /// <param name="str">String to decode.</param>
        /// <param name="indexOfLastChar">Index of last character in encoded string.</param>
        /// <returns>Decoded string.</returns>
        public static string Decode(string str, int indexOfLastChar)
        {
            Dictionary<char, int> amountOfChar = str.Distinct().ToDictionary(c => c, c => str.Count(x => x == c));
            char[] alphabet = amountOfChar.Keys.ToArray();
            Array.Sort(alphabet);
            var amountOfPrecedingChars = new Dictionary<char, int>();
            amountOfPrecedingChars[alphabet[0]] = 0;
            for (var i = 1; i < alphabet.Length; i++)
            {
                amountOfPrecedingChars[alphabet[i]] = amountOfChar[alphabet[i - 1]] + amountOfPrecedingChars[alphabet[i - 1]];
            }

            var permutation = new int[str.Length];
            permutation[indexOfLastChar] = amountOfPrecedingChars[str[indexOfLastChar]]++;
            for (var i = 0; i < indexOfLastChar; i++)
            {
                permutation[i] = amountOfPrecedingChars[str[i]]++;
            }

            for (var i = indexOfLastChar + 1; i < str.Length; i++)
            {
                permutation[i] = amountOfPrecedingChars[str[i]]++;
            }

            char[] decodedString = new char[str.Length];
            for (var i = str.Length - 1; i >= 0; i--)
            {
                decodedString[i] = str[indexOfLastChar];
                indexOfLastChar = permutation[indexOfLastChar];
            }

            return string.Join(string.Empty, decodedString);
        }

        private class Suffix : IComparable<Suffix>
        {
            public Suffix(int index, int rank, int secondaryRank)
            {
                this.Index = index;
                this.MainRank = rank;
                this.SecondaryRank = secondaryRank;
            }

            public int Index { get; }

            public int MainRank { get; set; }

            public int SecondaryRank { get; set; }

            public int CompareTo(Suffix? other)
            {
                return other == null ? 1 : this.MainRank != other.MainRank ?
                    this.MainRank - other.MainRank : this.SecondaryRank - other.SecondaryRank;
            }
        }
    }
}