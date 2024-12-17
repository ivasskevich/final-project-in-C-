using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project_in_C_
{
    public static class StorageHandler
    {
        public static void AddNewDevice(StorageInventory inventory, ILogger logger)
        {
            Console.WriteLine("Select the device type:\n1. Flash\n2. DVD\n3. HDD");
            int deviceType = InputUtility.GetIntInput("Device type", 1, 3);

            string manufacturer = InputUtility.GetNonEmptyString("Manufacturer");
            string model = InputUtility.GetNonEmptyString("Model");
            string name = InputUtility.GetNonEmptyString("Name");
            double capacity = InputUtility.GetDoubleInput("Capacity (GB)", 0);
            int quantity = InputUtility.GetIntInput("Quantity", 1, int.MaxValue);

            BaseStorageUnit unit;
            switch (deviceType)
            {
                case 1:
                    double usbRate = InputUtility.GetDoubleInput("USB transfer speed (MB/s)", 0);
                    unit = new FlashDrive
                    {
                        Manufacturer = manufacturer,
                        Model = model,
                        Name = name,
                        CapacityGB = capacity,
                        Quantity = quantity,
                        UsbTransferSpeed = usbRate
                    };
                    break;
                case 2:
                    double burnSpeed = InputUtility.GetDoubleInput("Writing speed (x)", 0);
                    unit = new DvdDisk
                    {
                        Manufacturer = manufacturer,
                        Model = model,
                        Name = name,
                        CapacityGB = capacity,
                        Quantity = quantity,
                        WritingSpeed = burnSpeed
                    };
                    break;
                case 3:
                    int rotationSpeed = InputUtility.GetIntInput("Spindle speed (RPM)", 1, int.MaxValue);
                    unit = new HardDiskDrive
                    {
                        Manufacturer = manufacturer,
                        Model = model,
                        Name = name,
                        CapacityGB = capacity,
                        Quantity = quantity,
                        SpindleSpeed = rotationSpeed
                    };
                    break;
                default:
                    throw new InvalidOperationException("Invalid device type.");
            }

            inventory.AddDevice(unit);
            logger.Print($"Added: {unit.GenerateReport()}");
        }

        public static void RemoveDeviceByFilter(StorageInventory inventory, ILogger logger)
        {
            Console.WriteLine("Select the criterion for removal:");
            Console.WriteLine("1. Manufacturer");
            Console.WriteLine("2. Model");
            Console.WriteLine("3. Name");
            Console.WriteLine("4. Capacity");
            Console.WriteLine("5. Quantity");
            int criterion = InputUtility.GetIntInput("Criterion", 1, 5);

            switch (criterion)
            {
                case 1:
                    string manufacturer = InputUtility.GetNonEmptyString("Enter the manufacturer");
                    inventory.DeleteDevices(u => u.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase), logger);
                    break;
                case 2:
                    string model = InputUtility.GetNonEmptyString("Enter the model");
                    inventory.DeleteDevices(u => u.Model.Equals(model, StringComparison.OrdinalIgnoreCase), logger);
                    break;
                case 3:
                    string name = InputUtility.GetNonEmptyString("Enter the name");
                    inventory.DeleteDevices(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase), logger);
                    break;
                case 4:
                    double capacity = InputUtility.GetDoubleInput("Enter the capacity (GB)", 0);
                    inventory.DeleteDevices(u => u.CapacityGB == capacity, logger);
                    break;
                case 5:
                    int quantity = InputUtility.GetIntInput("Enter the quantity", 1, int.MaxValue);
                    inventory.DeleteDevices(u => u.Quantity == quantity, logger);
                    break;
            }
        }

        public static void UpdateDevice(StorageInventory inventory, ILogger logger)
        {
            string name = InputUtility.GetNonEmptyString("Enter the name of the device to update");
            var device = inventory.FindUnit(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (device == null)
            {
                logger.Print("Device not found. Operation canceled.");
                return;
            }

            Console.WriteLine($"Updating device: {device.GenerateReport()}");
            Console.WriteLine("Select the field to update:");
            Console.WriteLine("1. Manufacturer");
            Console.WriteLine("2. Model");
            Console.WriteLine("3. Name");
            Console.WriteLine("4. Capacity");
            Console.WriteLine("5. Quantity");
            Console.WriteLine("6. Update all fields");
            int choice = InputUtility.GetIntInput("Your choice", 1, 6);

            switch (choice)
            {
                case 1:
                    device.Manufacturer = InputUtility.GetNonEmptyString("Enter new manufacturer");
                    break;
                case 2:
                    device.Model = InputUtility.GetNonEmptyString("Enter new model");
                    break;
                case 3:
                    device.Name = InputUtility.GetNonEmptyString("Enter new name");
                    break;
                case 4:
                    device.CapacityGB = InputUtility.GetDoubleInput("Enter new capacity (GB)", 0);
                    break;
                case 5:
                    device.Quantity = InputUtility.GetIntInput("Enter new quantity", 1, int.MaxValue);
                    break;
                case 6:
                    device.Manufacturer = InputUtility.GetNonEmptyString("Enter new manufacturer");
                    device.Model = InputUtility.GetNonEmptyString("Enter new model");
                    device.Name = InputUtility.GetNonEmptyString("Enter new name");
                    device.CapacityGB = InputUtility.GetDoubleInput("Enter new capacity (GB)", 0);
                    device.Quantity = InputUtility.GetIntInput("Enter new quantity", 1, int.MaxValue);

                    if (device is FlashDrive flash)
                        flash.UsbTransferSpeed = InputUtility.GetDoubleInput("Enter new USB speed (MB/s)", 0);
                    else if (device is DvdDisk dvd)
                        dvd.WritingSpeed = InputUtility.GetDoubleInput("Enter new writing speed (x)", 0);
                    else if (device is HardDiskDrive hdd)
                        hdd.SpindleSpeed = InputUtility.GetIntInput("Enter new spindle speed (RPM)", 1, int.MaxValue);
                    break;
                default:
                    logger.Print("Invalid choice. Operation canceled.");
                    return;
            }
            logger.Print("Device successfully updated.");
        }

        public static void SearchDevice(StorageInventory inventory, ILogger logger)
        {
            Console.WriteLine("Select search criterion:");
            Console.WriteLine("1. Manufacturer");
            Console.WriteLine("2. Model");
            Console.WriteLine("3. Name");
            Console.WriteLine("4. Capacity");
            Console.WriteLine("5. Quantity");
            int criterion = InputUtility.GetIntInput("Criterion", 1, 5);

            BaseStorageUnit found = null;

            switch (criterion)
            {
                case 1:
                    string manufacturer = InputUtility.GetNonEmptyString("Enter the manufacturer");
                    found = inventory.FindUnit(u => u.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase));
                    break;
                case 2:
                    string model = InputUtility.GetNonEmptyString("Enter the model");
                    found = inventory.FindUnit(u => u.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
                    break;
                case 3:
                    string name = InputUtility.GetNonEmptyString("Enter the name");
                    found = inventory.FindUnit(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                    break;
                case 4:
                    double capacity = InputUtility.GetDoubleInput("Enter the capacity (GB)", 0);
                    found = inventory.FindUnit(u => u.CapacityGB == capacity);
                    break;
                case 5:
                    int quantity = InputUtility.GetIntInput("Enter the quantity", 1, int.MaxValue);
                    found = inventory.FindUnit(u => u.Quantity == quantity);
                    break;
            }

            if (found == null)
                logger.Print("No device found.");
            else
                logger.Print($"Found: {found.GenerateReport()}");
        }

        public static void SaveAllData(StorageInventory inventory)
        {
            Console.WriteLine("Select serialization format:");
            Console.WriteLine("1. SOAP");
            Console.WriteLine("2. JSON");
            Console.WriteLine("3. XML");
            int formatChoice = InputUtility.GetIntInput("Format", 1, 3);

            IDataSerializer serializer = formatChoice switch
            {
                1 => new SoapDataSerializer(),
                2 => new JsonDataSerializer(),
                3 => new XmlDataSerializer(),
                _ => throw new InvalidOperationException("Invalid serialization format.")
            };

            string fileName = InputUtility.GetNonEmptyString("Enter file name, e.g. myDevices");
            inventory.PersistData(serializer, fileName);
            Console.WriteLine($"Data successfully saved to {fileName}.");
        }

        public static void LoadAllData(StorageInventory inventory)
        {
            Console.WriteLine("Select serialization format:");
            Console.WriteLine("1. SOAP");
            Console.WriteLine("2. JSON");
            Console.WriteLine("3. XML");
            int formatChoice = InputUtility.GetIntInput("Format", 1, 3);

            IDataSerializer serializer = formatChoice switch
            {
                1 => new SoapDataSerializer(),
                2 => new JsonDataSerializer(),
                3 => new XmlDataSerializer(),
                _ => throw new InvalidOperationException("Invalid serialization format.")
            };

            string fileName = InputUtility.GetNonEmptyString("Enter file name, e.g. myDevices");
            inventory.RetrieveData(serializer, fileName);
            Console.WriteLine($"Data successfully loaded from {fileName}.");
        }
    }
}
