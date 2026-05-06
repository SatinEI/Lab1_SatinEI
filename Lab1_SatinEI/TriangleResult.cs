using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SatinEI
{
    public class TriangleResult
    {
        public string? Type { get; set; }          // Тип треугольника
        public (int X, int Y)[] Vertices { get; set; } // 3 вершины
    }
}