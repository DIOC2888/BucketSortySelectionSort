using System;
using System.Diagnostics;

namespace SelectionSortApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tamaño del arreglo de prueba por defecto
            int n = 20000;

            Console.WriteLine("=== PRUEBA SELECTION SORT ===");
            Console.WriteLine("¿Desea elegir cuántos elementos tiene su arreglo? (s/n)");
            string respuesta = Console.ReadLine();

            if (respuesta.ToLower() == "s")
            {
                Console.WriteLine("Ingrese el tamaño del arreglo:");
                n = int.Parse(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Se utilizará el tamaño por defecto de 20000 elementos.");
            }

            Console.WriteLine($"\nTamaño del arreglo: {n} elementos\n");

            // =========================
            // MEJOR CASO
            // =========================
            int[] mejorCaso = GenerarMejorCaso(n);
            double tiempoMejor = MedirTiempoMinutos(SelectionSort, mejorCaso);
            TimeSpan tsMejor = TimeSpan.FromMinutes(tiempoMejor);

            Console.WriteLine("=== MEJOR CASO (arreglo ya ordenado ascendente) ===");
            Console.WriteLine($"Tiempo de ejecución: {tsMejor.TotalMinutes:F4} min ({tsMejor.Seconds} s {tsMejor.Milliseconds} ms)");
            Console.WriteLine();

            // =========================
            // CASO PROMEDIO
            // =========================
            int[] casoPromedio = GenerarArregloAleatorio(n, 0, 100000);
            double tiempoPromedio = MedirTiempoMinutos(SelectionSort, casoPromedio);
            TimeSpan tsPromedio = TimeSpan.FromMinutes(tiempoPromedio);

            Console.WriteLine("=== CASO PROMEDIO (valores aleatorios en un rango) ===");
            Console.WriteLine($"Tiempo de ejecución: {tsPromedio.TotalMinutes:F4} min ({tsPromedio.Seconds} s {tsPromedio.Milliseconds} ms)");
            Console.WriteLine();

            // =========================
            // PEOR CASO
            // =========================
            int[] peorCaso = GenerarPeorCaso(n);
            double tiempoPeor = MedirTiempoMinutos(SelectionSort, peorCaso);
            TimeSpan tsPeor = TimeSpan.FromMinutes(tiempoPeor);

            Console.WriteLine("=== PEOR CASO (arreglo ordenado descendente) ===");
            Console.WriteLine($"Tiempo de ejecución: {tsPeor.TotalMinutes:F4} min ({tsPeor.Seconds} s {tsPeor.Milliseconds} ms)");
            Console.WriteLine();

            Console.WriteLine("Pruebas finalizadas. Presioná cualquier tecla para salir...");
            Console.ReadKey();
        }

        // -----------------------------
        // Generar arreglo CASO PROMEDIO
        // -----------------------------
        public static int[] GenerarArregloAleatorio(int n, int min, int max)
        {
            int[] arr = new int[n];
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(min, max);
            }

            return arr;
        }

        // -----------------------------
        // Generar arreglo MEJOR CASO
        // Ya ordenado de menor a mayor
        // -----------------------------
        public static int[] GenerarMejorCaso(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = i;  // 0, 1, 2, 3, ...
            }
            return arr;
        }

        // -----------------------------
        // Generar arreglo PEOR CASO
        // Ordenado de mayor a menor
        // -----------------------------
        public static int[] GenerarPeorCaso(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = n - i;  // n, n-1, n-2, ...
            }
            return arr;
        }

        // -----------------------------
        // Algoritmo Selection Sort
        // -----------------------------
        public static void SelectionSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }

                if (minIndex != i)
                {
                    int temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                }
            }
        }

        // -----------------------------
        // Medir tiempo en minutos
        // -----------------------------
        public static double MedirTiempoMinutos(Action<int[]> algoritmo, int[] arr)
        {
            Stopwatch sw = Stopwatch.StartNew();
            algoritmo(arr);
            sw.Stop();
            return sw.Elapsed.TotalMinutes;
        }
    }
}
