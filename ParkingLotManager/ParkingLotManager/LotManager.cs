using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ParkingLotManager.Model;

namespace ParkingLotManager
{
    public static class LotManager
    {
        private const decimal CostPweWheel = 5;

        public static decimal GetTotalMoneyCollected(this ParkingLot parkingLot)
        {
            if (parkingLot == null)
            {
                throw new ArgumentNullException(nameof(parkingLot));
            }

            return Enumerable.Sum(from vehicle in parkingLot.ParkedVehicles select vehicle.CalculateCost());
        }

        public static IDictionary<string, decimal> GetTotalMoneyCollectedByVehicleType(this ParkingLot parkingLot)
        {
            if (parkingLot == null)
            {
                throw new ArgumentNullException(nameof(parkingLot));
            }

            return new ReadOnlyDictionary<string, decimal>(
                (parkingLot.ParkedVehicles.GroupBy(x => x.GetType().Name).Select(x => new
                {
                    x.Key,
                    TotalCost = Enumerable.Sum(from y in x
                                               select y.CalculateCost())
                })).ToDictionary(x => x.Key, x => x.TotalCost));
        }

        public static IList<Vehicle> ListAllVehicles(this ParkingLot parkingLot)
        {
            if (parkingLot == null)
            {
                throw new ArgumentNullException(nameof(parkingLot));
            }

            return parkingLot.ParkedVehicles.ToList().AsReadOnly();
        }

        public static IList<Vehicle> ListAllVehiclesByType(this ParkingLot parkingLot)
        {
            if (parkingLot == null)
            {
                throw new ArgumentNullException(nameof(parkingLot));
            }

            return parkingLot.ParkedVehicles.OrderBy(x => x.GetType().Name).ToList().AsReadOnly();
        }

        public static IList<Vehicle> ListAllVehiclesById(this ParkingLot parkingLot)
        {
            if (parkingLot == null)
            {
                throw new ArgumentNullException(nameof(parkingLot));
            }

            return parkingLot.ParkedVehicles.OrderBy(x => x.Id).ToList().AsReadOnly();
        }

        public static bool ParkVehicle(this ParkingLot parkingLot, Vehicle vehicle)
        {
            if (parkingLot == null)
            {
                throw new ArgumentNullException(nameof(parkingLot));
            }

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            if (parkingLot.NumberOfSlots > parkingLot.ParkedVehicles.Count)
            {
                parkingLot.ParkedVehicles.Add(vehicle);
                return true;
            }

            return false;
        }

        public static bool UnparkVehicle(this ParkingLot parkingLot, Vehicle vehicle)
        {
            if (parkingLot == null)
            {
                throw new ArgumentNullException(nameof(parkingLot));
            }

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            if (parkingLot.ParkedVehicles.Contains(vehicle))
            {
                parkingLot.ParkedVehicles.Remove(vehicle);
                return true;
            }

            return false;
        }

        private static decimal CalculateCost(this Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            return CostPweWheel * vehicle.WheelsCount + vehicle.GetSurcharge();
        }

        private static decimal GetSurcharge(this Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            switch (vehicle.GetType().Name)
            {
                case nameof(ElectricVehicle):
                    return 1;

                case nameof(FullSizeVehicle):
                    return 2;

                case nameof(PickupTruck):
                    return 3;

                default:
                    return 0;
            }
        }
    }
}
