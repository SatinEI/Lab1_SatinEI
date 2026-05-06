using System;
using System.Collections.Generic;

namespace Lab1_SatinEI
{
    public static class TriangleCalculator
    {
        // Вычисляет тип треугольника и координаты его вершин по трём сторонам (заданным как строки).
        //Возвращает объект TriangleResult с типом и массивом
        public static TriangleResult Calculate(string sideA, string sideB, string sideC)
        {
            // Проверка на возможность преобразования во float. Если нельзя – возвращаем координаты (-2,-2) (признак невалидных данных)
            if (!float.TryParse(sideA, out float a) ||
                !float.TryParse(sideB, out float b) ||
                !float.TryParse(sideC, out float c))
            {
                Logger.LogError("Нечисловые входные данные",
                    $"A={sideA}, B={sideB}, C={sideC}", null);
                return new TriangleResult
                {
                    Type = "",
                    Vertices = new (int, int)[] { (-2, -2), (-2, -2), (-2, -2) }
                };
            }

            // Проверка на положительность
            if (a <= 0 || b <= 0 || c <= 0)
            {
                Logger.LogError("Стороны не положительные числа",
                    $"A={a}, B={b}, C={c}", null);
                return new TriangleResult
                {
                    Type = "не треугольник",
                    Vertices = new (int, int)[] { (-1, -1), (-1, -1), (-1, -1) }
                };
            }

            // Сумма любых двух сторон должна быть больше третьей, иначе треугольник не существует.
            if (a + b <= c || a + c <= b || b + c <= a)
            {
                Logger.LogSuccess("не треугольник", $"A={a}, B={b}, C={c}",
                    "не треугольник", new (int, int)[] { (-1, -1), (-1, -1), (-1, -1) });
                return new TriangleResult
                {
                    Type = "не треугольник",
                    Vertices = new (int, int)[] { (-1, -1), (-1, -1), (-1, -1) }
                };
            }

            // Из-за погрешности float сравнение с допуском 0.0001.
            string type;
            if (Math.Abs(a - b) < 0.0001f && Math.Abs(b - c) < 0.0001f)
                type = "равносторонний";
            else if (Math.Abs(a - b) < 0.0001f || Math.Abs(b - c) < 0.0001f || Math.Abs(a - c) < 0.0001f)
                type = "равнобедренный";
            else
                type = "разносторонний";

            // Вычисление немасштабированных координат
            // A = (0, 0), B = (c, 0), C вычисляем
            double cx = (c * c + a * a - b * b) / (2.0 * c);
            double cy = Math.Sqrt(Math.Max(0, a * a - cx * cx));

            // Масштабирование в поле 100x100
            double minX = 0, maxX = Math.Max(c, cx);
            double minY = 0, maxY = cy;
            double scale = 100.0 / Math.Max(maxX - minX, maxY - minY);

            // Применяем масштаб и округляем
            var vertices = new List<(int, int)>
            {
                ((int)(0 * scale), (int)(0 * scale)),
                ((int)(c * scale), (int)(0 * scale)),
                ((int)(cx * scale), (int)(cy * scale))
            };

            var result = new TriangleResult
            {
                Type = type,
                Vertices = vertices.ToArray()
            };

            Logger.LogSuccess(type, $"A={a}, B={b}, C={c}", type, result.Vertices);

            return result;
        }
    }
}