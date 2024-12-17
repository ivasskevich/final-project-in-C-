using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project_in_C_
{
    public interface IDataSerializer
    {
        void Save(List<BaseStorageUnit> units, string fileName);
        List<BaseStorageUnit> Load(string fileName);
    }
}
