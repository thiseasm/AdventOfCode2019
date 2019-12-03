using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace AdventOfCode2019.Challenges
{
    class Day1 : Day
    {
        public override void Start()
        {
            CalculateFuel();
        }
        private void CalculateFuel()
        {
            var moduleWeights = ReadFileToArray("Day1.txt");
            var fuelNeededForModules = 0;
            var additionalFuelNeeded = 0;

            foreach(var module in moduleWeights)
            {
                var fuelRequired = (module / 3) - 2;
                fuelNeededForModules += fuelRequired;
                additionalFuelNeeded += CalculateExtraFuel(fuelRequired);
            }

            Console.WriteLine($"The required fuel for {moduleWeights.Length} modules is: {fuelNeededForModules}.");
            Console.WriteLine($"Taking into account the additional fuel needed, the required fuel is: {fuelNeededForModules + additionalFuelNeeded}.");
        }

        private int CalculateExtraFuel(int fuelOfModule)
        {
            var result = 0;

            do
            {
                var addedWeight = (fuelOfModule / 3) - 2;
                if (addedWeight > 0)
                {
                    result += addedWeight;
                }
                fuelOfModule = addedWeight;
            } while (fuelOfModule > 0);

            return result;
        }

    }
}
