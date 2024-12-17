using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace final_project_in_C_
{
    [Serializable]
    [DataContract]
    public class DvdDisk : BaseStorageUnit
    {
        [DataMember]
        public double WritingSpeed { get; set; }

        public override string GenerateReport() =>
            $"{base.GenerateReport()}, Writing speed: {WritingSpeed}x";

        public override void LoadData() =>
            Console.WriteLine($"{Name} (DVD) is reading data...");

        public override void SaveData() =>
            Console.WriteLine($"{Name} (DVD) is writing data at {WritingSpeed}x...");
    }
}
