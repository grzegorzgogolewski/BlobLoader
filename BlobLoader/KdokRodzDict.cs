using System.Collections.Generic;

namespace BlobLoader
{
    public class KdokRodzDict : Dictionary<int, KdokRodz>
    {
        public int GetIdRodzDok(string fileName)
        {
            foreach (KdokRodz kdokRodz in Values)
            {
                string prefix = kdokRodz.Prefix;

                if (fileName.Like(prefix))
                {
                    return kdokRodz.IdRodzDok;
                }
            }

            return 0;
        }

        public string GetOpis(string fileName)
        {
            foreach (KdokRodz kdokRodz in Values)
            {
                string prefix = kdokRodz.Prefix;

                if (fileName.Like(prefix))
                {
                    return kdokRodz.Opis;
                }
            }

            return string.Empty;
        }
    }
}
