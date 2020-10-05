using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonteCarloLibrary
{
    public class Pi
    {
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public List<Point> Points { get; set; }

        private Random _random = new Random();
        private int _pointsInCircle = 0;
        private int _precision { get; }

        public Pi(int precision)
        {
            _precision = precision + 1;
            Points = new List<Point>();
        }

        public void Randomize()
        {
            for (int i = 1; i < _precision; i++)
            {
                XCoordinate = _random.NextDouble();
                YCoordinate = _random.NextDouble();

                Points.Add(new Point(XCoordinate, YCoordinate));
            }
        }

        public void Count(List<double> collection)
        {
            for (int i = 1; i < _precision; i++)
            {
                if (Math.Pow(Points[i - 1].XCoordinate, 2) + Math.Pow(Points[i - 1].YCoordinate, 2) <= 1) 
                {
                    _pointsInCircle++;
                }
                collection.Add((double)_pointsInCircle / i * 4);
            }
        }
    }
}
