using System;
using Microsoft.AspNetCore.Mvc;

namespace Triangle.Controllers
{
    /// <summary>
    /// Used Zero based index for the simplicity of the calculations. 
    /// So the result set will not have values in the multiples of 10's.
    /// Since I used Zero based index, below logic will see A1 as even indexed
    /// triangle and A2 as odd indexed triangle.
    /// Exceptions will be handled gobally by setting up GlobalExceptionAttribute & ExceptionFilterAttribute
    /// ************************Sample Request to get coordinates******************************
    /// http://localhost:37863/api/triangle/Coordinates?row=A&column=1
    /// 
    /// ************************Sample Request to get row and column***************************
    /// http://localhost:37863/api/triangle/RowColumn?v1X=0&v1Y=9&v2X=0&v2Y=0&v3X=9&v3Y=9 
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TriangleController : Controller
    {
        [ActionName("Coordinates")]
        public TriangleCoordinates GetCoordinates(char row, int column)
        {
            row = char.ToUpper(row);

            if (row < 'A' || row > 'F')
            {
                throw new ArgumentOutOfRangeException(nameof(row), "Row must be between 'A' and 'F'");
            }

            var rowNum = row - 'A';

            if (column < 1 || column > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(row), "Column must be between 1 and 12");
            }

            var currentColumn = column - 1;
            var columnNum = currentColumn / 2;
            var evenColumn = currentColumn % 2 == 0;

            var x = columnNum * 10;
            var y = rowNum * 10;

            var v1X = evenColumn ? x : x + 9;
            var v1Y = evenColumn ? y + 9 : y;
            var v2X = x;
            var v2Y = y;
            var v3X = x + 9;
            var v3Y = y + 9;

            return new TriangleCoordinates(v1X, v1Y, v2X, v2Y, v3X, v3Y);
        }

        [ActionName("RowColumn")]
        public RowColumn GetRowColumn(int v1X, int v1Y, int v2X, int v2Y, int v3X, int v3Y)
        {
            var evenColumnx = v2X == v1X;
            var evenColumny = v2Y == v1Y - 9;

            if (evenColumnx != evenColumny)
            {
                throw new ArgumentException("x and y coordinates of vertex 1 do not match");
            }

            var x1 = v1X == v2X ? v1X : v1X - 9;
            var y1 = v1Y == v2Y ? v1Y : v1Y - 9;

            var x2 = v2X;
            var y2 = v2Y;
            var x3 = v3X - 9;
            var y3 = v3Y - 9;

            if (x1 != x2 || x2 != x3)
            {
                throw new ArgumentException("x coordinates of the given vertices do not match");
            }

            if (y1 != y2 || y2 != y3)
            {
                throw new ArgumentException("y coordinates of the given vertices do not match");
            }

            return new RowColumn((char)(y1 / 10 + 'A'), (x1 / 10 + 1) * 2 - (evenColumnx ? 1 : 0));
        }
    }

    public struct TriangleCoordinates
    {
        public int V1X { get; set; }
        public int V1Y { get; set; }
        public int V2X { get; set; }
        public int V2Y { get; set; }
        public int V3X { get; set; }
        public int V3Y { get; set; }

        public TriangleCoordinates(int v1X, int v1Y, int v2X, int v2Y, int v3X, int v3Y)
        {
            V1X = v1X;
            V1Y = v1Y;
            V2X = v2X;
            V2Y = v2Y;
            V3X = v3X;
            V3Y = v3Y;
        }
    }

    public struct RowColumn
    {
        public char Row { get; set; }
        public int Column { get; set; }

        public RowColumn(char row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
