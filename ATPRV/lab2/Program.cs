using System.Diagnostics;

static void main1()
{
    int[] sizes = { 1000, 2000, 3000 };
    int[] threadCounts = { 1, 2, 4, 8, 12, 16, 20 };

    foreach (int size in sizes)
    {
        Console.WriteLine($"\nРазмер матрицы: {size}x{size}");
        Console.WriteLine("----------------------------------------");

        var matrixA = GenerateMatrix(size);
        var matrixB = GenerateMatrix(size);

        double sequentialTime = 0;

        foreach (int threads in threadCounts)
        {
            var stopwatch = Stopwatch.StartNew();

            if (threads == 1)
            {
                MultiplyByRows(matrixA, matrixB, threads);
                stopwatch.Stop();
                sequentialTime = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"По строкам (1 поток): {sequentialTime} мс");
            }
            else
            {
                stopwatch.Restart();
                MultiplyByRows(matrixA, matrixB, threads);
                stopwatch.Stop();
                double rowTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                MultiplyByColumns(matrixA, matrixB, threads);
                stopwatch.Stop();
                double colTime = stopwatch.ElapsedMilliseconds;

                double rowSpeedup = sequentialTime / rowTime;
                double colSpeedup = sequentialTime / colTime;
                double rowEfficiency = (rowSpeedup / threads) * 100;
                double colEfficiency = (colSpeedup / threads) * 100;

                Console.WriteLine($"\nПотоков: {threads}");
                Console.WriteLine($"По строкам: {rowTime} мс, Speedup: {Math.Round(rowSpeedup, 2)}, Efficiency: {Math.Round(rowEfficiency, 2)}%");
                Console.WriteLine($"По столбцам: {colTime} мс, Speedup: {Math.Round(colSpeedup, 2)}, Efficiency: {Math.Round(colEfficiency, 2)}%");
            }
        }
    }
}

static double[,] GenerateMatrix(int size)
{
    double[,] matrix = new double[size, size];
    Random random = new Random();
    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            matrix[i, j] = random.NextDouble() * 100;
        }
    }

    return matrix;
}

static double[,] MultiplyByRows(double[,] matrixA, double[,] matrixB, int threadCount)
{
    int size = matrixA.GetLength(0);
    double[,] result = new double[size, size];
    
    ParallelOptions options = new ParallelOptions();
    options.MaxDegreeOfParallelism = threadCount;
    
    Parallel.For(0, size, options, i =>
    {
        for (int j = 0; j < size; j++)
        {
            double sum = 0;
            for (int k = 0; k < size; k++)
            {
                sum += matrixA[i, k] * matrixB[k, j];
            }
            result[i, j] = sum;
        }
    });
    
    return result;
}

static double[,] MultiplyByColumns(double[,] matrixA, double[,] matrixB, int threadCount)
{
    int size = matrixA.GetLength(0);
    double[,] result = new double[size, size];
    
    ParallelOptions options = new ParallelOptions();
    options.MaxDegreeOfParallelism = threadCount;
    
    Parallel.For(0, size, options, j =>
    {
        for (int i = 0; i < size; i++)
        {
            double sum = 0;
            for (int k = 0; k < size; k++)
            {
                sum += matrixA[i, k] * matrixB[k, j];
            }
            result[i, j] = sum;
        }
    });
    
    return result;
}

static void main2()
{
    double a = 0.1;
    double b = 10.0;

    double[] accuracies = { 0.001, 0.0001, 0.00001, 0.000001, 0.0000001, 0.00000001, 0.000000001, 0.0000000001 };
    int[] threadCounts = { 1, 2, 4, 8, 12, 16, 20 };

    Console.WriteLine("Точность\tПотоки\t\tВремя (мс)\tУскорение");

    foreach (double accuracy in accuracies)
    {
        Console.WriteLine($"\nТочность: {accuracy}");

        double sequentialTime = 0;

        foreach (int threads in threadCounts)
        {
            int iterations = 2;

            Stopwatch stopwatch = Stopwatch.StartNew();

            if (threads == 1)
            {
                double result = CalculateSeq(a, b, accuracy, ref iterations);
                stopwatch.Stop();
                sequentialTime = stopwatch.Elapsed.TotalMilliseconds;
                Console.WriteLine($"{accuracy:E}\t{threads}\t\t{sequentialTime:F2}\t\t-");
            }
            else
            {
                double result = CalculateParallel(a, b, accuracy, ref iterations, threads);
                stopwatch.Stop();
                double parallelTime = stopwatch.Elapsed.TotalMilliseconds;

                if (sequentialTime > 0)
                {
                    double speedup = sequentialTime / parallelTime;
                    Console.WriteLine($"{accuracy:E}\t{threads}\t\t{parallelTime:F2}\t\t{speedup:F2}");
                }
                else
                {
                    Console.WriteLine($"{accuracy:E}\t{threads}\t\t{parallelTime:F2}\t\tN/A");
                }
            }
        }
    }
}

static double CalculateFunction(double x)
{
    double result = 0;
    for (int i = 0; i < 100; i++)
    {
        result = Math.Log(1 + x * x);
    }
    return result;
}

static double CalculateSeq(double a, double b, double accuracy, ref int iterations)
{
    double previousResult = 0;
    double currentResult = CalculateWithSteps(a, b, iterations);

    while (Math.Abs(currentResult - previousResult) > accuracy)
    {
        iterations *= 2;
        previousResult = currentResult;
        currentResult = CalculateWithSteps(a, b, iterations);
    }

    return currentResult;
}

static double CalculateParallel(double a, double b, double accuracy, ref int iterations, int threadCount)
{
    double previousResult = 0;
    double currentResult = CalculateWithStepsParallel(a, b, iterations, threadCount);

    while (Math.Abs(currentResult - previousResult) > accuracy)
    {
        iterations *= 2;
        previousResult = currentResult;
        currentResult = CalculateWithStepsParallel(a, b, iterations, threadCount);
    }

    return currentResult;
}

static double CalculateWithSteps(double a, double b, int steps)
{
    double h = (b - a) / steps;
    double sum = 0;

    for (int i = 0; i < steps; i++)
    {
        double x = a + h * (i + 0.5);
        sum += CalculateFunction(x);
    }

    return sum * h;
}

static double CalculateWithStepsParallel(double a, double b, int steps, int threadCount)
{
    double h = (b - a) / steps;
    double totalSum = 0;
    object lockObj = new object();

    Thread[] threads = new Thread[threadCount];
    int stepsPerThread = steps / threadCount;
    int remainingSteps = steps % threadCount;

    for (int t = 0; t < threadCount; t++)
    {
        int threadIndex = t;
        int startStep = threadIndex * stepsPerThread;
        int endStep = startStep + stepsPerThread;

        if (threadIndex == threadCount - 1)
        {
            endStep += remainingSteps;
        }

        threads[t] = new Thread(() =>
        {
            double localSum = 0;

            for (int i = startStep; i < endStep; i++)
            {
                double x = a + h * (i + 0.5);
                localSum += CalculateFunction(x);
            }

            lock (lockObj)
            {
                totalSum += localSum;
            }
        });

        threads[t].Start();
    }

    foreach (Thread thread in threads)
    {
        thread.Join();
    }

    return totalSum * h;
}

main1();
// main2();