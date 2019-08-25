using System.Collections.Generic;
using System.Linq;

namespace BlobLoader
{
    public class Operaty : Dictionary<int, Operat>
    {
        public int GetOperatCount(string operatName)
        {
            return Values.Where(o => o.IdMaterialu == operatName).ToList().Count;
        }

        public int GetIdOp(string operatName)
        {
            return Values.Where(o => o.IdMaterialu == operatName).ToList()[0].IdOp;
        }
    }
}
