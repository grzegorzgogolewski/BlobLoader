using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace BlobLoader
{
    public static class Functions
    {
        public static bool Like(this string toSearch, string toFind)
        {
            return LikeOperator.LikeString(toSearch, toFind.Replace("%", "*"), CompareMethod.Text);
        }
    }
}
