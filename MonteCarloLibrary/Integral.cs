using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace MonteCarloLibrary
{
    public class Integral
    {
        public double XCoordinate { get; set; } = 0;
        public double YCoordinate { get; set; } = 0;

        private DataTable dataTable;
        private DataRow dataRow;
        private DataColumn column;

        private double _sum = 0;
        private int _precision;
        private double _step;
        private string _formula;

        public Integral(int precision, double xStart, double xEnd, string formula)
        {
            _precision = precision + 1;
            XCoordinate = xStart;
            _step = (xEnd - xStart) / _precision;
            _formula = formula;
            ReadFormula();
        }

        public void Count(List<double> collection)
        {
            double surface;

            for (int i = 1; i < _precision; i++)
            {
                surface = (EvaluateFun(XCoordinate) + EvaluateFun(XCoordinate + _step)) * _step / 2;
                XCoordinate += _step;
                _sum += surface;

                collection.Add(_sum);
            }
        }

        private void ReadFormula()
        {
            dataTable = new DataTable();
            dataRow = dataTable.NewRow();
            column = new DataColumn();
            dataTable.Columns.Add("x", typeof(double));
            dataTable.Rows.Add(dataRow);

            column.DataType = typeof(double);
            column.Expression = _formula;
            column.ColumnName = "Total";
            dataTable.Columns.Add(column);
        }

        public double EvaluateFun(double x)
        {
            double output;

            dataRow["x"] = x;
            output = (double)dataTable.Compute("Sum(Total)", "");

            return output;
        }
    }
}