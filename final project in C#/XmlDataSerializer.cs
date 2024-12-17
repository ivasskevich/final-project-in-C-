using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace final_project_in_C_
{
    public class XmlDataSerializer : IDataSerializer
    {
        public void Save(List<BaseStorageUnit> units, string fileName)
        {
            try
            {
                foreach (var unit in units)
                    unit.SaveData();

                var xmlSerializer = new XmlSerializer(typeof(List<BaseStorageUnit>),
                    new Type[] { typeof(FlashDrive), typeof(DvdDisk), typeof(HardDiskDrive) });

                using var stream = new FileStream(fileName + ".xml", FileMode.Create);
                xmlSerializer.Serialize(stream, units.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML serialization failed: {ex.Message}");
            }
        }

        public List<BaseStorageUnit> Load(string fileName)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(List<BaseStorageUnit>),
                    new Type[] { typeof(FlashDrive), typeof(DvdDisk), typeof(HardDiskDrive) });

                using var stream = new FileStream(fileName + ".xml", FileMode.Open);
                var units = (List<BaseStorageUnit>)xmlSerializer.Deserialize(stream);

                foreach (var unit in units)
                    unit.LoadData();

                return units;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML deserialization failed: {ex.Message}");
                return new List<BaseStorageUnit>();
            }
        }
    }
}
