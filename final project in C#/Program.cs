using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace final_project_in_C_
{
    class EntryPoint
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Select logging method:");
            Console.WriteLine("1. Console");
            Console.WriteLine("2. File");
            int logChoice = InputUtility.GetIntInput("Your choice", 1, 2);

            ILogger logger;

            switch (logChoice)
            {
                case 1:
                    logger = new ConsoleLogger();
                    break;
                case 2:
                    string filePath = InputUtility.GetNonEmptyString("Enter the file path for logs, e.g. log.txt");
                    logger = new FileLogger(filePath);
                    break;
                default:
                    throw new InvalidOperationException("Invalid logging method choice.");
            }

            StorageInventory storageInventory = new StorageInventory();
            bool shouldExit = false;

            while (!shouldExit)
            {
                Console.WriteLine("\nSelect an action:");
                Console.WriteLine("1. Add storage device");
                Console.WriteLine("2. Remove storage device");
                Console.WriteLine("3. Update storage device");
                Console.WriteLine("4. Find storage device");
                Console.WriteLine("5. Print all storage devices");
                Console.WriteLine("6. Save data");
                Console.WriteLine("7. Load data");
                Console.WriteLine("8. Exit");
                int choice = InputUtility.GetIntInput("Your choice", 1, 8);

                try
                {
                    switch (choice)
                    {
                        case 1:
                            StorageHandler.AddNewDevice(storageInventory, logger);
                            break;
                        case 2:
                            StorageHandler.RemoveDeviceByFilter(storageInventory, logger);
                            break;
                        case 3:
                            StorageHandler.UpdateDevice(storageInventory, logger);
                            break;
                        case 4:
                            StorageHandler.SearchDevice(storageInventory, logger);
                            break;
                        case 5:
                            storageInventory.DisplayAll(logger);
                            break;
                        case 6:
                            StorageHandler.SaveAllData(storageInventory);
                            break;
                        case 7:
                            StorageHandler.LoadAllData(storageInventory);
                            break;
                        case 8:
                            shouldExit = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    logger.Print($"Error: {ex.Message}");
                }
            }
        }
    }
}
