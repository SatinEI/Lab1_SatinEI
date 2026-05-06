using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Lab1_SatinEI
{
    // класс для логирования результатов вычислений.
    // Пишет одновременно в консоль (выделенную для WPF) и в файл triangle_log.txt.
    public static class Logger
    {
        private static readonly string LogFilePath = "triangle_log.txt";

        // Позволяет вывести консоль для WPF
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        private static bool consoleAllocated = false;

        public static void EnsureConsole()
        {
            if (!consoleAllocated)
            {
                AllocConsole();
                consoleAllocated = true;
            }
        }
        //ЛОГ Об удачной работе
        public static void LogSuccess(string type, string parameters,
            string resultType, (int, int)[] vertices)
        {
            string log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | УСПЕХ | Параметры: {parameters} | " +
                         $"Тип: {resultType} | Вершины: {VerticesToString(vertices)}";
            WriteLog(log);
        }
        //ЛОГ Об неудачной работе
        public static void LogError(string errorType, string parameters, Exception ex)
        {
            string log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ОШИБКА | {errorType} | Параметры: {parameters}";
            if (ex != null)
                log += $"\nИсключение: {ex.Message}\nСтек вызовов: {ex.StackTrace}";
            WriteLog(log);
        }
        //Фукнция вывода ЛОГА и запись в файл
        private static void WriteLog(string message)
        {
            Console.WriteLine(message);
            try
            {
                File.AppendAllText(LogFilePath, message + Environment.NewLine);
            }
            catch { /* игнорируем ошибки записи в файл */ }
        }

        private static string VerticesToString((int, int)[] vertices)
        {
            return string.Join(", ", Array.ConvertAll(vertices, v => $"({v.Item1},{v.Item2})"));
        }
    }
}