﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MonteCarloLibrary
{
    public class Point
    {
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }

        public Point(double xCoordinate, double yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }
    }
}
