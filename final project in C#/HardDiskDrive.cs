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
    public class HardDiskDrive : BaseStorageUnit
    {
        [DataMember]
        public int SpindleSpeed { get; set; }

        public override string GenerateReport() =>
            $"{base.GenerateReport()}, Spindle speed: {SpindleSpeed} RPM";

        public override void LoadData() =>
            Console.WriteLine($"{Name} (HDD) is reading data at {SpindleSpeed} RPM...");

        public override void SaveData() =>
            Console.WriteLine($"{Name} (HDD) is writing data at {SpindleSpeed} RPM...");
    }
}
