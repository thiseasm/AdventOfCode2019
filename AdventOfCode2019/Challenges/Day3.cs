using System;
using System.Collections.Generic;

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
            var cableNumber = 0;
            var meetingPoints = new List<Point>();
            var middlePoint = panel.GetLength(0) / 2;
            meetingPoints.Add(new Point(middlePoint, middlePoint));
            
            foreach(var cable in cablePaths)
            {
                var currentPoint = new Point(middlePoint,middlePoint);
                panel[currentPoint.PositionX, currentPoint.PositionY] = 99;
                cableNumber++;
                var path = cable.Split(',');

                foreach(var line in path)
                {
                    var movement = new Direction(line);
                    var steps = 0;
                    switch (movement.Positioning)
                    {
                        case 'D':
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionY--;
                                if (panel[currentPoint.PositionX, currentPoint.PositionY] != 0 && panel[currentPoint.PositionX, currentPoint.PositionY] != cableNumber)
                                    meetingPoints.Add(new Point(currentPoint.PositionX, currentPoint.PositionY));
                                panel[currentPoint.PositionX, currentPoint.PositionY] = cableNumber;
                                steps++;
                            }
                            break;
                        case 'U':
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionY++;
                                if (panel[currentPoint.PositionX, currentPoint.PositionY] != 0 && panel[currentPoint.PositionX, currentPoint.PositionY] != cableNumber)
                                    meetingPoints.Add(new Point(currentPoint.PositionX, currentPoint.PositionY));
                                panel[currentPoint.PositionX, currentPoint.PositionY] = cableNumber;
                                steps++;
                            }
                            break;
                        case 'L':
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionX--;
                                if (panel[currentPoint.PositionX, currentPoint.PositionY] != 0 && panel[currentPoint.PositionX, currentPoint.PositionY] != cableNumber)
                                    meetingPoints.Add(new Point(currentPoint.PositionX, currentPoint.PositionY));
                                panel[currentPoint.PositionX, currentPoint.PositionY] = cableNumber;
                                steps++;
                            }
                            break;
                        case 'R':
                            while (steps < movement.Distance)
                            {
                                currentPoint.PositionX++;
                                if (panel[currentPoint.PositionX, currentPoint.PositionY] != 0 && panel[currentPoint.PositionX, currentPoint.PositionY] != cableNumber)
                                    meetingPoints.Add(new Point(currentPoint.PositionX, currentPoint.PositionY));
                                panel[currentPoint.PositionX, currentPoint.PositionY] = cableNumber;
                                steps++;
                            }
                            break;
                    }

                }
            }
            FindClosestMeetingPoint(meetingPoints);

        }

        private void FindClosestMeetingPoint(List<Point> meetingPoints)
        {
            var closestPoint = new Point(6000, 6000);
            var closestDistance = 1000;
            for(var index = 1; index < meetingPoints.Count; index++)
            {
                var distanceX = Math.Abs(meetingPoints[index].PositionX - meetingPoints[0].PositionX);
                var distanceY = Math.Abs(meetingPoints[index].PositionY - meetingPoints[0].PositionY);
                var distance = distanceX + distanceY;

                var distanceClosestX = Math.Abs(closestPoint.PositionX - meetingPoints[0].PositionX);
                var distanceClosestY = Math.Abs(closestPoint.PositionY - meetingPoints[0].PositionY);
                var distanceClosest = distanceClosestX + distanceClosestY;
                if (distance < distanceClosest)
                {
                    closestPoint = meetingPoints[index];
                    closestDistance = distance;
                }
            }
            Console.WriteLine($"The distance between the two closest points where the two cables meet is: {closestDistance}");

        }

        private int[,] CalculatePanelSize(string cablePath)
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

        public class Point
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }

            public Point(int x, int y)
            {
                PositionX = x;
                PositionY = y;
            }
        }
    }
}
