using System.Collections.Generic;

namespace ParkingLotManager.Model
{
    public class ParkingLot
    {
        public ParkingLot(int count)
        {
            this.NumberOfSlots = count;
        }

        public int NumberOfSlots
        {
            get;
        }

        public IList<Vehicle> ParkedVehicles
        {
            get;
        } = new List<Vehicle>();
    }
}
