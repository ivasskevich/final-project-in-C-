using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace final_project_in_C_
{
    public class SoapDataSerializer : IDataSerializer
    {
        public void Save(List<BaseStorageUnit> units, string fileName)
        {
            try
            {
                foreach (var unit in units)
                    unit.SaveData();

                using var stream = new FileStream(fileName + ".soap", FileMode.Create);
                var formatter = new SoapFormatter();
                formatter.Serialize(stream, units.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SOAP serialization failed: {ex.Message}");
            }
        }

        public List<BaseStorageUnit> Load(string fileName)
        {
            try
            {
                using var stream = new FileStream(fileName + ".soap", FileMode.Open);
                var formatter = new SoapFormatter();
                var units = (List<BaseStorageUnit>)formatter.Deserialize(stream);

                foreach (var unit in units)
                    unit.LoadData();

                return units;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SOAP deserialization failed: {ex.Message}");
                return new List<BaseStorageUnit>();
            }
        }
    }
}
