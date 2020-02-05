using System;
using System.IO;
using System.Linq;

namespace BlobLoader
{
    public class BlobFile
    {
        public int IdFile { get; }
        public string FullFileName { get; }
        public string FileName { get; }
        public int FileSize { get; }
        public string IdMaterialu { get; }
        public int IdOp { get; set; }
        public string Prefix { get; set; }
        public int PrefixId { get; set; }
        public string Status { get; set; }

        public BlobFile(string pathFileName, int fileCounter)
        {
            IdFile = fileCounter;
            FullFileName = pathFileName;
            FileName = Path.GetFileName(pathFileName);
            FileSize = Convert.ToInt32(new FileInfo(pathFileName).Length / 1024);

            try
            {
                string fullPath = Path.GetDirectoryName(pathFileName)?.TrimEnd(Path.DirectorySeparatorChar);
                IdMaterialu = fullPath?.Split(Path.DirectorySeparatorChar).Last();
            }
            catch (ArgumentOutOfRangeException)
            {
                IdMaterialu = "BŁĄD!!!";
            }
        }
    }
}
