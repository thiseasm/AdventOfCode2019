using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Challenges
{
    public class Day6 : Day
    {
        public override void Start()
        {
            var orbitInput = ReadFile("Day6.txt");
            var orbits = orbitInput.Select(x => new Orbit {Outer = x.Split(')')[0], Inner = x.Split(')')[1]}).ToArray();
            var planets = orbits.Select(x => new Planet {Name = x.Outer, Orbits = new List<string>()}).ToArray();
            CalculateOrbits(orbits, planets);
            CalculateStepsToSanta(orbits);
        }

        private static void CalculateStepsToSanta(Orbit[] orbits)
        {
            var currentOrbit = orbits.First(x => x.Inner.Equals("YOU"));
            var santaOrbit = orbits.First(x => x.Inner.Equals("SAN"));
            var pathToComFromYou = CalculateOrbitsFromPoint(orbits,currentOrbit);
            var pathToComFromSanta = CalculateOrbitsFromPoint(orbits,santaOrbit);

            var meetingPoint = pathToComFromYou.First(x => pathToComFromSanta.Contains(x));
            pathToComFromSanta = pathToComFromSanta.GetRange(0,pathToComFromSanta.IndexOf(meetingPoint));
            pathToComFromYou = pathToComFromYou.GetRange(0,pathToComFromYou.IndexOf(meetingPoint));

            var stepsToSanta = pathToComFromSanta.Count + pathToComFromYou.Count;
            Console.WriteLine($"The steps required to reach Santa are: {stepsToSanta}.");
        }

        private static List<string> CalculateOrbitsFromPoint(Orbit[] orbits, Orbit startingOrbit)
        {
            var pathToCom = new List<string>();

            do
            {
                pathToCom.Add(startingOrbit.Outer);
                if (startingOrbit.Outer.Equals("COM")) break;


                startingOrbit = orbits.First(x => x.Inner.Equals(startingOrbit.Outer));
            } while (true);

            return pathToCom;
        }

        private static void CalculateOrbits(Orbit[] orbits, Planet[] planets)
        {
            foreach (var orbit in orbits)
            {
                var planet = planets.First(x => x.Name.Equals(orbit.Outer));

                while(true)
                {
                    if (!planet.Orbits.Contains(orbit.Inner)) planet.Orbits.Add(orbit.Inner);
                    if(planet.Name.Equals("COM")) break;

                    var nextPlanet = orbits.First(x => x.Inner.Equals(planet.Name));
                    planet = planets.First(x => x.Name.Equals(nextPlanet.Outer));
                }
            }
            var checksum = planets.Select(x => x.Orbits).Sum(orbit => orbit.Count);
            Console.WriteLine($"The checksum of the Stellar Map is: {checksum}.");
        }
    }

    public class Orbit
    {
        public string Outer { get; set; }
        public string Inner { get; set; }

    }

    public class Planet
    {
        public string Name { get; set; }
        public List<string> Orbits { get; set; }
    }
}