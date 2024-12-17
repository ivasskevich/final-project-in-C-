using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace final_project_in_C_
{
    public class JsonDataSerializer : IDataSerializer
    {
        public void Save(List<BaseStorageUnit> units, string fileName)
        {
            try
            {
                foreach (var unit in units)
                    unit.SaveData();

                using var stream = new FileStream(fileName + ".json", FileMode.Create);
                var serializer = new DataContractJsonSerializer(typeof(List<BaseStorageUnit>));
                serializer.WriteObject(stream, units.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON serialization failed: {ex.Message}");
            }
        }

        public List<BaseStorageUnit> Load(string fileName)
        {
            try
            {
                using var stream = new FileStream(fileName + ".json", FileMode.Open);
                var serializer = new DataContractJsonSerializer(typeof(List<BaseStorageUnit>));
                var units = (List<BaseStorageUnit>)serializer.ReadObject(stream);

                foreach (var unit in units)
                    unit.LoadData();

                return units;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON deserialization failed: {ex.Message}");
                return new List<BaseStorageUnit>();
            }
        }
    }
}
