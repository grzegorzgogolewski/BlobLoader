using System.Collections.Generic;
using BlobLoader.Tools;

namespace BlobLoader
{
    public class KdokRodzDict : List<KdokRodz>
    {
        public int GetIdRodzDok(string fileName)
        {
            foreach (KdokRodz kdokRodz in this)
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
            foreach (KdokRodz kdokRodz in this)
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
