namespace SherlockAnagrams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Anagrams
    {
        private string[][] Substrings;
        private string OriginalText;

        public Anagrams(string s)
        {
            this.OriginalText = s;
            this.Substrings = new string[s.Length - 1][];
        }

        public int SherlockReads()
        {
            var count = 0;

            for (var i = 0; i < this.Substrings.Length; i++)
            {
                SetSubstringArray(i);

                var pairs = new List<Tuple<int, int>>();

                // Compare them
                for (var j = 0; j < this.Substrings[i].Length; j++)
                {
                    var val1 = this.Substrings[i][j];

                    // Compare everyone to everyone
                    for (var z = 0; z < this.Substrings[i].Length; z++)
                    {
                        if (z == j)
                        {
                            continue;
                        }

                        var val2 = this.Substrings[i][z];

                        // Verify if not added yet to pairs, if equals
                        if (!CompareAsAnagrams(val1, val2))
                        {
                            continue;
                        }

                        if (PairsExist(pairs, j, z))
                        {
                            continue;
                        }

                        pairs.Add(new Tuple<int, int>(j, z));
                        count++;
                    }
                }
            }

            return count;
        }

        private void SetSubstringArray(int i)
        {
            var wordSize = i + 1;

            var arraySize = OriginalText.Length - i;

            this.Substrings[i] = new string[arraySize];

            //Setup the substrings
            for (var j = 0; j < this.Substrings[i].Length; j++)
            {
                this.Substrings[i][j] = this.OriginalText.Substring(j, wordSize);
            }

            //Console.WriteLine(string.Join(' ', this.Substrings[i]));
        }

        private bool CompareAsAnagrams(string val1, string val2)
        {
            if (val1.Length == 1 && val2.Length == 1)
            {
                return val1 == val2;
            }

            // Compare bigger anagrams
            var array1 = val1.ToCharArray().Select(x => x.ToString()).OrderBy(x => x).ToArray();
            var array2 = val2.ToCharArray().Select(x => x.ToString()).OrderBy(x => x).ToArray();

            return array1.SequenceEqual(array2);
        }

        private bool PairsExist(List<Tuple<int, int>> pairs, int i, int j)
        {
            return pairs.Any(x => (x.Item1 == i && x.Item2 == j) || (x.Item2 == i && x.Item1 == j));
        }
    }
}