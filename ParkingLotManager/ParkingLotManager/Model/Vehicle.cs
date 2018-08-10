using System;

namespace ParkingLotManager.Model
{
    public abstract class Vehicle
    {
        protected Vehicle()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id
        {
            get;
        }

        public string LicensePlate
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public int WheelsCount
        {
            get;
            protected set;
        }
    }
}
