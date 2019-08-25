namespace BlobLoader
{
    public static class Functions
    {
        public static bool Like(this string toSearch, string toFind)
        {
            if (toFind.StartsWith("%") && toFind.EndsWith("%"))
            {
                toFind = toFind.Replace("%", "");

                return toSearch.Contains(toFind);
            }

            if (toFind.StartsWith("%") && !toFind.EndsWith("%"))
            {
                toFind = toFind.Replace("%", "");

                return toSearch.EndsWith(toFind);
            }

            if (!toFind.StartsWith("%") && toFind.EndsWith("%"))
            {
                toFind = toFind.Replace("%", "");

                return toSearch.StartsWith(toFind);
            }

            return false;
        }
    }
}
