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
    public class FlashDrive : BaseStorageUnit
    {
        [DataMember]
        public double UsbTransferSpeed { get; set; }

        public override string GenerateReport() =>
            $"{base.GenerateReport()}, USB transfer speed: {UsbTransferSpeed} MB/s";

        public override void LoadData() =>
            Console.WriteLine($"{Name} (Flash) is reading data at {UsbTransferSpeed} MB/s");

        public override void SaveData() =>
            Console.WriteLine($"{Name} (Flash) is writing data at {UsbTransferSpeed} MB/s");
    }
}
