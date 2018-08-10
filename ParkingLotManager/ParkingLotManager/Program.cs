using System;
using ParkingLotManager.Model;

namespace ParkingLotManager
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fullSizeVehicle = new FullSizeVehicle();
            ParkingLot parkingLot = null;

            try
            {
                parkingLot.ParkVehicle(fullSizeVehicle);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Exceptioin thrown because we are passing in null.");
            }

            try
            {
                parkingLot.UnparkVehicle(fullSizeVehicle);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Exceptioin thrown because we are passing in null.");
            }

            try
            {
                parkingLot.GetTotalMoneyCollectedByVehicleType();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Exceptioin thrown because we are passing in null.");
            }

            parkingLot = GetparkingLot();

            try
            {
                parkingLot.ParkVehicle(null);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Exceptioin thrown because we are passing in null.");
            }

            try
            {
                parkingLot.ParkVehicle(fullSizeVehicle);
                parkingLot.ParkVehicle(new CompactVehicle());
                parkingLot.ParkVehicle(new FullSizeVehicle());
                parkingLot.ParkVehicle(new PickupTruck());
                parkingLot.ParkVehicle(new Motorcycle());
                parkingLot.ParkVehicle(new ElectricVehicle());
                parkingLot.ParkVehicle(new CompactVehicle());
                parkingLot.ParkVehicle(new FullSizeVehicle());
                parkingLot.ParkVehicle(new PickupTruck());
                parkingLot.ParkVehicle(new PickupTruck());
                parkingLot.ParkVehicle(new Motorcycle());

                DisplayAllDetails(parkingLot);

                parkingLot.UnparkVehicle(new PickupTruck());
                DisplayAllDetails(parkingLot);

                parkingLot.UnparkVehicle(fullSizeVehicle);
                DisplayAllDetails(parkingLot);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception occurred\r\n{ex}");
            }

        }

        private static void DisplayAllDetails(ParkingLot parkingLot)
        {
            Console.WriteLine("List all vehicles:");
            foreach (var vehicle in parkingLot.ListAllVehicles())
            {
                vehicle.WriteVehicle();
            }

            Console.WriteLine("List all vehicles orderd by type:");
            foreach (var vehicle in parkingLot.ListAllVehiclesByType())
            {
                vehicle.WriteVehicle();
            }

            Console.WriteLine("List all vehicles orderd by ID:");
            foreach (var vehicle in parkingLot.ListAllVehiclesById())
            {
                vehicle.WriteVehicle();
            }

            Console.WriteLine($"Get total money collected: {parkingLot.GetTotalMoneyCollected():C}");

            Console.WriteLine("Get total money collected by vehicle type:");
            foreach (var entry in parkingLot.GetTotalMoneyCollectedByVehicleType())
            {
                Console.WriteLine($"{entry.Key}: {entry.Value:C}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void WriteVehicle(this Vehicle vehicle)
        {
            Console.WriteLine($"    {vehicle.Id}: {vehicle.GetType().Name}");
        }

        private static ParkingLot GetparkingLot()
        {
            var count = 100;

            var parkingLot = new ParkingLot(count);
            return parkingLot;
        }
    }
}
