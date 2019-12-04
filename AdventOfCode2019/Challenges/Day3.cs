using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2019.Challenges
{
    public class Day3 : Day
    {
        public override void Start()
        {
            OpenWirePanel();
        }

        private void OpenWirePanel()
        {
            var cablePaths = ReadFile("Day3.txt");
            var panel = CalculatePanelSize(cablePaths[0]);

            var meetingPoints = FindAllMeetingPoints(cablePaths, panel);
            FindClosestMeetingPoint(meetingPoints);

            var stepCount = CountStepsRequiredForEachPoint(meetingPoints, cablePaths, panel);
            FindFewestSteps(stepCount,meetingPoints);
        }

        private static void FindFewestSteps(IReadOnlyCollection<StepCount> stepCount, IEnumerable<Point> meetingPoints)
        {
            var combinedStepsForEachPoint = new List<int>();
            foreach(var point in meetingPoints.Distinct())
            {
                var stepCounts = stepCount.Where(x => x.Point.Equals(point)).ToList();
                if (!stepCounts.Any()) continue;

                var stepCountCable1 = stepCounts.First(x => x.CableNumber == 1).StepsTaken;
                var stepCountCable2 = stepCounts.First(x => x.CableNumber == 2).StepsTaken;
                var combinedSteps = stepCountCable1 + stepCountCable2;
                combinedStepsForEachPoint.Add(combinedSteps);
            }

            Console.WriteLine($"The fewest combined stepsTaken need from both cables to reach an intersection are: {combinedStepsForEachPoint.Min()} ");
        }

        private static List<StepCount> CountStepsRequiredForEachPoint(IReadOnlyCollection<Point> meetingPoints, IEnumerable<string> cablePaths, int[,] panel)
        {
            var stepsRequired = new List<StepCount>();
            var middlePoint = panel.GetLength(0) / 2;
            var cableNumber = 0;

            foreach (var cable in cablePaths)
            {
                var stepCount = 0;
                var currentPoint = new Point(middlePoint, middlePoint);
                cableNumber++;
                var path = cable.Split(',');

                foreach (var line in path)
                {
                    var movement = new Direction(line);
                    var steps = 0;
                    switch (movement.Positioning)
                    {
                        case 'D':
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionY--;
                                stepCount++;
                                steps++;
                                if (!meetingPoints.Any(x =>x.Equals(currentPoint)))
                                    continue;
                                stepsRequired.Add(new StepCount(stepCount, cableNumber, currentPoint));

                            }
                            break;
                        case 'U':
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionY++;
                                stepCount++;
                                steps++;
                                if (!meetingPoints.Any(x =>x.Equals(currentPoint)))
                                    continue;
                                stepsRequired.Add(new StepCount(stepCount, cableNumber, currentPoint));
                            }
                            break;
                        case 'L':
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionX--;
                                steps++;
                                stepCount++;
                                if (!meetingPoints.Any(x =>x.Equals(currentPoint)))
                                    continue;
                                stepsRequired.Add(new StepCount(stepCount,cableNumber,currentPoint));
                            }
                            break;
                        case 'R':
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionX++;
                                steps++;
                                stepCount++;
                                if (!meetingPoints.Any(x =>x.Equals(currentPoint)))
                                    continue;
                                stepsRequired.Add(new StepCount(stepCount, cableNumber, currentPoint));
                            }
                            break;
                    }

                }
            }
            return stepsRequired;
        }

        private static List<Point> FindAllMeetingPoints(IEnumerable<string> cablePaths, int[,] panel)
        {
            var meetingPoints = new List<Point>();
            var middlePoint = panel.GetLength(0) / 2;
            meetingPoints.Add(new Point(middlePoint, middlePoint));

            var cableNumber = 0;
            foreach (var cable in cablePaths)
            {
                var currentPoint = new Point(middlePoint, middlePoint);
                cableNumber++;
                var path = cable.Split(',');

                foreach (var line in path)
                {
                    var movement = new Direction(line);
                    var steps = 0;
                    switch (movement.Positioning)
                    {
                        case 'D':
                        {
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionY--;
                                ExaminePoint(panel, currentPoint, cableNumber, meetingPoints);
                                steps++;
                            }
                            break;
                        }
                        case 'U':
                        {
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionY++;
                                ExaminePoint(panel, currentPoint, cableNumber, meetingPoints);
                                steps++;
                            }
                            break;
                        }
                        case 'L':
                        {
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionX--;
                                ExaminePoint(panel, currentPoint, cableNumber, meetingPoints);
                                steps++;
                            }
                            break;
                        }
                        case 'R':
                        {
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionX++;
                                ExaminePoint(panel, currentPoint, cableNumber, meetingPoints);
                                steps++;
                            }
                            break;
                        }
                    }
                }
            }
            return meetingPoints;
        }

        private static void ExaminePoint(int[,] panel, Point currentPoint, int cableNumber, ICollection<Point> meetingPoints)
        {
            if (panel[currentPoint.PositionX, currentPoint.PositionY] != 0 &&
                panel[currentPoint.PositionX, currentPoint.PositionY] != cableNumber)
                meetingPoints.Add(new Point(currentPoint.PositionX, currentPoint.PositionY));
            panel[currentPoint.PositionX, currentPoint.PositionY] = cableNumber;
        }

        private static void FindClosestMeetingPoint(IReadOnlyList<Point> meetingPoints)
        {
            var closestDistance = CalculateDistance(meetingPoints, meetingPoints.Count - 1);
            for (var index = 1; index < meetingPoints.Count; index++)
            {
                var distance = CalculateDistance(meetingPoints, index);

                if (distance >= closestDistance) continue;
                closestDistance = distance;
            }
            Console.WriteLine($"The distance between the two closest points where the two cables meet is: {closestDistance}");

        }

        private static int CalculateDistance(IReadOnlyList<Point> meetingPoints, int index)
        {
            var distanceX = Math.Abs(meetingPoints[index].PositionX - meetingPoints[0].PositionX);
            var distanceY = Math.Abs(meetingPoints[index].PositionY - meetingPoints[0].PositionY);
            return distanceX + distanceY;
        }

        private static int[,] CalculatePanelSize(string cablePath)
        {
            var path = cablePath.Split(',');
            var axisX = 0;
            var axisY = 0;

            foreach (var movement in path)
            {
                var direction = new Direction(movement);
                if (direction.Distance.Equals('U') || direction.Distance.Equals('D'))
                    axisY += direction.Positioning;
                else
                    axisX += direction.Positioning;
            }
            var size = axisX > axisY ? axisX : axisY;
            size *= 2;
            return new int[size, size];
        }

        public class Direction
        {
            public char Positioning { get; }
            public int Distance { get; }


            public Direction(string direction)
            {
                Positioning = direction[0];
                Distance = int.Parse(direction.Substring(1));
            }

        }

        public class Point : IEquatable<Point>
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }

            public Point(int x, int y)
            {
                PositionX = x;
                PositionY = y;
            }

            public Point(Point point)
            {
                PositionX = point.PositionX;
                PositionY = point.PositionY;
            }

            public bool Equals(Point other)
            {
                return other != null && (PositionX == other.PositionX && PositionY == other.PositionY);
            }
        }

        public class StepCount
        {
            public int StepsTaken { get;}
            public int CableNumber { get; }
            public Point Point{ get;}

            public StepCount(int stepsTaken, int cableNumber, Point point)
            {
                StepsTaken = stepsTaken;
                CableNumber = cableNumber;
                Point = new Point(point);
            }

        }
    }
}
