using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project_in_C_
{
    public class StorageInventory
    {
        private List<BaseStorageUnit> _storageUnits = new List<BaseStorageUnit>();

        public void AddDevice(BaseStorageUnit unit)
        {
            if (unit == null) throw new ArgumentNullException(nameof(unit));
            _storageUnits.Add(unit);
        }

        public void DisplayAll(ILogger logger)
        {
            if (!_storageUnits.Any())
            {
                logger.Print("The list is empty.");
                return;
            }

            foreach (var unit in _storageUnits)
            {
                string deviceType = unit switch
                {
                    FlashDrive => "Flash",
                    DvdDisk => "DVD",
                    HardDiskDrive => "HDD",
                    _ => "Unknown"
                };

                logger.Print($"Type: {deviceType}, {unit.GenerateReport()}");
            }
        }

        public void ModifyUnits(Func<BaseStorageUnit, bool> filter, Action<BaseStorageUnit> modifier)
        {
            foreach (var unit in _storageUnits.Where(filter))
                modifier(unit);
        }

        public BaseStorageUnit FindUnit(Func<BaseStorageUnit, bool> filter)
        {
            return _storageUnits.FirstOrDefault(filter);
        }

        public void PersistData(IDataSerializer serializer, string fileName)
        {
            serializer.Save(_storageUnits, fileName);
        }

        public void RetrieveData(IDataSerializer serializer, string fileName)
        {
            _storageUnits = serializer.Load(fileName);
        }

        public void DeleteDevices(Func<BaseStorageUnit, bool> filter, ILogger logger)
        {
            var matchingUnits = _storageUnits.Where(filter).ToList();
            if (matchingUnits.Count == 0)
            {
                logger.Print("No devices found matching the criteria.");
                return;
            }

            foreach (var unit in matchingUnits)
            {
                _storageUnits.Remove(unit);
                logger.Print($"Removed: {unit.GenerateReport()}");
            }
            logger.Print($"{matchingUnits.Count} device(s) removed.");
        }
    }
}
