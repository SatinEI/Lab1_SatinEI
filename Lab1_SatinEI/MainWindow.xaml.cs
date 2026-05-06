using System.Data;
using System.Windows;

namespace Lab1_SatinEI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Logger.EnsureConsole(); // показываем консоль
            Console.WriteLine("Приложение запущено. Лог записывается в файл triangle_log.txt");
        }
        //Логика нажатия кнопки
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string a = txtSideA.Text;
            string b = txtSideB.Text;
            string c = txtSideC.Text;

            var result = TriangleCalculator.Calculate(a, b, c);

            lblType.Content = string.IsNullOrEmpty(result.Type) ?
                "Ошибка: нечисловые данные" : $"Тип: {result.Type}";

            lblVertices.Content = result.Vertices != null ?
                $"Вершины: {string.Join(", ", System.Array.ConvertAll(result.Vertices, v => $"({v.X},{v.Y})"))}"
                : "";

            // Отрисовка треугольника
            DrawTriangle(result.Vertices);
        }

        private void DrawTriangle((int X, int Y)[] vertices)
        {
            trianglePolygon.Points.Clear();

            // Если координаты отрицательные – ничего не рисуем
            if (vertices == null || vertices.Any(v => v.X < 0 || v.Y < 0))
                return;

            // Добавляем точки в polygon
            foreach (var vertex in vertices)
            {
                trianglePolygon.Points.Add(new System.Windows.Point(vertex.X, vertex.Y));
            }
        }

    }
}