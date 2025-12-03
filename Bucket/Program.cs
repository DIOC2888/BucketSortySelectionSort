using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BucketSortApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tamaño del arreglo de prueba por defecto
            int n = 20000;

            Console.WriteLine("=== PRUEBA BUCKET SORT ===");
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
            double tiempoMejor = MedirTiempoMinutos(BucketSort, mejorCaso);
            TimeSpan tsMejor = TimeSpan.FromMinutes(tiempoMejor);

            Console.WriteLine("=== MEJOR CASO (distribución uniforme, cada bucket casi con 1 elemento) ===");
            Console.WriteLine($"Tiempo de ejecución: {tsMejor.TotalMinutes:F4} min ({tsMejor.Seconds} s {tsMejor.Milliseconds} ms)");
            Console.WriteLine();

            // =========================
            // CASO PROMEDIO
            // =========================
            int[] casoPromedio = GenerarArregloAleatorio(n, 0, 100000);
            double tiempoPromedio = MedirTiempoMinutos(BucketSort, casoPromedio);
            TimeSpan tsPromedio = TimeSpan.FromMinutes(tiempoPromedio);

            Console.WriteLine("=== CASO PROMEDIO (valores aleatorios en un rango) ===");
            Console.WriteLine($"Tiempo de ejecución: {tsPromedio.TotalMinutes:F4} min ({tsPromedio.Seconds} s {tsPromedio.Milliseconds} ms)");
            Console.WriteLine();

            // =========================
            // PEOR CASO
            // =========================
            int[] peorCaso = GenerarPeorCaso(n);
            double tiempoPeor = MedirTiempoMinutos(BucketSort, peorCaso);
            TimeSpan tsPeor = TimeSpan.FromMinutes(tiempoPeor);

            Console.WriteLine("=== PEOR CASO (muchos elementos concentrados en un solo bucket) ===");
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
        // Distribución uniforme: 0,1,2,...,n-1
        // Esto hace que cada valor caiga en un bucket diferente.
        // -----------------------------
        public static int[] GenerarMejorCaso(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = i;  // valores lineales, bien distribuidos
            }
            return arr;
        }

        // -----------------------------
        // Generar arreglo PEOR CASO
        // Muchos elementos con el mismo valor (min)
        // y un valor muy grande para forzar un rango enorme,
        // concentrando casi todo en un solo bucket.
        // -----------------------------
        public static int[] GenerarPeorCaso(int n)
        {
            int[] arr = new int[n];

            // Todos 0 menos el último muy grande
            for (int i = 0; i < n - 1; i++)
            {
                arr[i] = 0;
            }

            // Un valor muy grande para aumentar el rango
            arr[n - 1] = 1000000;

            return arr;
        }

        // -----------------------------
        // Algoritmo Bucket Sort
        // -----------------------------
        public static void BucketSort(int[] arr)
        {
            int n = arr.Length;
            if (n <= 0) return;

            int min = arr[0], max = arr[0];

            // Encontrar mínimo y máximo
            foreach (int x in arr)
            {
                if (x < min) min = x;
                if (x > max) max = x;
            }

            // Evitar división por cero si todos los valores son iguales
            int rango = max - min;
            if (rango == 0)
            {
                // Ya están "ordenados" porque todos son iguales
                return;
            }

            // Crear buckets
            List<int>[] buckets = new List<int>[n];
            for (int i = 0; i < n; i++)
                buckets[i] = new List<int>();

            // Distribuir elementos
            foreach (int value in arr)
            {
                int index = (int)((long)(value - min) * (n - 1) / rango);
                buckets[index].Add(value);
            }

            // Ordenar cada bucket
            for (int i = 0; i < n; i++)
                buckets[i].Sort();

            // Concatenar buckets
            int pos = 0;
            for (int i = 0; i < n; i++)
            {
                foreach (int value in buckets[i])
                    arr[pos++] = value;
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
