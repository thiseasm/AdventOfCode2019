using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode2019.Challenges
{
    public class Day6 : Day
    {
        public override void Start()
        {
            var orbitInput = ReadFile("Day6.txt");
            CalculateOrbits(orbitInput);
        }

        private static void CalculateOrbits(IEnumerable<string> orbitInput)
        {
            var orbits = orbitInput.Select(x => new Orbit {Outer = x.Split(')')[0], Inner = x.Split(')')[1]}).ToArray();
            var planets = orbits.Select(x => new Planet {Name = x.Outer, Orbits = new List<string>()}).ToArray();

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