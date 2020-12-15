namespace TwoStrings
{
    using System.Linq;

    public static class TwoStrings
    {
        public static string CheckStrings(string s1, string s2)
        {
            var merge = s1.Intersect(s2);

            return merge.Count() > 0 ? "YES" : "NO";
        }
    }
}