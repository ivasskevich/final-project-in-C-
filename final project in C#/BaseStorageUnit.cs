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
    [KnownType(typeof(FlashDrive))]
    [KnownType(typeof(DvdDisk))]
    [KnownType(typeof(HardDiskDrive))]
    public abstract class BaseStorageUnit
    {
        [DataMember]
        public string Manufacturer { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string Name { get; set; }

        private double _capacityGB;
        [DataMember]
        public double CapacityGB
        {
            get { return _capacityGB; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Capacity must be positive.");
                _capacityGB = value;
            }
        }

        private int _quantity;
        [DataMember]
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative.");
                _quantity = value;
            }
        }

        public virtual string GenerateReport() =>
            $"Manufacturer: {Manufacturer}, Model: {Model}, Name: {Name}, Capacity: {CapacityGB}GB, Quantity: {Quantity}";

        public virtual void LoadData() => Console.WriteLine($"{Name} is available for reading.");
        public virtual void SaveData() => Console.WriteLine($"{Name} is open for writing.");
    }
}
